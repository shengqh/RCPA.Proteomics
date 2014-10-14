using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using Microsoft.Office.Interop.Excel;
using RCPA.Utils;
using RCPA.Gui;
using System.Reflection;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SQLite;
using RCPA.Proteomics.Mascot;
using RCPA.Seq;

namespace RCPA.Proteomics.ProteomeDiscoverer
{
  public class MsfDatabaseToNoredundantProcessor : AbstractMsfToNoredundantProcessor
  {
    public override List<IIdentifiedProtein> ParseProteins(string fileName)
    {
      var proMap = ParseProteinMap(fileName);

      var pepMap = ParsePeptideMap(fileName);

      LinkPeptideToProtein(fileName, proMap, pepMap);

      return (from pro in proMap.Values
              where pro.Peptides.Count > 0
              select pro).ToList();
    }

    public Aminoacids ParseAminoacids(string fileName)
    {
      SQLiteDBHelper sqlite = new SQLiteDBHelper(fileName);

      var result = new Aminoacids();

      var aaReader = sqlite.ExecuteReader("select OneLetterCode, MonoisotopicMass, AverageMass from Aminoacids", null);
      while (aaReader.Read())
      {
        var aa = aaReader.GetString(0);
        if (string.IsNullOrEmpty(aa) || aa == " ")
        {
          continue;
        }

        var monomass = aaReader.GetDouble(1);
        var avemass = aaReader.GetDouble(2);
        if (monomass == 0.0 || avemass == 0.0)
        {
          continue;
        }

        result[aa[0]].ResetMass(monomass, avemass);
      }

      var staticModReader = sqlite.ExecuteReader("select ParameterName, ParameterValue from ProcessingNodeParameters", null);
      while (staticModReader.Read())
      {
        var name = staticModReader.GetString(0);
        var value = staticModReader.GetString(1);

        if (name.StartsWith("StatMod_"))
        {
          var parts = value.Split('#');
          if (parts.Length == 2)
          {
            var aaid = int.Parse(parts[0]);
            var modid = int.Parse(parts[1]);
            var aareader = sqlite.ExecuteReader(string.Format("select aa.OneLetterCode, aam.DeltaMass from Aminoacids as aa, AminoacidModifications as aam where aa.AminoacidID={0} and aam.AminoacidModificationID={1}", aaid, modid), null);
            while (aareader.Read())
            {
              var aaChar = aareader.GetString(0)[0];
              var aaDeltaMass = aareader.GetDouble(1);
              var curAminoacid = result[aaChar];
              curAminoacid.ResetMass(curAminoacid.MonoMass + aaDeltaMass, curAminoacid.AverageMass + aaDeltaMass);
            }
          }
        }
      }

      return result;
    }

    public void LinkPeptideToProtein(string fileName, Dictionary<int, IIdentifiedProtein> proMap, Dictionary<int, IIdentifiedPeptide> pepMap)
    {
      SQLiteDBHelper sqlite = new SQLiteDBHelper(fileName);

      var sqlLink = "select PeptideID, ProteinID from PeptidesProteins";
      var linkReader = sqlite.ExecuteReader(sqlLink, null);

      while (linkReader.Read())
      {
        var pepid = linkReader.GetInt32(0);
        var proid = linkReader.GetInt32(1);

        if (proMap.ContainsKey(proid) && pepMap.ContainsKey(pepid))
        {
          var pro = proMap[proid];
          var pep = pepMap[pepid];
          pro.Peptides.Add(pep);
          pep.AddProtein(pro.Name);
        }
      }
    }

    public Dictionary<int, IIdentifiedProtein> ParseProteinMap(string fileName)
    {
      SQLiteDBHelper sqlite = new SQLiteDBHelper(fileName);

      var result = new Dictionary<int, IIdentifiedProtein>();

      string sqlProtein = "select ps.ProteinID, pa.Description, pro.Sequence, ps.ProteinScore, ps.Coverage from ProteinAnnotations as pa, Proteins as pro, ProteinScores as ps where pro.ProteinID=pa.ProteinID and pro.ProteinID=ps.ProteinID";
      var proteinReader = sqlite.ExecuteReader(sqlProtein, null);
      Progress.SetMessage("Parsing proteins ...");
      while (proteinReader.Read())
      {
        var protein = new IdentifiedProtein();
        var proid = proteinReader.GetInt32(0);
        var des = proteinReader.GetString(1);
        if (des.Length > 0 && des[0] == '>')
        {
          des = des.Substring(1);
        }
        protein.Reference = des;
        protein.Sequence = proteinReader.GetString(2);
        protein.Score = proteinReader.GetDouble(3);
        protein.Coverage = proteinReader.GetDouble(4);
        result[proid] = protein;
      }

      return result;
    }

