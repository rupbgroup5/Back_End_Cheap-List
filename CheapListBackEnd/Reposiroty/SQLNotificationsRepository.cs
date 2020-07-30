using CheapListBackEnd.Models;
using CheapListBackEnd.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CheapListBackEnd.Reposiroty
{
    public class SQLNotificationsRepository : SQLGeneralRepository, INotificationsRepository
    {
        public List<Notifications> GetNotifactionsByID(int userID, int listID)
        {
            List<Notifications> allNotifications = new List<Notifications>();
            SqlConnection con = null;

            try
            {
                con = connect(false);

                string query = $"exec dbo.Notifications_GetNotificationsByID @userTo = {userID}, @listID  = {listID}";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Notifications n = new Notifications();
                    n.NotID = (int)sdr["notID"];
                    n.UserFrom = (int)sdr["userFrom"];
                    n.UserTo = (int)sdr["userTo"];
                    n.Title = Convert.ToString(sdr["title"]);
                    n.TypeNot = Convert.ToString(sdr["typeNot"]);
                    n.DataObject = Convert.ToString(sdr["dataObject"]);
                    n.HasRead = Convert.ToBoolean(sdr["hasRead"]);
                    n.HasDone = Convert.ToBoolean(sdr["hasDone"]);
                    n.GroupID = (int)sdr["groupID"];
                    n.ListID = (int)sdr["listID"];
                    n.Body = Convert.ToString(sdr["body"]);
                    allNotifications.Add(n);
                }
                return allNotifications;
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

        public List<Notifications> GetNotifactionsByGroupID(int userID, int groupID)
        {
            List<Notifications> allNotifications = new List<Notifications>();
            SqlConnection con = null;

            try
            {
                con = connect(false);

                string query = $"exec Notifications_GetNotificationsByGroupID @userTo=${userID}, @GroupID=${groupID}";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Notifications n = new Notifications();
                    n.NotID = (int)sdr["notID"];
                    n.UserFrom = (int)sdr["userFrom"];
                    n.UserTo = (int)sdr["userTo"];
                    n.Title = Convert.ToString(sdr["title"]);
                    n.Body = Convert.ToString(sdr["body"]);
                    n.TypeNot = Convert.ToString(sdr["typeNot"]);
                    n.DataObject = Convert.ToString(sdr["dataObject"]);
                    n.HasRead = Convert.ToBoolean(sdr["hasRead"]);
                    n.HasDone = Convert.ToBoolean(sdr["hasDone"]);
                    n.GroupID = (int)sdr["groupID"];
                    n.ListID = (int)sdr["listID"];
                    allNotifications.Add(n);
                }
                return allNotifications;
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

        public int PostNotifactions(Notifications notifications)
        {
            SqlConnection con = null;
            SqlCommand cmd;


            try
            {
                con = connect(false);
                string replaceTheName = "";
                if (notifications.DataObject != null && notifications.DataObject.Contains('"'))
                {
                    replaceTheName = notifications.DataObject.Replace("'", "`");
                }
                else replaceTheName = notifications.DataObject;

                string str = $"exec dbo.Notifications_PostNotifications ";
                str += $"@userFrom = {notifications.UserFrom}, ";
                str += $"@userTo = {notifications.UserTo}, ";
                str += $"@title = '{notifications.Title}', ";
                str += $"@body = '{notifications.Body}', ";
                str += $"@typeNot = '{notifications.TypeNot}', ";
                str += $"@dataObject = '{replaceTheName}', ";
                str += $"@groupID = {notifications.GroupID}, ";
                str += $"@listID = ";
                str += notifications.ListID == 0 ? "null" : notifications.ListID.ToString();

                cmd = new SqlCommand(str, con);
                return cmd.ExecuteNonQuery();

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

        public int PostNot2MultipleParticipants(Notifications notification)
        {
            SqlConnection con = null;
            SqlCommand cmd;


            try
            {
                con = connect(false);


                string str = "";
                foreach (var userId in notification.UsersTo)
                {
                    str += $"exec dbo.Notifications_PostNotifications ";
                    str += $"@userFrom = {notification.UserFrom}, ";
                    str += $"@userTo = {userId}, ";
                    str += $"@title = \'{notification.Title}\', ";
                    str += $"@body = \'{notification.Body}\', ";
                    str += $"@typeNot =  \'{notification.TypeNot}\', ";
                    str += $"@dataObject = '{notification.DataObject}', ";
                    if (notification.GroupID == 0)
                    {
                        str += $"@groupID = null, ";
                    }
                    else str += $"@groupID = {notification.GroupID}, ";
                    if (notification.ListID == 0)
                    {
                        str += $"@listID = null; ";
                    }
                    else str += $"@listID = {notification.ListID}; \r\n ";
                }



                cmd = new SqlCommand(str, con);
                return cmd.ExecuteNonQuery();

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

        public int UpdateNotifactions(List<Notifications> notifications)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect(false);
                string str = "";
                foreach (var item in notifications)
                {
                    str += $"exec dbo.Notifications_UpdateNotifications @notID = {item.NotID}, @hasRead = {item.HasRead}, @typeNot = '{item.TypeNot}'; ";
                }
                str += "delete Notifications where hasRead = 1 and typeNot != 'AskProduct'";
                cmd = new SqlCommand(str, con);
                return cmd.ExecuteNonQuery();


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







    }
}