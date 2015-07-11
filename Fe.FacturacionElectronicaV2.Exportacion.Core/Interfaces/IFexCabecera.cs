using System;
using System.Collections.Generic;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.Core.Interfaces;
using Fe.FacturacionElectronicaV2.Exportacion.Core.DatosSegunTabla;

namespace Fe.FacturacionElectronicaV2.Exportacion.Core
{
    public interface IFexCabecera : ISerializable
    {
        string ClausulaDeVenta { get; set; }
        string ClausulaDeVentaInformacionComplementaria { get; set; }
        string Cliente { get; set; }
        long ComprobanteNumero { get; set; }
        List<ComprobanteAsociado> ComprobantesAsociados { get; set; }
        long CuitPaisCliente { get; set; }
        string DomicilioCliente { get; set; }
        string FechaComprobante { get; set; }
        string FormaDePagoDescripcion { get; set; }
        long Id { get; set; }
        string IdImpositivo { get; set; }
        IdiomasComprobante IdiomaComprobante { get; set; }
        double ImporteTotal { get; set; }
        List<IFexItem> Items { get; set; }
        double MonedaCotizacion { get; set; }
        string MonedaId { get; set; }
        string Observaciones { get; set; }
        string ObservacionesComerciales { get; set; }
        int PaisDestinoComprobante { get; set; }
        string PermisoExistente { get; set; }
        List<IPermisoDeEmbarque> Permisos { get; set; }
        int PuntoDeVenta { get; set; }
        int TipoComprobante { get; set; }
        int TipoExportacion { get; set; }
    }
}
