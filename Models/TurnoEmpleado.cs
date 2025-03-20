using System;
using System.Collections.Generic;

namespace Crud.Models;

public partial class TurnoEmpleado
{
    public int Id { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFinal { get; set; }

    public int Turno { get; set; }

    public int Empleado { get; set; }

    public virtual Empleado EmpleadoNavigation { get; set; } = null!;

    public virtual Turno TurnoNavigation { get; set; } = null!;
}
