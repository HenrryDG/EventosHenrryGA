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
                 .Where(e => e.Habilitado)
                .Select(e => new EventoCLS
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Fecha = e.Fecha.Value.Date,
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


        [HttpGet("{id}")]
        public IActionResult GetEvento(int id)
        {
            try
            {
                var evento = bd.Eventos
                    .Include(e => e.Cliente)  // Carga la relación con Cliente
                    .Include(e => e.TipoEvento)  // Si también quieres incluir el TipoEvento
                    .Where(e => e.Id == id)
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
                    }).FirstOrDefault();

                if (evento == null)
                {
                    return NotFound();
                }
                return Ok(evento);
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

        [HttpDelete("{id}")]
        public IActionResult DeleteEvento(int id)
        {
            try
            {
                var evento = bd.Eventos.Where(e => e.Id == id).FirstOrDefault();

                if (evento == null)
                {
                    return NotFound();
                }
                else
                {
                    evento.Habilitado = false;
                    bd.SaveChanges();
                    return Ok($"{evento.Nombre} Eliminado Lógicamente");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult SaveEvento([FromBody] EventoCLS objEvento)
        {
            try
            {
                // Verificar si el objeto es nulo
                if (objEvento == null)
                {
                    return BadRequest("Los datos del evento no son válidos.");
                }

                if (objEvento.Id == 0) // Inserción de un nuevo evento
                {
                    var nuevoEvento = new Evento
                    {
                        Nombre = objEvento.Nombre,
                        Fecha = objEvento.Fecha,
                        Descripcion = objEvento.Descripcion,
                        Ubicacion = objEvento.Ubicacion,
                        ImagenEvento = string.IsNullOrEmpty(objEvento.ImagenEventoBase64) ? null : Convert.FromBase64String(objEvento.ImagenEventoBase64),
                        ArchivoEvento = objEvento.ArchivoEvento, // No es necesario convertir el archivo, ya lo recibes como byte[]
                        TipoEventoId = objEvento.TipoEventoId,
                        ClienteId = objEvento.ClienteId
                    };

                    bd.Eventos.Add(nuevoEvento);
                }
                else // Actualización de un evento existente
                {
                    var eventoExistente = bd.Eventos.FirstOrDefault(e => e.Id == objEvento.Id);

                    if (eventoExistente == null)
                    {
                        return NotFound("Evento no encontrado.");
                    }

                    eventoExistente.Nombre = objEvento.Nombre;
                    eventoExistente.Fecha = objEvento.Fecha;
                    eventoExistente.Descripcion = objEvento.Descripcion;
                    eventoExistente.Ubicacion = objEvento.Ubicacion;
                    eventoExistente.ImagenEvento = string.IsNullOrEmpty(objEvento.ImagenEventoBase64) ? eventoExistente.ImagenEvento : Convert.FromBase64String(objEvento.ImagenEventoBase64);
                    eventoExistente.ArchivoEvento = objEvento.ArchivoEvento ?? eventoExistente.ArchivoEvento; // Mantener el archivo actual si no se cambia
                    eventoExistente.TipoEventoId = objEvento.TipoEventoId;
                    eventoExistente.ClienteId = objEvento.ClienteId;
                }

                bd.SaveChanges();

                return Ok(new { message = "Evento guardado exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error: {ex.Message}");
            }
        }


    }
}
