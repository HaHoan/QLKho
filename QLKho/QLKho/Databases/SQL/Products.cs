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
    public class Products : DbQuery
    {
        public override IEnumerable<object> Select()
        {
            var list = new List<Product>();
            try
            {
                string sql = "select Product.*," +
                    " Unit.DisplayName as UnitName," +
                    "Suplier.DisplayName as SuplierName,Suplier.Address,Suplier.Phone,Suplier.Email,Suplier.MoreInfo, Suplier.ContractDate " +
                    "from Product " +
                    "inner join Unit on Product.IdUnit = Unit.Id " +
                    "inner join Suplier on Product.IdSuplier = Suplier.Id";
                SqlCommand command = new SqlCommand(sql, DataProvider.Instance.DB);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            string DisplayName = reader.GetString(reader.GetOrdinal("DisplayName"));
                            string BarCode = reader.GetString(reader.GetOrdinal("BarCode"));
                            int IdUnit = (int) reader[reader.GetOrdinal("IdUnit")];
                            string UnitName = reader[reader.GetOrdinal("UnitName")] as string;
                            int IdSuplier = (int)reader[reader.GetOrdinal("IdSuplier")] ;
                            string SuplierName = reader[reader.GetOrdinal("SuplierName")] as string;
                            string Address = reader[reader.GetOrdinal("Address")] as string;
                            string Phone = reader.GetString(reader.GetOrdinal("Phone"));
                            string Email = reader[reader.GetOrdinal("Email")] as string;
                            string MoreInfo = reader[reader.GetOrdinal("MoreInfo")] as string;
                            DateTime ContractDate = reader.GetDateTime(reader.GetOrdinal("ContractDate"));
                            string States = reader[reader.GetOrdinal("States")] as string;

                            list.Add(new Product()
                            {
                                Id = Id,
                                DisplayName = DisplayName,
                                States = States,
                                BarCode = BarCode,
                                IdUnit = IdUnit,
                                IdSuplier = IdSuplier,
                                Unit = new Unit() { Id = IdUnit, DisplayName = UnitName },
                                Suplier = new Suplier() { Id = IdSuplier, DisplayName = SuplierName, Address = Address, Phone = Phone, Email = Email, MoreInfo = MoreInfo, ContractDate = ContractDate }
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
                using (SqlCommand cmd = new SqlCommand("insert into Product(DisplayName, BarCode,IdUnit,IdSuplier,States) values(@DisplayName,@BarCode,@IdUnit,@IdSuplier,@States)" +
                    ";SELECT CAST(scope_identity() AS int)", DataProvider.Instance.DB))
                {
                    cmd.Parameters.AddWithValue("@DisplayName", (o as Product).DisplayName);
                    cmd.Parameters.AddWithValue("@BarCode", (o as Product).BarCode);
                    cmd.Parameters.AddWithValue("@IdUnit", (o as Product).IdUnit ?? null);
                    cmd.Parameters.AddWithValue("@IdSuplier", (o as Product).IdSuplier ?? null);
                    cmd.Parameters.AddWithValue("@States", (o as Product).States ?? "");

                    (o as Product).Id = (int)cmd.ExecuteScalar();
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
                using (SqlCommand cmd = new SqlCommand("update Product set DisplayName = @DisplayName," +
                                                                        "BarCode = @BarCode," +
                                                                        "IdUnit = @IdUnit," +
                                                                        "IdSuplier = @IdSuplier," +
                                                                        "States = @States " +
                                                                        "where Id = @Id", DataProvider.Instance.DB))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Value = (o as Product).Id;
                    cmd.Parameters.Add("@DisplayName", SqlDbType.NVarChar);
                    cmd.Parameters["@DisplayName"].Value = (o as Product).DisplayName;
                    cmd.Parameters.Add("@BarCode", SqlDbType.NVarChar);
                    cmd.Parameters["@BarCode"].Value = (o as Product).BarCode;
                    cmd.Parameters.Add("@IdUnit", SqlDbType.Int);
                    if ((o as Product).IdUnit != 0)
                    {
                        cmd.Parameters["@IdUnit"].Value = (o as Product).IdUnit;
                    }
                    else
                    {
                        cmd.Parameters["@IdUnit"].Value = DBNull.Value;
                    }
                    cmd.Parameters.Add("@IdSuplier", SqlDbType.Int);
                    if ((o as Product).IdSuplier != 0)
                    {
                        cmd.Parameters["@IdSuplier"].Value = (o as Product).IdSuplier;
                    } else
                    {
                        cmd.Parameters["@IdSuplier"].Value = DBNull.Value;
                    }

                    cmd.Parameters.Add("@States", SqlDbType.NVarChar);
                    cmd.Parameters["@States"].Value = ((o as Product).States ?? "");

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
                using (SqlCommand cmd = new SqlCommand("delete from Product where Id = @Id", DataProvider.Instance.DB))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Value = (o as Unit).Id;

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
