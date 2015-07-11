using System.Collections.Generic;
using Fe.FacturacionElectronicaV2.Core.Wrappers;
using Fe.FacturacionElectronicaV2.Core;
using Fe.FacturacionElectronicaV2.Core.Logueos;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.Core.Interfaces;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Nacional.Wrappers;
using System.Linq;
using Fe.FacturacionElectronicaV2.Core.LoginWSAA;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;
using System.Web.Services.Protocols;
using Fe.FacturacionElectronicaMTXCA.Nacional.WebServices;

namespace Fe.FacturacionElectronicaV2.Nacional
{
    public class ConsultasWSFe : ConsultasWS
    {
        private WSFEV1 wsfe;
        private WrapperAutorizacion wa;
        private ManagerErroresFe managerErrores;
        private LogueadorFe logueador;

        public ConsultasWSFe ( WSFEV1 wsfe )
	    {
            this.wsfe = wsfe;
            this.wa = new WrapperAutorizacion();
            this.logueador = new LogueadorFe();
            this.managerErrores = new ManagerErroresFe( this.logueador );
        }

        public ConsultasWSFe( WSFEV1 wsfe, LogueadorFe logueador )
        {
            this.wsfe = wsfe;
            this.wa = new WrapperAutorizacion();
            this.logueador = logueador;
            this.managerErrores = new ManagerErroresFe( this.logueador );
        }

        #region funciones de consulta para equivalencias
        public List<IValorRespuestaWS> ObtenerTiposDeMoneda( Autorizacion aut )
        {
            FEAuthRequest feAutRequest = this.wa.ConvertirFe( aut );
            MonedaResponse monedas = this.wsfe.FEParamGetTiposMonedas( feAutRequest );
            this.managerErrores.CapturarError( monedas.Errors );
            List<IValorRespuestaWS> tiposMoneda = new List<IValorRespuestaWS>();
            TipoMoneda tipoMoneda;
            foreach ( Moneda resultado in monedas.ResultGet )
            {
                tipoMoneda = new TipoMoneda();
                tipoMoneda.Id = resultado.Id;
                tipoMoneda.Descripcion = resultado.Desc;
                tiposMoneda.Add( tipoMoneda );
            }

            return tiposMoneda;
        }

        public override List<IValorRespuestaWS> ObtenerTiposDeComprobante( Autorizacion aut )
        {
            FEAuthRequest feAutRequest = this.wa.ConvertirFe( aut );
            CbteTipoResponse comprobantes = this.wsfe.FEParamGetTiposCbte( feAutRequest );
            this.managerErrores.CapturarError( comprobantes.Errors );
            List<IValorRespuestaWS> tiposComprobante = new List<IValorRespuestaWS>();
            Comprobante comprobante;
            foreach ( CbteTipo comp in comprobantes.ResultGet )
            {
                comprobante = new Comprobante();
                comprobante.Id = comp.Id;
                comprobante.Descripcion = comp.Desc;
                tiposComprobante.Add( comprobante );
            }
            return tiposComprobante;
        }

        public List<IValorRespuestaWS> ObtenerTiposDeConcepto( Autorizacion aut )
        {
            FEAuthRequest feAutRequest = this.wa.ConvertirFe( aut );
            ConceptoTipoResponse conceptos = this.wsfe.FEParamGetTiposConcepto( feAutRequest );
            this.managerErrores.CapturarError( conceptos.Errors );
            List<IValorRespuestaWS> tiposConcepto = new List<IValorRespuestaWS>();
            Concepto concepto;
            foreach ( ConceptoTipo comp in conceptos.ResultGet )
            {
                concepto = new Concepto();
                concepto.Id = comp.Id;
                concepto.Descripcion = comp.Desc;
                tiposConcepto.Add( concepto );
            }
            return tiposConcepto;
        }

        public List<IValorRespuestaWS> ObtenerTiposDeDocumento( Autorizacion aut )
        {
            FEAuthRequest feAutRequest = this.wa.ConvertirFe( aut );
            DocTipoResponse documentos = this.wsfe.FEParamGetTiposDoc( feAutRequest );
            this.managerErrores.CapturarError( documentos.Errors );
            List<IValorRespuestaWS> tiposDocumento = new List<IValorRespuestaWS>();
            TipoDeDocumento tipoDeDocumento;
            foreach ( DocTipo doc in documentos.ResultGet )
            {
                tipoDeDocumento = new TipoDeDocumento();
                tipoDeDocumento.Id = doc.Id;
                tipoDeDocumento.Descripcion = doc.Desc;
                tiposDocumento.Add( tipoDeDocumento );
            }
            return tiposDocumento;
        }

        public List<IValorRespuestaWS> ObtenerTiposDeIva( Autorizacion aut )
        {
            FEAuthRequest feAutRequest = this.wa.ConvertirFe( aut );
            IvaTipoResponse ivas = this.wsfe.FEParamGetTiposIva( feAutRequest );
            this.managerErrores.CapturarError( ivas.Errors );
            List<IValorRespuestaWS> tiposIva = new List<IValorRespuestaWS>();
            TipoDeIva tipoDeIva;
            foreach ( IvaTipo iva in ivas.ResultGet )
            {
                tipoDeIva = new TipoDeIva();
                tipoDeIva.Id = iva.Id;
                tipoDeIva.Descripcion = iva.Desc;
                tiposIva.Add( tipoDeIva );
            }
            return tiposIva;
        }

