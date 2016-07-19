#predefine_start

outputdir<-"H:/shengquanhu/projects/Masaru/20160210_deuterium/summary"
inputfile<-"H:/shengquanhu/projects/Masaru/20160210_deuterium/summary/slim.deuterium.boundary.tsv"

#predefine_end

loadOrInstallPackage <- function(x)
{
  if (!require(x,character.only = TRUE))
  {
    options("repos"="http://cran.us.r-project.org")
    install.packages(x,dep=TRUE)
    if(!require(x,character.only = TRUE)) {
      stop(paste0("Package ", x, " not found"))
    }
  }
}

library("grid")
loadOrInstallPackage("ggplot2")
loadOrInstallPackage("reshape2")
loadOrInstallPackage("XML")
loadOrInstallPackage("scales")
loadOrInstallPackage("cowplot")

attrFun <- function(x){
  xattrs <- xmlAttrs(x)
  c(xattrs)
}

filelist<-read.delim(inputfile, stringsAsFactors = FALSE, header=T)
index<-1

lastPercentage = 0
for (index in c(1:nrow(filelist))){
  curPercentage = round(index * 100.0 / nrow(filelist))
  if(curPercentage != lastPercentage){
    lastPercentage = curPercentage;
    cat(curPercentage, "%\n")
  }

  inputFile=paste0(filelist$ChroDirectory[index], "/", filelist$ChroFile[index], ".tsv")
  initBoundaryLeft<-filelist[index,"InitBoundaryLeft"]
  initBoundaryRight<-filelist[index,"InitBoundaryRight"]
  apexWindowLeft<-filelist[index,"ApexWindowLeft"]
  apexWindowRight<-filelist[index,"ApexWindowRight"]
	maxIdentifiedRetentionTime<-filelist[index,"MaxIdentifiedRetentionTime"]
  
  out<-read.table(inputFile, header=T)
  
  filtered<-out[out$RetentionTime >= apexWindowLeft & out$RetentionTime <= apexWindowRight,]
  profile<-aggregate(filtered$Intensity, by=list(Isotopic=filtered$Isotopic), FUN=sum)
  colnames(profile)<-c("Isotopic", "Abundance")
  profile$Percentage<-profile$Abundance / sum(profile$Abundance)

  xmlfile<-paste0(inputFile, ".xml")
  xdata<- xmlParse(xmlfile)
  isotopics<-as.data.frame(t(xpathSApply(xdata, "//*/Ion", attrFun)), stringsAsFactors = FALSE)
  isotopics<-isotopics[c(1:nrow(profile)),]
  isotopics$Mz<-as.numeric(isotopics$Mz)
  isotopics$Intensity<-as.numeric(isotopics$Intensity)
  
  profiledata<-cbind(isotopics, profile)
  colnames(profiledata)<-c("MZ", "Theoretical", "Isotopic", "Intensity", "Observed")
  profiledata<-profiledata[,c(3,1,2,5,4)]
  write.csv(profiledata, file=paste0(inputFile, ".csv"), row.names=F)
  
  imgFile<-paste0(inputFile, ".png")
  
  meltprofile<-melt(profiledata, id.vars = c("MZ", "Isotopic", "Intensity"))
  colnames(meltprofile)<-c("MZ", "Isotopic", "Intensity", "Profile", "Percentage")
  p1<-ggplot(meltprofile, aes(x=MZ, y=Percentage, fill=Profile, color=Profile)) + 
    geom_bar(stat = "identity", position=position_dodge()) + 
    scale_y_continuous(labels=percent) +
    theme_cowplot()

  out$Isotopic = factor(out$Isotopic)
  
  meltdata<-melt(out, id.vars=c("Scan", "RetentionTime", "Isotopic", "Mz", "ProfileCorrelation", "Identified"),
                 variable.name = "Category",
                 value.name="Value")
  meltdata<-meltdata[,c("RetentionTime", "Isotopic", "Category", "Value")]
  
  scans<-unique(out$Scan)

  iso0data<-out[out$Isotopic == 1,c("RetentionTime", "Isotopic", "ProfileCorrelation")]
  meltIso0<-melt(iso0data, id.vars=c("RetentionTime", "Isotopic"),
                 variable.name = "Category",
                 value.name="Value")
  
  meltall<-rbind(meltdata, meltIso0)

  identifiedScan = unique(out$RetentionTime[out$Identified == "True"])
  
  png(imgFile, res=300, width = 2000, height=4000)
  p2<-ggplot(meltall, aes(x=RetentionTime, y=Value, group=Isotopic, color=Isotopic)) + geom_line() + 
    geom_vline(xintercept = identifiedScan) + 
    facet_wrap(~ Category, ncol=1, scales="free") +
    ylab("")
  p2<-p2+geom_vline(xintercept = maxIdentifiedRetentionTime, colour="red")
  p2<-p2+geom_vline(xintercept = c(initBoundaryLeft, initBoundaryRight), colour="blue") +
	  geom_vline(xintercept = c(apexWindowLeft, apexWindowRight), colour="green") +
    theme_cowplot()
  
  grid.newpage()
  pushViewport(viewport(layout = grid.layout(5, 1)))
  vplayout <- function(x, y) viewport(layout.pos.row = x, layout.pos.col = y)
  print(p1, vp = vplayout(1:1, 1))
  print(p2, vp = vplayout(2:5, 1))  # key is to define vplayout
  dev.off()
  
#  break
}
