using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CheapListBackEnd.Reposiroty
{
    //whom inherit this class get the connect function
    public class SQLGeneralRepository
    {
        #region Connection strings

         string localConStr = @"Data Source=.;Initial Catalog=Cheap_List;Integrated Security=True";
         string globalConStr = "ConStr";
        #endregion

        protected SqlConnection connect( bool isLocal)
        {
            try
            {
                SqlConnection con = null;

                #region Define SqlConnection whether if local or not by isLocal Variable
                if (isLocal)
                {
                    con = new SqlConnection(localConStr);
                }
                else
                {
                    string cStr = WebConfigurationManager.ConnectionStrings[globalConStr].ConnectionString;
                    //dont forget to confige 
                    con = new SqlConnection(cStr);
                }
                #endregion

                con.Open();

                return con;
            }
            catch (Exception ex)
            {

                throw (ex);
            }
          
        }

    }
}