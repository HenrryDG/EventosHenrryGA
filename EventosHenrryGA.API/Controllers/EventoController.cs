using EventosHenrryGA.API.Models;
using Microsoft.AspNetCore.Http;
using EventosHenrryGA.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EventosHenrryGA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly BdeventosContext bd;

        public EventoController(BdeventosContext _bd)
        {
            this.bd = _bd;
        }

        [HttpGet]
        public IActionResult GetEventos()
        {
            try
            {
                var eventos = bd.Eventos.Select(e => new EventoCLS 
                { 
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Fecha = e.Fecha,
                    Descripcion = e.Descripcion,
                    Ubicacion = e.Ubicacion,
                    ImagenEvento = e.ImagenEvento,
                    ArchivoEvento = e.ArchivoEvento,
                    TipoEventoId = e.TipoEventoId,
                    ClienteId = e.ClienteId
                    }).ToList();

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
