using System;
using System.Net;
using System.Xml;

namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public class ServidorAutenticacion
    {
        private IConfiguracionWS configuracion;
        private IWSAAProxy webServiceAutenticacion;
        private IGeneradorTRA generadorTRA;
        private IAccesoWeb accesoWeb;
        private IFirmadorDeCertificado firmadorCertificados;
        private IManejadorDeErroresWSAA manejadorErrores;
        private IValidadorDeConfiguracion validadorDeConfiguracion;
        private IDeserializadorDeRespuestaLogin deserializadorDeRespuestaLogin;

        // https://wsaahomo.afip.gov.ar/ws/services/LoginCms Web service de testing Afip

        public ServidorAutenticacion( IFactoriaHerramientasWSAA factoriaHerramientas, IConfiguracionWS config )
        {
            this.configuracion = config;

            this.webServiceAutenticacion = factoriaHerramientas.ObtenerWSAA( config );
            this.generadorTRA = factoriaHerramientas.ObtenerGeneradorTRA();
            this.accesoWeb = factoriaHerramientas.ObtenerAccesoWeb();
            this.firmadorCertificados = factoriaHerramientas.ObtenerFirmadorDeCertificados();
            this.manejadorErrores = factoriaHerramientas.ObtenerManejadorErrores();
            this.validadorDeConfiguracion = factoriaHerramientas.ObtenerValidadorDeConfiguracion();
            this.deserializadorDeRespuestaLogin = factoriaHerramientas.ObtenerDeserializadorDeRespuestaLogin( config );

            if ( !String.IsNullOrEmpty( this.configuracion.ProxyServidor ) )
            {
                WebProxy proxy = new WebProxy( this.configuracion.ProxyServidor, this.configuracion.ProxyPuerto );
                proxy.Credentials = new NetworkCredential( this.configuracion.ProxyUsuario, this.configuracion.ProxyPass );
                this.webServiceAutenticacion.Proxy = proxy;
            }
        }

        public bool ValidarCertificado()
        {
            bool retorno = false;
            
            Autorizacion autorizacion = this.ObtenerAutorizacion();
            retorno = autorizacion.Token.Length != 0 && autorizacion.Sign.Length != 0;

            return retorno;
        }

        public Autorizacion ObtenerAutorizacion()
        {
            string resultadoLogin = "";
            ManagerAutorizaciones managerAuto = new ManagerAutorizaciones( this.configuracion.NombreServicio );
            Autorizacion autorizacion = new Autorizacion();

            if ( managerAuto.VerificarVigencia( this.configuracion ) )
            {
                autorizacion = managerAuto.ObtenerAutorizacionVigente();
            }
            else
            {
                this.VerificarDatosBasicos();
                try
                {
                    this.ChequearConectividad();
                    XmlDocument tra = this.generadorTRA.Crear( this.configuracion );
                    string certificado64 = this.FirmarCertificado( tra, this.configuracion.RutaCertificado );
                    resultadoLogin = this.Autorizar( certificado64 );

                    autorizacion = this.deserializadorDeRespuestaLogin.Deserializar( resultadoLogin );

                    managerAuto.SerializarVigencia( autorizacion );
                }
                catch ( Exception error )
                {
                    this.manejadorErrores.ManejarError( error, "ObtenerAutorizacion", error.Message );
                }
            }

            return autorizacion;
        }

        private string FirmarCertificado( XmlDocument tra, string p )
        {
            string retorno = "";
            
            try
            {
                retorno = this.firmadorCertificados.FirmarCertificado( tra, p );
            }
            catch ( Exception ex )
            {
                this.manejadorErrores.ManejarError( ex, "FirmarCertificado", ex.Message );
            }

            return retorno;
        }

        private string Autorizar(string certificado64)
        {
            string loginTicketResponse;
            using (this.webServiceAutenticacion)
            {
                this.webServiceAutenticacion.Url = this.configuracion.UrlLogin;
                loginTicketResponse = this.webServiceAutenticacion.loginCms(certificado64);
            }

            return loginTicketResponse;
        }

        private void VerificarDatosBasicos()
        {
            try
            {
                this.validadorDeConfiguracion.Validar( this.configuracion );
            }
            catch ( Exception ex)
            {
                this.manejadorErrores.ManejarError( ex, "VerificarDatosBasicos", ex.Message );
            }
        }

        private void ChequearConectividad()
        {
            try
            {
                this.accesoWeb.ChequearAcceso( this.configuracion.UrlLogin );
            }
            catch ( Exception ex )
            {
                this.manejadorErrores.ManejarError( ex, "ChequearConectividad", "No se pudo acceder al Web Service 'AFIP'. Verifique su coneccion a Internet." );
            }
        }
    }

}