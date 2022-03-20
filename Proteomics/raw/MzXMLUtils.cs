using RCPA.Proteomics.Spectrum;
using System;

namespace RCPA.Proteomics.Raw
{
  public static class MzxmlHelper
  {
    private static readonly int precision = 32;
    private static readonly int sigBytes = precision / 8;

    public static byte[] GetNetworkModeBytesFromFloat(float value)
    {
      byte[] hostModeBytes = BitConverter.GetBytes(value);
      return FlipArray(hostModeBytes, sigBytes);
    }

    public static float GetDoubleFromGetNetworkModeBytes(byte[] value)
    {
      byte[] hostModeBytes = FlipArray(value, sigBytes);
      return BitConverter.ToSingle(hostModeBytes, 0);
    }

    //flips the byte array from network to host mode or from host mode to network, 
    //rearranges certain number of bytes in an array (sigBytes no. of significant bytes) backwords
    public static byte[] FlipArray(byte[] array, int sigBytes)
    {
      var flippedArray = new byte[array.Length];

      for (int i = 0; i < array.Length; i += sigBytes)
      {
        for (int j = sigBytes - 1; j >= 0; j--)
        {
          flippedArray[i + j] = array[i + (sigBytes - j - 1)];
        }
      }

      return flippedArray;
    }

    public static string PeakListToBase64(PeakList<Peak> pkl)
    {
      var totalBytes = new byte[pkl.Count * 8];
      int index = -1;
      foreach (Peak peak in pkl)
      {
        index++;
        byte[] mzBytes = GetNetworkModeBytesFromFloat((float)peak.Mz);
        byte[] intensityBytes = GetNetworkModeBytesFromFloat((float)peak.Intensity);
        for (int i = 0; i < 4; i++)
        {
          totalBytes[index * 8 + i] = mzBytes[i];
          totalBytes[index * 8 + 4 + i] = intensityBytes[i];
        }
      }

      return Convert.ToBase64String(totalBytes);
    }

    public static PeakList<Peak> Base64ToPeakList(string peaksStr)
    {
      var result = new PeakList<Peak>();

      byte[] bytes = Convert.FromBase64String(peaksStr);
      for (int index = 0; index < bytes.Length; index += 8)
      {
        var mzBytes = new byte[4];
        var intensityBytes = new byte[4];
        for (int i = 0; i < 4; i++)
        {
          mzBytes[i] = bytes[index + i];
          intensityBytes[i] = bytes[index + 4 + i];
        }

        double mz = GetDoubleFromGetNetworkModeBytes(mzBytes);
        double intensity = GetDoubleFromGetNetworkModeBytes(intensityBytes);
        var p = new Peak(mz, intensity);
        result.Add(p);
      }

      return result;
    }
  }
}