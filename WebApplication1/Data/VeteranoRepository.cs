using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class VeteranoRepository
    {
        private readonly string _connectionString;

        public VeteranoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public async Task<List<Veterano>> ListarActivos()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spVeterano_ListarActivos", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Veterano>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapTopVeterano(reader));
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<Veterano> ObtenerPorId(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spVeterano_ObtenerPorId", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    Veterano response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapTopVeterano(reader);
                        }
                    }
                    return response;
                }
            }
        }

        public async Task Insertar(Veterano veterano)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spVeterano_Insertar", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DUI", veterano.Dui));
                    cmd.Parameters.Add(new SqlParameter("@Carnet", veterano.Carnet));
                    cmd.Parameters.Add(new SqlParameter("@PrimerNombre", veterano.PrimerNombre));
                    cmd.Parameters.Add(new SqlParameter("@SegundoNombre", veterano.SegundoNombre));
                    cmd.Parameters.Add(new SqlParameter("@PrimerApellido", veterano.PrimerApellido));
                    cmd.Parameters.Add(new SqlParameter("@SegundoApellido", veterano.SegundoApellido));
                    cmd.Parameters.Add(new SqlParameter("@FechaCreado", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@Activo", veterano.Activo));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task Actualizar(Veterano veterano)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spVeterano_Actualizar", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", veterano.Id));
                    cmd.Parameters.Add(new SqlParameter("@DUI", veterano.Dui));
                    cmd.Parameters.Add(new SqlParameter("@Carnet", veterano.Carnet));
                    cmd.Parameters.Add(new SqlParameter("@PrimerNombre", veterano.PrimerNombre));
                    cmd.Parameters.Add(new SqlParameter("@SegundoNombre", veterano.SegundoNombre));
                    cmd.Parameters.Add(new SqlParameter("@PrimerApellido", veterano.PrimerApellido));
                    cmd.Parameters.Add(new SqlParameter("@SegundoApellido", veterano.SegundoApellido));
                    cmd.Parameters.Add(new SqlParameter("@FechaCreado", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@Activo", veterano.Activo));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        private Veterano MapTopVeterano(SqlDataReader reader)
        {
            return new Veterano()
            {
                Id = (long)reader["Id"],
                Dui = reader["DUI"].ToString(),
                Carnet = reader["Carnet"].ToString(),
                PrimerNombre = reader["PrimerNombre"].ToString(),
                SegundoNombre = reader["SegundoNombre"].ToString(),
                PrimerApellido = reader["PrimerApellido"].ToString(),
                SegundoApellido = reader["SegundoApellido"].ToString(),
                FechaCreado = (DateTime)reader["FechaCreado"],
                Activo = (bool)reader["Activo"]
            };
        }

        //public async Task Eliminar(int Id)
        //{
        //    using (SqlConnection sql = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("spVeterano_Eliminar", sql))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@Id", Id));
        //            await sql.OpenAsync();
        //            await cmd.ExecuteNonQueryAsync();
        //            return;
        //        }
        //    }
        //}
    }
}
