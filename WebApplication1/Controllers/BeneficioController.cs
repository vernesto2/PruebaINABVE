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
    [Route("api/beneficio/")]
    [ApiController]
    public class BeneficioController : ControllerBase
    {
        private readonly BeneficioRepository _beneficioRepository;

        public BeneficioController(BeneficioRepository BeneficioRepository)
        {
            _beneficioRepository = BeneficioRepository ?? throw new ArgumentException(nameof(BeneficioRepository));
        }
        // GET: api/<UsuarioController>
        [HttpGet("listaractivos")]
        public async Task<List<Beneficio>> ListarActivos ()
        {
            return await _beneficioRepository.ListarActivos();
        }

        // GET api/<UsuarioController>/5
        [HttpGet("obtenerporid/{id}")]
        public async Task<ActionResult<Beneficio>> ObtenerPorId(int id)
        {
            var response = await _beneficioRepository.ObtenerPorId(id);
            if (response == null) { return NotFound(); }
            return response;
        }

        // POST api/<UsuarioController>
        [HttpPost("insertar")]
        public async Task Insertar([FromBody] Beneficio beneficio)
        {
            await _beneficioRepository.Insertar(beneficio);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("actualizar/{id}")]
        public async Task Actualizar([FromBody] Beneficio beneficio)
        {
            await _beneficioRepository.Actualizar(beneficio);
        }

        //// DELETE api/<UsuarioController>/5
        //[HttpDelete("{id}")]
        //public async Task Eliminar(int id)
        //{
        //    await _beneficioRepository.Eliminar(id);
        //}
    }
}
