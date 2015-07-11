using System;

namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public interface IValidadorDeConfiguracion
    {
        void Validar( IConfiguracionWS configuracion );
    }
}
