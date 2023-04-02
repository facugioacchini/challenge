using challenge.Helpers;
using challenge.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net;

namespace challenge.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ActivitiesHelper activities;
        public UserController(IConfiguration iConfig)
        {
            _configuration = iConfig;
        }

        [HttpGet]
        [Route("GetUsers")]
        public List<Usuario> GetUsers()
        {
            List<Usuario> users = new List<Usuario>();
            try
            {
                string connectionString = _configuration.GetSection("ConnectionString").Value;
                string queryString = "SELECT id_usuario, nombre, apellido, email, fecha_nacimiento, telefono, pais, info FROM Usuarios";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        do
                        {
                            foreach (DbDataRecord s in reader)
                            {
                                Usuario u = new Usuario()
                                {
                                    Id = s.GetInt32(0),
                                    Nombre = s.GetString(1),
                                    Apellido = s.GetString(2),
                                    Email = s.GetString(3),
                                    FechaNacimiento = s.GetDateTime(4),
                                    Telefono = s.GetString(5),
                                    Pais = s.GetString(6),
                                    Info = s.GetBoolean(7),
                                };
                                users.Add(u);
                            }
                        }
                        while (reader.Read());
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
                return users;
            }
            catch
            {
                return users;
            }
        }

        [HttpGet]
        [Route("GetUser")]
        public Usuario GetUser(int id)
        {
            Usuario user = new Usuario();
            try
            {
                string connectionString = _configuration.GetSection("ConnectionString").Value;
                string queryString = "SELECT id_usuario, nombre, apellido, email, fecha_nacimiento, telefono, pais, info FROM Usuarios WHERE id_usuario = @id";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                                user = new Usuario()
                                {
                                    Id = (Int32)reader["id_usuario"],
                                    Nombre = (string)reader["nombre"],
                                    Apellido = (string)reader["apellido"],
                                    Email = (string)reader["email"],
                                    FechaNacimiento = (DateTime)reader["fecha_nacimiento"],
                                    Telefono = (string)reader["telefono"],
                                    Pais = (string)reader["pais"],
                                    Info = (bool)reader["info"],
                                };
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
                return user;
            }
            catch
            {
                return user;
            }
        }


        [HttpPost]
        [Route("SaveUser")]
        public HttpResponseMessage SaveUser([FromBody] Usuario user)
        {
            int Id = 0;
            try
            {
                string connectionString = _configuration.GetSection("ConnectionString").Value;
                string queryString = "INSERT INTO Usuarios OUTPUT Inserted.id_usuario VALUES (@nombre, @apellido, @email, @fechaNacimiento, @telefono, @pais, @info)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@nombre", user.Nombre);
                    command.Parameters.AddWithValue("@apellido", user.Apellido);
                    command.Parameters.AddWithValue("@email", user.Email);
                    command.Parameters.AddWithValue("@fechaNacimiento", user.FechaNacimiento);
                    command.Parameters.AddWithValue("@telefono", user.Telefono);
                    command.Parameters.AddWithValue("@pais", user.Pais);
                    command.Parameters.AddWithValue("@info", user.Info);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            Id = (Int32)reader["id_usuario"];
                        }
                    }
                    finally
                    {
                        reader.Close();
                        ActivitiesHelper activities = new ActivitiesHelper(_configuration);
                        activities.SaveActivities(Id, "Creación de Usuario");
                    }
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [Route("UpdateUser")]
        public HttpResponseMessage UpdateUser([FromBody] Usuario user)
        {
            try
            {
                string connectionString = _configuration.GetSection("ConnectionString").Value;
                string queryString = "UPDATE Usuarios SET nombre = @nombre, apellido = @apellido, email = @email, fecha_nacimiento = @fechaNacimiento, telefono = @telefono, pais = @pais, info = @info WHERE id_usuario = @id";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@id", user.Id);
                    command.Parameters.AddWithValue("@nombre", user.Nombre);
                    command.Parameters.AddWithValue("@apellido", user.Apellido);
                    command.Parameters.AddWithValue("@email", user.Email);
                    command.Parameters.AddWithValue("@fechaNacimiento", user.FechaNacimiento);
                    command.Parameters.AddWithValue("@telefono", user.Telefono);
                    command.Parameters.AddWithValue("@pais", user.Pais);
                    command.Parameters.AddWithValue("@info", user.Info);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(String.Format("Succes", reader));
                            activities.SaveActivities(user.Id, "Actualización de Usuario");
                        }
                    }
                    finally
                    {
                        reader.Close();
                        ActivitiesHelper activities = new ActivitiesHelper(_configuration);
                        activities.SaveActivities(user.Id, "Actualización de Usuario");
                    }
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public HttpResponseMessage DeleteUser(int id)
        {
            try
            {
                string connectionString = _configuration.GetSection("ConnectionString").Value;
                string queryString = "DELETE FROM Usuarios WHERE id_usuario = @id";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@id", id);
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
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
