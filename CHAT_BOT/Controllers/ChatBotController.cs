using DAL.ChatBot.Interface;
using ENTITY;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.AplicacionWeb.Utilidades.Response;

namespace CHAT_BOT.Controllers
{
    public class ChatBotController : Controller
    {
        private readonly IBLL_ChatBot _BLL_ChatBotService;
        public ChatBotController(IBLL_ChatBot BLL_ChatBotService)
        {
            _BLL_ChatBotService = BLL_ChatBotService;
        }

        public IActionResult Chat()
        {
            ViewBag.TituloPage = "Chat Bot";
            return View();
        }

        [HttpGet]
        public IActionResult Lista_menu_asistente()
        {
            string mensaje = string.Empty;

            List<E_MenuAsitente> listaMenu = new List<E_MenuAsitente>();

            listaMenu = _BLL_ChatBotService.Lista_menu_asistente(out mensaje);

            GenericResponse<E_MenuAsitente> gResponse = new GenericResponse<E_MenuAsitente>();
            gResponse.Mensaje = mensaje;
            gResponse.ListaObjeto = listaMenu;

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpPost]
        public IActionResult Lista_paso_menu([FromBody] E_MenuAsitente menu)
        {
            string mensaje = string.Empty;

            List<E_PasoMenu> ListaPasoMenu = new List<E_PasoMenu>();

            ListaPasoMenu = _BLL_ChatBotService.Lista_paso_menu(menu.Id_menu_asistente, out mensaje);

            GenericResponse<E_PasoMenu> gResponse = new GenericResponse<E_PasoMenu>();
            gResponse.Mensaje = mensaje;
            gResponse.ListaObjeto = ListaPasoMenu;

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
    }
}
