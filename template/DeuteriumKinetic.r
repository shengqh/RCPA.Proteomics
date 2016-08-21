#predefine_start

outputdir<-"H:/shengquanhu/projects/Masaru/20160803_deuterium/"
inputfile<-"summary.noredundant.deuterium.tsv"
outputfile<-"summary.noredundant.rateConsistant.tsv"

#predefine_end

setwd(outputdir)

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

library(stats)

data<-read.delim(inputfile, stringsAsFactors = FALSE, header=T, check.names = F, row.names=1)
x<-as.numeric(colnames(data))

data$ratio<-unlist(apply(data, 1, function(y){
  notNA<-!is.na(y)
  yy<-y[notNA]
  if(length(yy) < 4){
    return(NA)
  }
  xx<-x[notNA]
  #plot(yy~xx,type="l")
  fit<-tryCatch(nls(yy~a*(1-exp(-b*xx)), start=list(a=0.8, b=0.1)), error=function(e) e)
  if(class(fit) == "nls"){
    return(summary(fit)$parameters[2,1])
  }else{
    return(NA)
  }
}))

savedata<-cbind(data.frame(Protein=rownames(data)), data)
write.table(savedata, file=outputfile, row.names=F, sep="\t", quote=F)
