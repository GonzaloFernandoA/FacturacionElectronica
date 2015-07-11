using System;

namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public interface IManejadorDeErroresWSAA
    {
        void ManejarError( Exception ex, string metodo, string mensajeLoco );
    }
}
