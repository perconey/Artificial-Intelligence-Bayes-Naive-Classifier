using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI3_Bayes
{
    public class BayesClassificator
    {
        private Spacer[] Decisions;
        private Int32 CorrectionFactor;

        private Double decisionClassFalsePower;
        private Double decisionClassTruePower;

        public BayesClassificator(Spacer[] decisions, Int32 correctionFactor)
        {
            Decisions = decisions;
            CorrectionFactor = correctionFactor;
        }

        public (Boolean, Double[]) GetSpacerClassificationAndProbability(Spacer spacerToClassify)
        {
            Boolean decision = false;

            decisionClassTruePower = (Double)Decisions.Count(d => d.Decision.Value);
            Double decisionClassTrueProb = decisionClassTruePower / (Double)Decisions.Count();

            decisionClassFalsePower = (Double)Decisions.Count(d => !d.Decision.Value);
            Double decisionClassFalseProb = decisionClassFalsePower / (Double)Decisions.Count();

            Double param1ProbTrue, param2ProbTrue, param3ProbTrue = 0d;
            Double param1ProbFalse, param2ProbFalse, param3ProbFalse = 0d;

            if (Decisions.Count(d => d.Weather == spacerToClassify.Weather && d.Decision.Value) == 0)
                param1ProbTrue = GetCorrectedProbability(typeof(WeatherKind), true);
            else
                param1ProbTrue = (((Double)Decisions.Count(d => d.Weather == spacerToClassify.Weather && d.Decision.Value) / 10d) / (decisionClassTrueProb));

            if (Decisions.Count(d => d.Wind == spacerToClassify.Wind && d.Decision.Value) == 0)
                param2ProbTrue = GetCorrectedProbability(typeof(WindKind), true);
            else
                param2ProbTrue = (((Double)Decisions.Count(d => d.Wind == spacerToClassify.Wind && d.Decision.Value) / 10d) / (decisionClassTrueProb));

            if (Decisions.Count(d => d.Temperature == spacerToClassify.Temperature && d.Decision.Value) == 0)
                param3ProbTrue = GetCorrectedProbability(typeof(TemperatureKind), true);
            else
                param3ProbTrue = (((Double)Decisions.Count(d => d.Temperature == spacerToClassify.Temperature && d.Decision.Value) / 10d) / (decisionClassTrueProb));

            if (Decisions.Count(d => d.Weather == spacerToClassify.Weather && !d.Decision.Value) == 0)
                param1ProbFalse = GetCorrectedProbability(typeof(WeatherKind), false);
            else
                param1ProbFalse = (((Double)Decisions.Count(d => d.Weather == spacerToClassify.Weather && !d.Decision.Value) / 10d) / (decisionClassFalseProb));

            if (Decisions.Count(d => d.Wind == spacerToClassify.Wind && !d.Decision.Value) == 0)
                param2ProbFalse = GetCorrectedProbability(typeof(WindKind), false);
            else
                param2ProbFalse = (((Double)Decisions.Count(d => d.Wind == spacerToClassify.Wind && !d.Decision.Value) / 10d) / (decisionClassFalseProb));

            if (Decisions.Count(d => d.Temperature == spacerToClassify.Temperature && !d.Decision.Value) == 0)
                param3ProbFalse = GetCorrectedProbability(typeof(TemperatureKind), false);
            else
                param3ProbFalse = (((Double)Decisions.Count(d => d.Temperature == spacerToClassify.Temperature && !d.Decision.Value) / 10d) / (decisionClassFalseProb));

            Double finalTrueClassProb = decisionClassTrueProb * param1ProbTrue * param2ProbTrue * param3ProbTrue;
            Double finalFalseClassProb = decisionClassFalseProb * param1ProbFalse * param2ProbFalse * param3ProbFalse;

            decision = finalTrueClassProb > finalFalseClassProb;

            return (decision, new Double[] { finalTrueClassProb, finalFalseClassProb });
        }

        private Double GetCorrectedProbability(System.Type parameter, Boolean decisionClass)
        {
            Console.WriteLine("Correction factor used!");
            return (0d + (Double)CorrectionFactor) / ((Double)(decisionClass ? decisionClassTruePower : decisionClassFalsePower) + (Double)CorrectionFactor * (Double)Enum.GetValues(parameter).Length);
        }

    }
}
