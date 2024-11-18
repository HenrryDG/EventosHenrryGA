using Microsoft.AspNetCore.Http;
using EventosHenrryGA.Entities;
using Microsoft.AspNetCore.Mvc;
using EventosHenrryGA.API.Models;

namespace EventosHenrryGA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEventoController : ControllerBase
    {
        private readonly BdeventosContext bd;

        public TipoEventoController(BdeventosContext _bd)
        {
            this.bd = _bd;
        }

        [HttpGet]
        public IActionResult GetTipoEventos()
        {
            try
            {
                var tipoEventos = bd.TipoEventos.Select(te=> new TipoEventoCLS
                {
                    Id = te.Id,
                    NombreTipo = te.NombreTipo
                }).ToList();

                return Ok(tipoEventos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
