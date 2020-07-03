using System;
using CheapListBackEnd.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CheapListBackEnd.Models;
using System.Data.SqlClient;

namespace CheapListBackEnd.Reposiroty
{
    public class SQLAppListRepository :SQLGeneralRepository, IAppListRepository
    {
        public IEnumerable<AppList>GetAllList(int groupID)
        {
            List<AppList> allLists = new List<AppList>();
            SqlConnection con = null;

            try
          {
                con = connect(false);

                string query = $"select  * from AppList where groupID = {groupID}";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    AppList al = new AppList();
                    al.ListID = (int)sdr["listID"];
                    al.GroupID = (int)sdr["groupID"];
                    al.ListName = Convert.ToString(sdr["listName"]);
                    al.ListEstimatedPrice = Convert.ToDouble(sdr["listEstimatedPrice"]);
                    al.CityName = Convert.ToString(sdr["cityName"]);
                    if (al.CityName == null)
                    {
                        al.CityName = "עדיין לא הוגדר עיר לחיפוש";
                    }
                    al.LimitPrice = (int)sdr["limitPrice"];
                    allLists.Add(al);
                }

                return allLists;
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
        public AppList GetAppListById(int groupID, int listID)
        {

            SqlConnection con = null;

            try
            {
                con = connect(true);

                string str = $"select  * from AppList where groupID={groupID} and listID = {listID} ";

                SqlCommand cmd = new SqlCommand(str, con);

                SqlDataReader sdr = cmd.ExecuteReader();

                AppList al = new AppList();

                while (sdr.Read())
                {                   
                    al.ListID = (int)sdr["listID"];
                    al.GroupID = (int)sdr["groupID"];
                    al.ListName = Convert.ToString(sdr["listName"]);
                    al.ListEstimatedPrice = Convert.ToDouble(sdr["listEstimatedPrice"]);
                    al.CityName = Convert.ToString(sdr["cityName"]);
                    if (al.CityName == null)
                    {
                        al.CityName = "הזן עיר לחיפוש";
                    }
                    al.LimitPrice = (int)sdr["limitPrice"];
                }
                return al;
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
        public AppList PostAppList(AppList appList)
        {
            SqlConnection con = null;
            SqlCommand cmd;
          
           

            try
            {
                con = connect(false);
                string str = "DECLARE @LastListID int;" +
                             $"insert into AppList (groupID,listName) values({appList.GroupID},'{appList.ListName}')" +
                             "select @LastListID = SCOPE_IDENTITY();" +
                             "insert into UserInList (userID,listID,groupID) values(" +
                             $"{appList.UserID},@LastListID,{appList.GroupID})" +
                             "select @LastListID as ListID";
                //select userid FROM AppUser WHERE UserName='{appList.UserID}'

                cmd = new SqlCommand(str, con);
                appList.ListID = Convert.ToInt32(cmd.ExecuteScalar());

                return appList;
                
            }
            catch (Exception ex)
            {
                
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
        public int DeleteAppList(int id)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect(false);
                string str = $"delete from AppList where listID = {id}" +
                             "delete from AppProduct where product_barcode not in (select product_barcode from ProductinList)";



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
        public int UpdateListName(AppList appList)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect(false);
                string str = "update AppList " +
                             $"set listName = '{appList.ListName}' " +
                             $"where listId = {appList.ListID}";

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

        public int UpdateCityName(string cityName ,int listID)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect(false);
                string str = "update AppList " +
                             $"set cityName = '{cityName}' " +
                             $"where listID = {listID}";

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

        public int UpdateLimitPrice(int limit, int listID)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect(false);
                string str = "update AppList " +
                             $"set limitPrice = {limit} " +
                             $"where listID = {listID}";

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