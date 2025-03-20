
# **Descripción Api**
Esta API permite gestionar empleados mediante operaciones CRUD (Crear, Leer, Actualizar y Eliminar). Se ha desarrollado utilizando **ASP.NET Core 8**, implementando el patrón de diseño **Inyección de Dependencias (DI)** para acceder a la base de datos de manera eficiente.

Se utiliza **Entity Framework** Core, un ORM (Object-Relational Mapper) que facilita la interacción con la base de datos a través de entidades y consultas LINQ. Además, AutoMapper se emplea para transformar las entidades del modelo en DTOs (Data Transfer Objects), lo que mejora la separación de capas y la seguridad al exponer solo los datos necesarios en las respuestas de la API.

La inyección de dependencias se usa para proporcionar el DbContext a los servicios y controladores, lo que mejora la modularidad y facilita la reutilización del código. Esto también permite realizar pruebas más fácilmente, ya que se puede sustituir el DbContext por una base de datos en memoria para pruebas unitarias.

**nota: Para acceder a esta api se debe clonar repositorio y abrir el archivo crud.sln**
