using Microsoft.AspNetCore.Mvc;
using SistemaGestionUsuarios.Models;
using SistemaGestionUsuarios.Models.DTO;
using SistemaGestionUsuarios.Models.Interfaces;
using System.Net;

namespace SistemaGestionUsuariosWebApi.Controllers
{
    /// <summary>
    /// Representa el controlador de acciones para gestionar usuarios desde web api
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        #region Properties
        /// <summary>
        /// Instancia de acceso al repositorio de datos para la entidad User
        /// </summary>
        private IUserRepository userRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor que inicializa una nueva instancia de UserController.
        /// </summary>
        /// <param name="userRepository">Instancia de acceso al repositorio de usuarios</param>
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        #endregion

        #region Methods

        #region GET

        #region GetUsers

        /// <summary>
        /// Metodo GET que obtiene todos los usuarios.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista de todos los usuarios disponibles en el sistema.
        /// </remarks>
        /// <returns>
        /// Un objeto IActionResult que representa la respuesta HTTP.
        /// Si la operación se realiza correctamente, devuelve un código de estado 200 (OK) con la lista de usuarios en el cuerpo de la respuesta.
        /// Si el usuario no se encuentra, devuelve un código de estado 404 (No encontrado) con un mensaje de error en el cuerpo de la respuesta.
        /// Si se produce un error durante la operación, devuelve un código de estado 500 (Error interno del servidor) con un mensaje de error en el cuerpo de la respuesta.
        /// </returns>
        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var usersDto = userRepository.GetUsers()
                .Select(x => new UserDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Password = x.Password
                }).ToList();
                if (usersDto == null)
                {
                    return NotFound("No se encontraron usuarios");
                }
                return Ok(usersDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al obtener los usuarios: " + ex.Message);
            }
        }
        #endregion

        #region GetUserById

        /// <summary>
        /// Metodo GET que obtiene a un usuario por el Id.
        /// </summary>
        /// <remarks>
        /// Devuelve la informacion de un usuario del sistema.
        /// </remarks>
        /// <returns>
        /// Un objeto IActionResult que representa la respuesta HTTP.
        /// Si la operación se realiza correctamente, devuelve un código de estado 200 (OK) con la informacion del usuario en el cuerpo de la respuesta.
        /// Si el usuario no se encuentra, devuelve un código de estado 404 (No encontrado) con un mensaje de error en el cuerpo de la respuesta.
        /// Si se produce un error durante la operación, devuelve un código de estado 500 (Error interno del servidor) con un mensaje de error en el cuerpo de la respuesta.
        /// </returns>
        [HttpGet("{Id}")]
        public IActionResult GetUserById(int Id)
        {
            try
            {
                var user = userRepository.GetUserById(Id);
                if (user == null)
                {
                    return NotFound("No se encontró el usuario: " + Id);
                }
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password
                };
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al obtener al usuario: " + ex.Message);
            }
        }
        #endregion

        #endregion

        #region CreateUser

        /// <summary>
        /// Metodo POST para la creacion de un usuario nuevo.
        /// </summary>
        /// <remarks>
        /// Este método permite crear un usuario nuevo en el sistema.
        /// </remarks>
        /// <param name="userDto">Los datos del usuario a crear.</param>
        /// <returns>
        /// Un objeto IActionResult que representa la respuesta HTTP.
        /// Si la operación se realiza correctamente, devuelve un código de estado 200 (OK) con un mensaje de éxito en el cuerpo de la respuesta.
        /// Si se produce un error durante la operación, devuelve un código de estado 500 (Error interno del servidor) con un mensaje de error en el cuerpo de la respuesta.
        /// </returns>
        [HttpPost]
        public IActionResult CreateUser(UserDto userDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new User
                    {
                        Id = userDto.Id,
                        Name = userDto.Name,
                        Email = userDto.Email,
                        Password = userDto.Password
                    };

                    if (userRepository.AddUser(user))
                    {
                        return Ok("¡El usuario se creó exitosamente!");
                    }
                }
                return StatusCode(500, "Error al crear el usuario.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al crear el usuario: " + ex.Message);
            }
        }
        #endregion

        #region EditUser

        /// <summary>
        /// Metodo PUT que actualiza la información de un usuario existente.
        /// </summary>
        /// <remarks>
        /// Este método actualiza la información de un usuario existente en el sistema.
        /// </remarks>
        /// <param name="Id">El identificador único del usuario que se desea actualizar.</param>
        /// <param name="userDto">Los datos actualizados del usuario.</param>
        /// <returns>
        /// Un objeto IActionResult que representa la respuesta HTTP.
        /// Si la operación se realiza correctamente, devuelve un código de estado 200 (OK) con un mensaje de éxito en el cuerpo de la respuesta.
        /// Si el usuario no se encuentra, devuelve un código de estado 404 (No encontrado) con un mensaje de error en el cuerpo de la respuesta.
        /// Si se produce un error durante la operación, devuelve un código de estado 500 (Error interno del servidor) con un mensaje de error en el cuerpo de la respuesta.
        /// </returns>
        [HttpPut("{Id}")]
        public IActionResult EditUser(int Id, UserDto userDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = userRepository.GetUsers().FirstOrDefault(x => x.Id == Id);
                    if (user == null)
                    {
                        return NotFound("No se encontró el usuario: " + Id);
                    }

                    user.Name = userDto.Name;
                    user.Email = userDto.Email;
                    user.Password = userDto.Password;

                    if (userRepository.UpdateUser(user))
                    {
                        return Ok("¡El usuario se actualizó exitosamente!");
                    }
                }
                return StatusCode(500, "Error al actualizar el usuario: " + Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al actualizar el usuario: " + ex.Message);
            }
        }
        #endregion

        #region DeleteUser

        /// <summary>
        /// Metodo DELETE que elimina un usuario existente.
        /// </summary>
        /// <remarks>
        /// Este método elimina un usuario existente en el sistema.
        /// </remarks>
        /// <param name="Id">El identificador único del usuario que se desea eliminar.</param>
        /// <returns>
        /// Un objeto IActionResult que representa la respuesta HTTP.
        /// Si la operación se realiza correctamente, devuelve un código de estado 200 (OK) con un mensaje de éxito en el cuerpo de la respuesta.
        /// Si el usuario no se encuentra, devuelve un código de estado 404 (No encontrado) con un mensaje de error en el cuerpo de la respuesta.
        /// Si se produce un error durante la operación, devuelve un código de estado 500 (Error interno del servidor) con un mensaje de error en el cuerpo de la respuesta.
        /// </returns>
        [HttpDelete("{Id}")]
        public IActionResult DeleteUser(int Id)
        {
            try
            {
                var user = userRepository.GetUsers().FirstOrDefault(x => x.Id == Id);
                if (user == null)
                {
                    return NotFound("No se encontró el usuario: " + Id);
                }
                if (userRepository.DeleteUser(user.Id))
                {
                    return Ok("¡El usuario se eliminó exitosamente!");
                }
                return StatusCode(500, "Error al eliminar al usuario: " + Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar al usuario: " + ex.Message);
            }
        }
        #endregion

        #endregion
    }
}
