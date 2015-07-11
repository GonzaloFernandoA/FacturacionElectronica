using Fe.FacturacionElectronicaV2.Core;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;

namespace Fe.FacturacionElectronicaV2.Nacional.Wrappers
{
    public class WrapperIvaFe
    {
        public AlicIva Convertir( IVA iva )
        {
            AlicIva alicIva = new AlicIva();
            alicIva.Id = iva.Id;
            alicIva.BaseImp = Redondeo.Aplicar( iva.BaseImponible );
            alicIva.Importe = Redondeo.Aplicar( iva.Importe );
             
            return alicIva;
        }

    }
}
