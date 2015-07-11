using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fe.FacturacionElectronicaV2.ExportacionV0.Equivalencias;
using Fe.FacturacionElectronicaV2.Core.LoginWSAA;
using Fe.FacturacionElectronicaV2.Exportacion.WebServices;
using Fe.FacturacionElectronicaV2.ExportacionV0.Wrappers;
using Fe.FacturacionElectronicaV2.Core.Logueos;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.ExportacionV0;
using Fe.FacturacionElectronicaV2.Core;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;

namespace Fe.FacturacionElectronicaV2
{
    public class FuncionesFex
    {
        public string MensajeDeError = "";
        private LogueadorFe logueador;
        private WSFEX wsfex;

        public WSFEX Wsfex
        {
            get { return wsfex; }
            set { wsfex = value; }
        }

        public FuncionesFex( WSFEX wsfe, LogueadorFe logueador )
        {
            this.wsfex = wsfe;
            this.logueador = logueador;
        }

        public CAERespuestaFex ObtenerCaeWSFEX( Autorizacion autorizacion, FexCabecera cabFex )
        {
            long ultimoNro = this.UltimoComprobanteFex( autorizacion, cabFex.PuntoDeVenta, cabFex.TipoComprobante );

            WrapperCabeceraFex wr = new WrapperCabeceraFex();

            ServidorFacturaElectronicaExportacion sfex = new ServidorFacturaElectronicaExportacion( this.wsfex, this.logueador );
            CAERespuestaFex respuesta = null;
            cabFex.Id = this.ObtenerIdFex( cabFex, ultimoNro, autorizacion );
            List<Observacion> diferencias;
            try
            {
                this.logueador.Loguear( "Consultando Afip....." );
                this.logueador.Loguear( cabFex.Serializar() );

                respuesta = sfex.ObtenerCae( autorizacion, cabFex );

                this.logueador.Loguear( "Respuesta Afip......." );
                this.logueador.Loguear( respuesta.Serializar() );

                if ( cabFex.ComprobanteNumero <= ultimoNro )
                {
                    ConsultasWSFex consulta = new ConsultasWSFex( wsfex );
                    ClsFEXGetCMPR comprobanteAfip = consulta.DatosDeComprobante( autorizacion, cabFex.TipoComprobante, cabFex.ComprobanteNumero, cabFex.PuntoDeVenta );
                    // Aca comparo los comprobantes y y si hay diferencias lo rechazo
                    diferencias = wr.Comparar( comprobanteAfip, cabFex );

                    if ( diferencias.Count > 0 )
                    {
                        respuesta.MotivosObservaciones = "Diferencias en comprobante.";
                        respuesta.Observaciones = diferencias;
                        respuesta.Resultado = "R";
                        respuesta.Cae = "";
                    }
                }
            }
            catch ( ExcepcionFe ex )
            {
                this.MensajeDeError = ex.Message;
                throw ex;
            }

            return respuesta;
        }

        public List<CAERespuestaFex> ObtenerCaeWSFEX( Autorizacion aut, List<FexCabecera> comprobantes )
        {
            long ultimoNro = this.UltimoComprobanteFex( aut, comprobantes[0].PuntoDeVenta, comprobantes[0].TipoComprobante );

            ServidorFacturaElectronicaExportacion sfex = new ServidorFacturaElectronicaExportacion( this.wsfex, this.logueador );
            List<CAERespuestaFex> retorno = new List<CAERespuestaFex>();
            CAERespuestaFex respuesta = null;

            foreach ( FexCabecera comprobante in comprobantes )
            {
                respuesta = this.ObtenerCaeWSFEX( aut, comprobante );
                retorno.Add( respuesta );
            }

            return retorno;
        }

        private long ObtenerIdFex( FexCabecera cabFex, long ultimoNro, Autorizacion autorizacion )
        {
            long retorno;
            if ( cabFex.ComprobanteNumero <= ultimoNro )
            {
                ConsultasWSFex consultas = new ConsultasWSFex( this.wsfex, this.logueador );
                ClsFEXGetCMPR consRet = consultas.DatosDeComprobante( autorizacion, cabFex.TipoComprobante, cabFex.ComprobanteNumero, cabFex.PuntoDeVenta );
                retorno = consRet.Id;
            }
            else
            {
                ConsultasWSFex consultas = new ConsultasWSFex( this.wsfex, this.logueador );
                retorno = consultas.UltimoId( autorizacion ) + 1;
            }
            return retorno;
        }

