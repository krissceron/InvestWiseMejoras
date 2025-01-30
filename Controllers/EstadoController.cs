using InvestWiseProyecto.DataConnection;
using InvestWiseProyecto.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestWiseProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        [HttpGet]
        [Route("ObtenerTodo")]
        public Respuesta ObtenerEstado()
        {
            EstadoConnection dbConexion = new EstadoConnection();
            Respuesta res = dbConexion.ObtenerEstado();
            return res;
        }
    }
}

