#predefine_start

outputdir<-"H:/shengquanhu/projects/rcpa/TurboRaw2Mgf/ions_0.01_new"
inputfile<-"20110915_iTRAQ_4plex_GK_6ug_Exp_1.raw.forward.ionfrequency.ms2charge2"
outputfile<-"20110915_iTRAQ_4plex_GK_6ug_Exp_1.raw.forward.ionfrequency.ms2charge2.sig.csv"

#predefine_end

require(MASS)

data<-read.csv(inputfile)

rl<-rlm(SamIntensity~-1+RefIntensity, data=data)
srl<-summary(rl)
coeff<-srl$coefficients
pvalue<-pt( coeff[3] , srl$df[2], lower.tail=F)*2

df<-data.frame(Ratio=coeff[1], StdErr=coeff[2], tValue<-coeff[3], pValue<-pvalue)
colnames(df)<-c(""Ratio"",""StdErr"",""tValue"",""pValue"")
write.csv(df,outputfile)
