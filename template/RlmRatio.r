#predefine_start

outputdir<-"H:/shengquanhu/projects/rcpa/IsobaricLabelling/iTRAQ4"
inputfile<-"09-deisotopic-remove-isobaric-low.noredundant.I114I115.isobaric.peptides.details.tsv"
outputfile<-"09-deisotopic-remove-isobaric-low.noredundant.I114I115.isobaric.peptides.tsv"
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

showpca <- function(fit, samname, refname, data, name){
  form<-paste0(" ~ ", samname, refname)
  r <- fit$rotation[2,1] / fit$rotation[1,1]
  inter <- fit$center[2] - r*fit$center[1]
  perc<-(fit$sdev[1] ** 2) / (fit$sdev[1] ** 2 + fit$sdev[2] ** 2)
  
  plot(data[, refname], data[, samname], main=paste0(name, "\n", samname, " = ", sprintf("%.f", inter), " + ", sprintf("%.2f", r), " * ", refname,
                                                     "\n", sprintf("perc=%.3f", perc)), xlab=refname, ylab=samname)
  lines(data[, refname], inter + data[, refname] * r, col="red")
}

setwd(outputdir)

alldata<-read.table(inputfile, header=T, sep="\t")

if(length(references) == 2){
  ratio<-alldata[,references[1]] / alldata[,references[2]]
  logratio<-log(ratio)
  logratioSR<-log(alldata[,references[1]] / alldata[,references[2]])
  s1<-!scores(logratioSR, type="z", prob=0.95)
  alldata<-alldata[s1,]
  
  alldata$REF<-(alldata[,references[1]] + alldata[,references[2]])/2
}else{
  alldata$REF<-alldata[,references[1]]
}

groups<-unique(alldata$PurePeptide)
datasets<-unique(alldata$Dataset)

group<-groups[1]
for(group in groups){
  data<-alldata[alldata$PurePeptide == group,]
  
  data$logratioSR<-log(data$I115 / data$I114)
  s1<-!scores(data$logratioSR, type="z", prob=0.95)
  
  pngfile<-paste0(outputfile, ".", group, ".png")
  png(pngfile, width=2000, height=2000, res=300)
  showlm(rlm(I115 ~ I114, data=data[s1,]), "I115", "I114", data, "Value", "Robust linear regression")
  dev.off()
}
