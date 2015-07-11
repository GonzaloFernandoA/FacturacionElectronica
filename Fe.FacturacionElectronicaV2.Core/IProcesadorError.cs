using System;
namespace Fe.FacturacionElectronicaV2.Core
{
    public interface IProcesadorError
    {
        string Procesar( ref ExcepcionFe ex );
    }
}
