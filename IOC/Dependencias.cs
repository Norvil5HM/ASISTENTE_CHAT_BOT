using DAL.ChatBot;
using DAL.ChatBot.Interface;
using DAL.Conexion.Implementacion;
using DAL.Conexion.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC
{
    public static class Dependencias
    {
        public static void InyectarDependencia(this IServiceCollection services,
           IConfiguration Configuration)
        {
            services.AddTransient(typeof(IDAL_ChatBot), typeof(DAL_ChatBot));
            services.AddTransient(typeof(IBLL_ChatBot), typeof(BLL_ChatBot));
            services.AddTransient(typeof(IConnectionStringProvider), typeof(ConnectionStringProvider));
        }
    }
}
