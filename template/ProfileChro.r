library(ggplot2)
library(grid)
library(changepoint)
library(reshape2)

#inputDir<-commandArgs(TRUE)[1]
inputDir<-"H:/shengquanhu/projects/Masaru/20160210_deuterium/chros/"
files<-list.files(path = inputDir, pattern = "*.chro$", all.files = TRUE, full.names = TRUE)
#inputFile<-paste0(inputDir, "CT102_0_Hour_ADSSPVKAGVETTTPSK_559_6715.chro")
#inputFile<-paste0(inputDir, "CT102_0_Hour_AEFAEVSK_441_7167.chro")
inputFile<-paste0(inputDir, "CT102_0_Hour_AEFAEVSKLVTDLTK_826_25754")
outputFile<-paste0(inputDir, "boundary.tsv")

MAX_ISOTOPIC<-3
MINIMUM_MEAN_PPM_DIFF<-2
MINIMUM_SCAN_EACH_SIDE<-5

findBoundary<-function(isodata, maxIdentifiedIndex, cpt, minDiff, minScan){
  leftBoundary<-1
  if(maxIdentifiedIndex > minScan){
    res<-cpt(isodata$PPMTolerance[1:maxIdentifiedIndex])
    if(ncpts(res) > 0){
      diffmean<-abs(param.est(res)$mean)
      resmean<-param.est(res)$mean
      if(abs(resmean[1]) > abs(resmean[2])){
        diffmean<-abs(resmean[1] -resmean[2])
        if(diffmean > minDiff){
          leftBoundary<-max(1, min(maxIdentifiedIndex-minScan, cpts(res)))
        }
      }
    }
  }
  
  rightBoundary<-nrow(isodata)
  if(nrow(isodata) - maxIdentifiedIndex > minScan){
    res<-cpt(isodata$PPMTolerance[maxIdentifiedIndex:nrow(isodata)])
    if(ncpts(res) > 0){
      diffmean<-abs(param.est(res)$mean)
      resmean<-param.est(res)$mean
      if(abs(resmean[1]) < abs(resmean[2])){
        diffmean<-abs(resmean[1] -resmean[2])
        if(diffmean > minDiff){
          rightBoundary<-min(nrow(isodata), max(maxIdentifiedIndex + cpts(res), maxIdentifiedIndex + minScan))
        }
      }
    }
  }
  
  boundaries<-isodata$RetentionTime[c(leftBoundary, rightBoundary)]
  return (boundaries)
}

for(inputFile in files){
  imgFile<-paste0(inputFile, ".png")
  if(file.exists(imgFile)){
    next
  }
  
  cat(inputFile, "\n")
  out<-read.table(inputFile, header=T)
  out$Isotopic = factor(out$Isotopic)
  
  meltdata<-melt(out, id.vars=c("Scan", "RetentionTime", "Isotopic", "Mz", "ProfileCorrelation", "Identified"),
                 variable.name = "Category",
                 value.name="Value")
  meltdata<-meltdata[,c("RetentionTime", "Isotopic", "Category", "Value")]
  
  iso0data<-out[out$Isotopic == "0",]
  iso0<-iso0data[,c("RetentionTime", "Isotopic", "ProfileCorrelation")]
  meltIso0<-melt(iso0, id.vars=c("RetentionTime", "Isotopic"),
                 variable.name = "Category",
                 value.name="Value")
  meltall<-rbind(meltdata, meltIso0)
  
  identified<-iso0data[iso0data$Identified == "True",]
  if(nrow(identified) == 0){
    maxIdentifiedIndex=round(nrow(iso0data) / 2)
  }else{
    index <-which.max(identified$Intensity)
    maxIdentifiedIndex<-which(iso0data$Scan==identified$Scan[index])
  }
  maxIdentifiedRetentionTime<-iso0data$RetentionTime[maxIdentifiedIndex]
  
  isos<-unique(out$Isotopic)
  isoBoundary<-out$RetentionTime[c(1,nrow(out))]
  for(iso in isos[1:MAX_ISOTOPIC]){
    isodata<-out[out$Isotopic == iso,]
    curBoundary<-findBoundary(isodata, maxIdentifiedIndex, cpt.mean, MINIMUM_MEAN_PPM_DIFF, 5)
    isoBoundary[1] = max(isoBoundary[1], curBoundary[1])
    isoBoundary[2] = min(isoBoundary[2], curBoundary[2])
    curBoundary<-findBoundary(isodata, maxIdentifiedIndex, cpt.meanvar, 0, 5)
    isoBoundary[1] = max(isoBoundary[1], curBoundary[1])
    isoBoundary[2] = min(isoBoundary[2], curBoundary[2])
  }

  png(imgFile, res=300, width = 2000, height=2000)
  
  identifiedScan = unique(out$RetentionTime[out$Identified == "True"])
  iso0<-out[out$Isotopic == "0",]
  
  #head(meltall)
  #meltall<-meltall[meltall$RetentionTime > 81,]
  
  p1<-ggplot(meltall, aes(x=RetentionTime, y=Value, group=Isotopic, color=Isotopic)) + geom_line() + 
    geom_vline(xintercept = identifiedScan) + 
    facet_grid(Category ~ ., scales="free")
  
  if(nrow(identified) != 0){
    p1<-p1+geom_vline(xintercept = maxIdentifiedRetentionTime, colour="red")
  }
  p1<-p1+geom_vline(xintercept = isoBoundary, colour="blue")
  print(p1)
  
  dev.off()
}
