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
    public class Inputs : DbQuery
    {
        public override IEnumerable<object> Select()
        {
            var list = new List<Input>();
            try
            {
                string sql = "select * from Input";
                SqlCommand command = new SqlCommand(sql, DataProvider.Instance.DB);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            DateTime DateInput = reader.GetDateTime(reader.GetOrdinal("DateInput"));
                            list.Add(new Input() { Id = Id, DateInput = DateInput });
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
                using (SqlCommand cmd = new SqlCommand("insert into Input(DateInput) values(@DateInput);SELECT CAST(scope_identity() AS int)", DataProvider.Instance.DB))
                {
                    cmd.Parameters.AddWithValue("@DateInput", (o as Input).DateInput);
                    (o as Input).Id = (int)cmd.ExecuteScalar();
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
                using (SqlCommand cmd = new SqlCommand("update Input set DateInput = @DateInput where Id = @Id", DataProvider.Instance.DB))
                {
                    cmd.Parameters.Add("@DateInput", SqlDbType.NVarChar);
                    cmd.Parameters["@DateInput"].Value = (o as Input).DateInput;
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
                using (SqlCommand cmd = new SqlCommand("delete from Input where Id = @Id", DataProvider.Instance.DB))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Value = (o as Input).Id;

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

        public int IsHaveInputEmpty(DateTime date)
        {
            try
            {
                string sql = "select * from Input where id not in (select InputInfo.IdInput from InputInfo) and DateInput = @DateInput";
                SqlCommand command = new SqlCommand(sql, DataProvider.Instance.DB);
                command.Parameters.AddWithValue("@DateInput", date);
                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            return Id;
                        }

                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.Write(e.Message.ToString());
                return 0;
            }
            
        }
    }
}
