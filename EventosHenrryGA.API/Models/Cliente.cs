using System;
using System.Collections.Generic;

namespace EventosHenrryGA.API.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public bool Habilitado { get; set; }

    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
}
