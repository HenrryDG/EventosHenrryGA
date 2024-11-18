using System;
using System.Collections.Generic;

namespace EventosHenrryGA.API.Models;

public partial class Evento
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Ubicacion { get; set; }

    public byte[]? ImagenEvento { get; set; }

    public byte[]? ArchivoEvento { get; set; }

    public int? TipoEventoId { get; set; }

    public int? ClienteId { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual TipoEvento? TipoEvento { get; set; }
}
