using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using WeatherSystem.Models;

namespace WeatherSystem.Pages
{
    public class WeatherParametersModel : PageModel
    {
        readonly IConfiguration _configuration;

        public List<WeatherParameters> weatherParameterList = new List<WeatherParameters>();

        string connectionString;

        public WeatherParametersModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {

            weatherParameterList = GetWeatherParameters();

        }

        private List<WeatherParameters> GetWeatherParameters()
        {
            connectionString = _configuration.GetConnectionString("ConnectionString");

            List<WeatherParameters> weatherParameterList = new List<WeatherParameters>();

            WeatherParameters weatherParameters = new WeatherParameters();

            weatherParameterList = weatherParameters.GetWeatherParameters(connectionString);

            return weatherParameterList;
        }
    }
}
