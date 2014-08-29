#predefine_start

outputdir<-"H:/shengquanhu/projects/rcpa/TurboRaw2Mgf/ions_0.01_new"
inputfile<-"20110915_iTRAQ_4plex_GK_6ug_Exp_1.raw.forward.ionfrequency.ms2charge2"
outputfile<-"20110915_iTRAQ_4plex_GK_6ug_Exp_1.raw.forward.ionfrequency.ms2charge2.sig.tsv"
minfreq<-0.01
probability<-0.95
minMedianIntensity<-0.05

#predefine_end

library(outliers)

setwd(outputdir)

data<-read.table(inputfile, sep="\t", header=T)

dd<-data[data$Frequency >= minfreq,]

ol1 <- scores(dd$Frequency, type = "z", prob = probability)
ol1data<-dd[ol1,]
ol1data<-ol1data[ol1data$MedianIntensity >= minMedianIntensity,]

write.table(ol1data, file=outputfile, row.names=F, sep="\t")
