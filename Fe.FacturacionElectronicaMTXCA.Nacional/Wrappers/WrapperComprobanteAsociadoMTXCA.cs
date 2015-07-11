using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fe.FacturacionElectronicaMTXCA.Nacional.WebServices;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;

namespace Fe.FacturacionElectronicaMTXCA.Nacional.Wrappers
{
    public class WrapperComprobanteAsociadoMTXCA
    {
        public ComprobanteAsociadoType Convertir( ComprobanteAsociado comprobante )
        {
            ComprobanteAsociadoType comprobanteAsoc = new ComprobanteAsociadoType();
            comprobanteAsoc.codigoTipoComprobante = (short) comprobante.Tipo;
            comprobanteAsoc.numeroPuntoVenta = (short) comprobante.PuntoDeVenta;
            comprobanteAsoc.numeroComprobante = comprobante.NumeroComprobante;

            return comprobanteAsoc;
        }
    }
}
