using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ChatBot.Interface
{
    public interface IDAL_ChatBot
    {
        List<E_MenuAsitente> Lista_menu_asistente(out string Mensaje);
        List<E_PasoMenu> Lista_paso_menu(int id_menu, out string Mensaje);
    }
}
