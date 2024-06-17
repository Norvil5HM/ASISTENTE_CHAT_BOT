using DAL.ChatBot.Interface;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ChatBot
{
    public class BLL_ChatBot : IBLL_ChatBot
    {
        private readonly IDAL_ChatBot _DAL_ChatService;

        public BLL_ChatBot(IDAL_ChatBot DAL_ChatService)
        {
            _DAL_ChatService = DAL_ChatService;
        }

        public List<E_MenuAsitente> Lista_menu_asistente(out string Mensaje)
        {
            List<E_MenuAsitente> listaMenu = _DAL_ChatService.Lista_menu_asistente(out Mensaje);

            return listaMenu;
        }

        public List<E_PasoMenu> Lista_paso_menu(int id_menu, out string Mensaje)
        {
            List<E_PasoMenu> listaPasoMenu = _DAL_ChatService.Lista_paso_menu(id_menu, out Mensaje);

            return listaPasoMenu;
        }
    }
}
