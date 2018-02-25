using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FireSafetyStore.Web.Client.Infrastructure.Common
{
    public class ADOHelper
    {
        private string ConnectionString;
        public ADOHelper(string connectionString)
        {
            ConnectionString = connectionString;
            
        }
        public DataTable ExecuteDataTable(string commandText, List<SqlParameter> parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand(commandText, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    parameters.ForEach(x => command.Parameters.Add(x));
                    adapter.Fill(dt);
                    command.Dispose();
                    conn.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }
    }
}