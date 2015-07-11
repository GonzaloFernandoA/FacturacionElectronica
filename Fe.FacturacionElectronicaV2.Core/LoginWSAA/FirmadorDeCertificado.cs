using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public class FirmadorDeCertificado : IFirmadorDeCertificado
    {
        private IManejadorDeErroresWSAA manejadorErrores;
        private IAccesoDisco accesoDisco;

        public FirmadorDeCertificado( IManejadorDeErroresWSAA manejadorErrores, IAccesoDisco accesoDisco )
        {
            this.manejadorErrores = manejadorErrores;
            this.accesoDisco = accesoDisco;
        }

        public string FirmarCertificado( XmlDocument XmlLoginTicketRequest, string rutaDelCertificadoFirmante )
        {
            string cmsFirmadoBase64;

            X509Certificate2 certFirmante = this.ObtieneCertificadoDesdeArchivo( rutaDelCertificadoFirmante );
            Encoding EncodedMsg = Encoding.UTF8;
            byte[] msgBytes = EncodedMsg.GetBytes( XmlLoginTicketRequest.OuterXml );
            byte[] encodedSignedCms = this.FirmaBytesMensaje( msgBytes, certFirmante );
            cmsFirmadoBase64 = Convert.ToBase64String( encodedSignedCms );

            return cmsFirmadoBase64;
        }

        private X509Certificate2 ObtieneCertificadoDesdeArchivo( string argArchivo )
        {
            X509Certificate2 objCert = new X509Certificate2();

            try
            {
                objCert.Import( this.accesoDisco.ObtenerBytes( argArchivo ) );
            }
            catch ( Exception error )
            {
                this.manejadorErrores.ManejarError( error, "ObtieneCertificadoDesdeArchivo", error.Message );
            }

            return objCert;
        }

        private byte[] FirmaBytesMensaje( byte[] argBytesMsg, X509Certificate2 argCertFirmante )
        {
            ContentInfo infoContenido = new ContentInfo( argBytesMsg );
            SignedCms cmsFirmado = new SignedCms( infoContenido );
            CmsSigner cmsFirmante = new CmsSigner( argCertFirmante );

            try
            {
                cmsFirmante.IncludeOption = X509IncludeOption.EndCertOnly;
                cmsFirmado.ComputeSignature( cmsFirmante );

            }
            catch ( Exception error )
            {
                this.manejadorErrores.ManejarError( error, "FirmaBytesMensaje", error.Message );
            }
            return cmsFirmado.Encode();
        }
    }
}
