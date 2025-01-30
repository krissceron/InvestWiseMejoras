using InvestWiseProyecto.DataConnection;
using InvestWiseProyecto.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestWiseProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        [HttpGet]
        [Route("ObtenerTodo")]
        public Respuesta ObtenerCategoria()
        {
            CategoriaConnection dbConexion = new CategoriaConnection();
            Respuesta res = dbConexion.ObtenerCategoria();
            return res;
        }
    }
}
