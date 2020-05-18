using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKho.Databases
{
    public class DataProvider
    {
        private static DataProvider _instance;
        public static DataProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    return new DataProvider();
                }
                else
                    return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public SqlConnection DB { get; set; }
     
        public DataProvider()
        {
            DB = DBUtils.GetDBConnection();
            try
            {
                Console.WriteLine("Openning Connection ...");
                DB.Open();
                Units = new Units();

                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            Console.Read();
        }
        public Units Units { get; set; }

        ~DataProvider()
        {
            try
            {
                Console.WriteLine("Closing Connection ...");
                DB.Close();
                Console.WriteLine("Close successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            Console.Read();
        }
    }
}
