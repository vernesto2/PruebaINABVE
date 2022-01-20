using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class BeneficioRepository
    {
        private readonly string _connectionString;

        public BeneficioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public async Task<List<Beneficio>> ListarActivos()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spBeneficio_ListarActivos", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Beneficio>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapTopBeneficio(reader));
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<Beneficio> ObtenerPorId(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spBeneficio_ObtenerPorId", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    Beneficio response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapTopBeneficio(reader);
                        }
                    }
                    return response;
                }
            }
        }

        public async Task Insertar(Beneficio beneficio)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spBeneficio_Insertar", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", beneficio.Nombre));
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", beneficio.Descripcion));
                    cmd.Parameters.Add(new SqlParameter("@FechaCreado", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@Activo", beneficio.Activo));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task Actualizar(Beneficio beneficio)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spBeneficio_Actualizar", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", beneficio.Id));
                    cmd.Parameters.Add(new SqlParameter("@Nombre", beneficio.Nombre));
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", beneficio.Descripcion));
                    cmd.Parameters.Add(new SqlParameter("@FechaCreado", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@Activo", beneficio.Activo));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        private Beneficio MapTopBeneficio(SqlDataReader reader)
        {
            return new Beneficio()
            {
                Id = (long)reader["Id"],
                Nombre = reader["Nombre"].ToString(),
                Descripcion = reader["Descripcion"].ToString(),
                FechaCreado = (DateTime)reader["FechaCreado"],
                Activo = (bool)reader["Activo"]
            };
        }

        //public async Task Eliminar(int Id)
        //{
        //    using (SqlConnection sql = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("spBeneficio_Eliminar", sql))
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
