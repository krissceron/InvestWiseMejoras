using InvestWiseProyecto.Model;
using InvestWiseProyecto.Repository;

namespace InvestWiseProyecto.Service
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public Respuesta CrearUsuario(Usuario usuario)
        {
            // Aquí puedes agregar lógica adicional, como validaciones
            return _usuarioRepository.InsertarUsuario(usuario);
        }

        public Respuesta ObtenerUsuarios()
        {
            return _usuarioRepository.ObtenerUsuario();
        }

        public Respuesta ObtenerUsuarioPorId(int idUsuario)
        {
            return _usuarioRepository.ObtenerUsuarioPorId(idUsuario);
        }

        public Respuesta ActualizarUsuario(UsuarioModificado usuarioModi)
        {
            return _usuarioRepository.ActualizarUsuario(usuarioModi);
        }

        public Respuesta EliminarUsuario(int idUsuario)
        {
            return _usuarioRepository.EliminarUsuario(idUsuario);
        }

        public Respuesta LoginUsuario(LoginUsuario loginUsuario)
        {
            return _usuarioRepository.LoginUsuario(loginUsuario);
        }
    }
}