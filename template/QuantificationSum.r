#predefine_start

outputdir<-"E:/shengquanhu/projects/2014-suzhiduan-TMT/quantification/summary"
inputfile<-"20141211_Tim_LiverLDs_proteome_assay1_06.noredundant.I126I129N.peptides.tsv"
outputfile<-"20141211_Tim_LiverLDs_proteome_assay1_06.noredundant.I126I129N.proteins.quan.Sum.tsv"
proteinfile<-"20141211_Tim_LiverLDs_proteome_assay1_06.noredundant.I126I129N.proteins.tsv"
peptidequanfile<-"20141211_Tim_LiverLDs_proteome_assay1_06.noredundant.I126I129N.peptides.quan.tsv"
missingvalue<-0.1
pvalue<-0.01
minFinalCount<-3

#predefine_end

library("outliers")

setwd(outputdir)

data<-read.delim(inputfile, header=T, stringsAsFactors=F)
data[data==missingvalue]<-NA
refindex<-which(colnames(data)=="REF")

sampleChannels<-colnames(data)[(refindex+1):ncol(data)]

colnames<-c("Subject", "Dataset")
for(sc in sampleChannels){
  colnames<-c(colnames, paste0("REF_", sc), sc)
}

result<-as.data.frame(setNames(c(replicate(2,character(0), simplify = F),
                                 replicate(length(colnames)-2,numeric(0), simplify = F)), colnames),
                      stringsAsFactors=F)

datasets<-unique(data$Dataset)
#ds<-datasets[1]
for(ds in datasets){
  dsdata<-data[data$Dataset == ds,]
  subjects<-unique(dsdata$Subject)
  
  curres <- as.data.frame(matrix(nrow = length(subjects), ncol = length(colnames), dimnames = list(NULL, colnames)), stringsAsFactors=F)
  index<-0
  
  #subject<-subjects[1]
  for(subject in subjects){
    sdata<-dsdata[dsdata$Subject==subject,]
    index <-index+1
    
    #sc<-sampleChannels[1]
    values<-c(subject, ds)
    for(sc in sampleChannels){
      scdata<-sdata[,c("REF", sc)]
      scdata<-na.omit(scdata)
      
      if(nrow(scdata) == 0){
        values<-c(values, NA, NA)
        next
      }
      
      scdata$LogRatio <- log(scdata[, 2] / scdata[, 1])
      scdata$IsOutlier<-FALSE
      
      #check outlier using Grubbs test
      while(nrow(scdata) > minFinalCount){
        gt<-grubbs.test(scdata$LogRatio)
        if(gt$p.value < pvalue ){
          if(grepl("lowest",gt$alternative )){
            outlierindex<-which.min(scdata$LogRatio)
          }else{
            outlierindex<-which.max(scdata$LogRatio)
          }
          
          scdata[outlierindex,"IsOutlier"]<-TRUE
          scdata<-scdata[!scdata$IsOutlier,]
        }else{
          break
        }
      }
      values<-c(values, sum(scdata[,1]), sum(scdata[,2]))
    }
    
    curres[index,]<-t(values)
  }
  result<-rbind(result, curres)
}

colnames(result)<-colnames

write.table(result, peptidequanfile, sep="\t", row.names=F, quote=F)

result<-read.delim(peptidequanfile)

for(index in c(3:ncol(result))){
  result[,index]<-as.numeric(result[,index])
}

prodata<-read.delim(proteinfile, stringsAsFactors=F)
proteins<-unique(prodata$Index)

datacolnames<-c()
for(ds in datasets){
  for(sc in sampleChannels){
    datacolnames<-c(datacolnames, paste0(ds, "_REF_", sc), paste0(ds, "_", sc), paste0(ds, "_", sc, "_Ratio"))
  }
}
procolnames<-c("GroupIndex", colnames(prodata)[3:ncol(prodata)], datacolnames)

proresult <- as.data.frame(matrix(nrow = length(proteins), ncol = length(procolnames), dimnames = list(NULL, procolnames)), stringsAsFactors=F)

protein<-proteins[2]
index<-0
for(protein in proteins){
  index<-index+1
  pdata<-prodata[prodata$Index==protein,]
  propeps<-pdata$Peptide
  ds<-datasets[1]
  values<-c(protein, pdata[1,3:ncol(pdata)])
  for(ds in datasets){
    dsresult<-result[result$Dataset == ds,]
    subjects<-unique(dsresult$Subject)
    curpeps<-propeps[propeps %in% subjects]
    sc<-sampleChannels[1]
    for(sc in sampleChannels){
      refname<-paste0("REF_", sc)
      scresult<-dsresult[dsresult$Subject %in% curpeps,c("Subject", refname, sc)]
      
      scresult<-na.omit(scresult)
      
      if(nrow(scresult) == 0){
        values<-c(values, NA)
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
      values<-c(values, screfsum, scsum, scsum / screfsum)
    }
  }
  proresult[index,]<-t(values)
}

colnames(proresult)<-procolnames

write.table(proresult, outputfile, sep="\t", row.names=F)

