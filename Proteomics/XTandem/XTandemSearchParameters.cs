using RCPA.Proteomics.MSGF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.XTandem
{
  public class XTandemSearchParameters
  {

    public XTandemSearchParameters()
    {
      StaticModification = "58.005479@C";
      DynamicModification = "15.994915@M";
      CleavageRole = "[RK]|{P}";
      RefineUnanticipatedCleavage = false;
      NumThreads = 8;
    }

    /// <summary>
    /// *.mzML, *.mzXML, *.mgf, *.ms2, *.pkl or *_dta.txt
    /// </summary>
    public string SpectrumFile { get; set; }

    /// <summary>
    /// *.fasta or *.fa
    /// </summary>
    public string DatabaseFile { get; set; }

    /// <summary>
    /// output result file
    /// </summary>
    public string OutputFile { get; set; }

    /// <summary>
    /// Precursor mass tolerance
    /// </summary>
    public double PrecursorTolerance { get; set; }

    /// <summary>
    /// ppm or Daltons
    /// </summary>
    public bool PrecursorTolerancePPM { get; set; }

    /// <summary>
    /// Fragment mass tolerance
    /// </summary>
    public double FragmentTolerance { get; set; }

    /// <summary>
    /// ppm or Daltons
    /// </summary>
    public bool FragmentTolerancePPM { get; set; }

    /// <summary>
    /// Number of concurrent threads to be executed, Default: 8
    /// </summary>
    public int NumThreads { get; set; }

    /// <summary>
    /// 58.005479@C
    /// </summary>
    public string StaticModification { get; set; }

    /// <summary>
    /// 15.994915@M
    /// </summary>
    public string DynamicModification { get; set; }

    /// <summary>
    /// [RK]|{P}
    /// </summary>
    public string CleavageRole { get; set; }

    /// <summary>
    /// no
    /// </summary>
    public bool RefineUnanticipatedCleavage { get; set; }

    public void SaveToFile(string fileName)
    {
      var taxonomyFile = Path.ChangeExtension(fileName, ".taxonomy.xml");
      using (var stream = new FileStream(taxonomyFile, FileMode.Create))
      {
        using (var sw = new StreamWriter(stream, Encoding.UTF8))
        {
          sw.WriteLine(@"<?xml version=""1.0""?>
<bioml label=""x! taxon-to-file matching list"">
	<taxon label=""all"">
		<file format=""peptide"" URL=""{0}"" />
	</taxon>
</bioml>", DatabaseFile);
        }
      }

      using (var stream = new FileStream(fileName, FileMode.Create))
      {
        using (var sw = new StreamWriter(stream, Encoding.UTF8))
        {
          sw.WriteLine(@"<?xml version=""1.0""?>
<?xml-stylesheet type=""text/xsl"" href=""tandem-input-style.xsl""?>
<bioml>
<note>list path parameters</note>
	<note type=""input"" label=""list path, taxonomy information"">" + taxonomyFile + @"</note>

<note>spectrum parameters</note>
	<note type=""input"" label=""spectrum, path"">" + SpectrumFile + @"</note>
	<note type=""input"" label=""spectrum, parent monoisotopic mass error plus"">" + PrecursorTolerance.ToString() + @"</note>
	<note type=""input"" label=""spectrum, parent monoisotopic mass error minus"">" + PrecursorTolerance.ToString() + @"</note>
	<note type=""input"" label=""spectrum, parent monoisotopic mass isotope error"">yes</note>
	<note type=""input"" label=""spectrum, parent monoisotopic mass error units"">" + (PrecursorTolerancePPM ? "ppm" : "Daltons") + @"</note>
	<note>The value for this parameter may be 'Daltons' or 'ppm': all other values are ignored</note>
	<note type=""input"" label=""spectrum, fragment monoisotopic mass error"">" + FragmentTolerance.ToString() + @"</note>
	<note type=""input"" label=""spectrum, fragment monoisotopic mass error units"">" + (FragmentTolerancePPM ? "ppm" : "Daltons") + @"</note>
	<note type=""input"" label=""spectrum, fragment mass type"">monoisotopic</note>
		<note>values are monoisotopic|average </note>

<note>spectrum conditioning parameters</note>
	<note type=""input"" label=""spectrum, dynamic range"">100.0</note>
		<note>The peaks read in are normalized so that the most intense peak
		is set to the dynamic range value. All peaks with values of less that
		1, using this normalization, are not used. This normalization has the
		overall effect of setting a threshold value for peak intensities.</note>
	<note type=""input"" label=""spectrum, total peaks"">50</note> 
		<note>If this value is 0, it is ignored. If it is greater than zero (lets say 50),
		then the number of peaks in the spectrum with be limited to the 50 most intense
		peaks in the spectrum. X! tandem does not do any peak finding: it only
		limits the peaks used by this parameter, and the dynamic range parameter.</note>
	<note type=""input"" label=""spectrum, maximum parent charge"">4</note>
	<note type=""input"" label=""spectrum, use noise suppression"">no</note>
	<note type=""input"" label=""spectrum, minimum parent m+h"">500.0</note>
	<note type=""input"" label=""spectrum, minimum fragment mz"">200.0</note>
	<note type=""input"" label=""spectrum, minimum peaks"">5</note> 
	<note type=""input"" label=""spectrum, threads"">" + NumThreads.ToString() + @"</note>
	<note type=""input"" label=""spectrum, sequence batch size"">1000</note>
	
<note>residue modification parameters</note>
	<note type=""input"" label=""residue, modification mass"">" + StaticModification + @"</note>
	<note type=""input"" label=""residue, potential modification mass"">" + DynamicModification + @"</note>
	<note type=""input"" label=""residue, potential modification motif""></note>
		<note>The format of this parameter is similar to residue, modification mass,
		with the addition of a modified PROSITE notation sequence motif specification.
		For example, a value of 80@[ST!]PX[KR] indicates a modification
		of either S or T when followed by P, and residue and the a K or an R.
		A value of 204@N!{P}[ST]{P} indicates a modification of N by 204, if it
		is NOT followed by a P, then either an S or a T, NOT followed by a P.
		Positive and negative values are allowed.
		</note>

<note>protein parameters</note>
	<note type=""input"" label=""protein, taxon"">all</note>
		<note>This value is interpreted using the information in taxonomy.xml.</note>
	<note type=""input"" label=""protein, cleavage site"">" + CleavageRole + @"</note>
		<note>this setting corresponds to the enzyme trypsin. The first characters
		in brackets represent residues N-terminal to the bond - the '|' pipe -
		and the second set of characters represent residues C-terminal to the
		bond. The characters must be in square brackets (denoting that only
		these residues are allowed for a cleavage) or french brackets (denoting
		that these residues cannot be in that position). Use UPPERCASE characters.
		To denote cleavage at any residue, use [X]|[X] and reset the 
		scoring, maximum missed cleavage site parameter (see below) to something like 50.
		</note>
	<note type=""input"" label=""protein, cleavage semi"">no</note>
	<note type=""input"" label=""protein, modified residue mass file""></note>
	<note type=""input"" label=""protein, cleavage C-terminal mass change"">17.002735</note>
	<note type=""input"" label=""protein, cleavage N-terminal mass change"">+1.007825</note>
	<note type=""input"" label=""protein, quick acetyl"">yes</note>
	<note type=""input"" label=""protein, quick pyrolidone"">yes</note>
	<note type=""input"" label=""protein, stP bias"">no</note>
	<note type=""input"" label=""protein, N-terminal residue modification mass"">0.0</note>
	<note type=""input"" label=""protein, C-terminal residue modification mass"">0.0</note>
	<note type=""input"" label=""protein, homolog management"">no</note>
		<note>if yes, an upper limit is set on the number of homologues kept for a particular spectrum</note>

<note>model refinement parameters</note>
	<note type=""input"" label=""refine"">yes</note>
	<note type=""input"" label=""refine, modification mass""></note>
	<note type=""input"" label=""refine, sequence path""></note>
	<note type=""input"" label=""refine, tic percent"">20</note>
	<note type=""input"" label=""refine, spectrum synthesis"">yes</note>
	<note type=""input"" label=""refine, maximum valid expectation value"">0.01</note>
	<note type=""input"" label=""refine, unanticipated cleavage"">" + (RefineUnanticipatedCleavage ? "yes" : "no") + @"</note>
	<note type=""input"" label=""refine, cleavage semi"">no</note>
	<note type=""input"" label=""refine, point mutations"">no</note>
	<note type=""input"" label=""refine, saps"">no</note>
	<note type=""input"" label=""refine, use potential modifications for full refinement"">no</note>
	<note type=""input"" label=""refine, potential N-terminus modifications""></note>
	<note type=""input"" label=""refine, potential C-terminus modifications""></note>
	<note type=""input"" label=""refine, potential modification mass""></note>
	<note type=""input"" label=""refine, potential modification motif""></note>
	<note>The format of this parameter is similar to residue, modification mass,
		with the addition of a modified PROSITE notation sequence motif specification.
		For example, a value of 80@[ST!]PX[KR] indicates a modification
		of either S or T when followed by P, and residue and the a K or an R.
		A value of 204@N!{P}[ST]{P} indicates a modification of N by 204, if it
		is NOT followed by a P, then either an S or a T, NOT followed by a P.
		Positive and negative values are allowed.
		</note>

<note>scoring parameters</note>
	<note type=""input"" label=""scoring, minimum ion count"">4</note>
	<note type=""input"" label=""scoring, maximum missed cleavage sites"">2</note>
	<note type=""input"" label=""scoring, x ions"">no</note>
	<note type=""input"" label=""scoring, y ions"">yes</note>
	<note type=""input"" label=""scoring, z ions"">no</note>
	<note type=""input"" label=""scoring, a ions"">no</note>
	<note type=""input"" label=""scoring, b ions"">yes</note>
	<note type=""input"" label=""scoring, c ions"">no</note>
	<note type=""input"" label=""scoring, cyclic permutation"">no</note>
		<note>if yes, cyclic peptide sequence permutation is used to pad the scoring histograms</note>
	<note type=""input"" label=""scoring, include reverse"">no</note>
		<note>if yes, then reversed sequences are searched at the same time as forward sequences</note>

<note>output parameters</note>
	<note type=""input"" label=""output, log path""></note>
	<note type=""input"" label=""output, message""></note>
	<note type=""input"" label=""output, one sequence copy"">no</note>
	<note type=""input"" label=""output, sequence path""></note>
	<note type=""input"" label=""output, path"">" + OutputFile + @"</note>
	<note type=""input"" label=""output, sort results by"">spectrum</note>
		<note>values = protein|spectrum (spectrum is the default)</note>
	<note type=""input"" label=""output, path hashing"">yes</note>
		<note>values = yes|no</note>
	<note type=""input"" label=""output, xsl path""></note>
	<note type=""input"" label=""output, parameters"">yes</note>
		<note>values = yes|no</note>
	<note type=""input"" label=""output, performance"">yes</note>
		<note>values = yes|no</note>
	<note type=""input"" label=""output, spectra"">yes</note>
		<note>values = yes|no</note>
	<note type=""input"" label=""output, histograms"">no</note>
		<note>values = yes|no</note>
	<note type=""input"" label=""output, proteins"">yes</note>
		<note>values = yes|no</note>
	<note type=""input"" label=""output, sequences"">no</note>
		<note>values = yes|no</note>
	<note type=""input"" label=""output, one sequence copy"">no</note>
		<note>values = yes|no, set to yes to produce only one copy of each protein sequence in the output xml</note>
	<note type=""input"" label=""output, results"">all</note>
		<note>values = all|valid|stochastic</note>
	<note type=""input"" label=""output, maximum valid expectation value"">100.0</note>
		<note>value is used in the valid|stochastic setting of output, results</note>
	<note type=""input"" label=""output, histogram column width"">50</note>
		<note>values any integer greater than 0. Setting this to '1' makes cutting and pasting histograms
		into spread sheet programs easier.</note>
<note type=""description"">ADDITIONAL EXPLANATIONS</note>
	<note type=""description"">Each one of the parameters for X! tandem is entered as a labeled note
			node. In the current version of X!, keep those note nodes
			on a single line.
	</note>
	<note type=""description"">The presence of the type 'input' is necessary if a note is to be considered
			an input parameter.
	</note>
	<note type=""description"">Any of the parameters that are paths to files may require alteration for a 
			particular installation. Full path names usually cause the least trouble,
			but there is no reason not to use relative path names, if that is the
			most convenient.
	</note>
	<note type=""description"">Any parameter values set in the 'list path, default parameters' file are
			reset by entries in the normal input file, if they are present. Otherwise,
			the default set is used.
	</note>
	<note type=""description"">The 'list path, taxonomy information' file must exist.
		</note>
	<note type=""description"">The directory containing the 'output, path' file must exist: it will not be created.
		</note>
	<note type=""description"">The 'output, xsl path' is optional: it is only of use if a good XSLT style sheet exists.
		</note>

</bioml>
");
        }
      }
    }
  }
}