    public List<Sequence> ParseProteinSequences(string fileName)
    {
      SQLiteDBHelper sqlite = new SQLiteDBHelper(fileName);

      var result = new List<Sequence>();

      string sqlProtein = "select pa.Description, pro.Sequence from ProteinAnnotations as pa, Proteins as pro where pro.ProteinID=pa.ProteinID";
      var proteinReader = sqlite.ExecuteReader(sqlProtein, null);
      Progress.SetMessage("Parsing proteins ...");
      while (proteinReader.Read())
      {
        var protein = new IdentifiedProtein();
        var des = proteinReader.GetString(0);
        if (des.Length > 0 && des[0] == '>')
        {
          des = des.Substring(1);
        }
        result.Add(new Sequence(des, proteinReader.GetString(1)));
      }

      return result;
    }

    public Dictionary<int, ModificationEntry> ParseModifications(string fileName)
    {
      Dictionary<int, ModificationEntry> result = new Dictionary<int, ModificationEntry>();

      AddModifications(fileName, result, "select distinct aam.AminoacidModificationID, aam.DeltaMass, aam.ModificationName, aam.PositionType from PeptidesAminoacidModifications as paam, AminoAcidModifications as aam where paam.AminoAcidModificationID=aam.AminoAcidModificationID");
      AddModifications(fileName, result, "select distinct aam.AminoacidModificationID, aam.DeltaMass, aam.ModificationName, aam.PositionType from PeptidesTerminalModifications as paam, AminoAcidModifications as aam where paam.TerminalModificationID=aam.AminoAcidModificationID");

      return result;
    }

    private static void AddModifications(string fileName, Dictionary<int, ModificationEntry> result, string sqlMod)
    {
      var modstr = ModificationConsts.MODIFICATION_CHAR;

      var modReader = new SQLiteDBHelper(fileName).ExecuteReader(sqlMod, null);

      while (modReader.Read())
      {
        var id = modReader.GetInt32(0);
        var deltamass = modReader.GetDouble(1);
        var modname = modReader.GetString(2);
        var positiontype = modReader.GetInt32(3);

        string position = string.Empty;
        if (positiontype == 1)
        {
          position = " (N-term)";
        }
        else if (positiontype == 2)
        {
          position = " (C-term)";
        }

        result[id] = new ModificationEntry()
        {
          PositionType = positiontype,
          SignChar = modstr[result.Count + 1],
          SignStr = string.Format("{0:0.000000} {1}{2}", deltamass, modname, position),
          DeltaMass = deltamass
        };
      }
    }

    public Dictionary<int, string> ParseFileMap(string fileName)
    {
      var result = new Dictionary<int, string>();

      SQLiteDBHelper sqlite = new SQLiteDBHelper(fileName);
      var fiReader = sqlite.ExecuteReader("Select FileID, FileName from FileInfos", null);

      while (fiReader.Read())
      {
        result[fiReader.GetInt32(0)] = Path.GetFileNameWithoutExtension(new FileInfo(fiReader.GetString(1)).Name);
      }

      return result;
    }

    public Dictionary<int, string> ParsePeptideFileMap(string fileName)
    {
      var result = new Dictionary<int, string>();

      var fileMap = ParseFileMap(fileName);

      SQLiteDBHelper sqlite = new SQLiteDBHelper(fileName);
      var fiReader = sqlite.ExecuteReader("Select FileID, FileName from FileInfos", null);

      Dictionary<int, string> fiMap = new Dictionary<int, string>();
      while (fiReader.Read())
      {
        fiMap[fiReader.GetInt32(0)] = Path.GetFileNameWithoutExtension(new FileInfo(fiReader.GetString(1)).Name);
      }

      return result;
    }

