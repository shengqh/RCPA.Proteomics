#predefine_start

outputdir<-"H:/shengquanhu/projects/rcpa/TurboRaw2Mgf/iTRAQ4/mascot/summary"
inputfile<-"8-deisotopic-top10-removeitraq-range.noredundant.I114I115.isobaric.GK.tsv"
outputfile<-"8-deisotopic-top10-removeitraq-range.noredundant.I114I115.isobaric.GK.sig.tsv"

#predefine_end

library("rrcov")

setwd(outputdir)

showpca <- function(fit, samname, refname, data, name){
  form<-paste0(" ~ ", samname, refname)
  r <- fit$rotation[2,1] / fit$rotation[1,1]
  inter <- fit$center[2] - r*fit$center[1]
  perc<-(fit$sdev[1] ** 2) / (fit$sdev[1] ** 2 + fit$sdev[2] ** 2)
  
  plot(data[, refname], data[, samname], main=paste0(name, "\n", samname, " = ", sprintf("%.f", inter), " + ", sprintf("%.2f", r), " * ", refname,
                                                     "\n", sprintf("perc=%.3f", perc)), xlab=refname, ylab=samname)
  lines(data[, refname], inter + data[, refname] * r, col="red")
}

alldata<-read.table(inputfile, header=T, sep="\t")
groups<-unique(alldata$GroupIndex)

group<-5
for(group in groups){
  data<-alldata[alldata$GroupIndex == group,]
  fit<-getPrcomp(PcaHubert(~I114+I115, data = data))
  r <- fit$rotation[2,1] / fit$rotation[1,1]
  inter <- fit$center[2] - r*fit$center[1]
  perc<-(fit$sdev[1] ** 2) / (fit$sdev[1] ** 2 + fit$sdev[2] ** 2)
  
  refname<-"I114"
  samname<-"I115"
  
  pngfile<-paste0(outputfile, ".", group, ".png")
  png(pngfile, width=2000, height=2000, res=300)
  plot(data[, refname], data[, samname], main=paste0(data[1,"Protein"], "\n" ,samname, " = ", sprintf("%.f", inter), " + ", sprintf("%.2f", r), " * ", refname,
                                                     "\n", sprintf("perc=%.3f", perc)), xlab=refname, ylab=samname)
  lines(data[, refname], inter + data[, refname] * r, col="red")
  dev.off()
}
