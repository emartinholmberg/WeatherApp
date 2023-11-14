using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey = "4f57dfb8521bba5b79b76a4d740491c7"; // API KEY
        private readonly string _baseUrl = "https://api.openweathermap.org/data/2.5/forecast";

        public WeatherController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(string city)
        {
            try
            {
                // If the 'city' parameter is empty, use a default city (e.g., Stockholm)
                if (string.IsNullOrEmpty(city))
                {
                    city = "Stockholm";
                }

                ViewData["City"] = city;

                var httpClient = _httpClientFactory.CreateClient();

                var apiUrl = $"{_baseUrl}?q={city}&appid={_apiKey}&units=metric";

                var response = await httpClient.GetStringAsync(apiUrl);
                var forecastData = JObject.Parse(response);

                var viewModel = new WeatherViewModel
                {
                    ForecastList = forecastData["list"]?.ToObject<List<ForecastItem>>()
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                ViewData["ErrorMessage"] = "An error occurred while fetching weather data.";
                return View();
            }
        }
    }
}
