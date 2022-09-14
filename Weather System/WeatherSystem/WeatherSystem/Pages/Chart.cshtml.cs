using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherSystem.Models;

namespace WeatherSystem.Pages
{
    public class ChartModel : PageModel
    {
        public string measurementName;

        public List<ChartData> chartDataList = new List<ChartData>();

        string connectionString;

        readonly IConfiguration _configuration;

        public ChartModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {

            measurementName = Request.Query["measurementname"];

            chartDataList = ChartData();

        }

        public void OnPut()
        {

        }


        private List<ChartData> ChartData()
        {
            connectionString = _configuration.GetConnectionString("ConnectionString");

            List<ChartData> chartDataList = new List<ChartData>();

            ChartData chartData = new ChartData();

            chartDataList = chartData.GetChartData(connectionString, measurementName);

            return chartDataList;
        }
    }
}
