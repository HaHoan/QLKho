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
   public class Supliers : DbQuery
    {
        public object SqlReader { get; private set; }

        public override IEnumerable<object> Select()
        {
            var list = new List<Suplier>();
            try
            {
                string sql = "select * from Suplier";
                SqlCommand command = new SqlCommand(sql, DataProvider.Instance.DB);
              
                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            Suplier suplier = new Suplier();
                            suplier.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            suplier.DisplayName = reader[reader.GetOrdinal("DisplayName")] as string;
                            suplier.Address = reader[reader.GetOrdinal("Address")] as string;
                            suplier.Phone = reader[reader.GetOrdinal("Phone")] as string;
                            suplier.Email = reader[reader.GetOrdinal("Email")] as string;
                            suplier.MoreInfo = reader[reader.GetOrdinal("MoreInfo")] as string;
                            suplier.ContractDate = reader.GetDateTime(reader.GetOrdinal("ContractDate"));
                            list.Add(suplier);
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
                using (SqlCommand cmd = new SqlCommand("insert into Suplier(DisplayName,Address,Phone,Email,MoreInfo,ContractDate) values(@DisplayName,@Address,@Phone,@Email,@MoreInfo,@ContractDate);SELECT CAST(scope_identity() AS int)", DataProvider.Instance.DB))
                {
                    cmd.Parameters.AddWithValue("@DisplayName", (o as Suplier).DisplayName);
                    cmd.Parameters.AddWithValue("@Address", (o as Suplier).Address);
                    cmd.Parameters.AddWithValue("@Phone", (o as Suplier).Phone);
                    cmd.Parameters.AddWithValue("@Email", (o as Suplier).Email ?? "");
                    cmd.Parameters.AddWithValue("@MoreInfo", (o as Suplier).MoreInfo ?? "");
                    cmd.Parameters.AddWithValue("@ContractDate", (o as Suplier).ContractDate);
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
                using (SqlCommand cmd = new SqlCommand("update Suplier set " +
                    "DisplayName = @DisplayName," +
                    "Address = @Address," +
                    "Phone = @Phone," +
                    "Email = @Email," +
                    "MoreInfo = @MoreInfo," +
                    "ContractDate = @ContractDate" +
                    " where Id = @Id", DataProvider.Instance.DB))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Value = (o as Suplier).Id;
                    cmd.Parameters.Add("@DisplayName", SqlDbType.NVarChar);
                    cmd.Parameters["@DisplayName"].Value = (o as Suplier).DisplayName;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar);
                    cmd.Parameters["@Address"].Value = (o as Suplier).Address;
                    cmd.Parameters.Add("@Phone", SqlDbType.NVarChar);
                    cmd.Parameters["@Phone"].Value = (o as Suplier).Phone;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                    cmd.Parameters["@Email"].Value = (o as Suplier).Email;
                    cmd.Parameters.Add("@MoreInfo", SqlDbType.NVarChar);
                    cmd.Parameters["@MoreInfo"].Value = (o as Suplier).MoreInfo;
                    cmd.Parameters.Add("@ContractDate", SqlDbType.DateTime);
                    cmd.Parameters["@ContractDate"].Value = (o as Suplier).ContractDate;

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
                using (SqlCommand cmd = new SqlCommand("delete from Suplier where Id = @Id", DataProvider.Instance.DB))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Value = (o as Suplier).Id;

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