    public Dictionary<int, IIdentifiedSpectrum> ParseSpectrumMap(string fileName)
    {
      SQLiteDBHelper sqlite = new SQLiteDBHelper(fileName);

      Dictionary<int, IIdentifiedSpectrum> result = new Dictionary<int, IIdentifiedSpectrum>();

      var fileMap = ParseFileMap(fileName);

      //读取肽段列表
      var peptideReader = sqlite.ExecuteReader("select sh.SpectrumID, sh.FirstScan, sh.LastScan, sh.RetentionTime, sh.Charge, mp.FileID, sh.Mass from spectrumheaders as sh, MassPeaks as mp where sh.MassPeakID=mp.MassPeakID", null);
      Progress.SetMessage("Parsing peptides ...");

      while (peptideReader.Read())
      {
        var specid = peptideReader.GetInt32(0);

        IIdentifiedSpectrum spectrum = new IdentifiedSpectrum();
        result[specid] = spectrum;

        spectrum.Query.FileScan.FirstScan = peptideReader.GetInt32(1);
        spectrum.Query.FileScan.LastScan = peptideReader.GetInt32(2);
        //retention time
        spectrum.Query.FileScan.Charge = peptideReader.GetInt32(4);
        spectrum.Query.FileScan.Experimental = fileMap[peptideReader.GetInt32(5)];
        spectrum.ExperimentalMH = peptideReader.GetDouble(6);
        spectrum.Rank = 1;
      }

      return result;
    }

