using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RCPA.Utils;
using RCPA.Proteomics.XTandem;

namespace RCPA.Proteomics.Summary
{
  public static class SummaryUtils
  {
    public static string FindSourceFile(string fileName, string fileKey)
    {
      using (var sr = new StreamReader(fileName))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          if (line.StartsWith(fileKey))
          {
            return line.Substring(fileKey.Length);
          }
        }
      }

      return string.Empty;
    }

    public static void FindSourceFile(ListView lvDatFiles, string fileKey, int index)
    {
      lvDatFiles.BeginUpdate();
      try
      {
        foreach (ListViewItem item in lvDatFiles.Items)
        {
          string datFile = item.Text;
          if (File.Exists(datFile))
          {
            while (item.SubItems.Count < index + 1)
            {
              item.SubItems.Add("");
            }

            using (var sr = new StreamReader(datFile))
            {
              string line;
              while ((line = sr.ReadLine()) != null)
              {
                if (line.StartsWith(fileKey))
                {
                  string sourceFilename = line.Substring(fileKey.Length);
                  item.SubItems[index].Text = sourceFilename;
                  break;
                }
              }
            }
          }
        }
      }
      finally
      {
        lvDatFiles.EndUpdate();
      }
    }

    public static void FindXtandemSourceXml(ListView lvDatFiles)
    {
      lvDatFiles.BeginUpdate();
      try
      {
        foreach (ListViewItem item in lvDatFiles.Items)
        {
          string xmlFile = item.Text;
          if (File.Exists(xmlFile))
          {
            while (item.SubItems.Count < 2)
            {
              item.SubItems.Add("");
            }

            item.SubItems[1].Text = new FileInfo(XTandemSpectrumXmlParser.GetSourceFile(xmlFile)).Name;
          }
        }
      }
      finally
      {
        lvDatFiles.EndUpdate();
      }
    }

    public static void RenameAsXtandemSourceXml(ListView lvDatFiles)
    {
      FindXtandemSourceXml(lvDatFiles);

      DoRename(lvDatFiles, "xml");
    }

    public static void FindPFindSource(ListView lvDatFiles)
    {
      FindSourceFile(lvDatFiles, "InputPath=", 1);
    }

    public static void FindPFindDatabase(ListView lvDatFiles)
    {
      FindSourceFile(lvDatFiles, "Fasta=", 2);
    }

    public static void RenameAsPFindSource(ListView lvDatFiles)
    {
      FindPFindSource(lvDatFiles);

      DoRename(lvDatFiles, "txt");
    }

    public static void FindMgf(ListView lvDatFiles)
    {
      FindSourceFile(lvDatFiles, "FILE=", 1);
    }

    public static void FindDatDB(ListView lvDatFiles)
    {
      FindDatDB(lvDatFiles, 2);
    }

    public static void FindDatDB(ListView lvDatFiles, int index)
    {
      FindSourceFile(lvDatFiles, "DB=", index);
    }

    public static void RenameAsMgf(ListView lvDatFiles)
    {
      FindMgf(lvDatFiles);

      DoRename(lvDatFiles, "dat");
    }

    private static void DoRename(ListView lvDatFiles, string extension)
    {
      lvDatFiles.BeginUpdate();
      try
      {
        foreach (ListViewItem item in lvDatFiles.Items)
        {
          if (item.Selected)
          {
            string datFile = item.Text;
            if (File.Exists(datFile))
            {
              if (item.SubItems[1].Text != "")
              {
                string datname = FileUtils.ChangeExtension(new FileInfo(item.SubItems[1].Text).Name, extension);
                string newDatFilename = Path.Combine(new FileInfo(datFile).DirectoryName, datname);

                if (newDatFilename != datFile)
                {
                  new FileInfo(datFile).MoveTo(newDatFilename);
                  item.SubItems[0].Text = newDatFilename;
                }
              }
            }
          }
        }
      }
      finally
      {
        lvDatFiles.EndUpdate();
      }
    }
  }
}
