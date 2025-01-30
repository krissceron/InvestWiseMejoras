using InvestWiseProyecto.Model;
using InvestWiseProyecto.Repository;

namespace InvestWiseProyecto.Service
{
    public class RolService
    {
        private readonly IRolRepository _rolRepository;
        public RolService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public Respuesta ObtenerRol()
        {
            return _rolRepository.ObtenerRol();
        }
    }
}
