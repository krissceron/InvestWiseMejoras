using InvestWiseProyecto.Model;

namespace InvestWiseProyecto.Repository
{
    public interface IPropuestaRepository
    {
        Respuesta InsertarPropuesta(Propuesta propuesta);
        Respuesta ObtenerPropuestas();
        Respuesta ObtenerPropuestaPorId(int idPropuesta);
        Respuesta ObtenerUsuariosPorPropuesta(int idPropuesta);
        Respuesta AceptarPropuesta(AceptarPropuesta usuarioPropuesta);
        Respuesta ActualizarPropuesta(PropuestaModificada propuestaModi);
        Respuesta EliminarPropuesta(int idPropuesta);
        Respuesta SalirPropuesta(AceptarPropuesta usuarioPropuesta);
    }

}
