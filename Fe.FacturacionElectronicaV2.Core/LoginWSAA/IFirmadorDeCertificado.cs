using System;
using System.Xml;

namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public interface IFirmadorDeCertificado
    {
        string FirmarCertificado( XmlDocument XmlLoginTicketRequest, string rutaDelCertificadoFirmante );
    }
}
