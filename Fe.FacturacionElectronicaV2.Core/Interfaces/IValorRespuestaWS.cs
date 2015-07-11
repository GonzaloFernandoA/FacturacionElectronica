using System;

namespace Fe.FacturacionElectronicaV2.Core.Interfaces
{
    public interface IValorRespuestaWS
    {
        string Equivalencia { get; set; }
        string Descripcion { get; set; }
        string ObtenerId();
    }
}
