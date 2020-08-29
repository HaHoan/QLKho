using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKho.Databases
{
    public class DBSQLServerUtils
    {
        public static SqlConnection
                 GetDBConnection(string datasource, string database)
        {
            //
            // Data Source=umc-c551;Initial Catalog=QLKHO;Integrated Security=True
            //
            string connString = Helper.Decrypt(System.Configuration.ConfigurationManager.ConnectionStrings["QLKHOEntities"].ConnectionString, "umcvn");
            var builder = new EntityConnectionStringBuilder(connString);
            var regularConnectionString = builder.ProviderConnectionString;
            SqlConnection conn = new SqlConnection(regularConnectionString);
            return conn;
        }


    }
}
