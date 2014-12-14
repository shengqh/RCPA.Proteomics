using System;
using System.Linq;
using System.Collections.Generic;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using RCPA.R;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricPurityCorrectionRCalculator
  {
    private string rExecute;
    private IsobaricType plexType;
    private List<UsedChannel> channels;
    private bool performPurityCorrection;
    private bool performGraph;

    public IsobaricPurityCorrectionRCalculator(IsobaricType plexType, List<UsedChannel> channels, bool performPurityCorrection, bool performGraph)
      : this(plexType, channels, ExternalProgramConfig.GetExternalProgram("R"), performPurityCorrection, performGraph)
    { }

    public IsobaricPurityCorrectionRCalculator(IsobaricType plexType, List<UsedChannel> channels, string rExecute, bool performPurityCorrection, bool performGraph)
    {
      this.plexType = plexType;
      this.channels = channels;
      this.rExecute = rExecute;
      if (this.rExecute == null)
      {
        throw new Exception("Define R location first!");
      }
      this.performPurityCorrection = performPurityCorrection;
      this.performGraph = performGraph;
    }

    public void Calculate(IsobaricResult ir, string tempFilename)
    {
      if (!this.performPurityCorrection && !this.performGraph)
      {
        return;
      }

      tempFilename = tempFilename.Replace("\\", "/");

      var dataFile = tempFilename + ".data";
      using (var sw = new StreamWriter(dataFile))
      {
        sw.WriteLine("Scan\t{0}", (from c in channels select c.Name).Merge("\t"));
        foreach (var ii in ir)
        {
          sw.WriteLine("{0}\t{1}", ii.Scan.Scan, (from rep in ii.Reporters select string.Format("{0:0.##}", rep)).Merge("\t"));
        }
      }

      var purityFile = tempFilename + ".purity";
      using (var sw = new StreamWriter(purityFile))
      {
        var purity = plexType.IsotopicTable;
        sw.WriteLine("Channel\t{0}", (from c in channels select c.Name).Merge("\t"));
        for (int i = 0; i < channels.Count; i++)
        {
          sw.Write(channels[i].Name);
          for (int j = 0; j < channels.Count; j++)
          {
            sw.Write("\t{0:0.####}", purity[channels[i].Index, channels[j].Index] / 100);
          }
          sw.WriteLine();
        }
      }

      var rfile = tempFilename + ".r";
      var pngFile = tempFilename + ".png";
      using (var sw = new StreamWriter(rfile))
      {
        sw.WriteLine(@"
data<-read.table(""" + dataFile + @""", header=T, row.names=1)
");

        if (this.performPurityCorrection)
        {
          sw.WriteLine(@"
library(nnls)

table<-read.table(""" + purityFile + @""", header=T, row.names=1)
m<-as.matrix(table)
m=t(m)

correctedData<-apply(data, 1, function(x){
  v<-nnls(m, x)
  v$x
})

correctedData<-t(correctedData)
colnames(correctedData)<-colnames(data)

data<-data.frame(correctedData)
");
        }

        sw.WriteLine(@"
write.csv(data, file=""" + tempFilename + @""")
");

        if (this.performGraph)
        {
          sw.WriteLine(@"

dd<-data.frame(data)
png(""" + pngFile + @""",
    width=1000 * ncol(dd), height=1000*ncol(dd), res=300)
split.screen(c(ncol(dd),ncol(dd)))
id<-0
cols<-rainbow(ncol(dd))
for(i in c(1:ncol(dd))){
  for(j in c(1:ncol(dd))){
    id<-id+1
    screen(id)
    if(i == j){
      dl<-list()
      maxy<-0
      for(k in c(1:ncol(dd))){
        if(k != i){
          notZero<-dd[,i] > 0 & dd[,k] > 0
          xx<-dd[notZero,i]
          yy<-dd[notZero,k]
          r<-log2(yy/xx)
          d<-density(r)
          dl[[colnames(dd)[k]]]<-d
          maxy<-max(maxy, max(d$y))
        }  
      }
      maxy<-floor(maxy) + 1
      s<-0
      for(k in c(1:ncol(dd))){
        if(k != i){
          s<-s+1
          if(s == 1){
            par(mar=c(2,2,3,2), new=TRUE)
            plot(dl[[colnames(dd)[k]]], xlim=c(-2,2), ylim=c(0,maxy), xlab="""", col=cols[k], main=colnames(dd)[i])
          }else{
            lines(dl[[colnames(dd)[k]]], col=cols[k])
          }
        }
      }
    }else{
      notZero<-dd[,i] > 0 & dd[,j] > 0
      xx<-dd[notZero,i]
      yy<-dd[notZero,j]
      m<-log2(yy/xx)
      a<-log2((yy+xx)/2)
      par(mar=c(2,2,3,2), new=TRUE)
      plot(x=a, y=m, col=cols[j],xlab="",ylab="",main=paste0(colnames(dd)[j], ""/"", colnames(dd)[i]), cex=0.1, pch=20, xlim=c(10,30), ylim=c(-5,5))
      abline(a=0, b=0)
    }
  }
}
close.screen()
dev.off()

");
        }
      }

      new RProcessor(rExecute, rfile, tempFilename).Process();

      if (!File.Exists(tempFilename))
      {
        throw new Exception(string.Format("Purity correction failed!\nMake sure that your R and R packages nnls, limma have been installed.\nOtherwise, send those three files to shengqh@gmail.com\n{0}\n{1}\n{2}",
          dataFile,
          purityFile,
          rfile));
      }

      //read back all corrected intensities
      using (var sr = new StreamReader(tempFilename))
      {
        var line = sr.ReadLine();
        var index = 0;
        while ((line = sr.ReadLine()) != null)
        {
          if (string.IsNullOrWhiteSpace(line))
          {
            break;
          }

          var parts = line.Split(',');
          for (int i = 1; i < parts.Length; i++)
          {
            ir[index][i - 1] = double.Parse(parts[i]);
          }
          index++;
        }
      }

      //File.Delete(dataFile);
      //File.Delete(purityFile);
      //File.Delete(rfile);
      //File.Delete(tempFilename);
    }
  }
}