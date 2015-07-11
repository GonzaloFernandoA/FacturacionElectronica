using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fe.FacturacionElectronicaMTXCA.Nacional.WebServices;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Core;

namespace Fe.FacturacionElectronicaMTXCA.Nacional.Wrappers
{
    public class WrapperIvaMTXCA
    {
        public SubtotalIVAType Convertir( IVA iva )
        {
            SubtotalIVAType alicIva = new SubtotalIVAType();
            alicIva.codigo = (short) iva.Id;
            alicIva.importe = Redondeo.AplicarDecimal( iva.Importe );

            return alicIva;
        }
    }
}
