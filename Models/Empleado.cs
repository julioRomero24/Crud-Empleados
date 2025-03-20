using System;
using System.Collections.Generic;

namespace Crud.Models;

public partial class Empleado
{
    
    public int Id { get; set; }

    public string Documento { get; set; } = null!;

    public int TipoDoc { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Titulo { get; set; } = null!;

    public double SalarioBasico { get; set; }

    public double SalarioHora { get; set; }

    public int Area { get; set; }

    public virtual Area AreaNavigation { get; set; } = null!;

    public virtual TipoDocumento TipoDocNavigation { get; set; } = null!;

    public virtual ICollection<TurnoEmpleado> TurnoEmpleados { get; set; } = new List<TurnoEmpleado>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
