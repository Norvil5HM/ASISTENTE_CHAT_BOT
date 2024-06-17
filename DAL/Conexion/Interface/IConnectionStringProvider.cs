using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Conexion.Interface
{
    public interface IConnectionStringProvider
    {
        string GetDBConnectionString();
    }
}
