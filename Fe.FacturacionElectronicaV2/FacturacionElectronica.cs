using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Services.Protocols;
using Fe.FacturacionElectronicaV2.Core;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.Core.Interfaces;
using Fe.FacturacionElectronicaV2.Core.LoginWSAA;
using Fe.FacturacionElectronicaV2.Core.Logueos;
using Fe.FacturacionElectronicaV2.ExportacionV0;
using Fe.FacturacionElectronicaV2.ExportacionV0.Equivalencias;
using Fe.FacturacionElectronicaV2.ExportacionV0.Wrappers;
using Fe.FacturacionElectronicaV2.Nacional;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Exportacion.WebServices;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;
using Fe.FacturacionElectronicaV2.DatosSegunTabla;

namespace Fe.FacturacionElectronicaV2
{
    public class FacturacionElectronica
    {
        private ContenedorDatosEquivalencias contenedorDatosEquivalvalencias;
        private LogueadorFe logueador;
        private FuncionesFe funcionesFe;
        private FuncionesFex funcionesFex;
        private TipoWebService tipoWebService;

        public ContenedorDatosEquivalencias ContenedorDatosEquivalvalencias
        {
            get { return this.contenedorDatosEquivalvalencias; }
            set { this.contenedorDatosEquivalvalencias = value; }
        }

        public string MensajeDeError
        {
            get 
            {
                switch ( this.tipoWebService )
                {
                    case TipoWebService.Nacional:
                    case TipoWebService.MTXCA:
                        return this.funcionesFe.MensajeDeError;
                    case TipoWebService.Exportacion:
                        return this.funcionesFex.MensajeDeError;
                    default:
                        return "";
                }
            }
        }

        public FacturacionElectronica( TipoWebService tipoWebService )
        {
            this.tipoWebService = tipoWebService;
            this.logueador = new LogueadorFe();

            if ( !this.tipoWebService.Equals( TipoWebService.Exportacion ) )
            {
                ConsultasWS consultas = FactoryConsultasWSNacional.ObtenerInstancia( this.tipoWebService, this.logueador );
                IServidorFacturaElectronica servidorFe = FactoryServidorFacturaElectronica.ObtenerInstancia( this.tipoWebService, consultas.ObtenerWS(), this.logueador );
                this.funcionesFe = new FuncionesFe( this.logueador, consultas, servidorFe );
            }
            this.funcionesFex = new FuncionesFex( new WSFEX(), this.logueador );
        }

        private Autorizacion IniciarWSAA( ConfiguracionWS config )
        {
            IFactoriaHerramientasWSAA factoriaHerramientasWSAA = new FactoriaHerramientasWSAA();
            ServidorAutenticacion wsaut = factoriaHerramientasWSAA.ObtenerServidorAutenticacion( config );
            return wsaut.ObtenerAutorizacion();
        }

        private void AplicarConfiguracionWS( SoapHttpClientProtocol ws, ConfiguracionWS config )
        {
            ws.Url = config.UrlNegocio;

            ws.Timeout = config.TiempoDeEspera;

            if ( !config.ProxyServidor.Equals( String.Empty ) )
            {
                WebProxy proxy = new WebProxy( config.ProxyServidor, config.ProxyPuerto );
                proxy.Credentials = new NetworkCredential( config.ProxyUsuario, config.ProxyPass );
                ws.Proxy = proxy;
            }
        }

        #region Facturacion Electrónica
        public CAERespuestaFe ObtenerCaeWSFE( ConfiguracionWS config, FeCabecera cabFe )
        {
            Autorizacion aut = this.IniciarWSAA( config );
            this.AplicarConfiguracionWS( this.funcionesFe.Wsfe, config );

            return this.funcionesFe.ObtenerCae( aut, cabFe );
        }

        public int ObtenerUltimoNumeroDeComprobante( ConfiguracionWS config, int pventa, int tipoComprobante )
        {
            Autorizacion aut = this.IniciarWSAA( config );
            this.AplicarConfiguracionWS( this.funcionesFe.Wsfe, config );

            return this.funcionesFe.ObtenerUltimoNumeroDeComprobante( aut, pventa, tipoComprobante );
        }

        public int CantidadMaximaSolicitudFE( ConfiguracionWS config )
        {
            Autorizacion aut = this.IniciarWSAA( config );
            this.AplicarConfiguracionWS( this.funcionesFe.Wsfe, config );

            return this.funcionesFe.CantidadMaximaSolicitudFE( aut );
        }

