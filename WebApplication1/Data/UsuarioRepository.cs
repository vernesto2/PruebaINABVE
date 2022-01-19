using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public async Task<List<Usuario>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllUsuarios", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Usuario>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapTopUsuario(reader));
                        }
                    }
                    return response;
                }
            }
        }

        private Usuario MapTopUsuario(SqlDataReader reader)
        {
            return new Usuario()
            {
                Id = (int)reader["Id"],
                Correo = reader["Correo"].ToString(),
                //Clave = reader["Clave"].ToString(),
                Tipo = (int)reader["Tipo"]
            };
        }

        public async Task<Usuario> GetById(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetUsuarioById", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    Usuario response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapTopUsuario(reader);
                        }
                    }
                    return response;
                }
            }
        }

        public async Task Insert(Usuario usuario)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertUsuario", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Correo", usuario.Correo));
                    cmd.Parameters.Add(new SqlParameter("@Clave", usuario.Clave));
                    cmd.Parameters.Add(new SqlParameter("@Tipo", usuario.Tipo));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task Put(Usuario usuario)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateUsuario", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", usuario.Id));
                    cmd.Parameters.Add(new SqlParameter("@Correo", usuario.Correo));
                    cmd.Parameters.Add(new SqlParameter("@Clave", usuario.Clave));
                    cmd.Parameters.Add(new SqlParameter("@Tipo", usuario.Tipo));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task DeleteById(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteUsuario", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
    }
}
