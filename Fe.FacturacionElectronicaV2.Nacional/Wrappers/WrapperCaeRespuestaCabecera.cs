using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;

namespace Fe.FacturacionElectronicaV2.Nacional.Wrappers
{
    public class WrapperCaeRespuestaCabecera
    {
        public CAECabeceraRespuesta Convertir( FECAECabResponse feCabResp )
        {
            CAECabeceraRespuesta cabResp = new CAECabeceraRespuesta();
            cabResp.Cuit = feCabResp.Cuit;
            cabResp.TipoComprobante = feCabResp.CbteTipo;
            cabResp.PuntoDeVenta = feCabResp.PtoVta;
            cabResp.FechaProceso = feCabResp.FchProceso;
            cabResp.CantidadDeRegistros = feCabResp.CantReg;
            cabResp.Resultado = feCabResp.Resultado;

            return cabResp;
        }
    }
}
