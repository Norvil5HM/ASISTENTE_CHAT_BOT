using DAL.Conexion.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Conexion.Implementacion
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {

        private readonly IConfiguration _configuration;

        public ConnectionStringProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetDBConnectionString()
        {
            String ConexionString = _configuration.GetConnectionString("SqlCadena");
            return ConexionString;
        }
    }
}
