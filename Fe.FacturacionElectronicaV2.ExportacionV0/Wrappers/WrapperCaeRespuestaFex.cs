using Fe.FacturacionElectronicaV2.ExportacionV0.Equivalencias;
using Fe.FacturacionElectronicaV2.Exportacion;
using Fe.FacturacionElectronicaV2.Exportacion.WebServices;

namespace Fe.FacturacionElectronicaV2.ExportacionV0.Wrappers
{
    public class WrapperCaeRespuestaFex
    {
        public CAERespuestaFex Convertir( ClsFEXOutAuthorize auth )
        {
            CAERespuestaFex caeRespuestaFex = new CAERespuestaFex();
            caeRespuestaFex.Cae = auth.Cae;
            caeRespuestaFex.ComprobanteNumero = auth.Cbte_nro;
            caeRespuestaFex.Cuit = auth.Cuit;
            caeRespuestaFex.FechaComprobante = auth.Fch_cbte;
            caeRespuestaFex.FechaVencimientoCae = auth.Fch_venc_Cae;
            caeRespuestaFex.Id = auth.Id;
            caeRespuestaFex.MotivosObservaciones = auth.Motivos_Obs;
            caeRespuestaFex.PuntoDeVenta = auth.Punto_vta;
            caeRespuestaFex.Reproceso = auth.Reproceso;
            caeRespuestaFex.Resultado = auth.Resultado;
            caeRespuestaFex.TipoComprobante = auth.Cbte_tipo;

            return caeRespuestaFex;
        }
    }
}
