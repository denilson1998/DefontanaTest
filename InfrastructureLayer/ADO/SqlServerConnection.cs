using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.ADO
{
    public class SqlServerConnection
    {
        private string propConnectionString { get; set; }

        public SqlServerConnection()
        {
            string connectionString = "Server=(HOST);Database=(DB);User Id=(USERNAME);password=(PASSWORD);Trusted_Connection=False;MultipleActiveResultSets=true;Connection Timeout=18000;";
            
            connectionString = connectionString.Replace("(HOST)", "lab-defontana.caporvnn6sbh.us-east-1.rds.amazonaws.com");
            
            connectionString = connectionString.Replace("(DB)", "Prueba");
            
            connectionString = connectionString.Replace("(USERNAME)", "ReadOnly");
            
            connectionString = connectionString.Replace("(PASSWORD)", "d*3PSf2MmRX9vJtA5sgwSphCVQ26*T53uU");
            
            this.propConnectionString = connectionString;
        }

        public SqlConnection prepareConnection()
        {
            try
            {
                SqlConnection objSqlConnection = new SqlConnection(this.propConnectionString);
                return objSqlConnection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
