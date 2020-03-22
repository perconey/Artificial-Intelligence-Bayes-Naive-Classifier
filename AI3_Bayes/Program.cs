using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI3_Bayes
{
    class Program
    {
        static void Main(string[] args)
        {
            Spacer[] spacerDecisions = new Spacer[]
            {
                new Spacer(WeatherKind.Sun, 23, WindKind.Normal, true),
                new Spacer(WeatherKind.Rain, 15, WindKind.Strong, false),
                new Spacer(WeatherKind.Clouds, 17, WindKind.Light, true),
                new Spacer(WeatherKind.Clouds, 21, WindKind.Normal, false),
                new Spacer(WeatherKind.Sun, 20, WindKind.Strong, true),
                new Spacer(WeatherKind.Sun, 25, WindKind.Light, true),
                new Spacer(WeatherKind.Rain, 22, WindKind.Light, true),
                new Spacer(WeatherKind.Sun, 14, WindKind.Strong, false),
                new Spacer(WeatherKind.Clouds, 19, WindKind.Strong, false),
                new Spacer(WeatherKind.Rain, 16, WindKind.Light, false),
            };

            Spacer spacerToClassify = new Spacer(WeatherKind.Rain, 24, WindKind.Light, null);

            BayesClassificator bayesClassificator = new BayesClassificator(spacerDecisions, 1);

            (Boolean, Double[]) result = bayesClassificator.GetSpacerClassificationAndProbability(spacerToClassify);

            Console.WriteLine($"Dla spaceru -> {spacerToClassify.ToString()} - sklasyfikowano klasę decyzji {(result.Item1 ? "Prawda" : "Fałsz")}");
            Console.WriteLine($"Prawdopodobieństwa dla obu klas decyzji to:  \n Prawda/Iśc na spacer -> {result.Item2[0]} \n Fałsz/Nie iść na spacer -> {result.Item2[1]}");

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Przy korzystaniu z wspol. korekcyjnego");

            Spacer spacerToClassifyWithZeroProblem = new Spacer(WeatherKind.Clouds, 14, WindKind.Strong, null);

            BayesClassificator bayesClassificatorEx = new BayesClassificator(spacerDecisions, 1);
            (Boolean, Double[]) resultEx = bayesClassificator.GetSpacerClassificationAndProbability(spacerToClassifyWithZeroProblem);

            Console.WriteLine($"Dla spaceru -> {spacerToClassifyWithZeroProblem.ToString()} - sklasyfikowano klasę decyzji {(resultEx.Item1 ? "Prawda" : "Fałsz")}");
            Console.WriteLine($"Prawdopodobieństwa dla obu klas decyzji to:  \n Prawda/Iśc na spacer -> {resultEx.Item2[0]} \n Fałsz/Nie iść na spacer -> {resultEx.Item2[1]}");

            Console.ReadLine();
        }
    }
}
