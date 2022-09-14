using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherSystem.Models
{
    public class ChartData
    {
        public int MeasurementDataId { get; set; }
        public string MeasurementTimeStamp { get; set; }
        public string MeasurementValue { get; set; }


        public List<ChartData> GetChartData(string connectionString, string measurementName)
        {
            List<ChartData> chartDataList = new List<ChartData>();

            SqlConnection con = new SqlConnection(connectionString);

            string selectSQL = "SELECT MeasurementDataId, FORMAT(MeasurementTimeStamp, 'MM.dd HH:mm') AS MeasurementTimeStamp, MeasurementValue FROM GetMeasurementData WHERE MeasurementTimeStamp between CONVERT(DATE, GETDATE()) AND GETDATE() AND MeasurementName='" + measurementName + "' ORDER BY MeasurementDataId";

            con.Open();

            SqlCommand cmd = new SqlCommand(selectSQL, con);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr != null)
            {
                while (dr.Read())
                {
                    ChartData chartData = new ChartData();

                    chartData.MeasurementDataId = Convert.ToInt32(dr["MeasurementDataId"]);
                    chartData.MeasurementTimeStamp = dr["MeasurementTimeStamp"].ToString();
                    chartData.MeasurementValue = dr["MeasurementValue"].ToString();

                    chartDataList.Add(chartData);
                }
            }
            return chartDataList;
        }

    }
}
