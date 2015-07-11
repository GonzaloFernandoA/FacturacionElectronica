
using System.IO;

namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public class ConfiguracionWS : IConfiguracionWS
    {
        private long cuit;
        private string rutaCertificado = "";
        private string nombreServicio = "";
        private string urlLogin = "";
        private string urlNegocio = "";
        private int tiempoDeEspera = 15000;

        private string proxyServidor = "";
        private int proxyPuerto = 80;
        private string proxyUsuario = "";
        private string proxyPass = "";

        public string ProxyPass
        {
            get { return this.proxyPass; }
            set { this.proxyPass = value; }
        }

        public string ProxyUsuario
        {
            get { return this.proxyUsuario; }
            set { this.proxyUsuario = value; }
        }

        public int ProxyPuerto
        {
            get { return this.proxyPuerto; }
            set { this.proxyPuerto = value; }
        }

        public string ProxyServidor
        {
            get { return this.proxyServidor; }
            set { this.proxyServidor = value; }
        }

        public int TiempoDeEspera
        {
            get { return this.tiempoDeEspera; }
            set { this.tiempoDeEspera = value; }
        }

        public string UrlNegocio
        {
            get { return this.urlNegocio; }
            set { this.urlNegocio = value; }
        }

        public string RutaCertificado
        {
            get { return this.rutaCertificado; }
            set { this.rutaCertificado = value; }
        }

        public string UrlLogin
        {
            get { return this.urlLogin; }
            set { this.urlLogin = value; }
        }

        public string NombreServicio
        {
            get { return this.nombreServicio; }
            set { this.nombreServicio = value.ToLower(); }
        }

        public long Cuit
        {
            get { return this.cuit; }
            set { this.cuit = value; }
        }
    }
}
