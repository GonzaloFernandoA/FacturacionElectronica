using ZooLogicSA.FacturacionElectronicaV2.ExportacionV1.Equivalencias;

namespace ZooLogicSA.FacturacionElectronicaV2.ExportacionV1.Wrappers
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
            caeRespuestaFex.TipoComprobante = auth.Tipo_cbte;

            return caeRespuestaFex;
        }
    }
}
