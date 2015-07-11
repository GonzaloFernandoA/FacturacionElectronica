using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services.Protocols;
using Fe.FacturacionElectronicaV2.Core;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.Core.Interfaces;
using Fe.FacturacionElectronicaV2.Core.LoginWSAA;
using Fe.FacturacionElectronicaV2.Core.Logueos;
using Fe.FacturacionElectronicaV2.Nacional;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;
using System.Security.Cryptography.X509Certificates;

namespace Fe.FacturacionElectronicaV2
{
    public class FuncionesFe : IFunciones
    {
        private string mensajeDeError = "";
        private LogueadorFe logueador;
        private ConsultasWS consultas;
        private IServidorFacturaElectronica servidorFe;

        public SoapHttpClientProtocol Wsfe
        {
            get { return this.consultas.ObtenerWS(); }
        }

        public string MensajeDeError
        {
            get { return mensajeDeError; }
        }

        public FuncionesFe( LogueadorFe logueador, ConsultasWS consultas, IServidorFacturaElectronica servidor )
        {
            this.consultas = consultas;
            this.servidorFe = servidor;
            this.logueador = logueador;
        }

        public CAERespuestaFe ObtenerCae( Autorizacion autorizacion, FeCabecera cabFe )
        {
            CAERespuestaFe Respuesta;
            try
            {
                int ultimoNro = this.ObtenerUltimoNumeroDeComprobante( autorizacion, cabFe.PuntoDeVenta, cabFe.TipoComprobante );
                this.Logueos_ObtenerCaeWSFE( cabFe, ultimoNro );
                FeDetalle item = cabFe.DetalleComprobantes[0];
                if ( item.ComprobanteDesde > ultimoNro + 1 )
                {
                    Respuesta = this.Respuesta_UltimoComprobante( cabFe, ultimoNro, autorizacion );
                }
                else
                {
                    FraccionadorDeLotesFe fdl = new FraccionadorDeLotesFe();
                    FeCabecera feCabProcesar = fdl.ObtenerProcesar( cabFe, ultimoNro );
                    FeCabecera feCabReprocesar = fdl.ObtenerReprocesar( cabFe, ultimoNro );
                    Respuesta = this.ObtenerRespuestasUnificadas( autorizacion, feCabProcesar, feCabReprocesar );
                }
            }
            catch ( ExcepcionFe ex )
            {
                this.mensajeDeError = ex.Message;
                throw ex;
            }
            return Respuesta;
        }

        private CAERespuestaFe Respuesta_UltimoComprobante( FeCabecera cabFe, int ultimoNro, Autorizacion autorizacion )
        {
            string numero = this.FormatearNumero( cabFe, ultimoNro );
            string tipoComprobante = this.consultas.ObtenerCodigoComprobante( autorizacion, cabFe.TipoComprobante );

            CAERespuestaFe Respuesta = new CAERespuestaFe();
            Respuesta.Cabecera = new CAECabeceraRespuesta();
            Respuesta.Detalle = new List<CAEDetalleRespuesta>();
            Respuesta.Cabecera.CantidadDeRegistros = 1;
            Respuesta.Cabecera.FechaProceso = DateTime.Today.ToString();
            Respuesta.Cabecera.PuntoDeVenta = cabFe.PuntoDeVenta;
            Respuesta.Cabecera.TipoComprobante = cabFe.TipoComprobante;
            Respuesta.Cabecera.Resultado = "R";
            CAEDetalleRespuesta Detalle = new CAEDetalleRespuesta();
            Detalle.Observaciones = new List<Observacion>();
            Observacion Obs = new Observacion();
            Detalle.Cae = "";
            Detalle.CaeFechaVencimiento = "";
            Detalle.Resultado = "R";
            Obs.Mensaje = "El próximo comprobante " + tipoComprobante + " a procesar debe ser el número " + numero;
            Detalle.Observaciones.Add( Obs );
            Respuesta.Detalle.Add( Detalle );
            return Respuesta;
        }

        private string FormatearNumero( FeCabecera cabFe, int ultimoNro )
        {
            string numero = cabFe.PuntoDeVenta.ToString().PadLeft( 4, '0' ) + "-" + ( ultimoNro + 1 ).ToString().PadLeft( 8, '0' );
            return numero;
        }

        private void Logueos_ObtenerCaeWSFE( FeCabecera cabFe, int ultimoNro )
        {
            this.logueador.Loguear( "* Ultimo número " );
            this.logueador.Loguear( "Punto de venta : " + cabFe.PuntoDeVenta.ToString() );
            this.logueador.Loguear( "Tipo comprobante : " + cabFe.TipoComprobante.ToString() );
            this.logueador.Loguear( "Número : " + ultimoNro.ToString() );
            this.logueador.Loguear( "*" );

            this.logueador.Loguear( "* Solicitados " );
            this.logueador.Loguear( "Punto de venta : " + cabFe.PuntoDeVenta.ToString() );
            this.logueador.Loguear( "Tipo comprobante : " + cabFe.TipoComprobante.ToString() );

            foreach ( FeDetalle item in cabFe.DetalleComprobantes )
            {
                this.logueador.Loguear( "Desde : " + item.ComprobanteDesde.ToString() );
                this.logueador.Loguear( "Hasta : " + item.ComprobanteHasta.ToString() );
            }
        }

