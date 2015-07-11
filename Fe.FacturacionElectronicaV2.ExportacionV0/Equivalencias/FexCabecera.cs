using System.Collections.Generic;
using Fe.FacturacionElectronicaV2.Core.Interfaces;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.Exportacion.Core;
using Fe.FacturacionElectronicaV2.Exportacion.Core.DatosSegunTabla;

namespace Fe.FacturacionElectronicaV2.ExportacionV0.Equivalencias
{
    public class FexCabecera : ISerializable
    {
        private long id;
        private short tipoComprobante;
        private string fechaComprobante;
        private short puntoDeVenta;
        private long comprobanteNumero;
        private int tipoExportacion;
        private string permisoExistente;
        private short paisDestinoComprobante;
        private string cliente;
        private long cuitPaisCliente;
        private string domicilioCliente;
        private string idImpositivo;
        private string monedaId;
        private double monedaCotizacion;
        private string observacionesComerciales;
        private double importeTotal;
        private string observaciones;
        private string formaDePagoDescripcion;
        private string clausulaDeVenta;
        private string clausulaDeVentaInformacionComplementaria;
        private IdiomasComprobante idiomaComprobante;
        private List<PermisoDeEmbarque> permisos = new List<PermisoDeEmbarque>();
        private List<ComprobanteAsociado> comprobantesAsociados = new List<ComprobanteAsociado>();
        private List<FexItem> items = new List<FexItem>();
 

        #region getters/setters
        public List<FexItem> Items
        {
            get { return this.items; }
            set { this.items = value; }
        }
        public List<ComprobanteAsociado> ComprobantesAsociados
        {
            get { return this.comprobantesAsociados; }
            set { this.comprobantesAsociados = value; }
        }

        public List<PermisoDeEmbarque> Permisos
        {
            get { return this.permisos; }
            set { this.permisos = value; }
        }

        public IdiomasComprobante IdiomaComprobante
        {
            get { return this.idiomaComprobante; }
            set { this.idiomaComprobante = value; }
        }
        public string ClausulaDeVentaInformacionComplementaria
        {
            get { return this.clausulaDeVentaInformacionComplementaria; }
            set { this.clausulaDeVentaInformacionComplementaria = value; }
        }
        public string ClausulaDeVenta
        {
            get { return this.clausulaDeVenta; }
            set { this.clausulaDeVenta = value; }
        }
        public string FormaDePagoDescripcion
        {
            get { return this.formaDePagoDescripcion; }
            set { this.formaDePagoDescripcion = value; }
        }
        public string Observaciones
        {
            get { return this.observaciones; }
            set { this.observaciones = value; }
        }
        public double ImporteTotal
        {
            get { return this.importeTotal; }
            set { this.importeTotal = value; }
        }
        public string ObservacionesComerciales
        {
            get { return this.observacionesComerciales; }
            set { this.observacionesComerciales = value; }
        }
        public double MonedaCotizacion
        {
            get { return this.monedaCotizacion; }
            set { this.monedaCotizacion = value; }
        }
        public string MonedaId
        {
            get { return this.monedaId; }
            set { this.monedaId = value; }
        }
        public string IdImpositivo
        {
            get { return this.idImpositivo; }
            set { this.idImpositivo = value; }
        }
        public string DomicilioCliente
        {
            get { return this.domicilioCliente; }
            set { this.domicilioCliente = value; }
        }
        public long CuitPaisCliente
        {
            get { return this.cuitPaisCliente; }
            set { this.cuitPaisCliente = value; }
        }
        public string Cliente
        {
            get { return this.cliente; }
            set { this.cliente = value; }
        }

        public int PaisDestinoComprobante
        {
            get { return this.paisDestinoComprobante; }
            set { this.paisDestinoComprobante = (short)value; }
        }
        public string PermisoExistente
        {
            get { return this.permisoExistente; }
            set { this.permisoExistente = value; }
        }

        public int TipoExportacion
        {
            get { return this.tipoExportacion; }
            set { this.tipoExportacion = value; }
        }


        public long ComprobanteNumero
        {
            get { return this.comprobanteNumero; }
            set { this.comprobanteNumero = value; }
        }
        public int PuntoDeVenta
        {
            get { return this.puntoDeVenta; }
            set { this.puntoDeVenta = (short)value; }
        }
        public string FechaComprobante
        {
            get { return this.fechaComprobante; }
            set { this.fechaComprobante = value; }
        }
        public int TipoComprobante
        {
            get { return this.tipoComprobante; }
            set { this.tipoComprobante = (short)value; }
        }
        public long Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        #endregion

        public string Serializar()
        {
            string retorno = "";

            retorno = "Punto de Venta: " + this.puntoDeVenta.ToString() + "\r\n";
            retorno = retorno + "Tipo Comprobante: " + this.tipoComprobante.ToString() + "\r\n";
            retorno = retorno + "Numero: " + this.comprobanteNumero.ToString() + "\r\n";

            foreach (FexItem item in this.items)
            {
                retorno = retorno + item.Serializar();

            }

            return retorno;
        }
    }
}
