namespace Crud.DTO_s
{
    public class EmpleadoDTO
    {
       // public int id { get; set; }

        public string Documento { get; set; } = null!;

        public int TipoDoc { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Titulo { get; set; } = null!;

        public double SalarioBasico { get; set; }

       public double SalarioHora { get; set; }

       public int Area { get; set; }

    }
}
