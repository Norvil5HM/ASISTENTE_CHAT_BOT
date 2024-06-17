using DAL.ChatBot.Interface;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DAL.Conexion.Interface;
using System.Data.SqlClient;

namespace DAL.ChatBot
{
    public class DAL_ChatBot : IDAL_ChatBot
    {
        private readonly IConnectionStringProvider _connectionStringProviderService;

        public DAL_ChatBot(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProviderService = connectionStringProvider;
        }

        #region menu_asistente
        public List<E_MenuAsitente> Lista_menu_asistente(out string Mensaje)
        {
            List<E_MenuAsitente> lista = new List<E_MenuAsitente>();
            Mensaje = String.Empty;

            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionStringProviderService.GetDBConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[USP_obtener_menu_asistente]", conexion);

                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new E_MenuAsitente()
                                {
                                    Id_menu_asistente = Convert.ToInt32(dr["ID_MENU_ASISTENTE"]),
                                    Desc_menu_asistente = dr["DESC_MENU_ASISTENTE"].ToString()
                                });
                        }
                    }

                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                lista = new List<E_MenuAsitente>();
                Mensaje = ex.Message;
            }
            return lista;

        }
        #endregion menu_asistente

        #region paso_menu
        public List<E_PasoMenu> Lista_paso_menu(int id_menu, out string Mensaje)
        {
            List<E_PasoMenu> lista = new List<E_PasoMenu>();
            Mensaje = String.Empty;

            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionStringProviderService.GetDBConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[USP_obtener_pasos_menu]", conexion);

                    cmd.Parameters.Add("ID_MENU", SqlDbType.Int).Value = id_menu;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new E_PasoMenu()
                                {
                                    Id_paso_menu = Convert.ToInt32(dr["ID_PASO_MENU"]),
                                    Desc_paso_menu = dr["DESC_PASO_MENU"].ToString()
                                });
                        }
                    }

                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                lista = new List<E_PasoMenu>();
                Mensaje = ex.Message;
            }
            return lista;
        }
        #endregion paso_menu
    }
}
