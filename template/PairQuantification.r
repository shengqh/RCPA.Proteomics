#predefine_start

outputdir<-"H:/shengquanhu/projects/rcpa/O18/paperdata/16O_18O_1_2/16O_18O_1_2.noredundant.details"
inputfile<-"H:/shengquanhu/projects/rcpa/O18/paperdata/16O_18O_1_2/16O_18O_1_2.noredundant.details/sp_P07335_KCRB_RAT.csv"
outputfile<-"H:/shengquanhu/projects/rcpa/O18/paperdata/16O_18O_1_2/16O_18O_1_2.noredundant.details/sp_P07335_KCRB_RAT.csv.linear"

#predefine_end

pvalue<-0.05
minFinalCount<-3

require(MASS)
require(outliers)

scdata<-read.csv(inputfile)
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

df<-data.frame(SumRefIntensity=sum(scdata$RefIntensity), SumSamIntensity=sum(scdata$SamIntensity), Ratio=coeff[1], StdErr=coeff[2], tValue=coeff[3], pValue=pvalue, Count=nrow(scdata))
write.table(df, outputfile, row.names=F,sep="\t");