        private CAERespuestaFe ObtenerRespuestasUnificadas( Autorizacion autorizacion, FeCabecera procesar, FeCabecera reprocesar )
        {
            CAERespuestaFe retorno = null;
            if ( procesar != null )
            {
                try
                {
                    this.logueador.Loguear( "Consultando Afip......\r\n" );
                    this.logueador.Loguear( procesar.Serializar() );

                    CAERespuestaFe respuestaProcesar = this.servidorFe.ObtenerCae( procesar, autorizacion );

                    this.logueador.Loguear( "Respuesta Afip.......\r\n" );
                    this.logueador.Loguear( respuestaProcesar.Serializar() );
                    
                    retorno = respuestaProcesar;
                }
                catch ( ExcepcionFe ex )
                {
                    this.mensajeDeError = ex.Message;
                    throw ex;
                }
                foreach ( CAEDetalleRespuesta respuesta in retorno.Detalle )
                {
                    if ( (respuesta.Resultado.Trim() == "O" || respuesta.Resultado.Trim() == "R") && respuesta.Observaciones != null )
                    {
                        foreach ( Observacion observacion in respuesta.Observaciones )
                        {
                            this.logueador.Loguear( observacion.Mensaje );
                        }
                    }
                }
            }
            if ( reprocesar != null )
            {
                this.logueador.Loguear( "Consultando Afip......\r\n" );
                this.logueador.Loguear( reprocesar.Serializar() );

                CAERespuestaFe respuestaReprocesar = this.servidorFe.ReprocesarComprobantes( reprocesar, autorizacion );
                this.logueador.Loguear( "Respuesta Afip.......\r\n" );
                this.logueador.Loguear( respuestaReprocesar.Serializar() );

                if ( retorno == null )
                {
                    retorno = respuestaReprocesar;
                }
                else
                {
                    retorno.Unir( respuestaReprocesar );
                }
            }

            retorno.Detalle = this.OrdenarDetalle( retorno.Detalle );

            return retorno;
        }

        private List<CAEDetalleRespuesta> OrdenarDetalle( List<CAEDetalleRespuesta> detalle )
        {
            var retornoOrdenado = from p in detalle orderby p.ComprobanteDesde select p;
            List<CAEDetalleRespuesta> retorno = new List<CAEDetalleRespuesta>();

            foreach ( CAEDetalleRespuesta itemOrdenado in retornoOrdenado )
            {
                retorno.Add( itemOrdenado );
            }

            return retorno;
        }

        public int ObtenerUltimoNumeroDeComprobante( Autorizacion autorizacion, int pventa, int tipoComprobante )
        {
            return this.consultas.UltimoComprobante( autorizacion, pventa, tipoComprobante );
        }

        public String ObtenerFechaDeVencimientoCertificadoDigital( String tcFileNameCertificadoDigital )
        {

            X509Certificate m_certificado = new X509Certificate ( tcFileNameCertificadoDigital );
            
            String lcFechaVencimiento = "";

            if (m_certificado != null)
            {
                lcFechaVencimiento = m_certificado.GetExpirationDateString();                
            }

            return lcFechaVencimiento;
        }

        public int ObtenerDiasHastaElVencimientoCertificadoDigital(String tcFileNameCertificadoDigital)
        {

            String lcFechaVencimientoString = this.ObtenerFechaDeVencimientoCertificadoDigital(tcFileNameCertificadoDigital);
            int Anio = Convert.ToInt32(lcFechaVencimientoString);
            int Mes =  Convert.ToInt32(lcFechaVencimientoString);
            int Dia = Convert.ToInt32(lcFechaVencimientoString);
            DateTime ldFechaVencimiento = new DateTime(Anio, Mes, Dia);

            DateTime ldFechaActual = DateTime.Now;

            TimeSpan ts = ldFechaVencimiento - ldFechaActual;

            int dias = ts.Days;
            return dias;
        }

        public int CantidadMaximaSolicitudFE( Autorizacion autorizacion )
        {
            return ( (ConsultasWSFe) this.consultas ).CantidadMaximaDeRegistrosSolicitud( autorizacion );
        }

        public int CantidadMaximaDeRegistros( Autorizacion autorizacion )
        {
            return ( (ConsultasWSFe) this.consultas ).CantidadMaximaDeRegistrosSolicitud( autorizacion );
        }

        public List<UltimoNumeroComprobante> NumeracionPorComprobantes( Autorizacion autorizacion, int puntoDeVenta )
        {
            List<UltimoNumeroComprobante> retorno = new List<UltimoNumeroComprobante>();
            try
            {
                IList<IValorRespuestaWS> comprobantes = this.consultas.ObtenerTiposDeComprobante( autorizacion );
                foreach ( Comprobante comprobante in comprobantes )
                {
                    int ultimoNumero = this.consultas.UltimoComprobante( autorizacion, puntoDeVenta, comprobante.Id );

                    retorno.Add( new UltimoNumeroComprobante() { TipoComprobante = comprobante.Id, UltimoNumero = ultimoNumero } );
                }
            }
            catch ( ExcepcionFe error )
            {
                ValidacionException ex = new ValidacionException( "NumeracionPorComprobantes", error.Message );
                this.mensajeDeError = error.Message;
                throw ex;
            }
            return retorno;
        }

    }
}
