using System;

namespace WeatherForecastAPI
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int? TemperatureC { get; set; }

        public int TemperatureF
        {
            get
            {
                if (!TemperatureC.HasValue)
                {
                    throw new InvalidOperationException("TemperatureC is null");
                }

                return 32 + (int)(TemperatureC.Value / 0.5556);
            }
        }

        public string Summary { get; set; }
    }
}
