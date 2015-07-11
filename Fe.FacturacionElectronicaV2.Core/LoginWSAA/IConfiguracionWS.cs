using System;
namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public interface IConfiguracionWS
    {
        long Cuit { get; set; }
        string NombreServicio { get; set; }
        string ProxyPass { get; set; }
        int ProxyPuerto { get; set; }
        string ProxyServidor { get; set; }
        string ProxyUsuario { get; set; }
        string RutaCertificado { get; set; }
        int TiempoDeEspera { get; set; }
        string UrlLogin { get; set; }
        string UrlNegocio { get; set; }
    }
}
