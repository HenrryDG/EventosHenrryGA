using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosHenrryGA.Entities
{
    public class EventoCLS
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Ubicacion { get; set; }
        public byte[]? ImagenEvento { get; set; }
        public string ImagenEventoBase64 { get; set; }
        public byte[]? ArchivoEvento { get; set; }

        public string ArchivoEventoBase64 { get; set; }
        public int? TipoEventoId { get; set; }
        public int? ClienteId { get; set; }

        public string? NombreCliente { get; set; }

        public string? NombreTipoEvento { get; set; }
    }
}
