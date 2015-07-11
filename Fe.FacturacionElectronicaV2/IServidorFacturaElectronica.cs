using System;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Core.LoginWSAA;

namespace Fe.FacturacionElectronicaV2
{
    public interface IServidorFacturaElectronica
    {
        CAERespuestaFe ObtenerCae( FeCabecera cabFe, Autorizacion aut );
        CAERespuestaFe ReprocesarComprobantes( FeCabecera cabFe, Autorizacion feAut );
        bool ServidorActivo();
    }
}
