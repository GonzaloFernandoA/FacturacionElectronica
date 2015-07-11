using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Nacional.Wrappers;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;

namespace Fe.FacturacionElectronicaV2.Wrappers
{
    public class WrapperCaeRespuestaFe
    {
        public CAERespuestaFe Convertir( FECAEResponse caeResp )
        {
            CAERespuestaFe respuesta = new CAERespuestaFe();

            WrapperCaeRespuestaCabecera wcrc = new WrapperCaeRespuestaCabecera();
            respuesta.Cabecera = wcrc.Convertir( caeResp.FeCabResp );

            WrapperCaeRespuestaDetalle wcrd = new WrapperCaeRespuestaDetalle();
            respuesta.Detalle = wcrd.Convertir( caeResp.FeDetResp );

            return respuesta;
        }

    }
}
