using System.Collections.Generic;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;

namespace Fe.FacturacionElectronicaV2.Nacional.Wrappers
{
    public class WrapperObservaciones
    {
        public List<Observacion> Convertir( Obs[] observaciones )
        {
            List<Observacion> obserResp = null;
            if ( observaciones != null )
            {
                obserResp = new List<Observacion>();
                Observacion obs;
                for ( int i = 0; i < observaciones.Length; i++ )
                {
                    obs = new Observacion();
                    obs.Codigo = observaciones[i].Code;
                    obs.Mensaje = observaciones[i].Msg;

                    obserResp.Add( obs );
                }
            }

            return obserResp;
        }
    }
}
