using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace WeatherSystem.Models
{
    
    public class MeasurementData
    {
        public int MeasurementId { get; set; }
        public int MeasurementDataId { get; set; }
        public string MeasurementName { get; set; }
        public string MeasurementAlias { get; set; }
        public double MeasurementValue { get; set; }
        public string Unit { get; set; }
        public string MeasurmentTimeStamp { get; set; }

        public MeasurementData GetCurrentMeasurmentData(string connectionString, string MeasurementName)
        {
            MeasurementData measurementData = new MeasurementData();

            SqlConnection con = new SqlConnection(connectionString);

            string selectSQL = "SELECT TOP 1 MeasurementId, MeasurementDataId, MeasurementName, MeasurementAlias, MeasurementValue, Unit, MeasurementTimeStamp FROM GetMeasurementData WHERE MeasurementTimeStamp between CONVERT(DATE, GETDATE()) AND GETDATE() AND MeasurementName ='" + MeasurementName + "' ORDER BY MeasurementTimeStamp DESC";

            con.Open();

            SqlCommand cmd = new SqlCommand(selectSQL, con);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr != null)
            {
                while (dr.Read())
                {
                    measurementData.MeasurementId = Convert.ToInt32(dr["MeasurementId"]);
                    measurementData.MeasurementDataId = Convert.ToInt32(dr["MeasurementDataId"]);
                    measurementData.MeasurementName =dr["MeasurementName"].ToString();
                    measurementData.MeasurementAlias = dr["MeasurementAlias"].ToString();
                    measurementData.MeasurementValue = Convert.ToDouble(dr["MeasurementValue"]);
                    measurementData.Unit = dr["Unit"].ToString();
                    measurementData.MeasurmentTimeStamp = dr["MeasurementTimeStamp"].ToString();
                }
            }
            return measurementData;
        }
    }
}
