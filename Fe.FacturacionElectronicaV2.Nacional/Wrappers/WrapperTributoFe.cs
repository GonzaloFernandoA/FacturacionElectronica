using Fe.FacturacionElectronicaV2.Core;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;

namespace Fe.FacturacionElectronicaV2.Wrappers
{
    public class WrapperTributoFe
    {
        public Tributo Convertir( TributoComprobante tributoComprobante )
        {
            Tributo tributo = new Tributo();
            tributo.Id =  (short) tributoComprobante.Id;
            tributo.Desc = tributoComprobante.Descripcion;
            tributo.BaseImp = Redondeo.Aplicar( tributoComprobante.BaseImponible );
            tributo.Alic = tributoComprobante.Alicuota;
            tributo.Importe = Redondeo.Aplicar( tributoComprobante.Importe );
            
            return tributo;
        }
    }
}
