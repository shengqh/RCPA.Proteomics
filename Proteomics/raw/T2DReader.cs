using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.GZip;

namespace RCPA.Proteomics.Raw
{
  public class FileHeader
  {
    public short checksum;
    public short fileHeaderSize;
    public long signature;
    public short softwareMajorVersion;
    public short softwareMinorVersion;
    public short fileMajorVersion;
    public short fileMinorVersion;
    public short spectrumHeaderSize;
    public short fileTrailerSize;
    public byte[] instrumentDBProvider = new byte[128];
    public byte[] fileID = new byte[16];

    public override string ToString()
    {
      string s = "Checksum = " + this.checksum + "\n";
      s = s + "File Header Size = " + this.fileHeaderSize + "\n";
      s = s + "Signature = " + this.signature + "\n";
      s = s + "Software Major Version = " + this.softwareMajorVersion + "\n";
      s = s + "Software Minor Version = " + this.softwareMinorVersion + "\n";
      s = s + "File Major Version = " + this.fileMajorVersion + "\n";
      s = s + "File Minor Version = " + this.fileMinorVersion + "\n";
      s = s + "Spectrum Header Size = " + this.spectrumHeaderSize + "\n";
      s = s + "File Trailer Size = " + this.fileTrailerSize + "\n";
      //    s = s + "Instrumnet DB Provider = " + new String(this.instrumentDBProvider) + "\n";
      //    s = s + "File ID = " + new String(this.fileID) + "\n";
      return s;
    }
  }

  public class Calibration
  {
    public static int maxCalibrationConstants = 10;
    public int equationType;
    public int nConstants;
    public double[] constants = new double[10];

    public override string ToString()
    {
      string s = "Equation Type = " + this.equationType + "\n";
      s = s + "Number Of Constants = " + this.nConstants + "\n";
      for (int i = 0; i < 10; ++i)
      {
        s = s + "Constant_" + i + "=" + this.constants[i] + "\r\n";
      }
      return s;
    }
  }

  public class LaserIntensity
  {
    public double min;
    public double max;

    public override string ToString()
    {
      string s = "Laser Intensity Min = " + this.min + "\n";
      s = s + "Laser Intensity Max = " + this.min + "\n";
      return s;
    }
  }

  public class SpectrumHeader
  {
    public int checksum;
    public int unused;
    public long signature;
    public byte[] spectrumID = new byte[16];
    public long instrumentID;
    public int operatingMode;
    public int dataFormat;
    public int compressionType;
    public int dataChecksum;
    public long sizePoints;
    public long sizeBytes;
    public double startTime;
    public double incrementTime;
    public double timeStamp;
    public double totalIONCount;
    public double basePeakTimeS;
    public double basePeakIntensity;
    public long totalShots;
    public long totalAccumulations;
    public Calibration defaultCalibration = new Calibration();
    public Calibration acquisitionCalibration = new Calibration();
    public double[] locationBounds;
    public LaserIntensity laserIntensity = new LaserIntensity();
    public long longflags;
    static char[] hexChar = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

    public override string ToString()
    {
      String s = "CheckSum = " + this.checksum + "\n";
      s = s + "Unused = " + this.unused + "\n";
      s = s + string.Format("Signature = 0x{0:X}", this.signature) + "\n";
      s = s + "Spectrum Id = " + toHexString(this.spectrumID) + "\n";
      s = s + "Instrument Id = " + this.instrumentID + "\n";
      s = s + "Operating Mode = " + this.operatingMode + "\n";
      s = s + "Data Format = " + this.dataFormat + "\n";
      s = s + "Compression Type = " + this.compressionType + "\n";
      s = s + "Data CheckSum = " + this.dataChecksum + "\n";
      s = s + "Size Points = " + this.sizePoints + "\n";
      s = s + "Size Bytes = " + this.sizeBytes + "\n";
      s = s + "Start Time = " + this.startTime + "\n";
      s = s + "Increment Time = " + this.incrementTime + "\n";
      s = s + "Time Stamp = " + this.timeStamp + "\n";
      s = s + "Total Ion Count = " + this.totalIONCount + "\n";
      s = s + "Base Peak Time =" + this.basePeakTimeS + "\n";
      s = s + "Base Peak Intensity=" + this.basePeakIntensity + "\n";
      s = s + "Total Shots = " + this.totalShots + "\n";
      s = s + "Total Accumulations = " + this.totalAccumulations + "\n";
      s = s + this.defaultCalibration.ToString();
      s = s + this.acquisitionCalibration.ToString();
      if (this.locationBounds != null)
      {
        for (int i = 0; i < this.locationBounds.Length; ++i)
        {
          s = s + string.Format("Location Bounds {0}={1}\r\n", i, this.locationBounds[i]);
        }
      }
      s = s + this.laserIntensity.ToString();
      s = s + string.Format("Long Flags = {0}\n", this.longflags);
      return s;
    }

    public static String toHexString(byte[] b)
    {
      return BitConverter.ToString(b).Replace("-", string.Empty);
    }
  }

