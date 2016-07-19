#predefine_start

outputdir<-"H:/shengquanhu/projects/20160714_de_calculator"
inputfile<-"H:/shengquanhu/projects/20160714_de_calculator/NASH6B-0h.deuterium.boundary.chros.tsv"
outputfile<-"H:/shengquanhu/projects/20160714_de_calculator/NASH6B-0h.deuterium.boundary.tsv"

#predefine_end

#ptm <- proc.time()

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
MINIMUM_APEX_PERCENTAGE<-0.1

files<-read.delim(inputfile, stringsAsFactors = F, header=T)

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

df <- data.frame(ChroLeft=numeric(), 
                 ChroRight=numeric(),
                 InitBoundaryLeft=numeric(), 
                 InitBoundaryRight=numeric(),
                 ApexWindowLeft=numeric(),
                 ApexWindowRight=numeric(),
                 RetentionTime=numeric(),
                 MaxIntensity=numeric(),
                 MaxIdentifiedRetentionTime=numeric(),
                 PeakArea=numeric(),
                 stringsAsFactors = FALSE)

lastPercentage = 0
for(index in c(1:nrow(files))){
  curPercentage = round(index * 100.0 / nrow(files))
  if(curPercentage != lastPercentage){
    lastPercentage = curPercentage;
    cat(curPercentage, "%\n")
  }
  
  chroFile=paste0(files$ChroDirectory[index], "/", files$ChroFile[index], ".tsv")
  out<-read.table(chroFile, header=T)
  out$Isotopic = factor(out$Isotopic)
  
  iso0data<-out[out$Isotopic == "1",]
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
  
  iso0win<-iso0data[iso0data$RetentionTime >= isoBoundary[1] & iso0data$RetentionTime <= isoBoundary[2],]
  maxIndex<-which.max(iso0win$Intensity)
  maxRetention=iso0win$RetentionTime[maxIndex]
  minIntensity<-iso0win$Intensity[maxIndex] * MINIMUM_APEX_PERCENTAGE
  removeLeft<-iso0win[iso0win$Intensity < minIntensity & iso0win$RetentionTime < maxRetention,]
  removeRight<-iso0win[iso0win$Intensity < minIntensity & iso0win$RetentionTime > maxRetention,]
  newwinLeft<-ifelse(nrow(removeLeft) == 0, iso0win$RetentionTime[1], max(removeLeft$RetentionTime))
  newwinRight<-ifelse(nrow(removeRight) == 0, iso0win$RetentionTime[nrow(iso0win)], min(removeRight$RetentionTime))
  
  apexwin<-iso0data[iso0data$RetentionTime >= isoBoundary[1] & iso0data$RetentionTime <= isoBoundary[2],]
  apexwin<-out[out$RetentionTime >= newwinLeft & out$RetentionTime <= newwinRight & (out$Isotopic == "1" | out$Isotopic == "2" | out$Isotopic == "3"),]
  
  newdata<-data.frame(ChroLeft=min(iso0data$RetentionTime),
                      ChroRight=max(iso0data$RetentionTime),
                      InitBoundaryLeft=isoBoundary[1], 
                      InitBoundaryRight=isoBoundary[2],
                      ApexWindowLeft=newwinLeft,
                      ApexWindowRight=newwinRight,
                      RetentionTime=maxRetention,
                      MaxIntensity=iso0win$Intensity[maxIndex],
                      MaxIdentifiedRetentionTime=maxIdentifiedRetentionTime,
                      PeakArea=sum(apexwin$Intensity))
  df<-rbind(df, newdata)
}

res<-cbind(files, df)

write.table(res, file=outputfile, row.names=FALSE, sep="\t")

#cat("Cost time ", proc.time() - ptm)