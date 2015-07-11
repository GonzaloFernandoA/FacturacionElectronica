using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using System.Collections.Generic;

namespace Fe.FacturacionElectronicaV2.Nacional.Wrappers
{
    public class WrapperCabeceraFe
    {
        public FECAERequest Convertir( FeCabecera cabFe )
        {
            FECAECabRequest cab = this.ConvertirCabecera( cabFe );

            int i = 0;
            FECAEDetRequest[] detalleComprobantes = new FECAEDetRequest[cabFe.DetalleComprobantes.Count];
            WrapperDetalleFe wdf = new WrapperDetalleFe();
            foreach ( FeDetalle detalle in cabFe.DetalleComprobantes )
            {
                detalleComprobantes[i] = wdf.Convertir( detalle );
                i++;
            }

            FECAERequest feCaeRequest = new FECAERequest();
            feCaeRequest.FeCabReq = cab;
            feCaeRequest.FeDetReq = detalleComprobantes;

            return feCaeRequest;            
        }

        private FECAECabRequest ConvertirCabecera( FeCabecera cabFe )
        {
            FECAECabRequest cab = new FECAECabRequest();
            cab.CantReg = cabFe.CantidadDeRegistros;
            cab.CbteTipo = (int)cabFe.TipoComprobante;
            cab.PtoVta = cabFe.PuntoDeVenta;

            return cab;
        }

        public List<Observacion> Comparar( CAEDetalleRespuesta comprobanteAfip, FeDetalle comprobante )
        {
            BuscadorDeDiferencias buscador = new BuscadorDeDiferencias();
            return buscador.Obtener( comprobanteAfip, comprobante );
        }
    }
}
