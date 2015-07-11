using System;
using Fe.FacturacionElectronicaV2.Core.LoginWSAA;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using System.Collections.Generic;
namespace Fe.FacturacionElectronicaV2
{
    public interface IFunciones
    {
        string MensajeDeError { get; }
        List<UltimoNumeroComprobante> NumeracionPorComprobantes( Autorizacion autorizacion, int puntoDeVenta );
        CAERespuestaFe ObtenerCae( Autorizacion autorizacion, FeCabecera cabFe );
        int ObtenerUltimoNumeroDeComprobante( Autorizacion autorizacion, int pventa, int tipoComprobante );
    }
}
