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
   public class OutputInfoes : DbQuery
    {
        public override IEnumerable<object> Select()
        {
            var list = new List<OutputInfo>();
            try
            {
                string sql = "select OutputInfo.*," +
                    "InputInfo.IdInput," +
                    "InputInfo.Count as InputCount," +
                    "InputInfo.InputPrice," +
                    "InputInfo.OutputPrice as DefaultOutputPrice," +
                    "InputInfo.States as InputState," +
                    "Input.DateInput," +
                    "Product.Id as IdProduct," +
                    "Product.DisplayName as ProductName," +
                    "Product.BarCode, Product.IdUnit," +
                    "Product.IdSuplier," +
                    "Unit.DisplayName as UnitName," +
                    "Suplier.DisplayName as SuplierName," +
                    "Suplier.Address," +
                    "Suplier.Phone, " +
                    "Suplier.Email," +
                    "Suplier.MoreInfo," +
                    "Suplier.ContractDate," +
                    "Product.States ProductStates," +
                    "Outputs.DateOutput," +
                    "Customer.DisplayName as CustomerName, " +
                    "Customer.Address as CustomerAddress," +
                    "Customer.Phone as CustomerPhone, " +
                    "Customer.Email as CustomerEmail," +
                    "Customer.MoreInfo as CustomerMoreInfo from OutputInfo " +
                    "inner join InputInfo on InputInfo.Id = OutputInfo.IdInputInfo " +
                    "inner join Product on Product.Id = InputInfo.IdProduct " +
                    "inner join Unit on Unit.Id = Product.IdUnit inner join Suplier on Suplier.Id = Product.IdSuplier " +
                    "inner join Outputs on Outputs.Id = OutputInfo.IdOutput " +
                    "inner join Input on Input.Id = InputInfo.IdInput "+
                    "inner join Customer on Customer.Id = OutputInfo.IdCustomer";
                SqlCommand command = new SqlCommand(sql, DataProvider.Instance.DB);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            int IdInputInfo = reader.GetInt32(reader.GetOrdinal("IdInputInfo"));
                            int IdOutput = reader.GetInt32(reader.GetOrdinal("IdOutput"));
                            int Count = reader.GetInt32(reader.GetOrdinal("Count"));
                            float OutputPrice = reader.GetFloat(reader.GetOrdinal("OutputPrice"));
                            string States = reader.GetString(reader.GetOrdinal("States"));
                            int IdCustomer = reader.GetInt32(reader.GetOrdinal("IdCustomer"));
                            int IdProduct = reader.GetInt32(reader.GetOrdinal("IdProduct"));
                            int IdInput = reader.GetInt32(reader.GetOrdinal("IdInput"));
                            int InputCount = reader.GetInt32(reader.GetOrdinal("InputCount"));
                            float DefaultOutputPrice = reader.GetFloat(reader.GetOrdinal("DefaultOutputPrice"));
                            float InputPrice = reader.GetFloat(reader.GetOrdinal("InputPrice"));
                            string InputState = reader.GetString(reader.GetOrdinal("InputState"));

                            string ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                            string BarCode = reader.GetString(reader.GetOrdinal("BarCode"));
                            int IdUnit = reader.GetInt32(reader.GetOrdinal("IdUnit"));
                            int IdSuplier = reader.GetInt32(reader.GetOrdinal("IdSuplier"));
                            string UnitName = reader.GetString(reader.GetOrdinal("UnitName"));
                            string SuplierName = reader.GetString(reader.GetOrdinal("SuplierName"));
                            string Address = reader.GetString(reader.GetOrdinal("Address"));
                            string Phone = reader.GetString(reader.GetOrdinal("Phone"));
                            string Email = reader.GetString(reader.GetOrdinal("Email"));
                            string MoreInfo = reader.GetString(reader.GetOrdinal("MoreInfo"));
                            string ProductStates = reader.GetString(reader.GetOrdinal("ProductStates"));
                            DateTime ContractDate = reader.GetDateTime(reader.GetOrdinal("ContractDate"));

                            DateTime DateOutput = reader.GetDateTime(reader.GetOrdinal("DateOutput"));
                            DateTime DateInput = reader.GetDateTime(reader.GetOrdinal("DateInput"));

                            string CustomerName = reader.GetString(reader.GetOrdinal("CustomerName"));
                            string CustomerAddress = reader.GetString(reader.GetOrdinal("CustomerAddress"));
                            string CustomerPhone = reader.GetString(reader.GetOrdinal("CustomerPhone"));
                            string CustomerEmail = reader.GetString(reader.GetOrdinal("CustomerEmail"));
                            string CustomerMoreInfo = reader.GetString(reader.GetOrdinal("CustomerMoreInfo"));

                            list.Add(new OutputInfo()
                            {   Id = Id,
                                IdInputInfo = IdInputInfo,
                                IdOutput = IdOutput,
                                Count = Count ,
                                OutputPrice = OutputPrice,
                                States = States,
                                IdCustomer = IdCustomer,
                                InputInfo = new InputInfo() { Id = IdInputInfo,
                                                              IdProduct = IdProduct,
                                                              IdInput = IdInput ,
                                                              Count = InputCount,
                                                              InputPrice = InputPrice,
                                                              OutputPrice = DefaultOutputPrice,
                                                              States = InputState,
                                                              Product = new Product()
                                                              {
                                                                Id = IdProduct,
                                                                DisplayName = ProductName,
                                                                BarCode = BarCode,
                                                                IdUnit = IdUnit,
                                                                IdSuplier = IdSuplier,
                                                                States = ProductStates,
                                                                Unit = new Unit()
                                    {
                                        Id = IdUnit,
                                        DisplayName = UnitName
                                    },
                                                                Suplier = new Suplier()
                                    {
                                        Id = IdSuplier,
                                        DisplayName = SuplierName,
                                        Address = Address,
                                        Phone = Phone,
                                        Email = Email,
                                        MoreInfo = MoreInfo,
                                        ContractDate = ContractDate
                                    }
                                                              },
                                                            Input = new Input() {
                                                                    Id = IdInput,
                                                                    DateInput = DateInput}
                                                                },Customer = new Customer()
                                                            {  Id = IdCustomer,
                                                               DisplayName = CustomerName,
                                                               Address = CustomerAddress,
                                                               Email = CustomerEmail,
                                                               Phone = CustomerPhone,
                                                               MoreInfo = CustomerMoreInfo}
                            } );
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
                using (SqlCommand cmd = new SqlCommand("insert into OutputInfo(IdInputInfo,IdOutput,Count,OutputPrice,States,IdCustomer) values(@IdInputInfo,@IdOutput,@Count,@OutputPrice,@States,@IdCustomer);SELECT CAST(scope_identity() AS int)", DataProvider.Instance.DB))
                {
                    cmd.Parameters.AddWithValue("@IdInputInfo", (o as OutputInfo).IdInputInfo);
                    cmd.Parameters.AddWithValue("@IdOutput", (o as OutputInfo).IdOutput);
                    cmd.Parameters.AddWithValue("@Count", (o as OutputInfo).Count);
                    cmd.Parameters.AddWithValue("@OutputPrice", (o as OutputInfo).OutputPrice);
                    cmd.Parameters.AddWithValue("@States", (o as OutputInfo).States);
                    cmd.Parameters.AddWithValue("@IdCustomer", (o as OutputInfo).IdCustomer);
                    (o as Unit).Id = (int)cmd.ExecuteScalar();
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
                using (SqlCommand cmd = new SqlCommand( "update OutputInfo set " +
                                                        "IdInputInfo = @IdInputInfo," +
                                                        "IdOutput = @IdOutput," +
                                                        "Count = @Count," +
                                                        "OutputPrice = @OutputPrice," +
                                                        "States = @States," +
                                                        "IdCustomer = @IdCustomer where Id = @Id", DataProvider.Instance.DB))
                {
                    cmd.Parameters.Add("@IdInputInfo", SqlDbType.Int);
                    cmd.Parameters["@IdInputInfo"].Value = (o as OutputInfo).IdInputInfo;
                    cmd.Parameters.Add("@IdOutput", SqlDbType.Int);
                    cmd.Parameters["@IdOutput"].Value = (o as OutputInfo).IdOutput;
                    cmd.Parameters.Add("@Count", SqlDbType.Int);
                    cmd.Parameters["@Count"].Value = (o as OutputInfo).Count;
                    cmd.Parameters.Add("@OutputPrice", SqlDbType.Float);
                    cmd.Parameters["@OutputPrice"].Value = (o as OutputInfo).OutputPrice;
                    cmd.Parameters.Add("@States", SqlDbType.NVarChar);
                    cmd.Parameters["@States"].Value = (o as OutputInfo).States;
                    cmd.Parameters.Add("@IdCustomer", SqlDbType.Int);
                    cmd.Parameters["@IdCustomer"].Value = (o as OutputInfo).IdCustomer;
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
                using (SqlCommand cmd = new SqlCommand("delete from OutputInfo where Id = @Id", DataProvider.Instance.DB))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Value = (o as OutputInfo).Id;

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
    }
}
