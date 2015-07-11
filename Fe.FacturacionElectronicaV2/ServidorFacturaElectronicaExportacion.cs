using System;
using System.IO;
using Fe.FacturacionElectronicaV2.Core;
using Fe.FacturacionElectronicaV2.Core.LoginWSAA;
using Fe.FacturacionElectronicaV2.Core.Logueos;
using Fe.FacturacionElectronicaV2.Core.Wrappers;
using Fe.FacturacionElectronicaV2.ExportacionV0.Equivalencias;
using Fe.FacturacionElectronicaV2.ExportacionV0.Wrappers;
using Fe.FacturacionElectronicaV2.Wrappers;
using Fe.FacturacionElectronicaV2.Core.Interfaces;
using Fe.FacturacionElectronicaV2.Exportacion.WebServices;

namespace Fe.FacturacionElectronicaV2
{
    public class ServidorFacturaElectronicaExportacion
    {
        private WSFEX wsfex;
        private LogueadorFe logueador;

        public ServidorFacturaElectronicaExportacion( WSFEX wsfex )
        {
            this.wsfex = wsfex;
            this.logueador = new LogueadorFe();
        }

        public ServidorFacturaElectronicaExportacion( WSFEX wsfex, LogueadorFe logueador )
        {
            this.wsfex = wsfex;
            this.logueador = logueador;
        }

        public CAERespuestaFex ObtenerCae( Autorizacion aut, FexCabecera cabFex )
        {
            ClsFEXAuthRequest feAut = this.ObtenerFeAuthReq( aut );
            CAERespuestaFex respuesta = this.SolicitarCAE( cabFex, feAut );
            
            return respuesta;
        }

        private CAERespuestaFex SolicitarCAE( FexCabecera cabFex, ClsFEXAuthRequest aut )
        {
            ClsFEXRequest feCaeRequest = this.ObtenerClsFexRequest( cabFex );
            
            SerializadorRequest serializador = new SerializadorRequest();
            serializador.Serializar<ClsFEXRequest>( feCaeRequest );
            
            FEXResponseAuthorize caeResponse = this.wsfex.FEXAuthorize( aut, feCaeRequest );
            ManagerErroresFe managerErrores = new ManagerErroresFe( this.logueador );
            
            if (this.HuboSolicitudesRechazadas( caeResponse ))
            {
                this.GenerarBackupArchivoSerializado(serializador, feCaeRequest);                
            }

            managerErrores.CapturarError(caeResponse.FEXErr, cabFex);

            WrapperCaeRespuestaFex wcrf = new WrapperCaeRespuestaFex();

            return wcrf.Convertir( caeResponse.FEXResultAuth );
        }

        public Boolean HuboSolicitudesRechazadas(FEXResponseAuthorize respuesta)
        {            
            Boolean retorno;
            retorno = false;
            if (respuesta.FEXResultAuth == null)
            {
                retorno = true;
            }
            else
            {
                if (respuesta.FEXResultAuth.Resultado == "R")
                {
                    retorno = true;
                }
            }                

            return retorno;
        }

        public void GenerarBackupArchivoSerializado(SerializadorRequest serializador, ClsFEXRequest cabecera)
        {
            String ruta;
            
            ClasificacionDeComprobantes comprobantes = new ClasificacionDeComprobantes();

            ruta = Directory.GetCurrentDirectory() + "\\Log\\FacturacionElectronica\\" + comprobantes.ObtenerTipoYLetraDeComprobante(cabecera.Cbte_Tipo) + "_" + cabecera.Punto_vta.ToString().PadLeft(4, '0') + "_" + cabecera.Cbte_nro.ToString().PadLeft(8, '0') +"_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml";
            
            serializador.SerializadorConRuta<ClsFEXRequest>(cabecera, ruta);
        }

        public bool ServidorActivo()
        {
            DummyResponse respuesta = this.wsfex.FEXDummy();
            bool retorno = true;
            if ( respuesta.AppServer != "OK" || respuesta.AuthServer != "OK" || respuesta.DbServer != "OK" )
            {
                retorno = false;
            }

            return retorno;
        }

        private ClsFEXAuthRequest ObtenerFeAuthReq( Autorizacion aut )
        {
            WrapperAutorizacion wa = new WrapperAutorizacion();
            return wa.ConvertirFex( aut );
        }

        private ClsFEXRequest ObtenerClsFexRequest( FexCabecera cab )
        {
            WrapperCabeceraFex wcf = new WrapperCabeceraFex();
            return wcf.Convertir( cab );
        }
    }
}
