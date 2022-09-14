using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherSystem.Models
{
    public class WeatherParameters
    {
        public int MeasurementId { get; set; }
        public string MeasurementName { get; set; }
        public string Unit { get; set; }

        public List<WeatherParameters> GetWeatherParameters(string connectionString)
        {

            List<WeatherParameters> weatherParameterList = new List<WeatherParameters>();

            SqlConnection con = new SqlConnection(connectionString);

            string selectSQL = "select MeasurementId, MeasurementName, Unit from MEASUREMENT";
            con.Open();

            SqlCommand cmd = new SqlCommand(selectSQL, con);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr != null)
            {
                while (dr.Read())
                {
                    WeatherParameters weatherParameter = new WeatherParameters();

                    weatherParameter.MeasurementId = Convert.ToInt32(dr["MeasurementId"]);
                    weatherParameter.MeasurementName = dr["MeasurementName"].ToString();
                    weatherParameter.Unit = dr["Unit"].ToString();

                    weatherParameterList.Add(weatherParameter);
                }

            }

            return weatherParameterList;
        }
    }
}
