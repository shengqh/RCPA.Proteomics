#predefine_start

outputdir<-"E:/shengquanhu/projects/2014-suzhiduan-TMT/quantification/summary"
inputfile<-"E:/shengquanhu/projects/2014-suzhiduan-TMT/quantification/summary/Liver_LDs.peptides.I126I129N.quan.tsv"
outputfile<-"E:/shengquanhu/projects/2014-suzhiduan-TMT/quantification/summary/Liver_LDs.noredundant.I126I129N.quan.Median.tsv"
proteinfile<-"E:/shengquanhu/projects/2014-suzhiduan-TMT/quantification/summary/Liver_LDs.noredundant.I126I129N.pro_pep.tsv"
method<-"Median"
pvalue<-0.01
minFinalCount<-3

#predefine_end

library("outliers")

setwd(outputdir)

data<-read.delim(inputfile)

for(index in c(3:ncol(data))){
  data[,index]<-as.numeric(data[,index])
}

datasets<-sort(unique(data$Dataset))
sampleChannels<-colnames(data)[4:ncol(data)]

prodata<-read.delim(proteinfile, stringsAsFactors=F)
proteins<-unique(prodata$Index)

ismedian<-method=="Median"

datacolnames<-c()
for(ds in datasets){
  datacolnames<-c(datacolnames, paste0(ds, "_REF"))
  for(sc in sampleChannels){
    datacolnames<-c(datacolnames, paste0(ds, "_", sc), paste0(ds, "_", sc, "_Ratio"))
  }
}
procolnames<-c("GroupIndex", colnames(prodata)[3:ncol(prodata)], datacolnames)

proresult <- as.data.frame(matrix(nrow = length(proteins), ncol = length(procolnames), dimnames = list(NULL, procolnames)), stringsAsFactors=F)

#protein<-proteins[1]
index<-0
for(protein in proteins){
  index<-index+1
  cat(index, "/", length(proteins), " ...\n")
  pdata<-prodata[prodata$Index==protein,]
  propeps<-pdata$Peptide
  #ds<-datasets[1]
  values<-c(protein, pdata[1,3:ncol(pdata)])
  for(ds in datasets){
    dsresult<-data[data$Dataset == ds,]
    subjects<-unique(dsresult$Subject)
    curpeps<-propeps[propeps %in% subjects]
    
    subjectresult<-dsresult[dsresult$Subject %in% curpeps,]
    #sc<-sampleChannels[1]
    refvalues<-c()
    channelvalues<-c()
    channelratios<-c()
    for(sc in sampleChannels){
      refname<-"REF"
      scresult<-subjectresult[,c("Subject", refname, sc)]
      
      scresult<-na.omit(scresult)
      
      if(nrow(scresult) == 0){
        refvalues<-c(refvalues,NA)
        channelvalues<-c(channelvalues,NA)
        channelratios<-c(channelratios,NA)
        next
      }
      
      scresult$LogRatio <- log(scresult[, 3] / scresult[, 2])
      scresult$IsOutlier<-FALSE
      
      #check outlier using Grubbs test
      while(nrow(scresult) > minFinalCount){
        gt<-grubbs.test(scresult$LogRatio)
        if(gt$p.value < pvalue ){
          if(grepl("lowest",gt$alternative )){
            outlierindex<-which.min(scresult$LogRatio)
          }else{
            outlierindex<-which.max(scresult$LogRatio)
          }
          
          scresult[outlierindex,"IsOutlier"]<-TRUE
          scresult<-scresult[!scresult$IsOutlier,]
        }else{
          break
        }
      }
      
      screfsum <- sum(scresult[,2])
      scsum = sum(scresult[,3])
      refvalues<-c(refvalues,screfsum)
      channelvalues<-c(channelvalues,scsum)
      
      if(ismedian){
        ratio<-exp(median(scresult$LogRatio))
      }else{
        ratio<-scsum / screfsum
      }
      
      channelratios<-c(channelratios,ratio)
    }
    
    rv<-refvalues[!is.na(refvalues)]
    refmedian<-median(rv)
    values<-c(values, round(refmedian))

    for(i in c(1:length(refvalues))){
      if(is.na(refvalues[i])){
        values<-c(values, NA, NA)
      }else{
        rfactor<-refvalues[i] / refmedian
        normalizedvalue<-channelvalues[i] / rfactor
        values<-c(values, normalizedvalue, channelratios[i])
      }
    }
  }
  proresult[index,]<-t(values)
}

colnames(proresult)<-procolnames
proresult[is.na(proresult)]<-""

write.table(proresult, outputfile, sep="\t", row.names=F)
