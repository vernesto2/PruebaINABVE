using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class BeneficiosVeteranosRepository
    {
        private readonly string _connectionString;

        public BeneficiosVeteranosRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public async Task<List<BeneficiosVeteranos>> ListarActivos()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spBeneficiosVeteranos_ListarActivos", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<BeneficiosVeteranos>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapTopBeneficiosVeteranos(reader));
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<List<BeneficiosVeteranosDTO>> ObtenerPorVeterano(long IdVeterano)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spBeneficiosVeteranos_ObtenerPorVeterano", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdVeterano", IdVeterano));
                    var response = new List<BeneficiosVeteranosDTO>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapTopBeneficiosVeteranosDTO(reader));
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<BeneficiosVeteranos> ObtenerPorId(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spBeneficiosVeteranos_ObtenerPorId", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    BeneficiosVeteranos response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapTopBeneficiosVeteranos(reader);
                        }
                    }
                    return response;
                }
            }
        }

        public async Task Insertar(BeneficiosVeteranos beneficiosVeteranos)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spBeneficiosVeteranos_Insertar", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdBeneficio", beneficiosVeteranos.IdBeneficio));
                    cmd.Parameters.Add(new SqlParameter("@IdVeterano", beneficiosVeteranos.IdVeterano));
                    cmd.Parameters.Add(new SqlParameter("@FechaCreado", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@Activo", beneficiosVeteranos.Activo));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task Actualizar(BeneficiosVeteranos beneficiosVeteranos)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spBeneficiosVeteranos_Actualizar", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", beneficiosVeteranos.Id));
                    cmd.Parameters.Add(new SqlParameter("@IdBeneficio", beneficiosVeteranos.IdBeneficio));
                    cmd.Parameters.Add(new SqlParameter("@IdVeterano", beneficiosVeteranos.IdVeterano));
                    cmd.Parameters.Add(new SqlParameter("@FechaCreado", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@Activo", beneficiosVeteranos.Activo));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        private BeneficiosVeteranos MapTopBeneficiosVeteranos(SqlDataReader reader)
        {
            return new BeneficiosVeteranos()
            {
                Id = (long)reader["Id"],
                IdBeneficio = (long)reader["IdBeneficio"],
                IdVeterano = (long)reader["IdVeterano"],
                FechaCreado = (DateTime)reader["FechaCreado"],
                Activo = (bool)reader["Activo"]
            };
        }

        private BeneficiosVeteranosDTO MapTopBeneficiosVeteranosDTO(SqlDataReader reader)
        {
            return new BeneficiosVeteranosDTO()
            {
                Id = (long)reader["Id"],
                IdBeneficio = (long)reader["IdBeneficio"],
                IdVeterano = (long)reader["IdVeterano"],
                FechaCreado = (DateTime)reader["FechaCreado"],
                Activo = (bool)reader["Activo"],
                Nombre = reader["Nombre"].ToString()

            };
        }

        //public async Task Eliminar(int Id)
        //{
        //    using (SqlConnection sql = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("spBeneficiosVeteranos_Eliminar", sql))
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
