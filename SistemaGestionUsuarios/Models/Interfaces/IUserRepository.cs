using SistemaGestionUsuarios.Data;

namespace SistemaGestionUsuarios.Models.Interfaces
{
    /// <summary>
    /// Interfaz que define las operaciones disponibles para acceder y manipular los datos de usuarios en el repositorio.
    /// </summary>
    public interface IUserRepository
    {

        #region GetUsers
        /// <summary>
        /// Obtiene una lista de todos los usuarios en la base de datos.
        /// </summary>
        /// <returns>Lista de usuarios de la base de datos.</returns>
        public List<User> GetUsers();
        #endregion

        #region GetUserById
        /// <summary>
        /// Obtiene un usuario de la base de datos por el Id.
        /// </summary>
        /// <returns>Usuario de la base de datos.</returns>
        public User? GetUserById(int Id);
        #endregion

        #region AddUser
        /// <summary>
        /// Agrega a un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="userdata">Objeto con la informacion del usuario</param>
        /// <returns><c>True</c> si realiza la insercion de forma exitosa</returns>
        public bool AddUser(User userdata);
        #endregion

        #region UpdateUser
        /// <summary>
        /// Actualiza la informacion de un usuario en la base de datos.
        /// </summary>
        /// <param name="userdata">Objeto con la informacion del usuario</param>
        /// <returns><c>True</c> si realiza la actualizacion de forma exitosa</returns>
        public bool UpdateUser(User userdata);
        #endregion

        #region DeleteUser
        /// <summary>
        /// Elimina a un usuario de la base de datos.
        /// </summary>
        /// <param name="iduser">Identificador del usuario</param>
        /// <returns><c>True</c> si realiza la eliminacion de forma exitosa</returns>
        public bool DeleteUser(int iduser);
        #endregion

    }
}
