using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public class DeserializadorDeRespuestaLogin : Fe.FacturacionElectronicaV2.Core.LoginWSAA.IDeserializadorDeRespuestaLogin
    {
        private IConfiguracionWS configuracion;

        public DeserializadorDeRespuestaLogin( IConfiguracionWS config )
        {
            this.configuracion = config;
        }

        public Autorizacion Deserializar( string resultadoLogin )
        {
            Autorizacion autorizacion = new Autorizacion();

            XmlDocument xml = new XmlDocument();
            xml.LoadXml( resultadoLogin );
            
            autorizacion.Sign = xml.SelectSingleNode( "//sign" ).InnerText;
            autorizacion.Token = xml.SelectSingleNode( "//token" ).InnerText;
            autorizacion.Cuit = this.configuracion.Cuit;
            autorizacion.Expiracion = Convert.ToDateTime( xml.SelectSingleNode( "//expirationTime" ).InnerText );

            return autorizacion;
        }
    }
}
