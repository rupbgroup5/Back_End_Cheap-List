using CheapListBackEnd.Models;
using CheapListBackEnd.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CheapListBackEnd.Reposiroty
{
    public class SQLAppGroupRepository : SQLGeneralRepository, IAppGroupRepository
    {
        public List<AppGroup> GetAllGroups()
        {
            List<AppGroup> allGroups = new List<AppGroup>();
            SqlConnection con = null;

            try
            {
                con = connect(false);

                string query = "select  * from AppGroup";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    AppGroup ag = new AppGroup();
                    ag.GroupID = (int)sdr["groupID"];
                    ag.GroupName = Convert.ToString(sdr["groupName"]);
                    ag.GroupImg = Convert.ToString(sdr["groupImg"]);
                    allGroups.Add(ag);
                }
                return allGroups;
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

        public List<AppGroup> GetAppGroupById(int id)
        {

            SqlConnection con = null;
            List<AppGroup> groupList = new List<AppGroup>();

            try
            {
                con = connect(false);
                string query = $" exec dbo.spAppGroup_GetGroupByUserID @UserID = {id}";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandTimeout = 30;

                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    AppGroup ag = new AppGroup();
                    ag.GroupID = (int)sdr["groupID"];
                    ag.GroupName = Convert.ToString(sdr["groupName"]);
                    ag.GroupImg = Convert.ToString(sdr["groupImg"]);
                    ag.UserID = id;
                    ag.UserName = (string)sdr["userName"];
                    groupList.Add(ag);
                }

                foreach (var group in groupList)
                {
                    con.Close();
                    con = connect(false);
                    query = $"exec dbo.spUserInGroup_GetMembersByGroupID @groupID = {group.GroupID}";
                    cmd = new SqlCommand(query, con);
                    sdr = cmd.ExecuteReader();
                    group.Participiants = new List<AppUser>();
                    while (sdr.Read())
                    {
                        AppUser au = new AppUser();
                        au.UserID = (int)sdr["userID"];
                        au.UserName = (string)sdr["userName"];
                        au.IsAdmin = Convert.ToBoolean(sdr["isAdmin"]);
                        group.Participiants.Add(au);
                    }
                }
                return groupList;
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

        public AppGroup PostAppGroup(AppGroup appGroup)
        {
            SqlConnection con = null;
            SqlCommand cmd;


            try
            {
                con = connect(false);
                string str = "DECLARE @LastGroupID int;" +
                             $"insert into AppGroup (groupName) values('{appGroup.GroupName}')" +
                             "select @LastGroupID = SCOPE_IDENTITY();" +
                             "insert into UserInGroup (userID, groupID, isAdmin) values " +
                             $"({appGroup.UserID},@LastGroupID,1)";

                foreach (var user in appGroup.Participiants)
                {
                    str += $",({user.UserID},@LastGroupID,0)";

                }
                //str = str.Substring(0, str.Length - 1);
                str += "select @LastGroupID as GroupID";


                cmd = new SqlCommand(str, con);
                appGroup.GroupID = Convert.ToInt32(cmd.ExecuteScalar());
                return appGroup;

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

        public int DeleteAppGroup(int id)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect(false);
                string str = $"delete from AppGroup where groupID = {id}" +
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

        public int UpdateGroupName(AppGroup appGroup)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect(false);
                string str = "update AppGroup " +
                             $"set groupName = '{appGroup.GroupName}' " +
                             $"where groupId = {appGroup.GroupID}";

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

        public int RemoveParticipantFromGroup(int userId, int groupId)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect(false);

                string query = $"exec spRemoveUserInGroup @providedUserID={userId}, @providedGroupID={groupId}";

                cmd = new SqlCommand(query, con);

                return cmd.ExecuteNonQuery();

            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                con.Close();
            }

        }

        public int AddUsers2UserInGroup(AppGroup appGroup)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect(false);
                string query = "";

                foreach (var p in appGroup.Participiants)
                {
                    query += $"exec spAddUser2UserInGroup @providedUserID={p.UserID}, @providedGroupID={appGroup.GroupID}\r\n";
                }

                cmd = new SqlCommand(query, con);

                return cmd.ExecuteNonQuery();

            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                con.Close();
            }
        }
    }
}