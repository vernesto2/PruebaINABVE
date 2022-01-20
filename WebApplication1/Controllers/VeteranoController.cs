using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/veterano/")]
    [ApiController]
    public class VeteranoController : ControllerBase
    {
        private readonly VeteranoRepository _veteranoRepository;

        public VeteranoController(VeteranoRepository veteranoRepository)
        {
            _veteranoRepository = veteranoRepository ?? throw new ArgumentException(nameof(veteranoRepository));
        }
        // GET: api/<UsuarioController>
        [HttpGet("listar")]
        public async Task<List<Veterano>> ListarActivos ()
        {
            return await _veteranoRepository.ListarActivos();
        }

        // GET api/<UsuarioController>/5
        [HttpGet("obtenerporid/{id}")]
        public async Task<ActionResult<Veterano>> ObtenerPorId(int id)
        {
            var response = await _veteranoRepository.ObtenerPorId(id);
            if (response == null) { return NotFound(); }
            return response;
        }

        // POST api/<UsuarioController>
        [HttpPost("crear")]
        public async Task Insertar([FromBody] Veterano veterano)
        {
            await _veteranoRepository.Insertar(veterano);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("editar/{id}")]
        public async Task Actualizar([FromBody] Veterano veterano)
        {
            await _veteranoRepository.Actualizar(veterano);
        }

        //// DELETE api/<UsuarioController>/5
        //[HttpDelete("{id}")]
        //public async Task Eliminar(int id)
        //{
        //    await _veteranoRepository.Eliminar(id);
        //}
    }
}
