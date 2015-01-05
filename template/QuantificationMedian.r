#predefine_start

outputdir<-"E:/shengquanhu/projects/2014-suzhiduan-TMT/quantification/summary"
inputfile<-"20141211_Tim_LiverLDs_proteome_assay1_06.noredundant.I126I129N.peptides.tsv"
outputfile<-"20141211_Tim_LiverLDs_proteome_assay1_06.noredundant.I126I129N.proteins.quan.Median.tsv"
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

colnames<-c("Subject", "Dataset", "REF", sampleChannels)

result<-as.data.frame(setNames(c(replicate(2,character(0), simplify = F),
                                 replicate(length(colnames)-2,numeric(0), simplify = F)), colnames),
                      stringsAsFactors=F)

datasets<-unique(data$Dataset)
ds<-datasets[1]
for(ds in datasets){
  dsdata<-data[data$Dataset == ds,]
  subjects<-unique(dsdata$Subject)
  
  curres <- as.data.frame(matrix(nrow = length(subjects), ncol = length(colnames), dimnames = list(NULL, colnames)), stringsAsFactors=F)
  index<-0
  
  subject<-"AADHVEDLPGALSTLSDLHAHK"
  for(subject in subjects){
    sdata<-dsdata[dsdata$Subject==subject,]
    index <-index+1
    
    sc<-"I127N"
    refvalues<-c()
    channelvalues<-c()
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
      refvalues<-c(refvalues, sum(scdata[,1]))
      channelvalues<-c(channelvalues, sum(scdata[,2]))
    }
    
    rv<-refvalues[!is.na(refvalues)]
    refmedian<-median(rv)
    for(i in c(1:length(refvalues))){
      factor<-refvalues[i] / refmedian
      channelvalues[i]<-channelvalues[i] * factor
    }
    
    values<-c(subject, ds, round(refmedian), round(channelvalues))
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
  datacolnames<-c(datacolnames, paste0(ds, "_REF"))
  for(sc in sampleChannels){
    datacolnames<-c(datacolnames, paste0(ds, "_", sc), paste0(ds, "_", sc, "_Ratio"))
  }
}
procolnames<-c("GroupIndex", colnames(prodata)[3:ncol(prodata)], datacolnames)

proresult <- as.data.frame(matrix(nrow = length(proteins), ncol = length(procolnames), dimnames = list(NULL, procolnames)), stringsAsFactors=F)

protein<-proteins[1]
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
    
    subjectresult<-dsresult[dsresult$Subject %in% curpeps,]
    sc<-sampleChannels[1]
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

	  logratio<-median(scresult$LogRatio)
      channelvalues<-c(channelvalues,scsum)
      channelratios<-c(channelratios,exp(logratio))
    }
    
    rv<-refvalues[!is.na(refvalues)]
    refmedian<-median(rv)
    values<-c(values, round(refmedian))

    for(i in c(1:length(refvalues))){
      factor<-refvalues[i] / refmedian
      channelvalues[i]<-channelvalues[i] * factor
      values<-c(values, channelvalues[i], channelratios[i])
    }
  }
  proresult[index,]<-t(values)
}

colnames(proresult)<-procolnames
proresult[is.na(proresult)]<-""

write.table(proresult, outputfile, sep="\t", row.names=F)

