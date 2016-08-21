#predefine_start

outputdir<-"H:/shengquanhu/projects/Masaru/20160803_deuterium/test"
inputfile<-"chros.tsv"
outputfile<-"boundary.tsv"
outputImage<-1
maximumProfileDistance<-0

#predefine_end

setwd(outputdir)

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

attrFun <- function(x){
  xattrs <- xmlAttrs(x)
  c(xattrs)
}

loadOrInstallPackage("changepoint")
loadOrInstallPackage("reshape2")
loadOrInstallPackage("ggplot2")
loadOrInstallPackage("grid")
loadOrInstallPackage("signal")
loadOrInstallPackage("cardidates")
loadOrInstallPackage("XML")
loadOrInstallPackage("scales")

#function from candidates package
peakwindow<-function (x, y = NULL, xstart = 0, xmax = max(x), minpeak = 0.1, 
                      mincut = 0.382) 
{
  numpeaks <- function(ft, y) {
    npeaks <- length(ft)
    ndata <- length(y)
    if (npeaks > 0) {
      fp <- x1 <- x2 <- numeric(npeaks)
      peakid <- numeric(length(y))
      for (i in 1:npeaks) {
        fp[i] <- ft[i]
        mp <- match(fp[i], ft)
        if (mp == 1) 
          x1[i] <- 1
        else x1[i] <- ft[mp - 1] + which.min(y[ft[mp - 
                                                    1]:fp[i]]) - 1
        if (mp == npeaks) 
          x2[i] <- ndata
        else x2[i] <- fp[i] + which.min(y[fp[i]:ft[mp + 
                                                     1]]) - 1
        peakid[x1[i]:x2[i]] <- i
      }
    }
    else {
      fp <- ft
      x1 <- x2 <- NULL
      peakid <- rep(0, ndata)
    }
    list(fp = fp, x1 = x1, x2 = x2, id = peakid)
  }
  xy <- xy.coords(x, y)
  x <- xy$x
  y <- xy$y
  iend <- max(c(1, which(x <= xmax)))
  tp <- turnpoints(y)
  ft <- tp$tppos
  if (all(is.na(ft))) {
    x1 <- 1
    x2 <- length(y)
    fp <- NULL
    ft <- NULL
  }
  else {
    if(is.na(tp$firstispeak)){
      ft <- ft[seq(1, length(ft), by = 2)]
    }else{
      ft <- ft[seq(2 - 1 * tp$firstispeak, length(ft), by = 2)]
    }
    if (y[1] > y[2]) 
      ft <- c(1, ft)
    if (y[iend] > y[iend - 1]) 
      ft <- c(ft, iend)
    ft <- ft[ft <= iend]
    ftsmall <- which(y < minpeak * max(y))
    ft <- ft[!ft %in% ftsmall]
    dosearch <- ifelse(length(ft) > 1, TRUE, FALSE)
    while (dosearch) {
      fl <- length(ft)
      eli <- NULL
      for (n in 1:(fl - 1)) {
        km <- which.min(y[ft[n:(n + 1)]])
        if (min(y[ft[n:(n + 1)]]) * mincut < min(y[ft[n]:ft[n + 
                                                            1]])) 
          eli <- c(eli, n + km - 1)
      }
      if (length(eli) == 0) 
        dosearch <- FALSE
      else ft <- ft[-eli]
      if (length(ft) < 2) 
        dosearch <- FALSE
    }
    if (length(ft) > 1) {
      if (any(x[ft] > xstart)) {
        fp <- min(ft[x[ft] > xstart])
      }
      else {
        fp <- max(ft[x[ft] <= xstart])
      }
      mp <- match(fp, ft)
      if (mp == 1) 
        x1 <- 1
      else x1 <- ft[mp - 1] + which.min(y[ft[mp - 1]:fp]) - 
        1
      if (mp == length(ft)) 
        x2 <- iend
      else x2 <- fp + which.min(y[fp:ft[mp + 1]]) - 1
    }
    else {
      fp <- ft
      x1 <- 1
      x2 <- iend
    }
  }
  allpeaks <- numpeaks(ft, y)
  ret <- list(peaks = data.frame(index = ft, xleft = allpeaks$x1, 
                                 x = x[ft], xright = allpeaks$x2, y = y[ft]), data = data.frame(x = x, 
                                                                                                y = y), smd.max.index = fp, smd.max.x = x[fp], smd.indices = x1:x2, 
              smd.x = x[x1:x2], smd.y = y[x1:x2], peakid = allpeaks$id)
  class(ret) <- c("list", "cardiPeakwindow")
  ret
}