  public class T2DReader
  {
    String fileName = "";
    BinaryReader fis;
    public FileHeader fileHeader = new FileHeader();
    public SpectrumHeader spectrumHeader = new SpectrumHeader();
    long cursor = 0L;
    public static int PBSpectrumFileUnsigned16 = 1;
    public static int PBSpectrumFileSigned32 = 2;
    public static int PBSpectrumFileFloat32 = 3;
    public static int PBSpectrumFileDouble64 = 4;

    public T2DReader(String fName)
    {
      this.fileName = fName;
    }

    public BinaryReader getDIS(String fileName)
    {
      FileStream fis = new FileStream(fileName, FileMode.Open);
      return new BinaryReader(fis);
    }

    void readCalibration(Calibration cal)
    {
      cal.equationType = readUnsignedShort();
      cal.nConstants = readUnsignedShort();
      for (int i = 0; i < 10; ++i)
      {
        cal.constants[i] = readDouble();
      }
    }

    public long getFileSize()
    {
      return new FileInfo(this.fileName).Length;
    }

    public bool findUShortPattern(int pattern)
    {
      this.fis = getDIS(this.fileName);

      int location = 0;
      try
      {
        while (true)
        {
          try
          {
            int result = fis.ReadUInt16();
            location += 2;
            if (result == 35615)
            {
              Console.WriteLine("" + location);
              return true;
            }
          }
          catch (EndOfStreamException)
          {
            break;
          }
        }
      }
      finally
      {
        this.fis.Close();
      }
      return false;
    }

    public void parse()
    {
      this.fis = getDIS(this.fileName);
      try
      {
        readSpectrumHeader();
      }
      finally
      {
        this.fis.Close();
      }
    }

    public void readFileHeader()
    {
      this.fileHeader.checksum = this.fis.ReadInt16();
      this.fileHeader.fileHeaderSize = this.fis.ReadInt16();
      this.fileHeader.signature = this.fis.ReadInt32();
      this.fileHeader.softwareMajorVersion = this.fis.ReadInt16();
      this.fileHeader.softwareMinorVersion = this.fis.ReadInt16();
      this.fileHeader.fileMajorVersion = this.fis.ReadInt16();
      this.fileHeader.fileMinorVersion = this.fis.ReadInt16();
      this.fileHeader.spectrumHeaderSize = this.fis.ReadInt16();
      this.fileHeader.fileTrailerSize = this.fis.ReadInt16();
      this.fileHeader.instrumentDBProvider = this.fis.ReadBytes(128);
      this.fileHeader.fileID = this.fis.ReadBytes(16);
    }

    long readUnsigedLong()
    {
      return readUnsignedInt();
    }

    int readUnsignedShort(BinaryReader dis)
    {
      return dis.ReadUInt16();
    }

    int readUnsignedShort()
    {
      return this.fis.ReadUInt16();
    }

    long readUnsignedInt()
    {
      return this.fis.ReadUInt32();
    }

    long readUnsignedLong2()
    {
      return this.fis.ReadUInt32();
    }

    double readDouble(GZipInputStream gz)
    {
      byte[] b = new byte[8];
      gz.Read(b, 0, 8);
      return BitConverter.ToDouble(b, 0);
    }

    double readDouble()
    {
      this.cursor += 8L;
      return this.fis.ReadDouble();
    }

    double[] readDouble(int size)
    {
      double[] d = new double[size];
      for (int i = 0; i < size; ++i)
      {
        d[i] = fis.ReadDouble();
      }
      return d;
    }

    byte[] read(int size)
    {
      return this.fis.ReadBytes(size);
    }

    void readSpectrumHeader()
    {
      this.spectrumHeader.checksum = readUnsignedShort();
      this.spectrumHeader.unused = readUnsignedShort();

      this.spectrumHeader.signature = readUnsigedLong();
      this.spectrumHeader.spectrumID = this.fis.ReadBytes(16);
      this.spectrumHeader.instrumentID = readUnsigedLong();
      this.spectrumHeader.operatingMode = readUnsignedShort();
      this.spectrumHeader.dataFormat = readUnsignedShort();
      this.spectrumHeader.compressionType = readUnsignedShort();
      this.spectrumHeader.dataChecksum = readUnsignedShort();
      this.spectrumHeader.sizePoints = readUnsigedLong();
      this.spectrumHeader.sizeBytes = readUnsigedLong();
      this.spectrumHeader.startTime = readDouble();
      this.spectrumHeader.incrementTime = readDouble();
      this.spectrumHeader.timeStamp = readDouble();
      this.spectrumHeader.totalIONCount = readDouble();
      this.spectrumHeader.basePeakTimeS = readDouble();
      this.spectrumHeader.basePeakIntensity = readDouble();
      this.spectrumHeader.totalShots = readUnsigedLong();
      this.spectrumHeader.totalAccumulations = readUnsigedLong();
      readCalibration(this.spectrumHeader.defaultCalibration);
      readCalibration(this.spectrumHeader.acquisitionCalibration);
      this.spectrumHeader.locationBounds = readDouble(4);

      this.spectrumHeader.laserIntensity.min = readDouble();
      this.spectrumHeader.laserIntensity.max = readDouble();
      this.spectrumHeader.longflags = readUnsigedLong();

      //byte[] b = new byte[(int)this.spectrumHeader.sizeBytes];

      //printZippedContents(b);
      //this.fis.Close();
    }