        public List<IValorRespuestaWS> ObtenerTiposDeTributo( Autorizacion aut )
        {
            FEAuthRequest feAutRequest = this.wa.ConvertirFe( aut );
            FETributoResponse tributos = this.wsfe.FEParamGetTiposTributos( feAutRequest );
            this.managerErrores.CapturarError( tributos.Errors );
            List<IValorRespuestaWS> tiposTributo = new List<IValorRespuestaWS>();
            TiposTributo tipoTributos;
            foreach ( TributoTipo tributo in tributos.ResultGet )
            {
                tipoTributos = new TiposTributo();
                tipoTributos.Id = tributo.Id;
                tipoTributos.Descripcion = tributo.Desc;
                tiposTributo.Add( tipoTributos );
            }
            return tiposTributo;
        }

        #endregion

        #region Otras Consultas
        public override int UltimoComprobante( Autorizacion aut, int ptovta, int tipo )
        {
            FEAuthRequest feAutRequest = this.wa.ConvertirFe( aut );
            FERecuperaLastCbteResponse respuesta = this.wsfe.FECompUltimoAutorizado( feAutRequest, ptovta, (int)tipo );
            this.managerErrores.CapturarError( respuesta.Errors );

            return respuesta.CbteNro;
        }

        public CAEConsulta ConsultarCAEOtorgado( Autorizacion aut, int periodo, short orden )
        {
            FEAuthRequest feAutRequest = this.wa.ConvertirFe( aut );
            FECAEAGetResponse respuesta = this.wsfe.FECAEAConsultar( feAutRequest, periodo, orden );
            this.managerErrores.CapturarError( respuesta.Errors );
            WrapperCaeConsulta wcc = new WrapperCaeConsulta();

            return wcc.Convertir( respuesta.ResultGet );
        }

        public double ConsultarCotizacion( Autorizacion aut, string idMoneda )
        {
            FEAuthRequest feAutRequest = this.wa.ConvertirFe( aut );
            FECotizacionResponse respuesta = this.wsfe.FEParamGetCotizacion( feAutRequest, idMoneda );
            this.managerErrores.CapturarError( respuesta.Errors );

            return respuesta.ResultGet.MonCotiz;
        }

        public int CantidadMaximaDeRegistrosSolicitud( Autorizacion aut )
        {
            FEAuthRequest feAutRequest = this.wa.ConvertirFe( aut );
            FERegXReqResponse respuesta = this.wsfe.FECompTotXRequest( feAutRequest );
            this.managerErrores.CapturarError( respuesta.Errors );
 
            return respuesta.RegXReq;
        }

        /// <summary>
        /// Obtiene informacion de respuesta de autorizacion para un comprobante
        /// </summary>
        /// <param name="aut">Autorizacion</param>
        /// <param name="tipoComprobante">Tipo de Comprobante</param>
        /// <param name="nroComprobante">Nro de Comprobante</param>
        /// <param name="ptoVta">Punto de Venta</param>
        /// <returns>Respuesta simil a la de solicitud de CAE</returns>
        public override CAEDetalleRespuesta DatosDeComprobante( Autorizacion aut, int tipoComprobante, int nroComprobante, int ptoVta )
        {
            FEAuthRequest feAutRequest = this.wa.ConvertirFe( aut );
            FECompConsultaReq solicitud = new FECompConsultaReq();
            solicitud.CbteNro = nroComprobante;
            solicitud.CbteTipo = tipoComprobante;
            solicitud.PtoVta = ptoVta;
            FECompConsultaResponse respuesta = this.wsfe.FECompConsultar( feAutRequest, solicitud );
            this.managerErrores.CapturarError( respuesta.Errors );

            ConversorConsultaCaeARespuestaCae conversor = new ConversorConsultaCaeARespuestaCae();
            CAEDetalleRespuesta respuestaConvertida = conversor.ConvertirDetalle( respuesta.ResultGet );

            return respuestaConvertida;
        }

        public List<PuntoDeVenta> ObtenerPuntosDeVenta( Autorizacion aut )
        {
            FEAuthRequest feAutRequest = this.wa.ConvertirFe( aut );
            FEPtoVentaResponse respuesta = this.wsfe.FEParamGetPtosVenta( feAutRequest );
            this.managerErrores.CapturarError( respuesta.Errors );
            List<PuntoDeVenta> puntosDeVenta = new List<PuntoDeVenta>();
            PuntoDeVenta puntoDeVenta;
            foreach ( PtoVenta punto in respuesta.ResultGet )
            {
                puntoDeVenta = new PuntoDeVenta();
                puntoDeVenta.Numero = punto.Nro;
                puntoDeVenta.Bloqueado = punto.Bloqueado;
                puntoDeVenta.EmisionTipo = punto.EmisionTipo;
                puntosDeVenta.Add( puntoDeVenta );
            }
            return puntosDeVenta;
        }
        #endregion

        public override SoapHttpClientProtocol ObtenerWS()
        {
            return this.wsfe;
        }

        public override CodigoDescripcionType[] ObtenerUnidadesDeMedida( Autorizacion aut )
        {
            throw new System.NotImplementedException();
        }
    }
}