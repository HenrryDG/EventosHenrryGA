using EventosHenrryGA.API.Models;
using Microsoft.AspNetCore.Http;
using EventosHenrryGA.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EventosHenrryGA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly BdeventosContext bd;

        public ClienteController(BdeventosContext _bd)
        {
            this.bd = _bd;
        }

        [HttpGet]
        public IActionResult GetClientes()
        {
            try
            {
                var clientes = bd.Clientes.Select(c => new ClienteCLS
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    ApellidoPaterno = c.ApellidoPaterno,
                    ApellidoMaterno = c.ApellidoMaterno
                }).ToList();

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
