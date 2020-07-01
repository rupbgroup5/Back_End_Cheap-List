
using CheapListBackEnd.Models;
using CheapListBackEnd.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CheapListBackEnd.Reposiroty
{
    public class SQLAppProductRepository : SQLGeneralRepository, IAppProductRepository
    {
        public IEnumerable<AppProduct> GetProductCart(int listID)
        {
            List<AppProduct> productCart = new List<AppProduct>();
            SqlConnection con = null;

            try
            {
                con = connect(false);

                string query = "select A.*, P.listID " +
                                "from AppProduct A inner join ProductInList P on A.product_barcode = P.product_barcode " +
                                $"where P.listID = {listID}";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    AppProduct ap = new AppProduct();
                    ap.product_barcode = (string)sdr["product_barcode"];
                    ap.product_name = (string)sdr["product_name"];
                    ap.product_description = Convert.ToString(sdr["product_description"]);
                    ap.product_image = Convert.ToString(sdr["product_image"]); ;
                    ap.manufacturer_name = Convert.ToString(sdr["manufacturer_name"]);
                    ap.estimatedProductPrice = Convert.ToDouble(sdr["estimatedProductPrice"]);
                    ap.store_id = Convert.ToInt32(sdr["store_id"]);
                    ap.ListID = (int)sdr["listID"];
                    productCart.Add(ap);
                }
                return productCart;
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

        public int PostAppProduct(AppProduct appProduct)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect(false);
                string str = "SET QUOTED_IDENTIFIER OFF" +
                              " insert into AppProduct (product_barcode, product_name, product_description, product_image, manufacturer_name, store_id,estimatedProductPrice) " +
                             $"values(\'{appProduct.product_barcode}\',\'{appProduct.product_name}\',\'{appProduct.product_description}\',\'{appProduct.product_image}\',\'{appProduct.manufacturer_name}\',{appProduct.store_id},{appProduct.estimatedProductPrice});" +
                             $"insert into ProductInList(product_barcode,listID,groupID) values (\'{appProduct.product_barcode}\',{appProduct.ListID},{appProduct.GroupId});" +
                             "UPDATE AppList SET listEstimatedPrice = (" +
                             "select sum(A.estimatedProductPrice) from AppProduct A inner join " +
                             $"ProductInList P on A.product_barcode = P.product_barcode where listID = {appProduct.ListID})" +
                             $"WHERE listID = {appProduct.ListID};" +
                             "SET QUOTED_IDENTIFIER ON";
                cmd = new SqlCommand(str, con);
                return cmd.ExecuteNonQuery();
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
        public int DeleteProduct(string barcode, int listID)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect(false);
                string str = $"delete from AppProduct where product_barcode = '{barcode}'" +
                              "UPDATE AppList SET listEstimatedPrice = (" +
                             "select sum(A.estimatedProductPrice) from AppProduct A inner join " +
                             $"ProductInList P on A.product_barcode = P.product_barcode where listID = {listID})" +
                             $"WHERE listID = {listID};";
                cmd = new SqlCommand(str, con);
                return cmd.ExecuteNonQuery();

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

        public int PostCities(List<Cities> cities)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect(false);
              

                foreach (var city in cities)  
                {
                    string str = " SET QUOTED_IDENTIFIER OFF INSERT INTO cities (cityID, cityName) values ";
                    string replaceTheName = "";
                    if (city.cityName.Contains('"'))
                    {
                        replaceTheName = city.cityName.Replace('"', '`');
                    }
                    else replaceTheName = city.cityName;
                    str += $"({city.cityID}, \"{replaceTheName}\"); ";
                    cmd = new SqlCommand(str, con);
                    cmd.ExecuteNonQuery();
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

        public IEnumerable<Cities> GetCities()
        {
            List<Cities> DataCities = new List<Cities>();
            SqlConnection con = null;

            try
            {
                con = connect(false);

                string query = "select * from cities";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Cities c = new Cities();
                    c.cityID = (int)sdr["cityID"];
                    c.cityName = (string)sdr["cityName"];
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
    }
}