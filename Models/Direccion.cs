using System;
using System.Collections.Generic;

namespace Crud.Models;

public partial class Direccion
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Jefe { get; set; }

    public virtual ICollection<Area> Areas { get; set; } = new List<Area>();
}
