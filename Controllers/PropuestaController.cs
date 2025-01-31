using InvestWiseProyecto.DataConnection;
using InvestWiseProyecto.Model;
using InvestWiseProyecto.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestWiseProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropuestaController : ControllerBase
    {
        private readonly PropuestaService _propuestaService; //KC
        public PropuestaController(PropuestaService propuestaService)
        {
            _propuestaService = propuestaService;
        }

        
        [HttpPost]
        [Route("Crear")]
        public Respuesta CrearPropuesta([FromBody] Propuesta propuesta)
        {

            return _propuestaService.InsertarPropuesta(propuesta);
        }

        
        [HttpGet]
        [Route("ObtenerTodo")]
        public Respuesta ObtenerPropuesta()
        {
            return _propuestaService.ObtenerPropuestas();
        }

        
        [HttpGet]
        [Route("ObtenerPorId/{idPropuesta}")]
        public Respuesta ObtenerPropuestaPorId(int idPropuesta)
        {
            return _propuestaService.ObtenerPropuestaPorId(idPropuesta);
        }

        
        [HttpGet]
        [Route("ObtenerUsuariosPorPropuesta/{idPropuesta}")]
        public Respuesta ObtenerUsuariosPorPropuesta(int idPropuesta)
        {
            return _propuestaService.ObtenerUsuariosPorPropuesta(idPropuesta);
        }

        [HttpPost]
        [Route("AceptarPropuesta")]
        public Respuesta AceptarPropuesta([FromBody] AceptarPropuesta usuarioPropuesta)
        {
            return _propuestaService.AceptarPropuesta(usuarioPropuesta);
        }

        [HttpPut]
        [Route("Editar")]
        public Respuesta ActualizarPropuesta([FromBody] PropuestaModificada propuestaModi)
        {
            return _propuestaService.ActualizarPropuesta(propuestaModi);
        }

        [HttpDelete]
        [Route("Eliminar/{idPropuesta}")]
        public Respuesta EliminarPropuesta(int idPropuesta)
        {
            return _propuestaService.EliminarPropuesta(idPropuesta);
        }

        [HttpPost]
        [Route("SalirPropuesta")]
        public Respuesta SalirPropuesta([FromBody] AceptarPropuesta usuarioPropuesta)
        {
            return _propuestaService.SalirPropuesta(usuarioPropuesta);
        }


    }
}
