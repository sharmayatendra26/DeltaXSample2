using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DataModel
{
    public class BaseConnection
    {
        public SqlConnection dbConnection
        { get; private set; }

        public BaseConnection()
        {
            try
            {
                string strConnection = ConfigurationManager.ConnectionStrings["DeltaXEntities"].ConnectionString;
                if (!string.IsNullOrEmpty(strConnection))
                    dbConnection = new SqlConnection(strConnection);
            }
            catch (SqlException ex)
            {
                throw new Exception("Problem while connecting to server(SQL):" + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Problem while connecting to server:" + ex.Message);
            }
        }
    }
}
