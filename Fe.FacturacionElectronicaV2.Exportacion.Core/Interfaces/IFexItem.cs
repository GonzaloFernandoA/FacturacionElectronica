using System;
using Fe.FacturacionElectronicaV2.Core.Interfaces;

namespace Fe.FacturacionElectronicaV2.Exportacion.Core
{
    public interface IFexItem : ISerializable
    {
        double ProductoCantidad { get; set; }
        string ProductoCodigo { get; set; }
        string ProductoDescripcion { get; set; }
        double ProductoImporteTotal { get; set; }
        double ProductoPrecioUnitario { get; set; }
        int ProductoUnidadDeMedida { get; set; }
    }
}
