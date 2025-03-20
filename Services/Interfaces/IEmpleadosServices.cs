using Crud.DTO_s;
using Crud.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Services.Interfaces
{
    public interface IEmpleadosServices
    {   //Task=metodo asincrono
        Task<EmpleadoDTO> GetEmpleadoById(int id);
        Task<List<EmpleadoDTO>> GetEmpleados();
        Task<bool> EmpleadoExists(int id);
        Task<CrearEmpleadoDTO> AddEmpleado([FromBody] CrearEmpleadoDTO empleado);
        Task<EmpleadoDTO> EliminarEmpleado(int id);
       Task<EmpleadoDTO> ActualizarEmpleado(int id, [FromBody] JsonPatchDocument<EmpleadoDTO> patchDocument);//actualizacion parcial con patch
       Task<EmpleadoDTO> ActualizarEmpleadoConPut(int id, [FromBody] EmpleadoDTO empleado);



    }
}
