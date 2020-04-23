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
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader sdr = null;

        public IEnumerable<AppUser> GetAllAppUsers()
        {
            List<AppUser> allUsers = new List<AppUser>();

            try
            {
                con = connect(true);

                string query = "select  * from AppUser";

                cmd = new SqlCommand(query, con);

                sdr = cmd.ExecuteReader();

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

        public AppUser GetAppUserByID(int userID)
        {
            AppUser au = new AppUser();

            try
            {
                con = connect(false);

                string query = $"select * from AppUser where userID = '{userID}'";

                cmd = new SqlCommand(query, con);

                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    au.UserID = (int)sdr["UserID"];
                    au.UserName = Convert.ToString(sdr["UserName"]);
                    au.UserPassword = Convert.ToString(sdr["UserPassword"]);
                    au.UserMail = Convert.ToString(sdr["UserMail"]); 
                    au.UserAdress = Convert.ToString(sdr["UserAdress"]); 
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

        public AppUser GetUser_forgotPass(string userMail)
        {

            try
            {
                AppUser au = new AppUser();
                con = connect(false);

                string query = $"exec spAppUser_GetUserPassword @userEmail='{userMail}'";

                cmd = new SqlCommand(query, con);

                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    au.UserID = (int)sdr["userID"];
                    au.UserMail = (string)sdr["userMail"];
                    au.UserPassword = (string)sdr["UserPassword"];
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

        public AppUser AuthenticateUserLogin(string userName, string password)
        {
            AppUser au = new AppUser();

            try
            {
                con = connect(false);

                string query = $"exec spAppUser_GetByNameAndPass @user_Name='{userName}', @user_Password='{password}'";

                cmd = new SqlCommand(query, con);

                sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    au.UserID = (int)sdr["UserID"];
                    au.UserName = Convert.ToString(sdr["UserName"]);
                    au.UserPassword = Convert.ToString(sdr["UserPassword"]);
                    au.UserMail = Convert.ToString(sdr["UserMail"]);
                    au.UserAdress = Convert.ToString(sdr["UserAdress"]);
                    //if the user exist and the password metch so I return appuser, else au = null
                }
                else { au = null; }
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

        public int DeleteAppUser(int id)
        {

            try
            {
                con = connect(true);

                string query = $"delete AppUser where UserID = {id}";

                cmd = new SqlCommand(query, con);

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

        public void PostAppUser(AppUser newUser)
        {

            try
            {
                con = connect(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {

                string query = "SET QUOTED_IDENTIFIER OFF\r\n"; // if there is an ' so it wont ruined the insertition
                query += "insert  into AppUser (UserName, UserMail, UserPassword, UserAdress)\r\n";
                query += $"VALUES (\"{newUser.UserName}\", \"{newUser.UserMail}\", \"{newUser.UserPassword}\", \"{newUser.UserAdress}\");";
                query += "declare @userID2Associate int;";
                query += "set @userID2Associate = Scope_identity()";

                query += "insert into Contacts (ContactName, ContactPhoneNumber, AppUserID) VALUES";
                foreach (var contact in newUser.Contacts)
                {
                    query += $"('{contact.Name}','{contact.PhoneNumber}', @userID2Associate),";
                }
                query = query.Substring(0, query.Length - 1);


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

        public int UpdateFeild(UpdateAppUser user2update)
        {
            try
            {
                con = connect(false); // true?

                string query = "SET QUOTED_IDENTIFIER OFF\r\n"; // if there is an ' so it wont ruined the UPDATE
                query += $"UPDATE AppUser SET {user2update.Column2update}=\"{user2update.NewValue}\"";
                query += $"WHERE UserID={user2update.Id};";

                cmd = new SqlCommand(query, con);

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

        public void UpdateUserContactsList(AppUser user)
        {
            try
            {
                con = connect(false);
                string query = "SET QUOTED_IDENTIFIER OFF \r\n";
                 query += $"delete Contacts where AppUserID={user.UserID} ";
                 query += "insert into Contacts(ContactName, ContactPhoneNumber, AppUserID) values ";
                
                foreach (var contact in user.Contacts)
                {
                    string replaceTheName = "";
                    if (contact.Name.Contains('"'))
                    {
                        replaceTheName = contact.Name.Replace('"', '`');
                    }
                    else
                    {
                        replaceTheName = contact.Name;
                    }
                    query += $" (\"{replaceTheName}\",\"{contact.PhoneNumber}\", {user.UserID}),";

                }
                query = query.Substring(0, query.Length - 1);

                cmd = new SqlCommand(query, con);

                cmd.ExecuteNonQuery();

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

        public List<Contact> GetUserContacts(int userID)
        {
            try
            {
                con = connect(false);

                string query = $"exec spContacts_GetContactsByUserID @userID={userID}";

                cmd = new SqlCommand(query, con);

                sdr = cmd.ExecuteReader();

                List<Contact> contacts = new List<Contact>();
                while(sdr.Read())
                {
                    Contact c = new Contact();
                    c.Name = (string)sdr["ContactName"];
                    c.PhoneNumber = (string)sdr["ContactPhoneNumber"];
                    contacts.Add(c);
                }
                return contacts;

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
