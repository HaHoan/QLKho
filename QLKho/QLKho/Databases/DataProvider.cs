using QLKho.Databases.SQL;
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
                Supliers = new Supliers();
                Customers = new Customers();
                Products = new Products();
                InputInfoes = new InputInfoes();
                Inputs = new Inputs();
                OutputInfoes = new OutputInfoes();
                Outputs = new Outputs();
                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            Console.Read();
        }
        public Units Units { get; set; }
        public Supliers Supliers { get; set; }

        public Customers Customers { get; set; }
        public Products Products { get;  set; }
        public InputInfoes InputInfoes { get; set; }
        public Inputs Inputs { get; set; }
        public OutputInfoes OutputInfoes { get; internal set; }
        public Outputs Outputs { get; internal set; }

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
