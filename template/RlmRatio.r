#predefine_start

outputdir<-"H:/shengquanhu/projects/rcpa/TurboRaw2Mgf/iTRAQ4/mascot/summary"
inputfile<-"8-deisotopic-top10-removeitraq-range.noredundant.I114I115.isobaric.GK.tsv"
outputfile<-"8-deisotopic-top10-removeitraq-range.noredundant.I114I115.isobaric.GK.sig.tsv"
references<-c("I114", "I115")
samples<-c("I116", "I117")

#predefine_end

require("outliers")
library("MASS")

showlm <- function(fit, samname, refname, data, estkey, name){
  form<-paste0(samname, " ~ ", refname)
  inter<-summary(fit)$coefficients["(Intercept)", estkey]
  r<-summary(fit)$coefficients[refname, estkey]
  plot(data[, refname], data[, samname], main=paste0(name, "\n", samname, " = ", sprintf("%.f", inter), " + ", sprintf("%.2f", r), " * ", refname),
       xlab=refname, ylab=samname)
  lines(data[, refname], inter + data[, refname] * r, col="red")
}

setwd(outputdir)

alldata<-read.table(inputfile, header=T, sep="\t")
groups<-unique(alldata$GroupIndex)

ratio<-alldata[,references[1]] / alldata[,references[2]]
logratio<-log(ratio)
alldata$logratioSR<-log(alldata[,references[1]] / alldata[,references[2]])
s1<-!scores(alldata$logratioSR, type="z", prob=0.95)


group<-1
for(group in groups){
  data<-alldata[alldata$GroupIndex == group,]
  
  if(length(references) == 2){

    refdata<-data[,references]
    data$REF<-apply(refdata, 1, mean)
    data$CV<-apply(refdata, 1, function(x) 100*(sd(x,na.rm=TRUE)/mean(x,na.rm=TRUE)) )
  }else{
    data$REF<-data[,references[1]]
    data$CV<-1
  }
  
  data$logratioSR<-log(data$I115 / data$I114)
  s1<-!scores(data$logratioSR, type="z", prob=0.95)
  
  pngfile<-paste0(outputfile, ".", group, ".png")
  png(pngfile, width=2000, height=2000, res=300)
  showlm(rlm(I115 ~ I114, data=data[s1,]), "I115", "I114", data, "Value", "Robust linear regression")
  dev.off()
}
