using System;
using System.Collections.Generic;
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
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Integrated Security=True";

            SqlConnection conn = new SqlConnection(connString);

            return conn;
        }


    }
}
