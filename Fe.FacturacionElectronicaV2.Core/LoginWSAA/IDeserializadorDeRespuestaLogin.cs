using System;

namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public interface IDeserializadorDeRespuestaLogin
    {
        Autorizacion Deserializar( string resultadoLogin );
    }
}
