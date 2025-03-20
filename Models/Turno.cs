using System;
using System.Collections.Generic;

namespace Crud.Models;

public partial class Turno
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public string DesCorta { get; set; } = null!;

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFinal { get; set; }

    public bool? Extra { get; set; }

    public int Jornada { get; set; }

    public int? Costo { get; set; }

    public virtual Jornadum JornadaNavigation { get; set; } = null!;

    public virtual ICollection<TurnoEmpleado> TurnoEmpleados { get; set; } = new List<TurnoEmpleado>();
}
