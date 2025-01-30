using InvestWiseProyecto.Model;

namespace InvestWiseProyecto.Repository
{
    public interface IUsuarioRepository //cambio KC
    {
        Respuesta InsertarUsuario(Usuario usuario);
        Respuesta ObtenerUsuario();
        Respuesta ObtenerUsuarioPorId(int idUsuario);
        Respuesta ActualizarUsuario(UsuarioModificado usuarioModi);
        Respuesta EliminarUsuario(int idUsuario);
        Respuesta LoginUsuario(LoginUsuario loginUsuario);
    }
}