        public int CantidadMaximaDeRegistros( ConfiguracionWS config )
        {
            Autorizacion aut = this.IniciarWSAA( config );
            this.AplicarConfiguracionWS( this.funcionesFe.Wsfe, config );

            return this.funcionesFe.CantidadMaximaDeRegistros( aut );
        }

        public List<UltimoNumeroComprobante> NumeracionPorComprobantes( ConfiguracionWS config, int puntoDeVenta )
        {
            Autorizacion aut = this.IniciarWSAA( config );
            this.AplicarConfiguracionWS( this.funcionesFe.Wsfe, config );

            return this.funcionesFe.NumeracionPorComprobantes( aut, puntoDeVenta );
        }

        #endregion

        #region Facturación electrónica exportación

        public CAERespuestaFex ObtenerCaeWSFEX( ConfiguracionWS config, FexCabecera cabFex )
        {
            Autorizacion aut = this.IniciarWSAA( config );
            this.AplicarConfiguracionWS( this.funcionesFex.Wsfex, config );

            return this.funcionesFex.ObtenerCaeWSFEX( aut, cabFex );
        }

        public List<CAERespuestaFex> ObtenerCaeWSFEX( ConfiguracionWS config, List<FexCabecera> comprobantes )
        {
            Autorizacion aut = this.IniciarWSAA( config );
            this.AplicarConfiguracionWS( this.funcionesFex.Wsfex, config );

            return this.funcionesFex.ObtenerCaeWSFEX( aut, comprobantes );
        }

        public int ObtenerUltimoNumeroDeComprobanteX( ConfiguracionWS config, int pventa, int tipoComprobante )
        {
            Autorizacion aut = this.IniciarWSAA( config );
            this.AplicarConfiguracionWS( this.funcionesFex.Wsfex, config );

            return this.funcionesFex.ObtenerUltimoNumeroDeComprobanteX( aut, pventa, tipoComprobante );
        }

        public List<UltimoNumeroComprobante> NumeracionPorComprobantesExportacion( ConfiguracionWS config, int puntoDeVenta )
        {
            Autorizacion aut = this.IniciarWSAA( config );
            this.AplicarConfiguracionWS( this.funcionesFex.Wsfex, config );

            return this.funcionesFex.NumeracionPorComprobantesExportacion( aut, puntoDeVenta );
        }

        public List<Incoterms> ObtenerValoresIncoterms( ConfiguracionWS config )
        {
            Autorizacion aut = this.IniciarWSAA( config );
            this.AplicarConfiguracionWS( this.funcionesFex.Wsfex, config );

            return this.funcionesFex.ObtenerValoresIncoterms( aut );
        }

        public List<TipoMoneda> ObtenerValoresMonedas( ConfiguracionWS config )
        {
            Autorizacion aut = this.IniciarWSAA( config );
            this.AplicarConfiguracionWS( this.funcionesFex.Wsfex, config );

            return this.funcionesFex.ObtenerValoresMonedas( aut );
        }

        public List<Idioma> ObtenerValoresIdiomas( ConfiguracionWS config )
        {
            Autorizacion aut = this.IniciarWSAA( config );
            this.AplicarConfiguracionWS( this.funcionesFex.Wsfex, config );

            return this.funcionesFex.ObtenerValoresIdiomas( aut );
        }

        public List<TipoExportacion> ObtenerValoresConceptos( ConfiguracionWS config )
        {
            Autorizacion aut = this.IniciarWSAA( config );
            this.AplicarConfiguracionWS( this.funcionesFex.Wsfex, config );

            return this.funcionesFex.ObtenerValoresConceptos( aut );
        }

        public List<UnidadDeMedida> ObtenerValoresUnidadDeMedida( ConfiguracionWS config )
        {
            Autorizacion aut = this.IniciarWSAA( config );
            this.AplicarConfiguracionWS( this.funcionesFex.Wsfex, config );

            return this.funcionesFex.ObtenerValoresUnidadDeMedida( aut );
        }

        public List<Pais> ObtenerCuitDePaises( ConfiguracionWS config )
        {
            Autorizacion aut = this.IniciarWSAA( config );
            this.AplicarConfiguracionWS( this.funcionesFex.Wsfex, config );

            return this.funcionesFex.ObtenerCuitDePaises( aut );
        }

        #endregion
    }
}