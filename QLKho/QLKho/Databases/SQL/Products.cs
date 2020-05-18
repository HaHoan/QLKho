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
                            int IdUnit = reader.GetInt32(reader.GetOrdinal("IdUnit"));
                            string UnitName = reader.GetString(reader.GetOrdinal("UnitName"));
                            int IdSuplier = reader.GetInt32(reader.GetOrdinal("IdSuplier"));
                            string SuplierName = reader.GetString(reader.GetOrdinal("SuplierName"));
                            string Address = reader.GetString(reader.GetOrdinal("Address"));
                            string Phone = reader.GetString(reader.GetOrdinal("Phone"));
                            string Email = reader.GetString(reader.GetOrdinal("Email"));
                            string MoreInfo = reader.GetString(reader.GetOrdinal("MoreInfo"));
                            DateTime ContractDate = reader.GetDateTime(reader.GetOrdinal("ContractDate"));
                            string States = reader.GetString(reader.GetOrdinal("States"));

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
                    cmd.Parameters.AddWithValue("@IdUnit", (o as Product).IdUnit);
                    cmd.Parameters.AddWithValue("@IdSuplier", (o as Product).IdSuplier);
                    cmd.Parameters.AddWithValue("@States", (o as Product).States);

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
                    cmd.Parameters.Add("@DisplayName", SqlDbType.NVarChar);
                    cmd.Parameters["@DisplayName"].Value = (o as Product).DisplayName;
                    cmd.Parameters.Add("@BarCode", SqlDbType.NVarChar);
                    cmd.Parameters["@BarCode"].Value = (o as Product).BarCode;
                    cmd.Parameters.Add("@IdUnit", SqlDbType.Int);
                    cmd.Parameters["@IdUnit"].Value = (o as Product).IdUnit;
                    cmd.Parameters.Add("@IdSuplier", SqlDbType.Int);
                    cmd.Parameters["@IdSuplier"].Value = (o as Product).IdSuplier;
                    cmd.Parameters.Add("@States", SqlDbType.NVarChar);
                    cmd.Parameters["@States"].Value = (o as Product).States;

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
