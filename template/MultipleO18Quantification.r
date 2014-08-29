#predefine_start

outputdir<-"H:/shengquanhu/projects/rcpa/TurboRaw2Mgf/ions_0.01_new"
inputfile<-"20110915_iTRAQ_4plex_GK_6ug_Exp_1.raw.forward.ionfrequency.ms2charge2"
outputfile<-"20110915_iTRAQ_4plex_GK_6ug_Exp_1.raw.forward.ionfrequency.ms2charge2.sig.tsv"

#predefine_end

require(MASS)

files<-read.csv(inputfile, row.names=1, check.names=F)

rlm_result<-apply(files, 1, function(x){
  data<-read.csv(x[1])
  rl<-rlm(SamIntensity~0+RefIntensity, data=data)
  srl<-summary(rl)
  coeff<-srl$coefficients
  pvalue<-pt( coeff[3] , srl$df[2], lower.tail=F)*2
  return (c(coeff[1], coeff[2], coeff[3], pvalue, nrow(data)))
})

rlm_result=t(rlm_result)
colnames(rlm_result)<-c(""Ratio"",""StdErr"",""tValue"",""pValue"",""Count"")

finalresult<-cbind(files, rlm_result)
write.csv(finalresult,""" + outputfile + @""")");
