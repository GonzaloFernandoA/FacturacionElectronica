using System.Collections.Generic;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;

namespace Fe.FacturacionElectronicaV2.Nacional.Wrappers
{
    public class WrapperCaeRespuestaDetalle
    {
        public List<CAEDetalleRespuesta> Convertir( FECAEDetResponse[] detalle )
        {
            List<CAEDetalleRespuesta> detalleRespuesta = new List<CAEDetalleRespuesta>();
            for ( int i = 0; i < detalle.Length; i++ )
            {
                detalleRespuesta.Add( this.ConvertirUnDetalle( detalle[i] ) );
            }

            return detalleRespuesta;
        }

        private CAEDetalleRespuesta ConvertirUnDetalle( FECAEDetResponse det )
        {
            CAEDetalleRespuesta detResp = new CAEDetalleRespuesta();
            detResp.Concepto = det.Concepto;
            detResp.TipoDocumento = det.DocTipo;
            detResp.DocumentoNumero = det.DocNro;
            detResp.ComprobanteDesde = det.CbteDesde;
            detResp.ComprobanteHasta = det.CbteHasta;
            detResp.ComprobanteFecha = det.CbteFch;
            detResp.Resultado = det.Resultado;
            detResp.Cae = det.CAE;
            detResp.CaeFechaVencimiento = det.CAEFchVto;

            WrapperObservaciones wo = new WrapperObservaciones();
            detResp.Observaciones = wo.Convertir( det.Observaciones );

            return detResp;
        }
    }
}
