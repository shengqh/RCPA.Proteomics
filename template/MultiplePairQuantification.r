#predefine_start

outputdir<-"H:/shengquanhu/projects/rcpa/O18/paperdata/16O_18O_1_2/16O_18O_1_2.noredundant.details"
inputfile<-"H:/shengquanhu/projects/rcpa/O18/paperdata/16O_18O_1_2/16O_18O_1_2.noredundant.details/rlm_file.csv"
outputfile<-"H:/shengquanhu/projects/rcpa/O18/paperdata/16O_18O_1_2/16O_18O_1_2.noredundant.details/rlm.linear"

#predefine_end

require(MASS)
require(outliers)

pvalue<-0.05
minFinalCount<-3

files<-read.csv(inputfile, check.names=F, stringsAsFactor=F)

rlm_result<-apply(files, 1, function(x){
  scdata<-read.csv(x[2])
  scdata[scdata==0.0]<-NA
  scdata<-na.omit(scdata)
  
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
  
  rl<-rlm(SamIntensity~0+RefIntensity, data=scdata)
  srl<-summary(rl)
  coeff<-srl$coefficients
  pvalue<-pt( coeff[3] , srl$df[2], lower.tail=F)*2
  return (c(sum(scdata$RefIntensity), sum(scdata$SamIntensity), coeff[1], coeff[2], coeff[3], pvalue, nrow(scdata)))
})

rlm_result=t(rlm_result)
colnames(rlm_result)<-c("SumRefIntensity", "SumSamIntensity", "Ratio","StdErr","tValue","pValue","Count")
finalresult<-cbind(files[,c(1:2)], rlm_result)
write.table(finalresult, outputfile, row.names=F,sep="\t");
