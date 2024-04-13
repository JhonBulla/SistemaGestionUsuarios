using Microsoft.AspNetCore.Mvc;
using SistemaGestionUsuarios.Models;
using SistemaGestionUsuarios.Models.DTO;
using SistemaGestionUsuarios.Models.Interfaces;
using SistemaGestionUsuarios.Models.Repository;

namespace SistemaGestionUsuarios.Controllers
{
    /// <summary>
    /// Representa el controlador de acciones para gestionar usuarios
    /// </summary>
    public class UserController : Controller
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

        #region GetUsers

        /// <summary>
        /// Acción que devuelve la vista principal de usuarios.
        /// </summary>
        /// <remarks>
        /// Esta acción obtiene todos los usuarios del repositorio de usuarios,
        /// los convierte en objetos UserDto y los envía a la vista principal para su visualización.
        /// </remarks>
        /// <returns>La vista principal de usuarios con la lista de usuarios a traves de UserDto.</returns>
        public IActionResult Index()
        {
            var usersDto = userRepository.GetUsers()
                .Select(x => new UserDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Password = x.Password
                }).ToList();
            return View(usersDto);
        }
        #endregion

        #region AddUser

        /// <summary>
        /// Acción GET para mostrar el formulario de creación de un nuevo usuario.
        /// </summary>
        /// <remarks>
        /// Esta acción se utiliza para mostrar el formulario de creación de un nuevo usuario.
        /// </remarks>
        /// <returns>La vista de formulario de creación de usuario.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Acción HTTP POST para agregar un nuevo usuario.
        /// </summary>
        /// <remarks>
        /// Esta acción se utiliza para procesar la solicitud de agregar un nuevo usuario al sistema.
        /// Se espera que el modelo de datos enviado desde el formulario sea válido y que cumpla con las reglas de validación definidas en el modelo UserDto.
        /// Se utiliza el atributo [ValidateAntiForgeryToken] para proteger la acción contra ataques de falsificación de solicitudes entre sitios (CSRF).
        /// Si la creación del usuario se realiza correctamente, la acción redirecciona a la vista principal de usuarios.
        /// Si la creación del usuario no se realiza correctamente, se muestra un mensaje de error en la vista de formulario de agregar usuario.
        /// </remarks>
        /// <param name="userDto">Objeto UserDto que contiene los datos del nuevo usuario.</param>
        /// <returns>
        /// Si la creación del usuario se realiza correctamente, la acción redirecciona a la vista principal de usuarios.
        /// Si la creación del usuario no se realiza correctamente, se devuelve la vista de formulario de agregar usuario con un mensaje de error.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    Password = userDto.Password
                };
                if (userRepository.AddUser(user))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["ErrorMessage"] = "Error al crear el usuario.";
                }
            }
            return View(userDto);
        }
        #endregion

        #region UpdateUser

        /// <summary>
        /// Acción GET para mostrar el formulario de actualización de usuario.
        /// </summary>
        /// <remarks>
        /// Esta acción se utiliza para mostrar el formulario de actualización de usuario, donde los usuarios pueden editar la información de un usuario específico.
        /// Se espera que el parámetro 'iduser' represente el identificador único del usuario que se desea actualizar.
        /// Si no se encuentra ningún usuario con el ID proporcionado, la acción devuelve un resultado HTTP 404 (Not Found).
        /// </remarks>
        /// <param name="Id">El identificador único del usuario que se desea actualizar.</param>
        /// <returns>
        /// Si se encuentra el usuario con el ID proporcionado, la acción devuelve la vista de formulario de actualización de usuario con los datos del usuario cargados.
        /// Si no se encuentra ningún usuario con el ID proporcionado, la acción devuelve un resultado HTTP 404 (Not Found).
        /// </returns>
        public IActionResult Edit(int Id)
        {
            var user = userRepository.GetUsers().FirstOrDefault(x => x.Id == Id);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };
            return View(userDto);
        }

        /// <summary>
        /// Acción HTTP POST para actualizar la información de un usuario.
        /// </summary>
        /// <remarks>
        /// Esta acción se utiliza para procesar la solicitud de actualización de la información de un usuario en el sistema.
        /// Se espera que el modelo de datos enviado desde el formulario sea válido y que cumpla con las reglas de validación definidas en el modelo UserDto.
        /// Se utiliza el atributo [ValidateAntiForgeryToken] para proteger la acción contra ataques de falsificación de solicitudes entre sitios (CSRF).
        /// Si la actualización del usuario se realiza correctamente, la acción redirecciona a la vista principal de usuarios.
        /// Si la actualización del usuario no se realiza correctamente, se muestra un mensaje de error en la vista de formulario de actualización de usuario.
        /// </remarks>
        /// <param name="userDto">Objeto UserDto que contiene los datos actualizados del usuario.</param>
        /// <returns>
        /// Si la actualización del usuario se realiza correctamente, la acción redirecciona a la vista principal de usuarios.
        /// Si la actualización del usuario no se realiza correctamente, se devuelve la vista de formulario de actualización de usuario con un mensaje de error.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserDto userDto)
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
                if (userRepository.UpdateUser(user))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["ErrorMessage"] = "Error al actualizar el usuario.";
                }
            }
            return View(userDto);
        }
        #endregion

        #region DeleteUser

        /// <summary>
        /// Acción GET para mostrar el formulario de eliminación de un usuario.
        /// </summary>
        /// <remarks>
        /// Esta acción se utiliza para mostrar el formulario de confirmación de eliminación de un usuario específico.
        /// Si no se encuentra ningún usuario con el ID proporcionado, la acción devuelve un resultado HTTP 404 (Not Found).
        /// </remarks>
        /// <param name="Id">El identificador único del usuario que se desea eliminar.</param>
        /// <returns>
        /// Si se encuentra el usuario con el ID proporcionado, la acción devuelve la vista de formulario de confirmación de eliminación de usuario con los datos del usuario cargados.
        /// Si no se encuentra ningún usuario con el ID proporcionado, la acción devuelve un resultado HTTP 404 (Not Found).
        /// </returns>
        public IActionResult Delete(int Id)
        {
            var user = userRepository.GetUsers().FirstOrDefault(x => x.Id == Id);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };
            return View(userDto);
        }

        /// <summary>
        /// Acción HTTP POST para eliminar un usuario existente.
        /// </summary>
        /// <remarks>
        /// Esta acción se utiliza para procesar la solicitud de eliminación de un usuario existente en el sistema.
        /// Se espera que el parámetro 'idUser' represente el identificador único del usuario que se desea eliminar.
        /// Se utiliza el atributo [ValidateAntiForgeryToken] para proteger la acción contra ataques de falsificación de solicitudes entre sitios (CSRF).
        /// Si la eliminación del usuario se realiza correctamente, la acción redirecciona a la vista principal de usuarios.
        /// Si la eliminación del usuario no se realiza correctamente, se muestra un mensaje de error en la vista de la acción actual.
        /// </remarks>
        /// <param name="Id">El identificador único del usuario que se desea eliminar.</param>
        /// <returns>
        /// Si la eliminación del usuario se realiza correctamente, la acción redirecciona a la vista principal de usuarios.
        /// Si la eliminación del usuario no se realiza correctamente, se devuelve la vista de la acción actual con un mensaje de error.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int Id)
        {
            if (userRepository.DeleteUser(Id))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["ErrorMessage"] = "Error al eliminar el usuario.";
                return View();
            }
        }
        #endregion

        #endregion
    }
}
