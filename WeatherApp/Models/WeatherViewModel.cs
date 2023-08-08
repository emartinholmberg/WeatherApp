namespace WeatherApp.Models
{
    public class WeatherViewModel
    {
        public List<ForecastItem> ?ForecastList { get; set; }
    }

    public class ForecastItem
    {
        public long Dt { get; set; }
        public Main ?Main { get; set; }
        public List<Weather> ?Weather { get; set; }
    }

    public class Main
    {
        public double Temp { get; set; }
    }

    public class Weather
    {
        public string ?Description { get; set; }
    }
}
