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
    [Route("api/beneficiosveteranos/")]
    [ApiController]
    public class BeneficiosVeteranosController : ControllerBase
    {
        private readonly BeneficiosVeteranosRepository _beneficiosVeteranosRepository;

        public BeneficiosVeteranosController(BeneficiosVeteranosRepository BeneficiosVeteranosRepository)
        {
            _beneficiosVeteranosRepository = BeneficiosVeteranosRepository ?? throw new ArgumentException(nameof(BeneficiosVeteranosRepository));
        }
        // GET: api/<UsuarioController>
        [HttpGet("listaractivos")]
        public async Task<List<BeneficiosVeteranos>> ListarActivos ()
        {
            return await _beneficiosVeteranosRepository.ListarActivos();
        }

        // GET api/<UsuarioController>/5
        [HttpGet("obtenerporid/{id}")]
        public async Task<ActionResult<BeneficiosVeteranos>> ObtenerPorId(int id)
        {
            var response = await _beneficiosVeteranosRepository.ObtenerPorId(id);
            if (response == null) { return NotFound(); }
            return response;
        }

        // POST api/<UsuarioController>
        [HttpPost("insertar")]
        public async Task Insertar([FromBody] BeneficiosVeteranos BeneficiosVeteranos)
        {
            await _beneficiosVeteranosRepository.Insertar(BeneficiosVeteranos);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("actualizar/{id}")]
        public async Task Actualizar([FromBody] BeneficiosVeteranos BeneficiosVeteranos)
        {
            await _beneficiosVeteranosRepository.Actualizar(BeneficiosVeteranos);
        }

        //// DELETE api/<UsuarioController>/5
        //[HttpDelete("{id}")]
        //public async Task Eliminar(int id)
        //{
        //    await _BeneficiosVeteranosRepository.Eliminar(id);
        //}
    }
}