    long computeUncompressedDataSize(int dataFormat, long sizePoints)
    {
      switch (dataFormat)
      {
        case 1:
          return (sizePoints * 2L);
        case 2:
          return (sizePoints * 4L);
        case 3:
          return (sizePoints * 4L);
        case 4:
          return (sizePoints * 8L);
      }
      throw new IOException("Unsupported value for 'dataFormat'");
    }

    //void printZippedContents(byte[] compressed) 
    //{
    //  int dataSize = (int)this.spectrumHeader.sizeBytes;
    //  int uncompressedDataSize = (int)computeUncompressedDataSize(this.spectrumHeader.dataFormat, this.spectrumHeader.sizePoints);

    //  int amountRead = this.fis.Read(compressed, 0, dataSize);

    //  if (amountRead != dataSize)
    //  {
    //    throw new IOException("Short read of compressed data");
    //  }

    //  Inflater inflater = new Inflater();

    //  byte[] uncompressed = new byte[uncompressedDataSize];

    //  inflater.setInput(compressed);
    //  try
    //  {
    //    int result = inflater.inflate(uncompressed);

    //    if (result != uncompressedDataSize)
    //    {
    //      throw new IOException("Inflation failure, expecting " + uncompressedDataSize + " bytes, only found " + result + " bytes");
    //    }

    //  }
    //  catch (DataFormatException dfe)
    //  {
    //    dfe.printStackTrace();
    //  }
    //  int position = 0;
    //  double[] datapoint = new double[(int)this.spectrumHeader.sizePoints];
    //  for (int p = 0; p < this.spectrumHeader.sizePoints; ++p)
    //  {
    //    datapoint[p] = parseFloat32(uncompressed, position);
    //    position += 4;
    //    Console.WriteLine(datapoint[p]);
    //  }
    //}

    //double parseFloat32(byte[] buffer, int position) {
    //  char c1 = (char)buffer[(position + 0)];
    //  char c2 = (char)buffer[(position + 1)];
    //  char c3 = (char)buffer[(position + 2)];
    //  char c4 = (char)buffer[(position + 3)];

    //  int b1 = c4 & 0xFF;
    //  int b2 = c3 & 0xFF;
    //  int b3 = c2 & 0xFF;
    //  int b4 = c1 & 0xFF;

    //  int intBits = b4 << 0 | b3 << 8 | b2 << 16 | b1 << 24;

    //  float flt = Float.intBitsToFloat(intBits);

    //  return flt;
    //}

    //void printZippedContents(int skip) 
    //{
    //  DataInputStream dis = getDIS(this.fileName);
    //  dis.skipBytes(skip + 2);
    //  byte[] m = read(1);
    //  int mm = new Integer(m[0]).intValue();
    //  GZIPInputStream gz = new GZIPInputStream(dis);
    //  for (int i = 0; i < this.spectrumHeader.sizePoints; ++i)
    //  {
    //    try
    //    {
    //      Console.WriteLine("" + readDouble(gz));
    //    }
    //    catch (EOFException e)
    //    {
    //      break label109:
    //    }
    //  }
    //  label109: gz.close();
    //}

    //void printZippedContents(DataInputStream dis)
    //  throws FileNotFoundException, IOException
    //{
    //  Console.WriteLine("cursor+=" + this.cursor);
    //  int k = readUnsignedShort(dis);
    //  GZIPInputStream gz = new GZIPInputStream(dis);

    //  for (int i = 0; i < this.spectrumHeader.sizePoints; ++i)
    //  {
    //    try
    //    {
    //      Console.WriteLine("" + readDouble(gz));
    //    }
    //    catch (EOFException e)
    //    {
    //      return;
    //    }
    //  }
    //}

    //void printZippedContents()
    //  throws FileNotFoundException, IOException
    //{
    //  DataInputStream is = new DataInputStream(new FileInputStream("tmp.txt"));

    //  GZIPInputStream gz = new GZIPInputStream(is);

    //  for (int i = 0; i < this.spectrumHeader.sizePoints; ++i)
    //  {
    //    try
    //    {
    //      Console.WriteLine("" + readDouble(gz));
    //    }
    //    catch (EOFException e)
    //    {
    //      break label84:
    //    }
    //  }
    //  label84: is.close();
    //}

    //public void toString(PrintStream ps) {
    //  this.spectrumHeader.toString(ps);
    //}

    //public static void main(String[] args)
    //{
    //  try {
    //    T2DReader t2d = new T2DReader("T2_279300.t2");

    //    t2d.parse();
    //    t2d.toString(System.out);
    //  }
    //  catch (Exception ex)
    //  {
    //    ex.printStackTrace();
    //  }
    //}
  }
}
