using System;
using System.Collections.Generic;

namespace Crud.Models;

public partial class Jornadum
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public string DesCorta { get; set; } = null!;

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
