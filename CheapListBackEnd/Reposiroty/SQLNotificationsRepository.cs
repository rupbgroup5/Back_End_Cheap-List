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
                string str = $"exec dbo.Notifications_PostNotifications @userFrom = {notifications.UserFrom}, @userTo = {notifications.UserTo},";
                       str += $"@title = '{notifications.Title}', @typeNot = '{notifications.Title}', @dataObject = '{notifications.DataObject}',";
                       str += $"@groupID = {notifications.GroupID}, @listID = {notifications.ListID}";

                //string str = "insert into Notifications " +
                //              "(userFrom, userTo, title, typeNot, dataObject, groupID, listID ) " +
                //              $"values({notifications.UserFrom}, {notifications.UserTo}, '{notifications.Title}', " +
                //              $"'{notifications.TypeNot}', '{notifications.DataObject}', {notifications.GroupID}, {notifications.ListID})";

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

        public int DeleteNotifactions(Notifications notifications)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect(false);
                string str = $"procedure dbo.Notifications_DeleteNotifications @hasRead = {notifications.HasRead}, @hasDone = {notifications.HasDone}";

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