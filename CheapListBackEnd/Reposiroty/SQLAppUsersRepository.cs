using CheapListBackEnd.Models;
using CheapListBackEnd.Reposiroty;
using CheapListBackEnd.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CheapListBackEnd.Repository
{
    public class SQLAppUsersRepository : SQLGeneralRepository, IAppUsersRepository
    {

        public IEnumerable<AppUser> GetAllAppUsers()
        {
            List<AppUser> allUsers = new List<AppUser>();
            SqlConnection con = null;

            try
            {
                con = connect(true); 

                string query = "select  * from AppUser";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    AppUser au = new AppUser();

                    au.UserID = (int)sdr["UserID"];
                    au.UserName = (string)sdr["UserName"];
                    au.UserPassword = (string)sdr["UserPassword"];
                    au.UserMail = (string)sdr["UserMail"];
                    au.UserAdress = (string)sdr["UserAdress"];

                    allUsers.Add(au);
                }
                return allUsers;
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

        public AppUser GetAppUserByName(string userName)
        {

            AppUser au = new AppUser();
            SqlConnection con = null;

            try
            {
                con = connect(false);

                string query = $"select * from temp_AppUser where UserName = '{userName}'";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    au.UserID = (int)sdr["UserID"];
                    au.UserName = (string)sdr["UserName"];
                    au.UserPassword = (string)sdr["UserPassword"];
                    au.UserMail = (string)sdr["UserMail"];
                    au.UserAdress = (string)sdr["UserAdress"];
                }
                return au;
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

        public void PostAppUser(AppUser newUser) 
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect(false);// seconde opt is: localConStr and true 
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                string query = BuildInsertNewUserQuery(newUser);
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }


        }


        //PAY ATTENTION THIS IS INERTION TO A TEMP_TABLE!!!
        private string BuildInsertNewUserQuery(AppUser newUser)
        {
            string query = "SET QUOTED_IDENTIFIER OFF\r\n"; // if there is an ' so it wont ruined the insertition
            query += "insert  into temp_AppUser (UserMail, UserPassword, UserName, UserAdress)\r\n";
            query += $"VALUES (\"{newUser.UserMail}\",\"{newUser.UserPassword}\",\"{newUser.UserName}\", \"{newUser.UserAdress}\");";
            return query;
        }

        public int DeleteAppUser(int id)
        {
            SqlConnection con = null;

            try
            {
                con = connect( true);

                string query = $"delete AppUser where UserID = {id}";

                SqlCommand cmd = new SqlCommand(query, con);

                int res = cmd.ExecuteNonQuery();
                return res;

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

        public int UpdateFeild(UpdateAppUser user2update)
        {
            SqlConnection con = null;

            try
            {
                con = connect(false);

                string query = "SET QUOTED_IDENTIFIER OFF\r\n"; // if there is an ' so it wont ruined the UPDATE
                query += $"UPDATE AppUser SET {user2update.Column2update}=\"{user2update.NewValue}\"";
                query += $"WHERE UserID={user2update.Id};";

                SqlCommand cmd = new SqlCommand(query, con);

                int res = cmd.ExecuteNonQuery();
                return res;

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
