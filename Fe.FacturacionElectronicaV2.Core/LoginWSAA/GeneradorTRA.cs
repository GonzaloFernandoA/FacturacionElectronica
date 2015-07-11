using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public class GeneradorTRA : IGeneradorTRA
    {
        private string XmlStrLoginTicketRequestTemplate = "<loginTicketRequest><header><uniqueId></uniqueId><generationTime></generationTime><expirationTime></expirationTime></header><service></service></loginTicketRequest>";
        private UInt32 identificadorUnico = 1;

        public XmlDocument Crear( IConfiguracionWS configuracion )
        {
            XmlDocument XmlLoginTicketRequest;
            XmlNode xmlNodoUniqueId;
            XmlNode xmlNodoGenerationTime;
            XmlNode xmlNodoExpirationTime;
            XmlNode xmlNodoService;

            XmlLoginTicketRequest = new XmlDocument();
            XmlLoginTicketRequest.LoadXml( this.XmlStrLoginTicketRequestTemplate );

            xmlNodoUniqueId = XmlLoginTicketRequest.SelectSingleNode( "//uniqueId" );
            xmlNodoGenerationTime = XmlLoginTicketRequest.SelectSingleNode( "//generationTime" );
            xmlNodoExpirationTime = XmlLoginTicketRequest.SelectSingleNode( "//expirationTime" );
            xmlNodoService = XmlLoginTicketRequest.SelectSingleNode( "//service" );

            xmlNodoGenerationTime.InnerText = DateTime.Now.AddMinutes( -10 ).ToString( "s" );
            xmlNodoExpirationTime.InnerText = DateTime.Now.AddMinutes( +10 ).ToString( "s" );
            xmlNodoUniqueId.InnerText = Convert.ToString( this.identificadorUnico );
            xmlNodoService.InnerText = configuracion.NombreServicio;
            this.identificadorUnico++;

            return XmlLoginTicketRequest;
        }
    }
}
