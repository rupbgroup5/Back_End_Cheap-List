using CheapListBackEnd.Models;
using CheapListBackEnd.Reposiroty;
using CheapListBackEnd.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
                    au.UserPassword = Convert.ToString(sdr["UserPassword"]);
                    au.UserMail = Convert.ToString(sdr["UserMail"]);
                    au.UserAdress = Convert.ToString(sdr["UserAdress"]);
                    au.WayOf_Registration = Convert.ToString(sdr["wayOF_Registration"]);
                    au.SocialID = Convert.ToString(sdr["socialID"]);
                    au.ExpoToken = Convert.ToString(sdr["ExpoToken"]);
                    au.PhoneNumber = Convert.ToString(sdr["PhoneNumber"]);


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
                    au.UserName = (string)sdr["UserName"];
                    au.UserPassword = Convert.ToString(sdr["UserPassword"]);
                    au.UserMail = Convert.ToString(sdr["UserMail"]);
                    au.UserAdress = Convert.ToString(sdr["UserAdress"]);
                    au.WayOf_Registration = Convert.ToString(sdr["wayOF_Registration"]);
                    au.SocialID = Convert.ToString(sdr["socialID"]);
                    au.ExpoToken = Convert.ToString(sdr["ExpoToken"]);
                    au.PhoneNumber = Convert.ToString(sdr["PhoneNumber"]);

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

        public string UserForgotPassword(string userMail)
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
                    au.UserPassword = Convert.ToString(sdr["UserPassword"]);
                }
                else
                {
                    return $" {au.UserMail} המייל המצויין אינו מזוהה במערכת שלנו";
                    // so there is no mail associated with the id providded and I want to handle it on the front end
                }
                try
                {
                    SendMail(au.UserMail, au.UserPassword);
                    return $" {au.UserMail} סיסמתך נשלחה למייל";
                }
                catch (Exception exp)
                {
                    throw exp;
                }

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

        public static void SendMail(string toAddress, string userPassword)
        {
            string password = WebConfigurationManager.AppSettings["SecurePassword"];
            var smtp = new SmtpClient
            {
                Host = "Smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("rupbgroup5@gmail.com", password)
            };

            using (var mailMessage = new MailMessage("rupbgroup5@gmail.com", toAddress)
            {
                Subject = "צוות אויש שחכתי cheap list",
                Body = "" +
                "אהלן, הבנו ששחכת את הסיסמה ?\r\n" +
                "שום בעיה ! \r\n" +
                "הנה הסיסמה שלך כמו שהיא שמורה אצלנו \r\n" +
                $"{userPassword} \r\n" +
                $"צוות אויש שחכתי מאחל לכם המשך קנייה חכמה =] ",
            })
                try
                {
                    smtp.Send(mailMessage);
                }
                catch (Exception exp)
                {
                    throw new Exception($"something went wrong in SendMail method: \n {exp.Message}");
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

        public int PostAppUser(AppUser newUser)
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

                query += "insert  into AppUser (UserName, UserMail, UserPassword, wayOf_Registration, socialID, ExpoToken, PhoneNumber)\r\n";
                query += $"VALUES (\"{newUser.UserName}\", \"{newUser.UserMail}\", \"{newUser.UserPassword}\", \"{newUser.WayOf_Registration}\", \"{newUser.SocialID}\",";
                query += $"\"{newUser.ExpoToken}\",\"{newUser.PhoneNumber}\"); ";
                query += "declare @userID2Associate int;";
                query += "set @userID2Associate = Scope_identity()";
                query += "insert into Contacts (ContactName, ContactPhoneNumber, AppUserID) VALUES";
                foreach (var contact in newUser.Contacts)
                {
                    if (contact.PhoneNumber.Length >= 10)
                    {
                        string replaceTheName = "";
                        if (contact.Name.Contains('"'))
                        {
                            replaceTheName = contact.Name.Replace('"', '`');
                        }
                        else replaceTheName = contact.Name;

                        string tempTrim = contact.PhoneNumber.Trim();
                        string temp = tempTrim;
                        if (tempTrim.Contains(' '))
                        {
                            temp = tempTrim.Replace(" ", "");
                        }
                        string temp2 = temp;
                        if (temp.Contains('-'))
                        {
                            temp2 = temp.Replace("-", "");
                        }

                        string formatContact = temp2;

                        if (temp2[0] == '+')
                        {
                            formatContact = temp2.Remove(0, 4);
                            formatContact = '0' + formatContact;
                        }
                        else if (temp2[0] == '9')
                        {
                            formatContact = temp2.Remove(0, 3);
                            formatContact = '0' + formatContact;
                        }

                        query += $" (\"{replaceTheName}\",\"{formatContact}\", @userID2Associate),";
                    }

                }
                query = query.Substring(0, query.Length - 1);
                query += "select @userID2Associate as userID";

                cmd = new SqlCommand(query, con);

                int res = (int)cmd.ExecuteScalar();

                return res;
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

        public int PostSystemAppUser(AppUser userBySystem)
        {
            SqlConnection con = null;
            SqlCommand cmd;

            try
            {
                con = connect(false);
                string str = $"exec dbo.spAppUser_InsertUserBySystem @UserName='{userBySystem.UserName}', @WayOf_Registration='system', @PhoneNumber = '{userBySystem.PhoneNumber}'";


                cmd = new SqlCommand(str, con);
                userBySystem.UserID = Convert.ToInt32(cmd.ExecuteScalar());
                return userBySystem.UserID;

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
                    if (contact.PhoneNumber.Length >= 10)
                    {
                        string replaceTheName = "";
                        if (contact.Name.Contains('"'))
                        {
                            replaceTheName = contact.Name.Replace('"', '`');
                        }
                        else replaceTheName = contact.Name;

                        string tempTrim = contact.PhoneNumber.Trim();
                        string temp = tempTrim;
                        if (tempTrim.Contains(' '))
                        {
                            temp = tempTrim.Replace(" ", "");
                        }
                        string temp2 = temp;
                        if (temp.Contains('-'))
                        {
                            temp2 = temp.Replace("-", "");
                        }

                        string formatContact = temp2;

                        if (temp2[0] == '+')
                        {
                            formatContact = temp2.Remove(0, 4);
                            formatContact = '0' + formatContact;
                        }
                        else if (temp2[0] == '9')
                        {
                            formatContact = temp2.Remove(0, 3);
                            formatContact = '0' + formatContact;
                        }
                        query += $" (\"{replaceTheName}\",\"{formatContact}\", {user.UserID}),";
                    }
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
                while (sdr.Read())
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

        public AppUser GetExsistUserSocailID(string socailID)
        {
            AppUser au;

            try
            {
                con = connect(false);

                string query = $"exec sp_AppUser_GetUserBySocialID @ProvidedSocialID = '{socailID}'";

                cmd = new SqlCommand(query, con);

                sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    au = new AppUser()
                    {

                        UserID = (int)sdr["UserID"],
                        UserName = Convert.ToString(sdr["UserName"]),
                        UserMail = Convert.ToString(sdr["UserMail"]),
                        UserPassword = Convert.ToString(sdr["UserPassword"]),
                        UserAdress = Convert.ToString(sdr["UserAdress"]),
                        WayOf_Registration = Convert.ToString(sdr["wayOf_Registration"]),
                        SocialID = Convert.ToString(sdr["socialID"])
                    };
                }
                else
                {
                    au = new AppUser(); //EMPTY OBJECT....
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

        public int UpdateUserExpoToken(AppUser user)
        {
            try
            {
                con = connect(false); // true?

                string query = $"exec spAppUser_UpdateUserExpoToken @NewToken='{user.UserID}',@userID = {user.UserID}";

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

        public bool IsExpoTokenUpdated(int userID)
        {
            AppUser au;
            try
            {
                con = connect(false);

                string query = $"exec spAppUser_GetDateOf_LastRegistration @appUserID={userID}";

                cmd = new SqlCommand(query, con);

                sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    au = new AppUser() { DateOfLast_Register = (string)sdr["DateOfLast_Register"] };
                }
                else
                {
                    throw new EntryPointNotFoundException($"there is no such user with the provided ID: {userID}");
                }

                string[] lastRegisterSplitedDate = au.DateOfLast_Register.Split('/');

                int regDay = int.Parse(lastRegisterSplitedDate[0]);
                int regMonth = int.Parse(lastRegisterSplitedDate[1]);
                int regYear = int.Parse(lastRegisterSplitedDate[2]);

                string today = DateTime.Today.ToShortDateString();
                string[] splitedToday = today.Split('/');

                int dayOfToday = int.Parse(splitedToday[0]);
                int monthOfToday = int.Parse(splitedToday[1]);
                int yearOfToday = int.Parse(splitedToday[2]);

                bool onProduction = true; //manual maintence 
                if (onProduction)
                {
                    return (
                    regDay == monthOfToday &&
                    regMonth == dayOfToday && // !!! this is not a mistake! the stupid remote pc save date as format: mm/dd/yyyy
                    regYear == yearOfToday
                    ); // = allready registered today..
                }
                return (
                        regDay == dayOfToday &&
                        regMonth == monthOfToday &&
                        regYear == yearOfToday
                        ); // = allready registered today...
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

        public AppUser AuthenticateContact(string phoneNumber)
        {

            string query = $"exec dbo.spAppUser_GetUserByPhoneNumber @PhoneNumber = '{phoneNumber}'";

            try
            {
                con = connect(false);

                cmd = new SqlCommand(query, con);

                sdr = cmd.ExecuteReader();
                AppUser au = new AppUser();

                while (sdr.Read())
                {
                    au.UserID = (int)sdr["UserID"];
                    au.UserName = (string)sdr["UserName"];
                    au.UserMail = Convert.ToString(sdr["UserMail"]);
                    au.ExpoToken = Convert.ToString(sdr["ExpoToken"]);
                    au.PhoneNumber = Convert.ToString(sdr["PhoneNumber"]);
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



    }

}

