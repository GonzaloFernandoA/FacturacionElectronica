using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fe.FacturacionElectronicaMTXCA.Nacional.WebServices;
using Fe.FacturacionElectronicaV2.Nacional;
using Fe.FacturacionElectronicaV2.Core.Wrappers;
using Fe.FacturacionElectronicaV2.Core.Logueos;
using Fe.FacturacionElectronicaV2.Core;
using Fe.FacturacionElectronicaV2.Core.LoginWSAA;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.Core.Interfaces;
using System.Web.Services.Protocols;

namespace Fe.FacturacionElectronicaMTXCA.Nacional
{
    public class ConsultasWSMTXCA : ConsultasWS
    {
        private WSMTXCA ws;
        private WrapperAutorizacion wa;
        private ManagerErroresFe managerErrores;
        private LogueadorFe logueador;

        public ConsultasWSMTXCA( WSMTXCA ws )
        {
            this.ws = ws;
            this.wa = new WrapperAutorizacion();
            this.logueador = new LogueadorFe();
            this.managerErrores = new ManagerErroresFe( this.logueador );
        }

        public ConsultasWSMTXCA( WSMTXCA ws, LogueadorFe logueador )
        {
            this.ws = ws;
            this.wa = new WrapperAutorizacion();
            this.logueador = logueador;
            this.managerErrores = new ManagerErroresFe( this.logueador );
        }

        public override int UltimoComprobante( Autorizacion aut, int ptovta, int tipo )
        {
            AuthRequestType feAutRequest = this.wa.ConvertirMTXCA( aut );
            ConsultaUltimoComprobanteAutorizadoRequestType consReqType = new ConsultaUltimoComprobanteAutorizadoRequestType();
            consReqType.codigoTipoComprobante = (short) tipo;
            consReqType.numeroPuntoVenta = (short) ptovta;
            long ultimoNro;
            bool numeroEspecificado;
            CodigoDescripcionType[] errores;
            CodigoDescripcionType evento;
            
            this.ws.consultarUltimoComprobanteAutorizado( feAutRequest, consReqType, out ultimoNro, out numeroEspecificado, out errores, out evento );
            this.managerErrores.CapturarError( errores );

            return (int) ultimoNro;
        }

        public override List<IValorRespuestaWS> ObtenerTiposDeComprobante( Autorizacion aut )
        {
            AuthRequestType feAutRequest = this.wa.ConvertirMTXCA( aut );
            CodigoDescripcionType evento;
            CodigoDescripcionType[] comprobantes = this.ws.consultarTiposComprobante( feAutRequest, out evento );

            List<IValorRespuestaWS> tiposComprobante = new List<IValorRespuestaWS>();
            Comprobante comprobante;
            foreach ( CodigoDescripcionType comp in comprobantes )
            {
                comprobante = new Comprobante();
                comprobante.Id = comp.codigo;
                comprobante.Descripcion = comp.descripcion;
                tiposComprobante.Add( comprobante );
            }

            return tiposComprobante;
        }

        public override SoapHttpClientProtocol ObtenerWS()
        {
            return this.ws;
        }

        public override CAEDetalleRespuesta DatosDeComprobante( Autorizacion aut, int tipoComprobante, int nroComprobante, int ptoVta )
        {
            AuthRequestType feAutRequest = this.wa.ConvertirMTXCA( aut );
            ConsultaComprobanteRequestType solicitud = new ConsultaComprobanteRequestType();
            solicitud.numeroComprobante = nroComprobante;
            solicitud.codigoTipoComprobante = (short) tipoComprobante;
            solicitud.numeroPuntoVenta = (short) ptoVta;
            
            CodigoDescripcionType[] observaciones;
            CodigoDescripcionType[] errores;
            CodigoDescripcionType evento;

            ComprobanteType respuesta = this.ws.consultarComprobante( feAutRequest, solicitud, out observaciones, out errores, out evento );
            this.managerErrores.CapturarError( errores );

            ConversorConsultaCaeARespuestaCae conversor = new ConversorConsultaCaeARespuestaCae();
            CAEDetalleRespuesta respuestaConvertida = conversor.ConvertirDetalle( respuesta );

            return respuestaConvertida;
        }
        public override CodigoDescripcionType[] ObtenerUnidadesDeMedida( Autorizacion aut )
        {
            AuthRequestType feAutRequest = this.wa.ConvertirMTXCA( aut );
            WSMTXCA ws = new WSMTXCA();
            CodigoDescripcionType evento;
            CodigoDescripcionType[] consulta;

            consulta = this.ws.consultarUnidadesMedida( feAutRequest, out evento );

            return consulta;
        }
    }
}
