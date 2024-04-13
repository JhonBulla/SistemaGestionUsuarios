using SistemaGestionUsuarios.Data;
using SistemaGestionUsuarios.Models.Interfaces;

namespace SistemaGestionUsuarios.Models.Repository
{
    /// <summary>
    /// Gestiona el acceso y la manipulación de los datos de los usuarios en la base de datos.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        #region Properties
        /// <summary>
        /// Contexto de la base de datos utilizada.
        /// </summary>
        private readonly ModelContext modelContext;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor que inicializa una nueva instancia de UserRepository.
        /// </summary>
        /// <param name="modelContext">Contexto de base de datos.</param>
        public UserRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }
        #endregion

        #region Methods

        #region GetUsers
        /// <summary>
        /// Obtiene una lista de todos los usuarios en la base de datos.
        /// </summary>
        /// <returns>Lista de usuarios de la base de datos.</returns>
        public List<User> GetUsers()
        {
            return modelContext.Users.ToList();
        }
        #endregion

        #region GetUserById
        /// <summary>
        /// Obtiene un usuario de la base de datos por el Id.
        /// </summary>
        /// <returns>Usuario de la base de datos.</returns>
        public User? GetUserById(int Id)
        {
            return modelContext.Users.ToList().FirstOrDefault(x => x.Id == Id);
        }
        #endregion

        #region AddUser
        /// <summary>
        /// Agrega a un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="userdata">Objeto con la informacion del usuario</param>
        /// <returns><c>True</c> si realiza la insercion de forma exitosa</returns>
        public bool AddUser(User userdata)
        {
            try
            {
                modelContext.Users.Add(userdata);
                modelContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region UpdateUser
        /// <summary>
        /// Actualiza la informacion de un usuario en la base de datos.
        /// </summary>
        /// <param name="userdata">Objeto con la informacion del usuario</param>
        /// <returns><c>True</c> si realiza la actualizacion de forma exitosa</returns>
        public bool UpdateUser(User userdata)
        {
            try
            {
                modelContext.Users.Update(userdata);
                modelContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region DeleteUser
        /// <summary>
        /// Elimina a un usuario de la base de datos.
        /// </summary>
        /// <param name="iduser">Identificador del usuario</param>
        /// <returns><c>True</c> si realiza la eliminacion de forma exitosa</returns>
        public bool DeleteUser(int iduser)
        {
            var user = modelContext.Users.Find(iduser);
            if (user != null)
            {
                try
                {
                    modelContext.Users.Remove(user);
                    modelContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
        #endregion

        #endregion
    }
}
