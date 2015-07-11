using System;

namespace Fe.FacturacionElectronicaV2.Core
{
    public class ClasificacionDeComprobantes
    {

        public String ObtenerTipoYLetraDeComprobante(int tipoDeComprobante)
        {
            String retorno;

            switch (tipoDeComprobante)
            {
                case 1:
                    retorno = "FC_A";
                    break;
                case 2:
                    retorno = "ND_A";
                    break;
                case 3:
                    retorno = "NC_A";
                    break;
                case 6:
                    retorno = "FC_B";
                    break;
                case 7:
                    retorno = "ND_B";
                    break;
                case 8:
                    retorno = "NC_B";
                    break;
                case 11:
                    retorno = "FC_C";
                    break;
                case 12:
                    retorno = "ND_C";
                    break;
                case 13:
                    retorno = "NC_C";
                    break;
                case 19:
                    retorno = "FC_E";
                    break;                    
                case 20:
                    retorno = "ND_E";
                    break;                    
                case 21:
                    retorno = "NC_E";
                    break;
                default:
                    retorno = ""; 
                    break;
            }
            return retorno;
        }
    }
}
