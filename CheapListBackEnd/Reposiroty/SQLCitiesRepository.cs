using CheapListBackEnd.Models;
using CheapListBackEnd.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CheapListBackEnd.Reposiroty
{
    public class SQLCitiesRepository : SQLGeneralRepository, ICitiesRepository
    {
        public IEnumerable<Cities> GetCities()
        {
            List<Cities> DataCities = new List<Cities>();
            SqlConnection con = null;

            try
            {
                con = connect(false);

                string str = "select * from cities order by cities.cityName";

                SqlCommand cmd = new SqlCommand(str, con);

                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Cities c = new Cities();
                    c.cityID = (int)sdr["cityID"];
                    c.cityName = (string)sdr["cityName"];
                    c.Lat = (string)sdr["lat"];
                    c.Lng = (string)sdr["lng"];
                    DataCities.Add(c);
                }
                return DataCities;
            }
            catch (Exception exp)
            {
                throw (exp);
            }
            finally
            {
                con.Close();
            }
        }

        //run once
        public int PostCities(List<Cities> cities)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect(false);

               

                foreach (var city in cities)
                {
                     string str = " SET QUOTED_IDENTIFIER OFF INSERT INTO cities (cityID, cityName, lat, lng) values ";
                    string replaceTheName = "";
                    if (city.cityName.Contains('"'))
                    {
                        replaceTheName = city.cityName.Replace('"', '`');
                    }
                    else replaceTheName = city.cityName;
                    str += $"({city.cityID}, \"{replaceTheName}\", '{city.Lat}', '{city.Lng}')";
                    cmd = new SqlCommand(str, con);
                    cmd.ExecuteNonQuery(); // the max value in 1 query is 1000 ! 

                }

                return 1;

              
            }
            catch (Exception ex)
        {
                return 0;
                throw (ex);

            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

    }
}