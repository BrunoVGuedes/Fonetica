using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Lider.DPVAT.APIFonetica.UI.Extension
{
    public class SqlConnectionHealthCheck : DbConnectionHealthCheck
    {
        private static readonly string DefaultTestQuery = "Select 1";
        public SqlConnectionHealthCheck(string connectionString)
                : this(connectionString, testQuery: DefaultTestQuery)
        {
        }
        public SqlConnectionHealthCheck(string connectionString, string testQuery)
                : base(connectionString, testQuery ?? DefaultTestQuery)
        {
        }
        protected override DbConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
