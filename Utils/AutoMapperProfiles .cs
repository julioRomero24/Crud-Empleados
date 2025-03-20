using AutoMapper;
using Crud.DTO_s;
using Crud.Models;

namespace Crud.Utils
{
    public class AutoMapperProfiles : Profile
    {
      
        public AutoMapperProfiles()
        {
            // Definir los mapeos entre tus entidades y DTOs CreateMap<Empleado, EmpleadoDTO>(); CreateMap<CrearActualizarEmpleadoDTO, Empleado>();
            CreateMap<Empleado, EmpleadoDTO>();// mapea desde Empleado hacia EmpleadoDTO y viceversa
            //CreateMap<CrearActualizarEmpleadoDTO, Empleado>();
            CreateMap<CrearEmpleadoDTO, Empleado>();
            CreateMap<EmpleadoDTO, Empleado>();

        }

    }
}
