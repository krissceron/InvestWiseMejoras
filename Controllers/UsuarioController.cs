using InvestWiseProyecto.Data;
using InvestWiseProyecto.Model;
using InvestWiseProyecto.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestWiseProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService; //KC
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("Login")]
        public Respuesta Login([FromBody] LoginUsuario loginUsuario)
        {
            return _usuarioService.LoginUsuario(loginUsuario);
        }


        [HttpPost]
        [Route("Crear")]
        public Respuesta CrearUsuario([FromBody] Usuario usuario)
        {
            return _usuarioService.CrearUsuario(usuario);
        }

        [HttpGet]
        [Route("ObtenerTodo")]
        public Respuesta ObtenerUsuarios()
        {
            return _usuarioService.ObtenerUsuarios();
        }//KC

        [HttpGet]
        [Route("ObtenerPorId/{idUsuario}")]
        public Respuesta ObtenerUsuarioPorId(int idUsuario)
        {
            return _usuarioService.ObtenerUsuarioPorId(idUsuario);
        }


        [HttpPut]
        [Route("Editar")]
        public Respuesta ActualizarUsuario([FromBody] UsuarioModificado usuarioModi)
        {
            return _usuarioService.ActualizarUsuario(usuarioModi);
        }

        [HttpDelete]
        [Route("Eliminar/{idUsuario}")]
        public Respuesta EliminarUsuario(int idUsuario)
        {
            return _usuarioService.EliminarUsuario(idUsuario);
        }


    }
}
