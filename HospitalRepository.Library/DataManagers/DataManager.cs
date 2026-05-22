using System;
using System.Data;
using System.Data.SqlClient;

namespace HospitalRepository.Library.DataManagers
{
    public class DataManager
    {
        public static string ConnectionString()
        {
            // const string connectionString = @"Server=localhost; Database=HospitalBillingDb; User Id= sa; Password=sapassword; Trusted_Connection=False;";
            const string connectionString =
     @"Server=DESKTOP-SV3HLMJ\SQLEXPRESS;
      Database=HospitalBillingDb;
      Integrated Security=True;
      pooling=false;"; 
            return connectionString;


            //const string connectionString =
            //    "Data Source=SQL5026.site4now.net;Initial Catalog=DB_A61505_HospitalBilling;User Id=DB_A61505_HospitalBilling_admin;Password=Netsoft2020#;";
            //return connectionString;

            //return String.Format("Server=DESKTOP-25UCJ5C;Database=HospitalBillingDb;Trusted_Connection=True;");
            // return String.Format("Server=localhost;Database=HospitalBillingDb;User ID=sa;Password=sapassword;pooling=False;Trusted_Connection=False;");
        }

        /// <summary>
        /// ExecuteNonQuery for Insert, Update, Delete In Database
        /// </summary>
        /// <param name="query">Command Text</param>
        /// <param name="connectionString">Sql Connection String</param>
        /// <returns>Return 0 or 1</returns>
        public static int ExecuteNonQuery(string query, string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand(query, connection);
            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            return rowAffected;
        }
        public static int ExecuteNonQuerySP(string spName, SqlParameter[] parameters, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(spName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();

                    return command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// SqlDataReader for Read Data From Database
        /// </summary>
        /// <param name="query">Command Text</param>
        /// <param name="connectionString">Sql Connection String</param>
        /// <returns>Return Database Table Row Data</returns>
        public static SqlDataReader SqlDataReader(string query, string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand(query, connection);
            connection.Open();
            var reader = command.ExecuteReader();
            return reader;
        }

        /// <summary>
        /// DataTable for Read Data From Database
        /// </summary>
        /// <param name="query">Command Text</param>
        /// <param name="connectionString">Sql Connection String</param>
        /// <param name="tableName">Database Table Name Table Name</param>
        /// <returns></returns>
        public static DataTable DataTable(string query, string connectionString, string tableName)
        {
            var connection = new SqlConnection(connectionString);
            var dataAdapter = new SqlDataAdapter(query, connection);
            var dataSet = new DataSet();
            dataAdapter.Fill(dataSet, tableName);
            dataSet.Tables[0].TableName = tableName;
            return dataSet.Tables[0];
        }

        public static int Transaction(string connectionString, string query1, string query2)
        {
            var connection = new SqlConnection(connectionString);
            SqlTransaction aTransaction;
            connection.Open();
            aTransaction = connection.BeginTransaction();


            var command1 = new SqlCommand(query1, connection, aTransaction);
            command1.ExecuteNonQuery();

            command1 = new SqlCommand(query2, connection, aTransaction);
            int mstId = Convert.ToInt32(command1.ExecuteScalar());

            aTransaction.Commit();
            return mstId;
        }

        /// <summary>
        /// Get Auto Id Form Table Name 
        /// </summary>
        /// <param name="tableName">Auto Id Table Name</param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static string AutoId(string tableName, string connectionString)
        {
            string autoId = "";
            var connection = new SqlConnection(connectionString);

            string query = "SELECT TOP(1) Id FROM " + tableName + " ORDER BY Id DESC";

            var command = new SqlCommand(query, connection);
            connection.Open();

            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                autoId = reader["Id"].ToString();
            }
            if (autoId == "")
            {
                autoId = "0001";
            }
            else
            {
                int idIncrement = Convert.ToInt32(autoId);
                idIncrement++;
                autoId = "000" + idIncrement;
            }

            return autoId;
        }

        public static DataTable ExecuteQuery(string connectionString, string query, string Table)
        {
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter(query, myConnection))
                {
                    DataSet ds = new DataSet();
                    myAdapter.Fill(ds, Table);
                    ds.Tables[0].TableName = Table;
                    return ds.Tables[0];
                }
            }
        }
    }
}
