using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace challenge.Helpers
{
    public class ActivitiesHelper
    {
        private readonly IConfiguration _configuration;
        public ActivitiesHelper(IConfiguration iConfig)
        {
            _configuration = iConfig;
        }
        public void SaveActivities(int userId, string actividad)
        {
            DateTime date = DateTime.Now;
            try
            {
                string connectionString = _configuration.GetSection("ConnectionString").Value;
                string queryString = "INSERT INTO Actividades VALUES (@fechaCreacion, @userId, @actividad)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@fechaCreacion", date);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@actividad", actividad);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(String.Format("Succes", reader));
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
                return;
            }
            catch
            {
                return;
            }
        }
    }
}
