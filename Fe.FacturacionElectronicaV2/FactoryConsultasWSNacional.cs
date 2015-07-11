using Fe.FacturacionElectronicaV2.Core.Logueos;
using Fe.FacturacionElectronicaV2.Nacional;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;
using Fe.FacturacionElectronicaMTXCA.Nacional.WebServices;
using Fe.FacturacionElectronicaMTXCA.Nacional;
using Fe.FacturacionElectronicaV2.ExportacionV0;
using Fe.FacturacionElectronicaV2.Exportacion.WebServices;

namespace Fe.FacturacionElectronicaV2
{
    public class FactoryConsultasWSNacional
    {
        public static ConsultasWS ObtenerInstancia( TipoWebService tipo, LogueadorFe logueador )
        {
            ConsultasWS retorno = null;
            switch ( tipo )
            {
                case TipoWebService.Nacional:
                    retorno = new ConsultasWSFe( new WSFEV1(), logueador );
                    break;
                case TipoWebService.MTXCA:
                    retorno = new ConsultasWSMTXCA( new WSMTXCA(), logueador );
                    break;
                default:
                    break;
            }
            return retorno;
        }
    }
}
