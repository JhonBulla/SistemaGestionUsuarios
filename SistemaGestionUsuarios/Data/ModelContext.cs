using Microsoft.EntityFrameworkCore;
using SistemaGestionUsuarios.Models;
using SistemaGestionUsuarios.Models.DTO;

namespace SistemaGestionUsuarios.Data
{
    /// <summary>
    /// Contexto de base de datos para el sistema de gestión de usuarios.
    /// </summary>
    public class ModelContext : DbContext
    {
        /// <summary>
        /// Constructor de la clase ModelContext.
        /// </summary>
        /// <param name="options">Opciones de configuración del contexto de base de datos.</param>
        public ModelContext(DbContextOptions<ModelContext> options) : base(options)
        {
        }

        /// <summary>
        /// Conjunto de entidades de usuarios en la base de datos.
        /// </summary>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Representa el conjunto de entidades de tipo UserDto en el contexto de la base de datos.
        /// </summary>
        public DbSet<SistemaGestionUsuarios.Models.DTO.UserDto> UserDto { get; set; } = default!;
    }
}
