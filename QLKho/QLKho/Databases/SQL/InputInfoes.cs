using QLKho.Databases.Entity_FW;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKho.Databases.SQL
{
    public class InputInfoes : DbQuery
    {
        public override IEnumerable<object> Select()
        {
            var list = new List<InputInfo>();
            try
            {
                string sql = "select InputInfo.*," +
                    "Product.DisplayName as ProductName," +
                    "Product.BarCode," +
                    "Product.IdUnit," +
                    "Product.IdSuplier," +
                    "Unit.DisplayName as UnitName," +
                    "Suplier.DisplayName as SuplierName," +
                    "Suplier.Address," +
                    "Suplier.Phone," +
                    "Suplier.Email," +
                    "Suplier.MoreInfo," +
                    "Suplier.ContractDate," +
                    "Product.States as ProductStates," +
                    "Input.DateInput from InputInfo " +
                    "inner join Product on InputInfo.IdProduct = Product.Id " +
                    "inner join Input on InputInfo.IdInput = Input.Id " +
                    "inner join Unit on Unit.Id = Product.IdUnit " +
                    "inner join Suplier on Suplier.Id = Product.IdSuplier";
                SqlCommand command = new SqlCommand(sql, DataProvider.Instance.DB);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            int IdProduct = reader.GetInt32(reader.GetOrdinal("IdProduct"));
                            int IdInput = reader.GetInt32(reader.GetOrdinal("IdInput"));
                            int Count = reader.GetInt32(reader.GetOrdinal("Count"));
                            double InputPrice = (double)reader[reader.GetOrdinal("InputPrice")];
                            double OuputPrice = (double)reader[reader.GetOrdinal("OutputPrice")];
                            string States = reader[reader.GetOrdinal("States")] as string;
                            string ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                            string BarCode = reader.GetString(reader.GetOrdinal("BarCode"));
                            int IdUnit = reader.GetInt32(reader.GetOrdinal("IdUnit"));
                            int IdSuplier = reader.GetInt32(reader.GetOrdinal("IdSuplier"));
                            string UnitName = reader.GetString(reader.GetOrdinal("UnitName"));
                            string SuplierName = reader.GetString(reader.GetOrdinal("SuplierName"));
                            string Address = reader.GetString(reader.GetOrdinal("Address"));
                            string Phone = reader.GetString(reader.GetOrdinal("Phone"));
                            string Email = reader[reader.GetOrdinal("Email")] as string;
                            string MoreInfo = reader[reader.GetOrdinal("MoreInfo")] as string;
                            DateTime ContractDate = reader.GetDateTime(reader.GetOrdinal("ContractDate"));
                            string ProductStates = reader[reader.GetOrdinal("ProductStates")] as string;
                            DateTime DateInput = reader.GetDateTime(reader.GetOrdinal("DateInput"));

                            list.Add(new InputInfo() { Id = Id,IdProduct = IdProduct, IdInput = IdInput, Count = Count, InputPrice = InputPrice,OutputPrice = OuputPrice,States = States,
                                Product = new Product() { Id = IdProduct, DisplayName = ProductName, BarCode = BarCode,
                                                          Unit = new Unit() { Id = IdUnit, DisplayName = UnitName},
                                                          Suplier = new Suplier() { Id = IdSuplier, DisplayName = SuplierName, Address = Address, Phone = Phone, Email = Email, MoreInfo = MoreInfo, ContractDate = ContractDate},
                                                          States = ProductStates
                                },
                                Input = new Input() { Id = IdInput, DateInput = DateInput}
                            });
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message.ToString());
                return null;
            }
            return list;
        }

        public override object Insert(object o)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("insert into InputInfo(IdProduct,IdInput,Count,InputPrice,OutputPrice,States) " +
                    "values(@IdProduct,@IdInput,@Count,@InputPrice,@OutputPrice,@States);" +
                    "SELECT CAST(scope_identity() AS int)", DataProvider.Instance.DB))
                {
                    cmd.Parameters.AddWithValue("@IdProduct", (o as InputInfo).IdProduct);
                    cmd.Parameters.AddWithValue("@IdInput", (o as InputInfo).IdInput);
                    cmd.Parameters.AddWithValue("@Count", (o as InputInfo).Count);
                    cmd.Parameters.AddWithValue("@InputPrice", (o as InputInfo).InputPrice);
                    cmd.Parameters.AddWithValue("@OutputPrice", (o as InputInfo).OutputPrice);
                    cmd.Parameters.AddWithValue("@States", (o as InputInfo).States ?? "");
                    
                    (o as InputInfo).Id = (int)cmd.ExecuteScalar();
                    return o;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return null;
            }
        }

        public override int Update(object o)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("update InputInfo set IdProduct = @IdProduct," +
                                                                             "IdInput = @IdInput," +
                                                                             "Count = @Count," +
                                                                             "InputPrice = @InputPrice," +
                                                                             "OutputPrice = @OutputPrice," +
                                                                             "States = @States" +
                                                                             " where Id = @Id", DataProvider.Instance.DB))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Value = (o as InputInfo).Id;
                    cmd.Parameters.Add("@IdProduct", SqlDbType.Int);
                    cmd.Parameters["@IdProduct"].Value = (o as InputInfo).IdProduct;
                    cmd.Parameters.Add("@IdInput", SqlDbType.Int);
                    cmd.Parameters["@IdInput"].Value = (o as InputInfo).IdInput;
                    cmd.Parameters.Add("@Count", SqlDbType.Int);
                    cmd.Parameters["@Count"].Value = (o as InputInfo).Count;
                    cmd.Parameters.AddWithValue("@InputPrice", (o as InputInfo).InputPrice);
                    cmd.Parameters.AddWithValue("@OutputPrice", (o as InputInfo).OutputPrice);
                    cmd.Parameters.Add("@States", SqlDbType.NVarChar);
                    cmd.Parameters["@States"].Value = (o as InputInfo).States ?? "";
                    int rowCount = cmd.ExecuteNonQuery();
                    return rowCount;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return 0;
            }
        }

        public override int Delete(object o)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("delete from InputInfo where Id = @Id", DataProvider.Instance.DB))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Value = (o as InputInfo).Id;

                    int rowCount = cmd.ExecuteNonQuery();
                    return rowCount;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return 0;
            }
        }

        public int TotalInput(DateTime from, DateTime to)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("Sum_Input", DataProvider.Instance.DB))
                {
                    cmd.Parameters.Add("@from", SqlDbType.DateTime);
                    cmd.Parameters["@from"].Value = from;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime);
                    cmd.Parameters["@to"].Value = to;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                               return reader.GetInt32(reader.GetOrdinal("Total"));
                            }

                        }
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return 0;
            }
        }
    }
}
