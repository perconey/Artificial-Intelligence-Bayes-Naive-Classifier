using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI3_Bayes
{
    public class Spacer
    {
        public WeatherKind Weather { get; set; }

        public TemperatureKind Temperature
        {
            get
            {
                if (CelsiusTemperature > 20)
                    return TemperatureKind.Hot;
                else if (CelsiusTemperature >= 16)
                    return TemperatureKind.Medium;
                else return TemperatureKind.Cold;
            }
        }

        public Int32 CelsiusTemperature { get; set; }

        public WindKind Wind { get; set; }
    
        public Boolean? Decision { get; set; }

        public Guid Guid;

        public Spacer(WeatherKind weather, Int32 temp, WindKind wind, Boolean? decision)
        {
            Weather = weather;
            CelsiusTemperature = temp;
            Wind = wind;
            Decision = decision;
            Guid = Guid.NewGuid();
        }

        public override String ToString()
        {
            return $"({Enum.GetName(typeof(WindKind), Wind)}), ({Enum.GetName(typeof(WeatherKind), Weather)}), ({Enum.GetName(typeof(TemperatureKind), Temperature)})";
        }
    }

    public enum WeatherKind
    {
        Sun = 1,
        Rain = 2,
        Clouds = 3
    }

    public enum TemperatureKind
    {
        Hot = 1,
        Medium = 2,
        Cold = 3
    }

    public enum WindKind
    {
        Strong = 1,
        Normal = 2,
        Light = 3
    }
}
