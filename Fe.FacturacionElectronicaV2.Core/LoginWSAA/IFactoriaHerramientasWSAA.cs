using System;

namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public interface IFactoriaHerramientasWSAA
    {
        IAccesoWeb ObtenerAccesoWeb();
        IGeneradorTRA ObtenerGeneradorTRA();
        ServidorAutenticacion ObtenerServidorAutenticacion( ConfiguracionWS config );
        IWSAAProxy ObtenerWSAA( IConfiguracionWS config );
        IFirmadorDeCertificado ObtenerFirmadorDeCertificados();
        IManejadorDeErroresWSAA ObtenerManejadorErrores();
        IValidadorDeConfiguracion ObtenerValidadorDeConfiguracion();
        IDeserializadorDeRespuestaLogin ObtenerDeserializadorDeRespuestaLogin( IConfiguracionWS config );
    }
}
