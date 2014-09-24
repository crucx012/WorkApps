using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;

namespace WorkApplications.Models
{
    public class DbInteractor
    {
        private readonly DbContext _dataContext;

        public DbInteractor(DbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public int RunStoredProcedure(string spName, object paramObject)
        {
            using (var conn = new SqlConnection(_dataContext.Database.Connection.ConnectionString))
            using (var cmd = new SqlCommand(spName, conn) { CommandType = CommandType.StoredProcedure })
            {
                conn.Open();
                LoadParameters(cmd, paramObject);
                cmd.ExecuteNonQuery();
                return (int)cmd.Parameters["@RETURN_VALUE"].Value;
            }
        }

        private static void LoadParameters(SqlCommand cmd, object paramObject)
        {
            SqlCommandBuilder.DeriveParameters(cmd);

            foreach (SqlParameter p in cmd.Parameters)
                if (p.ParameterName != "@RETURN_VALUE")
                    GetParamValue(p, paramObject);
        }

        private static void GetParamValue(SqlParameter p, object paramObject)
        {
            var propertyInfo = paramObject.GetType().GetProperty(p.ParameterName.Substring(1));
            var value = propertyInfo.GetValue(paramObject, null);
            if (value != null) p.Value = value;
        }
    }
}