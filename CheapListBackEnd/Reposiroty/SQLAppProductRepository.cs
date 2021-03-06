﻿
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

                string query = "select A.*, P.listID, p.quantity " +
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
                    ap.estimatedProductPrice = Convert.ToDouble(sdr["estimatedProductPrice"]);
                    ap.Quantity = (int)sdr["quantity"];
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
                string str = $"BEGIN " +
                              $"if NOT exists( select product_barcode from AppProduct where product_barcode = '{appProduct.product_barcode}')" +
                              $"BEGIN " +
                              "SET QUOTED_IDENTIFIER OFF " +
                              "insert into AppProduct (product_barcode, product_name, product_description, product_image,estimatedProductPrice) " +
                             $"values(\'{appProduct.product_barcode}\',\'{appProduct.product_name}\',\'{appProduct.product_description}\',\'{appProduct.product_image}\',{appProduct.estimatedProductPrice}); " +
                             "END END " +
                             $"insert into ProductInList(product_barcode,listID,groupID,quantity) values (\'{appProduct.product_barcode}\',{appProduct.ListID},{appProduct.GroupId},{appProduct.Quantity}); " +
                             " UPDATE AppList SET listEstimatedPrice = ( " +
                             "select sum(A.estimatedProductPrice * P.quantity ) from AppProduct A inner join " +
                             $"ProductInList P on A.product_barcode = P.product_barcode where listID = {appProduct.ListID}) " +
                             $"WHERE listID = {appProduct.ListID}; " +
                             $"delete Notifications where notID = ${appProduct.NotID} " +
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
                             "select sum(A.estimatedProductPrice * P.quantity ) from AppProduct A inner join " +
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

        public int UpdateQuantity(AppProduct appProduct, bool HasAddedQ) //action = ture => plus
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect(false);
                string str = "";
                if (HasAddedQ)
                {
                    str = "update ProductInList " +
                          $"Set quantity = quantity + {appProduct.Quantity} " +
                          $"where product_barcode = '{appProduct.product_barcode}' " +
                          $"delete Notifications where notID = ${appProduct.NotID}";
                }
                else
                {
                    str = "update ProductInList " +
                        $"Set quantity = quantity - {appProduct.Quantity} " +
                        $"where product_barcode = '{appProduct.product_barcode}'";
                }

                str += "UPDATE AppList SET listEstimatedPrice = ( " +
                       "select sum(A.estimatedProductPrice * P.quantity ) from AppProduct A inner join " +
                      $"ProductInList P on A.product_barcode = P.product_barcode where listID = {appProduct.ListID}) " +
                      $"WHERE listID = {appProduct.ListID};";

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
    }
}