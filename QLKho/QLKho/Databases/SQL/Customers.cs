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
    public class Customers : DbQuery
    {
        public override IEnumerable<object> Select()
        {
            var list = new List<Customer>();
            try
            {
                string sql = "select * from Customer";
                SqlCommand command = new SqlCommand(sql, DataProvider.Instance.DB);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            int Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            string DisplayName = reader.GetString(reader.GetOrdinal("DisplayName"));
                            string Address = reader.GetString(reader.GetOrdinal("Address"));
                            string Phone = reader.GetString(reader.GetOrdinal("Phone"));
                            string Email = reader.GetString(reader.GetOrdinal("Email"));
                            string MoreInfo = reader.GetString(reader.GetOrdinal("MoreInfo"));
                            list.Add(new Customer() { Id = Id, DisplayName = DisplayName, Address = Address, Phone = Phone, Email = Email, MoreInfo = MoreInfo});
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
                using (SqlCommand cmd = new SqlCommand("insert into Customer(DisplayName,Address,Phone,Email,MoreInfo) values(@DisplayName,@Address,@Phone,@Email,@MoreInfo);SELECT CAST(scope_identity() AS int)", DataProvider.Instance.DB))
                {
                    cmd.Parameters.AddWithValue("@DisplayName", (o as Customer).DisplayName);
                    cmd.Parameters.AddWithValue("@Address", (o as Customer).Address);
                    cmd.Parameters.AddWithValue("@Phone", (o as Customer).Phone);
                    cmd.Parameters.AddWithValue("@Email", (o as Customer).Email);
                    cmd.Parameters.AddWithValue("@MoreInfo", (o as Customer).MoreInfo);
                    (o as Suplier).Id = (int)cmd.ExecuteScalar();
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
                using (SqlCommand cmd = new SqlCommand("update Customer set " +
                    "DisplayName = @DisplayName," +
                    "Address = @Address," +
                    "Phone = @Phone," +
                    "Email = @Email," +
                    "MoreInfo = @MoreInfo" +
                    " where Id = @Id", DataProvider.Instance.DB))
                {
                    cmd.Parameters.Add("@DisplayName", SqlDbType.NVarChar);
                    cmd.Parameters["@DisplayName"].Value = (o as Customer).DisplayName;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar);
                    cmd.Parameters["@Address"].Value = (o as Customer).Address;
                    cmd.Parameters.Add("@Phone", SqlDbType.NVarChar);
                    cmd.Parameters["@Phone"].Value = (o as Customer).Phone;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                    cmd.Parameters["@Email"].Value = (o as Customer).Email;
                    cmd.Parameters.Add("@MoreInfo", SqlDbType.NVarChar);
                    cmd.Parameters["@MoreInfo"].Value = (o as Customer).MoreInfo;

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
                using (SqlCommand cmd = new SqlCommand("delete from Customer where Id = @Id", DataProvider.Instance.DB))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Value = (o as Customer).Id;

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
