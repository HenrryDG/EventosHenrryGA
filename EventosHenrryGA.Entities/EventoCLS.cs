using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosHenrryGA.Entities
{
    public class EventoCLS
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del concierto es requerido")]
        [StringLength(100, ErrorMessage = "El nombre del concierto no debe exceder los 100 caracteres")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "La descripción del concierto es requerida")]
        [StringLength(200, ErrorMessage = "La descripción del concierto no debe exceder los 200 caracteres")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha del concierto es requerida")]
        public DateTime? Fecha { get; set; }

        [Required(ErrorMessage = "La ubicación del concierto es requerida")]
        public string? Ubicacion { get; set; }

        public byte[]? ImagenEvento { get; set; }
        public string? ImagenEventoBase64 { get; set; }
        public byte[]? ArchivoEvento { get; set; }

        public int? TipoEventoId { get; set; }
        public int? ClienteId { get; set; }

        public string? NombreCliente { get; set; }
        public string? NombreTipoEvento { get; set; }
    }

}
