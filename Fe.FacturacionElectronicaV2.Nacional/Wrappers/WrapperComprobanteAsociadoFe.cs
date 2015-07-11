using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;

namespace Fe.FacturacionElectronicaV2.Nacional.Wrappers
{
    public class WrapperComprobanteAsociadoFe
    {
        public CbteAsoc Convertir( ComprobanteAsociado comprobante )
        {
            CbteAsoc comprobanteAsoc = new CbteAsoc();
            comprobanteAsoc.Tipo = (int)comprobante.Tipo;
            comprobanteAsoc.PtoVta = comprobante.PuntoDeVenta;
            comprobanteAsoc.Nro = comprobante.NumeroComprobante;

            return comprobanteAsoc;
        }
    }
}
