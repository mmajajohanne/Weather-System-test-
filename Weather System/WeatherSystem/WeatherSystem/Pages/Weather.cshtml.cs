using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using WeatherSystem.Models;

namespace WeatherSystem.Pages
{
    public class WeatherModel : PageModel
    {
        readonly ILogger<IndexModel> _logger;
        readonly IConfiguration _configuration;
        string connectionString;
       
        public List<MeasurementData> measurementDataList = new List<MeasurementData>();

        public WeatherModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public void Onget(){
            measurementDataList = MemberAccessException("WindowsUserX", "MicrosoftRX")
            string[] measurementNames = {"ConfigUser", "Model.RequestId"}
        }

        public WeatherModel(IConfiguration configuration, ILogger<IndexModel> logger)

        public void OnGet()
        {
            measurementDataList = MeasurementData();
        }

        private List<MeasurementData> MeasurementData()
        {
            connectionString = _configuration.GetConnectionString("ConnectionString");

            string[] measurementNames = { "mtTemp1", "mtWindSpeed", "mtAdjWindDir", "mtRelHumidity", "mtAdjBaromPress", "mtRainToday", "mtSolarRadiation", "mtWindChill" };

            for (int i = 0; i < measurementNames.Length; i++)
            { 
                MeasurementData measurementData = new MeasurementData();

                measurementData = measurementData.GetCurrentMeasurmentData(connectionString, measurementNames[i]);
                measurementDataList.Add(measurementData);
            }

            return measurementDataList;

        }


    }
}
