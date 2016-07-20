#predefine_start

outputdir<-"H:/shengquanhu/projects/20160714_de_calculator"
inputfile<-"H:/shengquanhu/projects/20160714_de_calculator/NASH6B-0h.deuterium.boundary.tsv"
outputfile<-"H:/shengquanhu/projects/20160714_de_calculator/NASH6B-0h.deuterium.calc.tsv"
outputImage<-1
excludeIsotopic0<-0
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

attrFun <- function(x){
  xattrs <- xmlAttrs(x)
  c(xattrs)
}

filelist<-read.delim(inputfile, stringsAsFactors = FALSE, header=T)
index<-1
filelist$Deuterium<-0

for (index in c(1:nrow(filelist))){
  inputFile=paste0(filelist$ChroDirectory[index], "/", filelist$ChroFile[index], ".tsv")
  left<-filelist[index,"ApexWindowLeft"]
  right<-filelist[index,"ApexWindowRight"]
  
  cat(inputFile, "\n")
  out<-read.table(inputFile, header=T)

  if(excludeIsotopic0){
    out$Isotopic = out$Isotopic - 1
  }
  
  filtered<-out[out$RetentionTime >= left & out$RetentionTime <= right,]
  profile<-aggregate(filtered$Intensity, by=list(Isotopic=filtered$Isotopic), FUN=sum)
  colnames(profile)<-c("Isotopic", "Abundance")
  profile$Percentage<-profile$Abundance / sum(profile$Abundance)
  profile$Deuterium<-profile$Isotopic * profile$Percentage
  
  deuterium<-sum(profile$Deuterium)
  filelist[index, "Deuterium"]<-deuterium
  
  xmlfile<-paste0(inputFile, ".xml")
  xdata<- xmlParse(xmlfile)
  isotopics<-as.data.frame(t(xpathSApply(xdata, "//*/Ion", attrFun)), stringsAsFactors = FALSE)
  isotopics<-isotopics[c(1:nrow(profile)),]
  isotopics$Mz<-as.numeric(isotopics$Mz)
  isotopics$Intensity<-as.numeric(isotopics$Intensity)
  
  profiledata<-cbind(isotopics, profile)
  colnames(profiledata)<-c("MZ", "Theoretical", "Isotopic", "Intensity", "Observed", "CalculatedDeuterium")
  profiledata<-profiledata[,c(3,1,2,5,4,6)]
  write.csv(profiledata, file=paste0(inputFile, ".csv"), row.names=F)
  
  if(outputImage){
    imgFile<-paste0(inputFile, ".png")
    
    meltprofile<-melt(profiledata, id.vars = c("MZ", "Isotopic", "Intensity", "CalculatedDeuterium"))
    colnames(meltprofile)<-c("MZ", "Isotopic", "Intensity", "CalculatedDeuterium", "Profile", "Percentage")
    p1<-ggplot(meltprofile, aes(x=MZ, y=Percentage, fill=Profile, color=Profile)) + 
      geom_bar(stat = "identity", position=position_dodge()) + 
      ggtitle(paste0("Deuterium = ", sprintf("%.4f", deuterium))) + 
      scale_y_continuous(labels=percent) +
      theme_bw() +
      theme(plot.background = element_blank())
    
    out$Isotopic = factor(out$Isotopic)
    
    meltdata<-melt(out, id.vars=c("Scan", "RetentionTime", "Isotopic", "Mz", "ProfileCorrelation", "Identified"),
                   variable.name = "Category",
                   value.name="Value")
    meltdata<-meltdata[,c("RetentionTime", "Isotopic", "Category", "Value")]
    
    scans<-unique(out$Scan)
    deuteriums<-unlist(lapply(scans, function(x){
      scandata<-out[out$Scan == x,]
      scandata$Percentage<-scandata$Intensity / sum(scandata$Intensity)
      sum(as.numeric(scandata$Isotopic) * scandata$Percentage)
    }))
    
    iso0data<-out[out$Isotopic == 1,c("RetentionTime", "Isotopic", "ProfileCorrelation")]
    iso0data$Deuterium <- deuteriums
    meltIso0<-melt(iso0data, id.vars=c("RetentionTime", "Isotopic"),
                   variable.name = "Category",
                   value.name="Value")
    
    meltall<-rbind(meltdata, meltIso0)
    
    iso0data<-out[out$Isotopic == 1,c("RetentionTime", "Scan", "Intensity", "Identified")]
    identified<-iso0data[iso0data$Identified == "True",]
    if(nrow(identified) == 0){
      maxIdentifiedIndex=round(nrow(iso0data) / 2)
    }else{
      index <-which.max(identified$Intensity)
      maxIdentifiedIndex<-which(iso0data$Scan==identified$Scan[index])
    }
    maxIdentifiedRetentionTime<-iso0data$RetentionTime[maxIdentifiedIndex]
    
    png(imgFile, res=300, width = 2000, height=4000)
    
    identifiedScan = unique(out$RetentionTime[out$Identified == "True"])
    
    p2<-ggplot(meltall, aes(x=RetentionTime, y=Value, group=Isotopic, color=Isotopic)) + geom_line() + 
      geom_vline(xintercept = identifiedScan) + 
      facet_wrap(~ Category, ncol=1, scales="free") +
      ylab("")
    if(nrow(identified) != 0){
      p2<-p2+geom_vline(xintercept = maxIdentifiedRetentionTime, colour="red")
    }
    p2<-p2+geom_vline(xintercept = c(left, right), colour="blue") +
      theme_bw() +
      theme(plot.background = element_blank())
    
    grid.newpage()
    pushViewport(viewport(layout = grid.layout(5, 1)))
    vplayout <- function(x, y) viewport(layout.pos.row = x, layout.pos.col = y)
    print(p1, vp = vplayout(1:1, 1))
    print(p2, vp = vplayout(2:5, 1))  # key is to define vplayout
    dev.off()
  }
}

write.table(filelist, file=outputfile, row.names=F, sep="\t")
