using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKho.Databases
{
    public class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string datasource = @"umc-c551";

            string database = "QLKHO";
            return DBSQLServerUtils.GetDBConnection(datasource, database);
        }
    }
}
