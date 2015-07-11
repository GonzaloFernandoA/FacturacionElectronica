using System.Collections.Generic;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.Core.LoginWSAA;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;
using Fe.FacturacionElectronicaMTXCA.Nacional.WebServices;
using System.Linq;

namespace Fe.FacturacionElectronicaV2.Nacional
{
    public class ConversorConsultaCaeARespuestaCae
    {
        public CAEDetalleRespuesta ConvertirDetalle( FECompConsResponse origen )
        {
            CAEDetalleRespuesta destino = new CAEDetalleRespuesta();

            destino.Cae = origen.CodAutorizacion;
            destino.CaeFechaVencimiento = origen.FchVto;
            destino.ComprobanteDesde = origen.CbteDesde;
            destino.ComprobanteHasta = origen.CbteHasta;
            destino.ComprobanteFecha = origen.CbteFch;
            destino.Concepto = origen.Concepto;
            destino.DocumentoNumero = origen.DocNro;
            destino.Resultado = origen.Resultado;
            destino.TipoDocumento = origen.DocTipo;
            destino.ImporteTotal = origen.ImpTotal;
            destino.ImporteExento = origen.ImpOpEx;
            destino.ImporteIVA = origen.ImpIVA;
            destino.ImporteNeto = origen.ImpNeto;
            destino.ImporteTotalConceptos = origen.ImpTotConc;
            destino.ImporteTributos = origen.ImpTrib;
            destino.Observaciones = this.ConvertirObservaciones( origen );

            return destino;
        }

        public CAEDetalleRespuesta ConvertirDetalle( ComprobanteType origen )
        {
            CAEDetalleRespuesta destino = new CAEDetalleRespuesta();

            destino.Cae = origen.codigoAutorizacion.ToString();
            destino.CaeFechaVencimiento = origen.fechaVencimiento.ToString( "yyyyMMdd" );
            destino.ComprobanteDesde = origen.numeroComprobante;
            destino.ComprobanteHasta = origen.numeroComprobante;
            destino.ComprobanteFecha = origen.fechaEmision.ToString( "yyyyMMdd" );
            destino.Concepto = origen.codigoConcepto;
            destino.DocumentoNumero = origen.numeroDocumento;
            destino.Resultado = origen.codigoTipoAutorizacion.ToString(); // ver si es este valor
            destino.TipoDocumento = origen.codigoTipoDocumento;
            destino.ImporteTotal = (double) origen.importeTotal;
            destino.ImporteExento = (double) origen.importeExento;
            destino.ImporteIVA = (double) origen.arraySubtotalesIVA.Sum( x => x.importe );
            destino.ImporteNeto = (double) origen.importeGravado;
            //destino.ImporteTotalConceptos = origen.impo;
            destino.ImporteTributos = (double) origen.importeOtrosTributos;
            destino.Observaciones = new List<Observacion>();
            if ( !string.IsNullOrEmpty( origen.observaciones ) )
            {
                destino.Observaciones.Add( new Observacion() { Mensaje = origen.observaciones } );
            }

            return destino;
        }

        private List<Observacion> ConvertirObservaciones( FECompConsResponse origen )
        {
            List<Observacion> retorno = null;
            if ( origen.Observaciones != null && origen.Observaciones.Length > 0 )
            {
                retorno = new List<Observacion>();
                Observacion observacion;
                foreach ( Obs obser in origen.Observaciones )
                {
                    observacion = new Observacion();
                    observacion.Codigo = obser.Code;
                    observacion.Mensaje = obser.Msg;

                    retorno.Add( observacion );
                }
            }

            return retorno;
        }

        public CAECabeceraRespuesta ConvertirCabecera( FeCabecera feCab, Autorizacion feAut )
        {
            CAECabeceraRespuesta caeCab = new CAECabeceraRespuesta();
            caeCab.PuntoDeVenta = feCab.PuntoDeVenta;
            caeCab.TipoComprobante = feCab.TipoComprobante;
            caeCab.CantidadDeRegistros = feCab.CantidadDeRegistros;
            caeCab.Cuit = feAut.Cuit;

            return caeCab;
        }
    }
}
