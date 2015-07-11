using System;
using System.Collections.Generic;
using Fe.FacturacionElectronicaV2.Core;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.Core.Logueos;
using Fe.FacturacionElectronicaV2.Core.Wrappers;
using Fe.FacturacionElectronicaV2.Nacional;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Nacional.Wrappers;
using Fe.FacturacionElectronicaV2.Wrappers;
using Fe.FacturacionElectronicaV2.Core.LoginWSAA;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;
using System.IO;
using System.Xml.Serialization;
using System.Web.Services.Protocols;

namespace Fe.FacturacionElectronicaV2
{
    public class ServidorFacturaElectronica : IServidorFacturaElectronica
    {
        private WSFEV1 wsfe;
        private LogueadorFe logueador;

        public ServidorFacturaElectronica( SoapHttpClientProtocol wsfe )
        {
            this.wsfe = (WSFEV1) wsfe;
            this.logueador = new LogueadorFe();
        }

        public ServidorFacturaElectronica( SoapHttpClientProtocol wsfe, LogueadorFe logueador )
        {
            this.wsfe = (WSFEV1) wsfe;
            this.logueador = logueador;
        }

        public CAERespuestaFe ObtenerCae( FeCabecera cabFe, Autorizacion aut )
        {   
            if ( aut == null )
            {
                ArgumentNullException ex = new ArgumentNullException( "aut", "Se debe indicar una autorización" );
                throw ex;
            }

            FEAuthRequest feAut = this.ObtenerFeAuthReq( aut );

            CAERespuestaFe respuesta = this.SolicitarCAE( cabFe, feAut );

            return respuesta;
        }

        public CAERespuestaFe ReprocesarComprobantes( FeCabecera cabFe , Autorizacion feAut )
        {
            ConversorConsultaCaeARespuestaCae conversor = new ConversorConsultaCaeARespuestaCae();
            CAERespuestaFe retorno = new CAERespuestaFe();
            WrapperCabeceraFe wrapperFe = new WrapperCabeceraFe();
            List<Observacion> diferencias;

            retorno.Cabecera = conversor.ConvertirCabecera( cabFe, feAut );
            
            ConsultasWSFe cwsfe = new ConsultasWSFe( this.wsfe, this.logueador );
            CAEDetalleRespuesta respuestaReproceso;
            retorno.Detalle = new List<CAEDetalleRespuesta>();

            foreach ( FeDetalle lote in cabFe.DetalleComprobantes )
            {
                respuestaReproceso = cwsfe.DatosDeComprobante( feAut, cabFe.TipoComprobante, (int)lote.ComprobanteDesde, cabFe.PuntoDeVenta );
                diferencias = wrapperFe.Comparar( respuestaReproceso, lote );

                if (diferencias.Count > 0)
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

        private CAERespuestaFe SolicitarCAE( FeCabecera cabFe, FEAuthRequest aut )
        {
            FECAERequest feCaeRequest = this.ObtenerCaeRequest( cabFe );
            
            SerializadorRequest serializador = new SerializadorRequest();
            serializador.Serializar<FECAERequest>( feCaeRequest );

            FECAEResponse caeResponse = this.wsfe.FECAESolicitar( aut, feCaeRequest );
            ManagerErroresFe managerErrores = new ManagerErroresFe( this.logueador );

            if (this.HuboSolicitudesRechazadas( caeResponse ))
            {
                this.GenerarBackupArchivoSerializado(serializador, feCaeRequest);                
            }

            managerErrores.CapturarError(caeResponse.Errors, cabFe);

            WrapperCaeRespuestaFe wcrf = new WrapperCaeRespuestaFe();

            return wcrf.Convertir( caeResponse );
        }

        public Boolean HuboSolicitudesRechazadas(FECAEResponse respuesta)
        {            
            Boolean retorno;
            retorno = false;
            foreach (FECAEDetResponse oItem in respuesta.FeDetResp )
            {
                if (oItem.Resultado == "R")
                {
                    retorno = true;
                    break;
                }                    
            }            
            return retorno;
        }

        public void GenerarBackupArchivoSerializado(SerializadorRequest serializador, FECAERequest cabecera)
        {
            String ruta, rutaAux;
            
            ClasificacionDeComprobantes comprobantes = new ClasificacionDeComprobantes();

            if( cabecera.FeCabReq.CantReg == 1 ){
                rutaAux = cabecera.FeDetReq[0].CbteDesde.ToString().PadLeft(8, '0');
            }else{
                rutaAux = cabecera.FeDetReq[0].CbteDesde.ToString().PadLeft(8, '0') + "-" + cabecera.FeDetReq[cabecera.FeCabReq.CantReg - 1].CbteDesde.ToString().PadLeft(8, '0');
            }
            ruta = Directory.GetCurrentDirectory() + "\\Log\\FacturacionElectronica\\" + comprobantes.ObtenerTipoYLetraDeComprobante(cabecera.FeCabReq.CbteTipo) + "_" + cabecera.FeCabReq.PtoVta.ToString().PadLeft(4, '0') + "_" + rutaAux + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml";

            serializador.SerializadorConRuta<FECAERequest>(cabecera, ruta);
        }

        public bool ServidorActivo()
        {
            DummyResponse respuesta = this.wsfe.FEDummy();
            bool retorno = true;
            if ( respuesta.AppServer != "OK" || respuesta.AuthServer != "OK" || respuesta.DbServer != "OK" )
            {
                retorno = false;
            }

            return retorno;
        }

        private FECAERequest ObtenerCaeRequest( FeCabecera cabFe )
        {
            WrapperCabeceraFe wc = new WrapperCabeceraFe();
            return wc.Convertir( cabFe );
        }

        private FEAuthRequest ObtenerFeAuthReq( Autorizacion aut )
        {
            WrapperAutorizacion wa = new WrapperAutorizacion();
            return wa.ConvertirFe( aut );
        }
    }
}