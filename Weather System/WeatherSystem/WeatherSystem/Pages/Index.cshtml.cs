using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using WeatherSystem.Models;

namespace WeatherSystem.Pages
{
    public class IndexModel : PageModel
    {
        readonly ILogger<IndexModel> _logger;
        readonly IConfiguration _configuration;
        string connectionString;
       
        public List<MeasurementData> measurementDataList = new List<MeasurementData>();
        public string weatherTemperatureText = "";
        public string weatherWindSpeedText = "";

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void OnGet()
        {
            measurementDataList = MeasurementData();
        }

        private List<MeasurementData> MeasurementData()
        {
            connectionString = _configuration.GetConnectionString("ConnectionString");

            string[] measurementNames = { "mtTemp1", "mtWindSpeed"};

            for (int i = 0; i < measurementNames.Length; i++)
            { 
                MeasurementData measurementData = new MeasurementData();

                measurementData = measurementData.GetCurrentMeasurmentData(connectionString, measurementNames[i]);
                measurementDataList.Add(measurementData);
            }


            double temperatureValue = measurementDataList[0].MeasurementValue;
            string temperatureUnit = measurementDataList[0].Unit;

            double windSpeedValue = measurementDataList[1].MeasurementValue;
            string windSpeedUnit = measurementDataList[1].Unit;

            //Temperature---------------------------------------------------------
            weatherTemperatureText = "<i class='fas fa-temperature-low' style='font-size:72px;color:red;'></i> " + temperatureValue + temperatureUnit;

            //Wind Speed---------------------------------------------------------
            if (windSpeedValue < 0.3)
                weatherWindSpeedText = "<i class='fas fa-sign' style='font-size:72px;color:lavender;'></i>&nbsp;Calm";
            if (windSpeedValue > 0.3 && windSpeedValue <= 1.5)
                weatherWindSpeedText = "<i class='fas fa-sign' style='font-size:72px;color:lightcyan;'></i>&nbsp;Light Air";
            if (windSpeedValue > 1.5 && windSpeedValue <= 3.3)
                weatherWindSpeedText = "<i class='fas fa-sign' style='font-size:72px;color:lightcyan;'></i>&nbsp;Light Breeze";
            if (windSpeedValue > 3.3 && windSpeedValue <= 5.4)
                weatherWindSpeedText = "<i class='fas fa-sign' style='font-size:72px;color:lightcyan;'></i>&nbsp;Gentle Breeze";
            if (windSpeedValue > 5.4 && windSpeedValue <= 7.9)
                weatherWindSpeedText = "<i class='fas fa-sign' style='font-size:72px;color:lightcyan;'></i>&nbsp;Moderate Breeze";
            if (windSpeedValue > 7.9 && windSpeedValue <= 10.7)
                weatherWindSpeedText = "<i class='fas fa-sign' style='font-size:72px;color:lightcyan;'></i>&nbsp;Fresh Breeze";
            if (windSpeedValue > 10.7 && windSpeedValue <= 13.8)
                weatherWindSpeedText = "<i class='fas fa-sign' style='font-size:72px;color:lightcyan;'></i>&nbsp;Strong Breeze";
            if (windSpeedValue > 13.8 && windSpeedValue <= 17.1)
                weatherWindSpeedText = "<i class='fas fa-sign' style='font-size:72px;color:lightcyan;'></i>&nbsp;High Wind";
            if (windSpeedValue > 17.1 && windSpeedValue <= 20.7)
                weatherWindSpeedText = "<i class='fas fa-sign' style='font-size:72px;color:lightcyan;'></i>&nbsp;Gale";
            if (windSpeedValue > 20.7 && windSpeedValue <= 24.4)
                weatherWindSpeedText = "<i class='fas fa-sign' style='font-size:72px;color:lightcyan;'></i>&nbsp;String Gale";
            if (windSpeedValue > 24.4 && windSpeedValue <= 28.4)
                weatherWindSpeedText = "<i class='fas fa-sign' style='font-size:72px;color:lightcyan;'></i>&nbsp;Storm";
            if (windSpeedValue > 28.4 && windSpeedValue <= 32.6)
                weatherWindSpeedText = "<i class='fas fa-sign' style='font-size:72px;color:lightcyan;'></i>&nbsp;Violent Storm";
            if (windSpeedValue > 32.6)
                weatherWindSpeedText = "<i class='fas fa-sign' style='font-size:72px;color:lightcyan;'></i>&nbsp;Hurricane";

            weatherWindSpeedText += " (" + windSpeedValue.ToString("0.0") + windSpeedUnit + ")";

            return measurementDataList;

        }


    }
}
