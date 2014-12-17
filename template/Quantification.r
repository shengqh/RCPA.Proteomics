#predefine_start

outputdir<-"E:/shengquanhu/projects/2014-suzhiduan-TMT/quantification/summary"
inputfile<-"20141211_Tim_LiverLDs_proteome_assay1_06.noredundant.I126I129N.isobaric.peptides.tsv"
outputfile<-"20141211_Tim_LiverLDs_proteome_assay1_06.noredundant.I126I129N.isobaric.proteins.quan.tsv"
proteinfile<-"20141211_Tim_LiverLDs_proteome_assay1_06.noredundant.I126I129N.isobaric.proteins.tsv"
peptidequanfile<-"20141211_Tim_LiverLDs_proteome_assay1_06.noredundant.I126I129N.isobaric.peptides.quan.tsv"
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
  
  #subject<-"AADHVEDLPGALSTLSDLHAHK"
  for(subject in subjects){
    sdata<-dsdata[data$Subject==subject,]
    index <-index+1
    
    sc<-"I127N"
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

for(index in c(3:ncol(result))){
  result[,index]<-as.numeric(result[,index])
}

prodata<-read.delim(proteinfile, stringsAsFactors=F)
proteins<-unique(prodata$Protein)

procolnames<-c("Protein")
for(ds in datasets){
  for(sc in sampleChannels){
    procolnames<-c(procolnames, paste0(ds, "_REF_", sc), paste0(ds, "_", sc), paste0(ds, "_", sc, "/REF"))
  }
}

proresult<-as.data.frame(setNames(c(replicate(1, character(0)), replicate(length(procolnames) - 1,numeric(0), simplify = F)), procolnames),
                      stringsAsFactors=F)

protein<-proteins[2]
for(protein in proteins){
  propeps<-prodata[prodata$Protein==protein,2]
  ds<-datasets[1]
  values<-c(protein)
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
        values<-c(values, NA, NA, NA)
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
      values<-c(values, screfsum, scsum, screfsum / scsum)
    }
  }
  proresult<-rbind(proresult, t(values))
}

colnames(proresult)<-procolnames

write.table(proresult, outputfile, sep="\t", row.names=F)

