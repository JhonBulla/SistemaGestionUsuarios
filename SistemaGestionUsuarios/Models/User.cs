using System.ComponentModel.DataAnnotations;

namespace SistemaGestionUsuarios.Models
{
    /// <summary>
    /// Clase que representa la informacion de un usuario en el sistema.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        public string? Password { get; set; }
    }
}
