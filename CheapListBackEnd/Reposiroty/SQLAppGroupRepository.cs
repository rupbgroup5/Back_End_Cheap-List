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
        public IEnumerable<AppGroup> GetAllGroups()
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

        public AppGroup GetAppGroupById(int id)
        {

            SqlConnection con = null;

            try
            {
                con = connect(true);

                string query = $"select  * from AppGroup where UserID={id}";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader sdr = cmd.ExecuteReader();

                AppGroup ag = new AppGroup();

                while (sdr.Read())
                {
                    ag.GroupID = (int)sdr["groupID"];
                    ag.GroupName = Convert.ToString(sdr["groupName"]);
                    ag.GroupImg = Convert.ToString(sdr["groupImg"]);
                }
                return ag;
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
                             "insert into UserInGroup (userID, groupID, isAdmin) values(" +
                             $"{appGroup.UserID},@LastGroupID,1)" +
                             " select @LastGroupID as GroupID";

                //select userid FROM AppUser WHERE UserName='{appGroup.CreatorName}'



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
    }
}