        public int ObtenerUltimoNumeroDeComprobanteX( Autorizacion autorizacion, int pventa, int tipoComprobante )
        {
            return (int) this.UltimoComprobanteFex( autorizacion, pventa, tipoComprobante );
        }

        private long UltimoComprobanteFex( Autorizacion autorizacion, int pventa, int tipoComprobante )
        {
            ConsultasWSFex consulta = new ConsultasWSFex( this.wsfex, this.logueador );
            return consulta.UltimoComprobante( autorizacion, pventa, tipoComprobante );
        }

        public List<UltimoNumeroComprobante> NumeracionPorComprobantesExportacion( Autorizacion autorizacion, int puntoDeVenta )
        {
            List<UltimoNumeroComprobante> retorno = new List<UltimoNumeroComprobante>();
            ConsultasWSFex consulta = new ConsultasWSFex( this.wsfex );
            try
            {
                List<Comprobante> comprobantes = consulta.ObtenerTiposDeComprobante( autorizacion );
                foreach ( Comprobante comprobante in comprobantes )
                {
                    long ultimoNumero = consulta.UltimoComprobante( autorizacion, puntoDeVenta, comprobante.Id );

                    retorno.Add( new UltimoNumeroComprobante() { TipoComprobante = comprobante.Id, UltimoNumero = (int) ultimoNumero } );
                }
            }
            catch ( ExcepcionFe error )
            {
                ValidacionException ex = new ValidacionException( "NumeracionPorComprobantes", error.Message );
                this.MensajeDeError = error.Message;
                throw ex;
            }
            return retorno;
        }

        public List<Incoterms> ObtenerValoresIncoterms( Autorizacion autorizacion )
        {
            ConsultasWSFex consulta = new ConsultasWSFex( this.wsfex );
            return consulta.ObtenerIncoterms( autorizacion );
        }

        public List<TipoMoneda> ObtenerValoresMonedas( Autorizacion autorizacion )
        {
            ConsultasWSFex consulta = new ConsultasWSFex( this.wsfex );
            return consulta.ObtenerTiposDeMoneda( autorizacion );
        }

        public List<Idioma> ObtenerValoresIdiomas( Autorizacion autorizacion )
        {
            ConsultasWSFex consulta = new ConsultasWSFex( this.wsfex );
            return consulta.ObtenerIdiomas( autorizacion );
        }

        public List<TipoExportacion> ObtenerValoresConceptos( Autorizacion autorizacion )
        {
            ConsultasWSFex consulta = new ConsultasWSFex( this.wsfex );
            return consulta.ObtenerTiposDeExportacion( autorizacion );
        }

        public List<UnidadDeMedida> ObtenerValoresUnidadDeMedida( Autorizacion autorizacion )
        {
            ConsultasWSFex consulta = new ConsultasWSFex( this.wsfex );
            return consulta.ObtenerTiposDeUnidadDeMedida( autorizacion );
        }

        public List<Pais> ObtenerCuitDePaises( Autorizacion autorizacion )
        {
            ConsultasWSFex consulta = new ConsultasWSFex( this.wsfex );

            List<Pais> paises = consulta.ObtenerCodigosPaises( autorizacion );
            List<CuitPais> cuits = consulta.ObtenerCuitDePaises( autorizacion );
            List<CuitPais> temporal;
            foreach ( Pais item in paises )
            {
                temporal = cuits.FindAll( x => x.Descripcion.ToUpper().Contains( item.Descripcion.ToUpper() + " " ) );
                item.Cuits = new List<CuitPais>();
                if ( temporal != null )
                {
                    foreach ( CuitPais cuit in temporal )
                    {
                        cuit.Descripcion = this.ExtraerTipoCuit( cuit.Descripcion );
                        item.Cuits.Add( cuit );
                    }
                }
            }
            return paises;
        }

        private string ExtraerTipoCuit( string descripcion )
        {
            string retorno;
            if ( descripcion.ToUpper().Contains( "PERSONA FÍSICA" ) )
            {
                retorno = "Persona Física";
            }
            else if ( descripcion.ToUpper().Contains( "PERSONA JURÍDICA" ) )
            {
                retorno = "Persona Jurídica";
            }
            else
            {
                retorno = "Otro tipo de entidad";
            }

            return retorno;
        }
    }
}
