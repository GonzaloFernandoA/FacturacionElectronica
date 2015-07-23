using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaProbar
{
    public class ConfiguracionCliente
    {
        public string RutaCertificado { get; set; }
        public string NombreServicio { get; set; }
        public int TimeOut { get; set; }
        public long Cuit { get; set; }
       
        public string ServidorAutorizacion { get; set; }
        public string UrlNegocio { get; set; }
    }
}

