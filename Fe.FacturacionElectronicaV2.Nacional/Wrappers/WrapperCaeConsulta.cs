using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;

namespace Fe.FacturacionElectronicaV2.Nacional.Wrappers
{
    public class WrapperCaeConsulta
    {
        public CAEConsulta Convertir( FECAEAGet caeAGet )
        {
            CAEConsulta caeConsulta = new CAEConsulta();
            caeConsulta.CAEA = caeAGet.CAEA;
            caeConsulta.FechaProceso = caeAGet.FchProceso;
            caeConsulta.FechaTopeParaInformar = caeAGet.FchTopeInf;
            caeConsulta.FechaVigenciaDesde = caeAGet.FchVigDesde;
            caeConsulta.FechaVigenciaHasta = caeAGet.FchVigHasta;
            caeConsulta.Orden = caeAGet.Orden;
            caeConsulta.Periodo = caeAGet.Periodo;

            return caeConsulta;
        }
    }
}