MAX_ISOTOPIC<-3
MINIMUM_MEAN_PPM_DIFF<-3
MINIMUM_VARIANCE_PPM_DIFF<-2
MINIMUM_SCAN_EACH_SIDE<-1
MINIMUM_APEX_PERCENTAGE<-0.1

files<-read.delim(inputfile, stringsAsFactors = F, header=T)

df <- data.frame(ChroLeft=numeric(), 
                 ChroRight=numeric(),
                 InitBoundaryLeft=numeric(), 
                 InitBoundaryRight=numeric(),
                 ApexWindowLeft=numeric(),
                 ApexWindowRight=numeric(),
                 DistanceLeft=numeric(),
                 DistanceRight=numeric(),
                 RetentionTime=numeric(),
                 MaxIntensity=numeric(),
                 MaxIdentifiedRetentionTime=numeric(),
                 PeakArea=numeric(),
                 stringsAsFactors = FALSE)

lastPercentage = 0
indecies=c(1:nrow(files))
#indecies=c(2)
for(index in indecies){
  curPercentage = round(index * 100.0 / nrow(files))
  if(curPercentage != lastPercentage){
    lastPercentage = curPercentage;
    cat(curPercentage, "%\n")
  }
  
  chroFile=paste0(files$ChroDirectory[index], "/", files$ChroFile[index], ".tsv")
  
  cat(index, " : ", chroFile, "\n")
  
  out<-read.table(chroFile, header=T)
  out$Isotopic = factor(out$Isotopic)
  
  isos<-unique(out$Isotopic)
  
  out$IntensitySmooth=out$Intensity
  for(iso in isos[1:MAX_ISOTOPIC]){
    isodata<-out[out$Isotopic == iso, "Intensity"]
    
    #find window containing most abundant identified peptide scan
    if(length(isodata) > 5){
      out[out$Isotopic == iso, "IntensitySmooth"]=sgolayfilt(isodata)
    }
  }  
  
  iso0data<-out[out$Isotopic == "1",]
  identified<-iso0data[iso0data$Identified == "True",]
  if(nrow(identified) == 0){
    maxIdentifiedIndex<-which.max(iso0data$Intensity)
  }else{
    index <-which.max(identified$Intensity)
    maxIdentifiedIndex<-which(iso0data$Scan==identified$Scan[index])
  }
  maxIdentifiedRetentionTime<-iso0data$RetentionTime[maxIdentifiedIndex]
  leftMaxRetention=ifelse(maxIdentifiedIndex-MINIMUM_SCAN_EACH_SIDE > 0, iso0data$RetentionTime[maxIdentifiedIndex-MINIMUM_SCAN_EACH_SIDE], iso0data$RetentionTime[1])
  rightMinRetention=ifelse(maxIdentifiedIndex+MINIMUM_SCAN_EACH_SIDE < nrow(iso0data), iso0data$RetentionTime[maxIdentifiedIndex+MINIMUM_SCAN_EACH_SIDE], iso0data$RetentionTime[nrow(iso0data)])
  
  #find window containing most abundant identified peptide scan
  peakWindow<-c(min(iso0data$RetentionTime), max(iso0data$RetentionTime))
  for(iso in isos[1:2]){
    isodata<-out[out$Isotopic == iso,]

    #find window containing most abundant identified peptide scan
    pw<-isodata[isodata$RetentionTime >= peakWindow[1] & isodata$RetentionTime <= peakWindow[2],]
    while(1){
      maxIdentifiedIndex=which(pw$RetentionTime==maxIdentifiedRetentionTime)
      peak=peakwindow(pw$RetentionTime, pw$IntensitySmooth)
      maxpeakid=peak$peakid[maxIdentifiedIndex]
      pw=pw[peak$peakid==maxpeakid,]
      if(length(unique(peak$peakid))== 1){
        break
      }
    }
    peakWindow<-c(min(pw$RetentionTime), max(pw$RetentionTime))
  }
  
  pwIso0<-iso0data[iso0data$RetentionTime >= peakWindow[1] & iso0data$RetentionTime <= peakWindow[2],]
  
  #assume the most abundant peak in the window is correct
  maxPeakRetention=pwIso0$RetentionTime[which.max(pwIso0$Intensity)]
  
  isoBoundary<-out$RetentionTime[c(1,nrow(out))]
  iso<-isos[3]
  for(iso in isos[1:MAX_ISOTOPIC]){
    isodata<-out[out$Isotopic == iso,]
    
    #find window containing most abundant identified peptide scan
    maxPeakIndex=which(isodata$RetentionTime==maxPeakRetention) 
    
    #find the change point beside the most abundant peak
    leftBoundary=peakWindow[1]
    leftData=isodata[1:maxPeakIndex,]
    if(nrow(leftData) > 5){
      resLeft=cpt.mean(leftData$PPMTolerance)
      if(ncpts(resLeft) > 0){
        diffmean<-abs(param.est(resLeft)$mean)
        resmean<-param.est(resLeft)$mean      
        if(abs(resmean[1]) > abs(resmean[2])){
          diffmean<-abs(resmean[1] -resmean[2])
          if(diffmean > MINIMUM_MEAN_PPM_DIFF){
            leftBoundary<-max(leftBoundary, min(leftMaxRetention, leftData$RetentionTime[cpts(resLeft)]))
          }
        }
      }
    }
    
    rightBoundary=peakWindow[2]
    rightData=isodata[maxPeakIndex:nrow(isodata),]
    if(nrow(rightData) > 5){
      resRight=cpt.mean(rightData$PPMTolerance)
      if(ncpts(resRight) > 0){
        diffmean<-abs(param.est(resRight)$mean)
        resmean<-param.est(resRight)$mean      
        if(abs(resmean[1]) < abs(resmean[2])){
          diffmean<-abs(resmean[1] -resmean[2])
          if(diffmean > MINIMUM_MEAN_PPM_DIFF){
            rightBoundary<-min(rightBoundary, max(rightMinRetention, rightData$RetentionTime[cpts(resRight)]))
          }
        }
      }
    }
    isoBoundary<-c(max(isoBoundary[1], leftBoundary), min(isoBoundary[2], rightBoundary))
  }
  
  isoBoundaryData=iso0data[iso0data$RetentionTime >= isoBoundary[1] & iso0data$RetentionTime <= isoBoundary[2],]

  maxIntensity = max(isoBoundaryData$IntensitySmooth)
  if(maxIntensity == -Inf){
    cat("Why? \n")
  }
  maxPeakIndex = which(isoBoundaryData$IntensitySmooth == maxIntensity)
  maxPeakRetention = isoBoundaryData$RetentionTime[maxPeakIndex]
  
  #find apex window
  apexBoundary<-isoBoundary
  minIntensity = maxIntensity * MINIMUM_APEX_PERCENTAGE
  unvalid = isoBoundaryData[isoBoundaryData$IntensitySmooth < minIntensity,]
  leftUnvalid = unvalid$RetentionTime[unvalid$RetentionTime < maxPeakRetention]
  if(length(leftUnvalid) > 0){
    apexBoundary[1] = min(isoBoundaryData$RetentionTime[isoBoundaryData$RetentionTime > max(leftUnvalid)])
  }
  
  rightUnvalid = unvalid$RetentionTime[unvalid$RetentionTime > maxPeakRetention]
  if(length(rightUnvalid) > 0){
    apexBoundary[2] = max(isoBoundaryData$RetentionTime[isoBoundaryData$RetentionTime < min(rightUnvalid)])
  }
  chroLeft=max(isoBoundary[1], apexBoundary[1])
  chroRight=min(isoBoundary[2], apexBoundary[2])
  
  distanceBoundary<-c(NA,NA)
  if(maximumProfileDistance > 0){
    #filter by profile distance
    unvalid = isoBoundaryData[isoBoundaryData$ProfileDistance > maximumProfileDistance,]
    leftUnvalid = unvalid$RetentionTime[unvalid$RetentionTime < maxPeakRetention]
    if(length(leftUnvalid) > 0){
      distanceBoundary[1] = min(isoBoundaryData$RetentionTime[isoBoundaryData$RetentionTime > max(leftUnvalid)])
    }
  
    rightUnvalid = unvalid$RetentionTime[unvalid$RetentionTime > maxPeakRetention]
    if(length(rightUnvalid) > 0){
      distanceBoundary[2] = max(isoBoundaryData$RetentionTime[isoBoundaryData$RetentionTime < min(rightUnvalid)])
    }
  
    chroLeft=max(chroLeft, distanceBoundary[1])
    chroRight=min(chroRight, distanceBoundary[2])
  }
  
  iso0win<-iso0data[iso0data$RetentionTime >= chroLeft & iso0data$RetentionTime <= chroRight,]
  
  maxIndex<-which.max(iso0win$Intensity)
  maxRetention=iso0win$RetentionTime[maxIndex]
  
  newdata<-data.frame(ChroLeft=chroLeft,
                      ChroRight=chroRight,
                      InitBoundaryLeft=isoBoundary[1], 
                      InitBoundaryRight=isoBoundary[2],
                      ApexWindowLeft=apexBoundary[1],
                      ApexWindowRight=apexBoundary[2],
                      DistanceLeft=distanceBoundary[1],
                      DistanceRight=distanceBoundary[2],
                      RetentionTime=maxRetention,
                      MaxIntensity=iso0win$Intensity[maxIndex],
                      MaxIdentifiedRetentionTime=maxIdentifiedRetentionTime,
                      PeakArea=sum(iso0win$Intensity))
  df<-rbind(df, newdata)
  
  if(outputImage){
    filtered<-out[out$RetentionTime >= chroLeft & out$RetentionTime <= chroRight,]
    profile<-aggregate(filtered$Intensity, by=list(Isotopic=filtered$Isotopic), FUN=sum)
    colnames(profile)<-c("Isotopic", "Abundance")
    
    observedSum<-sum(profile$Abundance)
    profile$Percentage<-profile$Abundance / observedSum
    
    xmlfile<-paste0(chroFile, ".xml")
    xdata<- xmlParse(xmlfile)
    isotopics<-as.data.frame(t(xpathSApply(xdata, "//*/Ion", attrFun)), stringsAsFactors = FALSE)
    isotopics<-isotopics[c(1:nrow(profile)),]
    isotopics$Mz<-as.numeric(isotopics$Mz)
    isotopics$Intensity<-as.numeric(isotopics$Intensity)
    theoreticalSum<-sum(isotopics$Intensity)
    isotopics$TheoreticalPercentage<-isotopics$Intensity / theoreticalSum
    
    profiledata<-cbind(isotopics, profile)
    
    colnames(profiledata)<-c("MZ", "TheoreticalAbundance", "TheoreticalPercentage", "Isotopic", "ObservedAbundance", "ObservedPercentage")
    
    write.csv(profiledata, file=paste0(chroFile, ".csv"), row.names=F)
    
    imgFile<-paste0(chroFile, ".png")
    
    slimprofile<-profiledata[,c("MZ", "TheoreticalPercentage", "ObservedPercentage")]
    colnames(slimprofile)<-c("MZ", "Theoretical", "Observed")
    meltprofile<-melt(slimprofile, id.vars = c("MZ"))
    colnames(meltprofile)<-c("MZ", "Profile", "Percentage")
    
    p1<-ggplot(meltprofile, aes(x=MZ, y=Percentage, fill=Profile, color=Profile)) + 
      geom_bar(stat = "identity", position=position_dodge()) + 
      scale_y_continuous(labels=percent) +
      xlab("m/z") +
      theme_bw() +
      theme(plot.background = element_blank(),
            plot.margin=unit(c(5,5,5,20),"points"))
    
    out$Isotopic = factor(out$Isotopic)
    
    slimdata<-out[,c("RetentionTime", "Isotopic", "Intensity", "PPMTolerance")]
    
    meltdata<-melt(slimdata, id.vars=c("RetentionTime", "Isotopic"))
    
    scans<-unique(out$Scan)
    deuteriums<-unlist(lapply(scans, function(x){
      scandata<-out[out$Scan == x,]
      scandata$Percentage<-scandata$Intensity / sum(scandata$Intensity)
      sum(as.numeric(scandata$Isotopic) * scandata$Percentage)
    }))
    
    iso0data<-out[out$Isotopic == 1,c("RetentionTime", "Isotopic", "ProfileCorrelation", "ProfileDistance")]
    meltIso0<-melt(iso0data, id.vars=c("RetentionTime", "Isotopic"))
    
    meltall<-rbind(meltdata, meltIso0)
    colnames(meltall)<-c("RetentionTime", "Isotopic", "Category", "Value")
    
    png(imgFile, res=300, width = 1500, height=2500)
    
    identifiedScan = unique(out$RetentionTime[out$Identified == "True"])
    
    p2<-ggplot(meltall, aes(x=RetentionTime, y=Value, group=Isotopic, color=Isotopic)) + 
      geom_line() + 
      geom_vline(xintercept = identifiedScan, colour="red") + 
      facet_wrap(~ Category, ncol=1, scales="free") +
      ylab("")
    p2<-p2+geom_vline(xintercept = c(chroLeft, chroRight), colour="blue") +
      theme_bw() +
      theme(plot.background = element_blank(),
            strip.background = element_blank())
    
    grid.newpage()
    pushViewport(viewport(layout = grid.layout(5, 1)))
    vplayout <- function(x, y) viewport(layout.pos.row = x, layout.pos.col = y)
    print(p1, vp = vplayout(1:1, 1))
    print(p2, vp = vplayout(2:5, 1))  # key is to define vplayout
    
    xpos<-0.06
    grid.text("(A)",x=xpos,y=0.99)
    grid.text("(B)",x=xpos,y=0.79)
    grid.text("(C)",x=xpos,y=0.59)
    grid.text("(D)",x=xpos,y=0.39)
    grid.text("(E)",x=xpos,y=0.19)
    
    dev.off()
  }
}

res<-cbind(files, df)

write.table(res, file=outputfile, row.names=FALSE, sep="\t")

#cat("Cost time ", proc.time() - ptm)
