using NorthwindBasedWebAPI.Data;
using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Repositories.Base;
using NorthwindBasedWebAPI.Repositories.IRepository;
using System.Data.SqlClient;

namespace NorthwindBasedWebAPI.Repositories.Repository
{
    public class LogRepository : ILogRepository
    {
        public List<SystemLog> GetAll()
        {

            List<SystemLog> systemLogs = new List<SystemLog>();

            SqlConnection conn = new SqlConnection("Server=ABDULHAMIT;Database=northwind-based-web-api;Trusted_Connection=True;");

            string query = "SELECT * FROM SystemLogs";

            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    systemLogs.Add(new SystemLog
                    {
                        Id = Convert.ToInt32(reader["ID"]),
                        Message = Convert.ToString(reader["Message"]),
                        Level = Convert.ToString(reader["Level"]),
                        Timestamp = Convert.ToDateTime(reader["TimeStamp"]),
                        MethodType = Convert.ToString(reader["MethodType"]),
                        User = Convert.ToString(reader["User"]),
                        Role = Convert.ToString(reader["Role"]),
                        Details = Convert.ToString(reader["Details"]),
                        StatusCode = Convert.ToString(reader["StatusCode"]),
                        Success = Convert.ToBoolean(reader["Success"]),
                        Failed = Convert.ToBoolean(reader["Failed"]),
                        ErrorMessage = Convert.ToString(reader["ErrorMessage"]),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                    });
                }

                reader.Close();
                conn.Close();
            }
            catch(Exception ex)
            {
                conn.Close();
            }

            return systemLogs;
        }
    }
}
