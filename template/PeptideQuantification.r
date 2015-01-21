#predefine_start

outputdir<-"E:/shengquanhu/projects/2014-suzhiduan-TMT/quantification/summary"
inputfile<-"20141211_Tim_LiverLDs_proteome_assay1_06.noredundant.I126I129N.peptides.tsv"
outputfile<-"20141211_Tim_LiverLDs_proteome_assay1_06.noredundant.I126I129N.peptides.quan.tsv"
missingvalue<-0.1
pvalue<-0.01
minFinalCount<-3

#predefine_end

library("outliers")

setwd(outputdir)

data<-read.delim(inputfile, header=T, stringsAsFactors=F)
data[data<=missingvalue]<-NA
refindex<-which(colnames(data)=="REF")

sampleChannels<-colnames(data)[(refindex+1):ncol(data)]

colnames<-c("Subject", "Dataset", "REF", sampleChannels)

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
    sdata<-dsdata[dsdata$Subject==subject,]
    index <-index+1
    
    #sc<-"I127N"
    refvalues<-c()
    channelvalues<-c()
    for(sc in sampleChannels){
      scdata<-sdata[,c("REF", sc)]
      scdata<-na.omit(scdata)
      
      if(nrow(scdata) == 0){
	    refvalues<-c(refvalues, NA)
	    channelvalues<-c(channelvalues, NA)
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
      if(!is.na(refvalues[i])){
        rfactor<-refvalues[i] / refmedian
        channelvalues[i]<-channelvalues[i] / rfactor
      }
    }
    
    values<-c(subject, ds, round(refmedian), round(channelvalues))
    curres[index,]<-t(values)
  }
  result<-rbind(result, curres)
}

colnames(result)<-colnames

result[is.na(result)]<-""

write.table(result, outputfile, sep="\t", row.names=F, quote=F)
