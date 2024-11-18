using System;
using System.Collections.Generic;

namespace EventosHenrryGA.API.Models;

public partial class TipoEvento
{
    public int Id { get; set; }

    public string? NombreTipo { get; set; }

    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
}
