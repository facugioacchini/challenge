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
    [Route("Activities")]
    public class ActivitiesController : Controller
    {
        private readonly IConfiguration _configuration;
        public ActivitiesController(IConfiguration iConfig)
        {
            _configuration = iConfig;
        }

        [HttpGet]
        [Route("GetActivities")]
        public List<Actividades> GetActivities()
        {
            List<Actividades> actividades = new List<Actividades>();
            try
            {
                string connectionString = _configuration.GetSection("ConnectionString").Value;
                string queryString = "SELECT id_actividad, fecha_alta,(nombre + ' ' + apellido) as nombre, actividad FROM Actividades a INNER JOIN Usuarios u ON u.id_usuario = a.id_usuario ORDER BY id_actividad DESC";

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
                                Actividades a = new Actividades()
                                {
                                    Id = s.GetInt32(0),
                                    FechaCreacion = s.GetDateTime(1),
                                    NombreUsuario = s.GetString(2),
                                    Detalle = s.GetString(3)
                                };
                                actividades.Add(a);
                            }
                        }  
                        while (reader.Read());
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
                return actividades;
            }
            catch
            {
                return actividades;
            }
        }
    }
}
