using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fe.FacturacionElectronicaMTXCA.Nacional.WebServices;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Core;

namespace Fe.FacturacionElectronicaMTXCA.Nacional.Wrappers
{
    public class WrapperTributoMTXCA
    {
        public OtroTributoType Convertir( TributoComprobante tributoComprobante )
        {
            OtroTributoType tributo = new OtroTributoType();
            tributo.codigo =  (short) tributoComprobante.Id;
            tributo.descripcion = tributoComprobante.Descripcion;
            tributo.baseImponible = Redondeo.AplicarDecimal( tributoComprobante.BaseImponible );
            tributo.importe = Redondeo.AplicarDecimal( tributoComprobante.Importe );
            
            return tributo;
        }
    }
}
