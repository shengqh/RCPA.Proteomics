##predefine_begin
setwd("H:/shengquanhu/projects/rcpa/TurboRaw2Mgf/ions_0.01_new")
inputfile<-"20110915_iTRAQ_4plex_GK_6ug_Exp_1.raw.forward.ionfrequency.ms2charge2"
outputfile<-"20110915_iTRAQ_4plex_GK_6ug_Exp_1.raw.forward.ionfrequency.ms2charge2.sig"
pvalue<-0.05
##predefine_end

data2<-read.table("20110915_iTRAQ_4plex_GK_6ug_Exp_1.raw.forward.ionfrequency.ms2charge2.tsv", header=T, sep="\t")

z.pvalue <- function( x ){
  se <- sd( x )/sqrt( length( x ) )
  xbar <- mean( x )
  z <- ( xbar - 0 )/se
  pnorm( z, mean = 0, sd = 1, lower.tail = FALSE)*2
}

data<-read.table(inputfile, header=T, sep="\t")

ions<-unique(data$mz)
scancount<-length(unique(data$scan))

ion<-ions[1]
pvalues<-lapply(ions, function(ion){
  iondata<-data[data$mz == ion,]
  intensities<-c(iondata$intensity, rep(0, scancount - nrow(iondata)))
  z.pvalue(intensities)
})

pp<-unlist(pvalues)

mat<-data.frame(Ion=ions, Pvalue=pp)
