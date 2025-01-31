using InvestWiseProyecto.Data;
using InvestWiseProyecto.DataConnection;
using InvestWiseProyecto.Model;
using InvestWiseProyecto.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestWiseProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly RolService _rolService; //KC
        public RolController(RolService rolService)
        {
            _rolService = rolService;
        }
        [HttpGet]
        [Route("ObtenerTodo")]
        public Respuesta ObtenerRol()
        {
            return _rolService.ObtenerRol();
        }
        

    }
}
