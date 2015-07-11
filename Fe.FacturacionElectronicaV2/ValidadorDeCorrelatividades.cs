using System.Collections.Generic;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;

namespace Fe.FacturacionElectronicaV2
{
    public class ValidadorDeCorrelatividades
    {
        public FeDetalle Validar( List<FeDetalle> lotes )
        {
            long hasta = lotes[0].ComprobanteHasta;
            FeDetalle retorno = null;
            for ( int i = 1; i < lotes.Count; i++ )
            {
                if ( lotes[i].ComprobanteDesde != hasta + 1 )
                {
                    retorno = lotes[i];
                    break;
                }
                hasta = lotes[i].ComprobanteHasta;
            }

            return retorno;
        }
    }
}
