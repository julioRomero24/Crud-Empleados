using System;
using System.Collections.Generic;

namespace Crud.Models;

public partial class TipoDocumento
{
    public int Id { get; set; }

    public string Documento { get; set; } = null!;

    public string DesCorta { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
