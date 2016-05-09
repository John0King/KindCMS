using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace KindCMS.Utility
{
    public class SqlHelper
    {
        public static SqlConnection DbConn()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "";
            conn.Open();
            return conn;
        }
        static void ExecuteNoneQuery(string sql)
        {
            var Conn = DbConn();
            var Trans = Conn.BeginTransaction();
            try
            {
                var cmd = Conn.CreateCommand();
                cmd.Transaction = Trans;
                cmd.Connection = Conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql;
                
                cmd.ExecuteNonQuery();
                Trans.Commit();
            }
            catch
            {
                Trans.Rollback();
            }
        }
        static SqlDataReader Excute(string sql)
        {
            var Conn = DbConn();
           
            var cmd = Conn.CreateCommand();
            cmd.Connection = Conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sql;

            var r = cmd.ExecuteReader();
            return r;
          
        }
        public long Update(string sql,Dictionary<string,object> param)
        {
            var Conn = DbConn();
            long Afc = 0;
            var Trans = Conn.BeginTransaction();
            try
            {
                var cmd = Conn.CreateCommand();
                cmd.Transaction = Trans;
                cmd.Connection = Conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(GetList(param));
                Afc= (long)cmd.ExecuteScalar();
                Trans.Commit();
            }
            catch
            {
                Trans.Rollback();
            }
            return Afc;
        }

        public long Delete(string sql)
        {
            var Conn = DbConn();
            long Afc = 0;
            var Trans = Conn.BeginTransaction();
            try
            {
                var cmd = Conn.CreateCommand();
                cmd.Transaction = Trans;
                cmd.Connection = Conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql;
                Afc = (long)cmd.ExecuteScalar();
                Trans.Commit();
            }
            catch
            {
                Trans.Rollback();
            }
            return Afc;
        }

        private SqlParameter[] GetList(Dictionary<string,object> param)
        {
            var List = new List<SqlParameter>();
            if (param == null)
            {
                return List.ToArray();
            }
            foreach(var item in param)
            {
                SqlParameter sp = new SqlParameter();
                sp.ParameterName = item.Key;
                sp.Value = item.Value;
                if(item.Value is string)
                {
                    sp.SqlDbType = System.Data.SqlDbType.NVarChar;
                }


                List.Add(sp);
            }
            return List.ToArray();
        }
    }
}
