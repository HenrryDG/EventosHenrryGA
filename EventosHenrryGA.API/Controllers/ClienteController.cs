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
                var clientes = bd.Clientes
                    .Where(c => c.Habilitado) // Filtra solo los clientes habilitados
                    .Select(c => new ClienteCLS
                    {
                        Id = c.Id,
                        Nombre = c.Nombre,
                        ApellidoPaterno = c.ApellidoPaterno,
                        ApellidoMaterno = c.ApellidoMaterno,
                    })
                    .ToList();

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCliente(int id)
        {
            try
            {
                var cliente = bd.Clientes
                    .Where(c => c.Id == id)
                    .Select(c => new ClienteCLS
                    {
                        Id = c.Id,
                        Nombre = c.Nombre,
                        ApellidoPaterno = c.ApellidoPaterno,
                        ApellidoMaterno = c.ApellidoMaterno,
                    })
                    .FirstOrDefault();

                if (cliente == null)
                {
                    return NotFound();
                }

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCliente(int id)
        {
            try
            {
                var cliente = bd.Clientes.Find(id);

                if (cliente == null)
                {
                    return NotFound();
                }


                cliente.Habilitado = false;
                bd.SaveChanges();
                return Ok( $"{cliente.Nombre} Eliminado lógicamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
