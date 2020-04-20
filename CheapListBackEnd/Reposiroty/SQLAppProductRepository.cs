
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

                string query = "select A.*, P.estimatedPrice,P.price " +
                                "from AppProduct A inner join ProductCart P on A.product_barcode = P.product_barcode " +
                                $"where P.listID = {listID}";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    AppProduct ap = new AppProduct ();
                    ap.product_barcode = (int)sdr["product_barcode"];
                    ap.product_name = (string)sdr["product_name"];
                    ap.product_description = Convert.ToString(sdr["product_description"]);
                    ap.product_image = Convert.ToString(sdr["product_image"]);
                    ap.manufacturer_name = Convert.ToString(sdr["manufacturer_name"]);
                    ap.store_id = Convert.ToInt32(sdr["store_id"]);
                    ap.EstimatedPrice = Convert.ToInt32(sdr["estimatedPrice"]);
                    if (ap.EstimatedPrice == 0)
                    {

                    }
                    ap.Price = Convert.ToInt32(sdr["price"]);
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
                string str = "insert into AppProduct (product_barcode, product_name, product_description, product_image, manufacturer_name, store_id)" +
                             $"values(3,'{appProduct.product_name}','{appProduct.product_description}','{appProduct.product_image}','{appProduct.manufacturer_name}',{appProduct.store_id});" +
                             $"insert into ProductCart (product_barcode,listID,groupID) values (3,{appProduct.ListID},{appProduct.GroupId})";
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
        public int DeleteProduct(int barcode)
        {
            throw new NotImplementedException();
        }
    }
}