    public Dictionary<int, IIdentifiedPeptide> ParsePeptideMap(string fileName)
    {
      SQLiteDBHelper sqlite = new SQLiteDBHelper(fileName);

      Dictionary<int, IIdentifiedPeptide> result = new Dictionary<int, IIdentifiedPeptide>();

      var pniReader = sqlite.ExecuteReader("select distinct(ProcessingNodeID) from peptidescores", null);
      if (!pniReader.Read())
      {
        return result;
      }
      var nodeid = pniReader.GetInt32(0);

      var pniScore = sqlite.ExecuteReader(string.Format("select scoreid from processingnodescores where processingnodeid={0} and ismainscore=1", nodeid), null);
      if (!pniScore.Read())
      {
        return result;
      }
      var scoreid = pniScore.GetInt32(0);

      Dictionary<int, IIdentifiedSpectrum> spectra = ParseSpectrumMap(fileName);
      var aas = ParseAminoacids(fileName);

      //读取肽段列表
      string sqlPeptide = string.Format("select pep.SpectrumID, pep.PeptideID, pep.TotalIonsCount, pep.MatchedIonsCount, pep.ConfidenceLevel, pep.Sequence, pep.MissedCleavages, ps.ScoreValue from Peptides as pep, PeptideScores as ps where pep.PeptideID=ps.PeptideID and ps.ScoreID={0} order by pep.SpectrumID, pep.SearchEngineRank", scoreid);
      var peptideReader = sqlite.ExecuteReader(sqlPeptide, null);
      Progress.SetMessage("Parsing peptides ...");

      while (peptideReader.Read())
      {
        var specid = peptideReader.GetInt32(0);
        if (!spectra.ContainsKey(specid))
        {
          continue;
        }

        var pepid = peptideReader.GetInt32(1);
        var seq = peptideReader.GetString(5);
        var missedCleavage = peptideReader.GetInt32(6);
        var score = peptideReader.GetDouble(7);

        IIdentifiedSpectrum spectrum = spectra[specid];
        if (spectrum.Peptides.Count == 0)
        {
          spectrum.TheoreticalIonCount = peptideReader.GetInt32(2);
          spectrum.MatchedIonCount = peptideReader.GetInt32(3);

          IdentifiedPeptide peptide = new IdentifiedPeptide(spectrum);
          peptide.ConfidenceLevel = peptideReader.GetInt32(4);
          peptide.Sequence = seq;
          spectrum.NumMissedCleavages = missedCleavage;

          spectrum.Score = score;
          spectrum.TheoreticalMass = aas.MonoPeptideMass(peptide.Sequence);
          spectrum.Rank = 1;

          spectrum.DeltaScore = 1.0;

          result[pepid] = peptide;
          continue;
        }
        else
        {
          if (score == spectrum.Score)
          {
            IIdentifiedPeptide peptide = new IdentifiedPeptide(spectrum);
            peptide.ConfidenceLevel = peptideReader.GetInt32(4);
            peptide.Sequence = seq;
            result[pepid] = peptide;
            continue;
          }

          if (seq == spectrum.Peptide.Sequence)
          {
            continue;
          }

          spectrum.DeltaScore = (spectrum.Score - score) / spectrum.Score;
        }
      }

      //动态氨基酸修饰
      var modMap = ParseModifications(fileName);
      string sqlPeptideMod = "select PeptideID, AminoAcidModificationID, Position from PeptidesAminoacidModifications order by Position desc";
      var pepModReader = sqlite.ExecuteReader(sqlPeptideMod, null);
      Progress.SetMessage("Parsing peptide modifications ...");
      while (pepModReader.Read())
      {
        var pepid = pepModReader.GetInt32(0);
        if (!result.ContainsKey(pepid))
        {
          continue;
        }

        var modid = pepModReader.GetInt32(1);
        var position = pepModReader.GetInt32(2);

        var mod = modMap[modid];

        var peptide = result[pepid];
        var aminoacid = peptide.Sequence[position];

        if (peptide.IsTopOne())
        {
          var modStr = string.Format("{0} ({1})", mod.SignStr, aminoacid);
          if (string.IsNullOrEmpty(peptide.Spectrum.Modifications))
          {
            peptide.Spectrum.Modifications = modStr;
          }
          else
          {
            peptide.Spectrum.Modifications = peptide.Spectrum.Modifications + "; " + modStr;
          }
          peptide.Spectrum.TheoreticalMass += mod.DeltaMass;
        }

        var modchar = mod.SignChar;
        var seq = peptide.Sequence;
        peptide.Sequence = seq.Insert(position + 1, modchar.ToString());
      }

      //动态末端修饰
      string sqlTermMod = "select PeptideID, TerminalModificationID from PeptidesTerminalModifications";
      var termModReader = sqlite.ExecuteReader(sqlTermMod, null);
      Progress.SetMessage("Parsing terminal modifications ...");
      while (termModReader.Read())
      {
        var pepid = termModReader.GetInt32(0);
        if (result.ContainsKey(pepid))
        {
          var modid = termModReader.GetInt32(1);
          var peptide = result[pepid];
          var mod = modMap[modid];

          if (peptide.IsTopOne())
          {
            if (string.IsNullOrEmpty(peptide.Spectrum.Modifications))
            {
              peptide.Spectrum.Modifications = mod.SignStr;
            }
            else if (mod.PositionType == 1)
            {
              peptide.Spectrum.Modifications = mod.SignStr + "; " + peptide.Spectrum.Modifications;
            }
            else
            {
              peptide.Spectrum.Modifications = peptide.Spectrum.Modifications + "; " + mod.SignStr;
            }
            peptide.Spectrum.TheoreticalMass += mod.DeltaMass;
          }

          var modchar = mod.SignChar;
          var seq = peptide.Sequence;
          if (mod.PositionType == 1)
          {
            seq = modchar.ToString() + seq;
          }
          else
          {
            seq = seq + modchar.ToString();
          }
          peptide.Sequence = seq;
        }
      }

      //其他Score
      var dcReader = sqlite.ExecuteReader("select ps.PeptideID, pns.ScoreName, ps.ScoreValue from PeptideScores as ps, ProcessingNodeScores as pns where ps.ScoreID=pns.ScoreID and pns.IsMainScore=0", null);
      while (dcReader.Read())
      {
        var pepid = dcReader.GetInt32(0);
        if (result.ContainsKey(pepid))
        {
          var pep = result[pepid];
          if (pep.IsTopOne())
          {
            var name = dcReader.GetString(1);
            var value = dcReader.GetDouble(2);
            if (name.Equals("SpScore"))
            {
              pep.Spectrum.SpScore = value;
            }
            else if (name.Equals("ProbabilityScore"))
            {
              pep.Spectrum.PValue = value;
            }
          }
        }
      }

      return result;
    }

    protected override void RefineModifications(IIdentifiedResult ir) { }
  }
}
