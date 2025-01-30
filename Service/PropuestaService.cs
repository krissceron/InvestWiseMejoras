using InvestWiseProyecto.Model;
using InvestWiseProyecto.Repository;

namespace InvestWiseProyecto.Service
{
    public class PropuestaService
    {
        private readonly IPropuestaRepository _propuestaRepository;

        public PropuestaService(IPropuestaRepository propuestaRepository)
        {
            _propuestaRepository = propuestaRepository;
        }

        public Respuesta InsertarPropuesta(Propuesta propuesta)
        {
            return _propuestaRepository.InsertarPropuesta(propuesta);
        }
        public Respuesta ObtenerPropuestas()
        {
            return _propuestaRepository.ObtenerPropuestas();
        }
        public Respuesta ObtenerPropuestaPorId(int idPropuesta)
        {
            return _propuestaRepository.ObtenerPropuestaPorId(idPropuesta);
        }
        public Respuesta ObtenerUsuariosPorPropuesta(int idPropuesta)
        {
            return _propuestaRepository.ObtenerUsuariosPorPropuesta(idPropuesta);
        }
        public Respuesta AceptarPropuesta(AceptarPropuesta usuarioPropuesta)
        {
            return _propuestaRepository.AceptarPropuesta( usuarioPropuesta);
        }
        public Respuesta ActualizarPropuesta(PropuestaModificada propuestaModi)
        {
            return _propuestaRepository.ActualizarPropuesta( propuestaModi);
        }
        public Respuesta EliminarPropuesta(int idPropuesta)
        {
            return _propuestaRepository.EliminarPropuesta(idPropuesta);
        }
        public Respuesta SalirPropuesta(AceptarPropuesta usuarioPropuesta)
        {
            return _propuestaRepository.SalirPropuesta(usuarioPropuesta);
        }
    }
}
