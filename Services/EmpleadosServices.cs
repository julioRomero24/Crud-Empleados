using AutoMapper;
using Crud.Controllers;
using Crud.DTO_s;
using Crud.Models;
using Crud.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Crud.Services
{
    public class EmpleadosServices : IEmpleadosServices
    {
        private readonly NominaPreasyContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<EmpleadosServices> _logger;
        public EmpleadosServices(NominaPreasyContext context, IMapper mapper, ILogger<EmpleadosServices> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
                     
        }

        //sin autoMapper
        /*
        public async Task<EmpleadoDTO> GetEmpleadoById(int id)
        {
            try
            {
                var empleado = await _context.Empleados.FindAsync(id);


                // Manejar el caso en que no se encuentra el empleado
                if (empleado == null) return null;

                var empleadoDTO = new EmpleadoDTO {
                    Nombre = empleado.Nombre, 
                    Apellido = empleado.Apellido, 
                    Titulo = empleado.Titulo 
                };

                return empleadoDTO;

            }
            catch (Exception ex) {
                // Registro y re-lanzamiento de la excepción para que el controlador la capture
                _logger.LogError(ex, "Error en la capa de servicio al obtener el empleado con ID: {Id}", id);
                throw; // Relanzar la excepción para que sea manejada por el controlador

            }


    
        }*/

        //con AutoMapper
        
        public async Task<EmpleadoDTO> GetEmpleadoById(int id)
        {

            try
            {
                var empleado = await _context.Empleados.FindAsync(id);
                
                if(empleado==null) return  null;
                var empleadoDTO = _mapper.Map<EmpleadoDTO>(empleado);
                return empleadoDTO;
            }
            catch(Exception ex)
            {
                // Registro y re-lanzamiento de la excepción para que el controlador la capture
                _logger.LogError(ex, "Error en la capa de servicio al obtener el empleado con ID: {Id}", id);
                throw; // Relanzar la excepción para que sea manejada por el controlador


            }





        }

        //sin autoMapper
        /* public async Task<List<EmpleadoDTO>> GetEmpleados()
         {
             var empleados=await _context.Empleados.ToListAsync();
             var empleadoDTO = new List<EmpleadoDTO>();//lista DTO de retorno
             foreach (var em in empleados) //aqui se hace el mapeo de Empleado-> EmpleadoDTO
             { 
                 empleadoDTO.Add(new EmpleadoDTO { Nombre=em.Nombre, Apellido=em.Apellido, Titulo=em.Titulo }); 

             }
             return empleadoDTO;
         }*/

        //con AutoMapper
        public async Task<List<EmpleadoDTO>> GetEmpleados()
        {
            try
            {
                var empleados = await _context.Empleados.ToListAsync();
                if (empleados == null) return null;

                
                var empleadoDTO = _mapper.Map<List<EmpleadoDTO>>(empleados);//Aquí se hace el mapeo;

                return empleadoDTO;

            }
            catch (Exception ex)
            {
                // Registro y re-lanzamiento de la excepción para que el controlador la capture
                _logger.LogError(ex, "Error en la capa de servicio al obtener los empleados ");
                throw; // Relanzar la excepción para que sea manejada por el controlador
            }
            

        }
      

        public async Task<bool> EmpleadoExists(int id)
        {
            try
            {
                var empleado = await _context.Empleados.AnyAsync(x => x.Id == id);
                return empleado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"error en la capa de servicio al tratar de validar existencia de empleado");
                 throw;

            }

          

        }


        
        public async Task<CrearEmpleadoDTO> AddEmpleado([FromBody] CrearEmpleadoDTO emp)
        {
            if (emp == null) return null;
           
            try
            {
                var ingresarEmpleado = _mapper.Map<Empleado>(emp);///se hace el mapeo de emp que es CrearEmpleadoDTO a Empleado
                _logger.LogInformation("CrearEmpleado mapeado a Empleado");
                await _context.Empleados.AddAsync(ingresarEmpleado);
                await _context.SaveChangesAsync();
                return emp;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"se generó un problema en la capa de servicios al intentar accedeer a la base de datos");
                throw;
            }
            
            

              
        }

        public async Task<EmpleadoDTO> EliminarEmpleado(int id)
        {   
            try
            {
                var entidad = await _context.Empleados.FindAsync(id);
                if (entidad == null) return null;

                _context.Empleados.Remove(entidad);
                await _context.SaveChangesAsync();
                var empleadoEliminado = _mapper.Map<EmpleadoDTO>(entidad);

                return empleadoEliminado;


            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la capa de servicios al intentar conectarse con la base de datos");
                throw;

            }

        }


       public async Task<EmpleadoDTO> ActualizarEmpleado(int id, JsonPatchDocument<EmpleadoDTO> patchDocument)
        {
            if (patchDocument == null || id < 1)
            {
                _logger.LogWarning("El documento de parche es nulo o el ID es inválido. id: {Id}, patchDocument: {PatchDocument}", id, patchDocument);
                return null;
            }

            try
            {
                _logger.LogInformation("Buscando el empleado con ID: {Id}", id);
                var empleadoAntes = await _context.Empleados.FindAsync(id);
                if (empleadoAntes == null)
                {
                    _logger.LogWarning("No se encontró el empleado con ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Mapeando la entidad Empleado a un DTO.");
                var empleadoDTO = _mapper.Map<EmpleadoDTO>(empleadoAntes);
                if (empleadoDTO == null)
                {
                    _logger.LogWarning("El mapeo a EmpleadoDTO falló para el empleado con ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Aplicando el JsonPatchDocument al DTO.");
                patchDocument.ApplyTo(empleadoDTO);
               

                _logger.LogInformation("Mapeando los cambios del DTO de vuelta a la entidad.");
                _mapper.Map(empleadoDTO, empleadoAntes);

                _logger.LogInformation("Guardando los cambios en la base de datos.");
                await _context.SaveChangesAsync();

                _logger.LogInformation("Mapeando la entidad actualizada a un DTO para retornar.");
                var empleadoActualizado = _mapper.Map<EmpleadoDTO>(empleadoAntes);

                return empleadoActualizado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un problema en la capa de servicios intentando actualizar el empleado. Detalles del PatchDocument: {@patchDocument}", patchDocument);
                throw new Exception("Ocurrió un error al intentar actualizar el empleado. Detalles: " + ex.Message, ex);
            }
        }


        public async Task<EmpleadoDTO> ActualizarEmpleadoConPut(int id, EmpleadoDTO empleadoActualizar)
        {
            if (empleadoActualizar == null & id==0) return null;
            
            var empleado=await _context.Empleados.FindAsync(id);//empleado a actualizar
            if (empleado == null) return null;
            try
            {
                //var guardarEmpleado = _mapper.Map<Empleado>(empleadoActualizar);
                //var empleadoDTO=_mapper.Map<EmpleadoDTO>(empleado);

                //_mapper.Map(empleadoActualizar,empleadoDTO);// mapeo los datos nuevos del empleado
                _mapper.Map(empleadoActualizar, empleado);

                //_mapper.Map(empleadoDTO,empleado);
                _logger.LogInformation("->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> el empleado a actualizaar tiene esto {@empleadoActualizar}", empleadoActualizar);
                _logger.LogInformation("->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> el empleado tiene esto {@empleado}", empleado);
                await _context.SaveChangesAsync();

                var empleadoDTO = _mapper.Map<EmpleadoDTO>(empleado);

                return empleadoDTO;


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrio un problema en la capa de servicios intentando actualizar la entidad 'empleado'. detalles del empleado:{@empleadoActualizar}", empleadoActualizar);
                throw new Exception("Ocurrió un error al intentar actualizar el empleado. Detalles: " + ex.Message, ex);
            }
            
        }


    }
}
