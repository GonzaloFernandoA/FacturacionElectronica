using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fe.FacturacionElectronicaMTXCA.Nacional.WebServices;
using Fe.FacturacionElectronicaV2.Core.Logueos;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Core.LoginWSAA;
using Fe.FacturacionElectronicaV2.Core.Wrappers;
using Fe.FacturacionElectronicaMTXCA.Nacional.Wrappers;
using Fe.FacturacionElectronicaV2.Core;
using System.Web.Services.Protocols;
using Fe.FacturacionElectronicaV2.Nacional;
using Fe.FacturacionElectronicaV2.Nacional.Wrappers;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaMTXCA.Nacional;

namespace Fe.FacturacionElectronicaV2
{
    public class ServidorFacturaElectronicaMTXCA : IServidorFacturaElectronica
    {
        private WSMTXCA wsfe;
        private LogueadorFe logueador;

        public ServidorFacturaElectronicaMTXCA( SoapHttpClientProtocol wsfe )
        {
            this.wsfe = (WSMTXCA) wsfe;
            this.logueador = new LogueadorFe();
        }

        public ServidorFacturaElectronicaMTXCA( SoapHttpClientProtocol wsfe, LogueadorFe logueador )
        {
            this.wsfe = (WSMTXCA) wsfe;
            this.logueador = logueador;
        }

        public CAERespuestaFe ObtenerCae( FeCabecera cabFe, Autorizacion aut )
        {
            if ( aut == null )
            {
                ArgumentNullException ex = new ArgumentNullException( "aut", "Se debe indicar una autorización" );
                throw ex;
            }

            AuthRequestType feAut = this.ObtenerAuthReq( aut );

            CAERespuestaFe respuesta = this.SolicitarCAE( cabFe, feAut );

            return respuesta;
        }

        private CAERespuestaFe SolicitarCAE( FeCabecera cabFe, AuthRequestType aut )
        {
            ComprobanteType comprobante = this.ObtenerCaeRequest( cabFe );

            SerializadorRequest serializador = new SerializadorRequest();
            serializador.Serializar<ComprobanteType>( comprobante );

            ManagerErroresFe managerErrores = new ManagerErroresFe( this.logueador );
            ResultadoSimpleType resultado;
            ComprobanteCAEResponseType caeResponse;
            CodigoDescripcionType[] observaciones = null;
            CodigoDescripcionType[] errores = null;
            CodigoDescripcionType evento;
            resultado = this.wsfe.autorizarComprobante( aut, comprobante, out caeResponse, out observaciones, out errores, out evento );
            
            if (this.HuboSolicitudesRechazadas( errores ))
            {
                this.GenerarBackupArchivoSerializado(serializador, comprobante);                
            }

            managerErrores.CapturarError( errores, cabFe );

            WrapperCaeRespuestaMTXCA wcrm = new WrapperCaeRespuestaMTXCA();

            return wcrm.Convertir( resultado, caeResponse, observaciones, errores );
        }

        public Boolean HuboSolicitudesRechazadas(CodigoDescripcionType[] errores)
        {            
            Boolean retorno;
            retorno = false;

           if (errores != null)
            {
                retorno = true;
            }

            return retorno;
        }

        public void GenerarBackupArchivoSerializado(SerializadorRequest serializador, ComprobanteType cabecera)
        {
            String ruta;
            
            ClasificacionDeComprobantes comprobantes = new ClasificacionDeComprobantes();
                                    
            ruta = Directory.GetCurrentDirectory() + "\\Log\\FacturacionElectronica\\" + comprobantes.ObtenerTipoYLetraDeComprobante(cabecera.codigoTipoComprobante) + "_" + cabecera.numeroPuntoVenta.ToString().PadLeft(4, '0') + "_" + cabecera.numeroComprobante.ToString().PadLeft(8, '0') +"_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml";

            serializador.SerializadorConRuta<ComprobanteType>(cabecera, ruta);

        }

        public bool ServidorActivo()
        {
            DummyResponseType respuesta = this.wsfe.dummy();
            bool retorno = true;
            if ( respuesta.appserver != "OK" || respuesta.authserver != "OK" || respuesta.dbserver != "OK" )
            {
                retorno = false;
            }

            return retorno;
        }

        private ComprobanteType ObtenerCaeRequest( FeCabecera cabFe )
        {
            WrapperCabeceraMTXCA wc = new WrapperCabeceraMTXCA();
            return wc.Convertir( cabFe );
        }

        private AuthRequestType ObtenerAuthReq( Autorizacion aut )
        {
            WrapperAutorizacion wa = new WrapperAutorizacion();
            return wa.ConvertirMTXCA( aut );
        }

        public CAERespuestaFe ReprocesarComprobantes( FeCabecera cabFe, Autorizacion feAut )
        {
            ConversorConsultaCaeARespuestaCae conversor = new ConversorConsultaCaeARespuestaCae();
            CAERespuestaFe retorno = new CAERespuestaFe();
            WrapperCabeceraFe wrapperFe = new WrapperCabeceraFe();
            List<Observacion> diferencias;

            retorno.Cabecera = conversor.ConvertirCabecera( cabFe, feAut );

            ConsultasWSMTXCA consultas = new ConsultasWSMTXCA( this.wsfe, this.logueador );
            CAEDetalleRespuesta respuestaReproceso;
            retorno.Detalle = new List<CAEDetalleRespuesta>();

            foreach ( FeDetalle lote in cabFe.DetalleComprobantes )
            {
                respuestaReproceso = consultas.DatosDeComprobante( feAut, cabFe.TipoComprobante, (int) lote.ComprobanteDesde, cabFe.PuntoDeVenta );
                diferencias = wrapperFe.Comparar( respuestaReproceso, lote );

                if ( diferencias.Count > 0 )
                {
                    respuestaReproceso.Resultado = "R";
                    respuestaReproceso.Cae = "";
                    respuestaReproceso.Observaciones = diferencias;
                }
                retorno.Detalle.Add( respuestaReproceso );
            }
            this.logueador.LoguearObservaciones( retorno.Detalle, cabFe );

            return retorno;
        }
    }
}
