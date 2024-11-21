using EventosHenrryGA.API.Models;
using Microsoft.AspNetCore.Http;
using EventosHenrryGA.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                var eventos = bd.Eventos
                    .Include(e => e.Cliente)  // Carga la relación con Cliente
                    .Include(e => e.TipoEvento)  // Si también quieres incluir el TipoEvento
                    .Select(e => new EventoCLS
                    {
                        Id = e.Id,
                        Nombre = e.Nombre,
                        Fecha = e.Fecha,
                        Descripcion = e.Descripcion,
                        Ubicacion = e.Ubicacion,
                        ImagenEventoBase64 = e.ImagenEvento != null ? Convert.ToBase64String(e.ImagenEvento) : null,
                        ArchivoEvento = e.ArchivoEvento,
                        TipoEventoId = e.TipoEventoId,
                        ClienteId = e.ClienteId,
                        NombreCliente = $"{e.Cliente.Nombre} {e.Cliente.ApellidoPaterno} {e.Cliente.ApellidoMaterno}",
                        NombreTipoEvento = e.TipoEvento.NombreTipo,
                    })
                    .ToList();

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("recuperarArchivo/{idEvento}")]
        public IActionResult GetArchivo(int idEvento)
        {
            try
            {
                var evento = bd.Eventos.Where(e => e.Id == idEvento).FirstOrDefault();
                if (evento == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(evento.ArchivoEvento);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
