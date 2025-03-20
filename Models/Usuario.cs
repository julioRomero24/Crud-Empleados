using System;
using System.Collections.Generic;

namespace Crud.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool Estado { get; set; }

    public int Empleado { get; set; }

    public int Perfil { get; set; }

    public virtual Empleado EmpleadoNavigation { get; set; } = null!;

    public virtual Perfil PerfilNavigation { get; set; } = null!;
}
