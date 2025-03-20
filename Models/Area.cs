using System;
using System.Collections.Generic;

namespace Crud.Models;

public partial class Area
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Direccion { get; set; }

    public int? Jefe { get; set; }

    public virtual Direccion DireccionNavigation { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
