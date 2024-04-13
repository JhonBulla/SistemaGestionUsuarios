using System.ComponentModel.DataAnnotations;

namespace SistemaGestionUsuarios.Models.DTO
{
    /// <summary>
    /// Clase que representa un objeto de transferencia de datos (DTO) para un usuario.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        [EmailAddress(ErrorMessage = "El campo Email no es una dirección de correo electrónico válida")]
        public string? Email { get; set; }
        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        public string? Password { get; set; }
    }
}
