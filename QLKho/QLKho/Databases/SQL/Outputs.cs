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
    public class Outputs : DbQuery
    {
        public override IEnumerable<object> Select()
        {
            var list = new List<Output>();
            try
            {
                string sql = "select * from Outputs";
                SqlCommand command = new SqlCommand(sql, DataProvider.Instance.DB);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int idIndex = reader.GetOrdinal("Id");
                            int Id = reader.GetInt32(idIndex);
                            DateTime DateOutput = reader.GetDateTime(reader.GetOrdinal("DateOutput"));
                            list.Add(new Output() { Id = Id, DateOutput = DateOutput });
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
                using (SqlCommand cmd = new SqlCommand("insert into Outputs(DateOutput) values(@DateOutput);SELECT CAST(scope_identity() AS int)", DataProvider.Instance.DB))
                {
                    cmd.Parameters.AddWithValue("@DateOutput", (o as Output).DateOutput);
                    (o as Output).Id = (int)cmd.ExecuteScalar();
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
                using (SqlCommand cmd = new SqlCommand("update Outputs set DateOutput = @DateOutput where Id = @Id", DataProvider.Instance.DB))
                {
                    cmd.Parameters.Add("@DateOutput", SqlDbType.DateTime);
                    cmd.Parameters["@DateOutput"].Value = (o as Output).DateOutput;
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
                using (SqlCommand cmd = new SqlCommand("delete from Outputs where Id = @Id", DataProvider.Instance.DB))
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
