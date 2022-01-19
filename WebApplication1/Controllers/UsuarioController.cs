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
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentException(nameof(usuarioRepository));
        }
        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<List<Usuario>> Get()
        {
            return await _usuarioRepository.GetAll();
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var response = await _usuarioRepository.GetById(id);
            if (response == null) { return NotFound(); }
            return response;
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task Post([FromBody] Usuario usuario)
        {
            await _usuarioRepository.Insert(usuario);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public async Task Put([FromBody] Usuario usuario)
        {
            await _usuarioRepository.Put(usuario);
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _usuarioRepository.DeleteById(id);
        }
    }
}
