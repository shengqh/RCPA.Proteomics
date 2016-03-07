#predefine_start

outputdir<-"H:/shengquanhu/projects/Masaru/20160210_deuterium/chros"
inputfile<-"H:/shengquanhu/projects/Masaru/20160210_deuterium/chros"
outputfile<-"H:/shengquanhu/projects/Masaru/20160210_deuterium/chros/boundary.csv"

#predefine_end

loadOrInstallPackage <- function(x)
{
  if (!require(x,character.only = TRUE, quietly = TRUE))
  {
    options("repos"="http://cran.us.r-project.org")
    install.packages(x,dep=TRUE)
    if(!require(x,character.only = TRUE, quietly = TRUE)) {
      stop(paste0("Package ", x, " not found"))
    }
  }
}

loadOrInstallPackage("changepoint")

MAX_ISOTOPIC<-3
MINIMUM_MEAN_PPM_DIFF<-2
MINIMUM_VARIANCE_PPM_DIFF<-2
MINIMUM_SCAN_EACH_SIDE<-5

files<-list.files(path = inputfile, pattern = "*.chro.tsv$", all.files = TRUE, full.names = TRUE)

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

df <- data.frame(File=character(), 
                 LeftBoundary=numeric(), 
                 RightBoundary=numeric(),
                 stringsAsFactors = FALSE)
for(inputFile in files){
  cat(inputFile, "\n")
  out<-read.table(inputFile, header=T)
  out$Isotopic = factor(out$Isotopic)
  
  iso0data<-out[out$Isotopic == "0",]
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
    curBoundary<-findBoundary(isodata, maxIdentifiedIndex, cpt.mean, MINIMUM_MEAN_PPM_DIFF, MINIMUM_SCAN_EACH_SIDE)
    isoBoundary[1] = max(isoBoundary[1], curBoundary[1])
    isoBoundary[2] = min(isoBoundary[2], curBoundary[2])
    curBoundary<-findBoundary(isodata, maxIdentifiedIndex, cpt.meanvar, MINIMUM_VARIANCE_PPM_DIFF, MINIMUM_SCAN_EACH_SIDE)
    isoBoundary[1] = max(isoBoundary[1], curBoundary[1])
    isoBoundary[2] = min(isoBoundary[2], curBoundary[2])
  }
  
  newdata<-data.frame(File=inputFile, 
                      LeftBoundary=isoBoundary[1], 
                      RightBoundary=isoBoundary[2])
  df<-rbind(df, newdata)
}

write.csv(df, file=outputfile, row.names=FALSE)
