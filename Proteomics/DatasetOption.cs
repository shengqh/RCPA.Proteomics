using RCPA.Proteomics.MSGF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics
{
  public class DatasetOption
  {
    public DatasetOption()
    {
      DatasetName = string.Empty;
      Database = string.Empty;
      StaticModification = "C 57.021464";
      DynamicModification = "M * 15.994915";
      MinimumTerminiCleavages = 2;
      MaximumMissedCleavages = 2;
      MaximumDynamicModifications = 2;
      DecoyPrefix = string.Empty;
      EnzymeId = MSGFEnzyme.Trypsin;
    }

    public string DatasetName { get; set; }

    /// <summary>
    /// Specifies the FASTA protein database to be searched.
    /// </summary>
    public string Database { get; set; }

    /// <summary>
    /// A generated sequence candidate is only compared to an experimental spectrum if the candidates mass 
    /// is within this tolerance of the experimental spectrum’s precursor mass. 
    /// </summary>
    public double PrecursorTolerance { get; set; }

    /// <summary>
    /// If the precursor tolerance in PPM unit, otherwise in dalton unit
    /// </summary>
    public bool PrecursorTolerancePPM { get; set; }

    /// <summary>
    /// This parameter controls how much tolerance there is on each side of the calculated m/z when looking 
    /// for an ion fragment peak during candidate scoring.
    /// </summary>
    public double FragmentTolerance { get; set; }

    /// <summary>
    /// If the fragment tolerance in PPM unit, otherwise in dalton unit
    /// </summary>
    public bool FragmentTolerancePPM { get; set; }


    public MSGFFragmentMethod FragmentMethodID { get; set; }
    public MSGFMS2Detector MS2DetectorID { get; set; }
    public MSGFEnzyme EnzymeId { get; set; }
    public MSGFProtocol ProtocolID { get; set; }
    public string StaticModification { get; set; }
    public string DynamicModification { get; set; }
    public int MinimumTerminiCleavages { get; set; }
    public int MaximumMissedCleavages { get; set; }
    public int MaximumDynamicModifications { get; set; }
    public string DecoyPrefix { get; set; }
  }
}
