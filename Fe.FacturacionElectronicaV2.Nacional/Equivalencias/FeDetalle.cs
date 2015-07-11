using System.Collections.Generic;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.Core.Interfaces;

namespace Fe.FacturacionElectronicaV2.Nacional.Equivalencias
{
    public class FeDetalle : ISerializable
    {
        private int concepto;
        private int documentoTipo;
        private long documentoNumero = 0;
        private long comprobanteDesde;
        private long comprobanteHasta;
        private string comprobanteFecha;
        private double importeTotal = 0;
        private double importeNetoNoGravado = 0;
        private double importeNeto = 0;
        private double importeExento = 0;
        private double importeIVA = 0;
        private double importeTributos;
        private string fechaServicioDesde;
        private string fechaServicioHasta;
        private string fechaVencimientoDePago;
        private string monedaId;
        private double monedaCotizacion;
        private List<ComprobanteAsociado> comprobantesAsociados;
        private List<TributoComprobante> tributos = new List<TributoComprobante>();
        private List<IVA> iva = new List<IVA>();
        private int da_CondicionTitular;
        private int da_TipoDocumento;
        private string da_NumeroDocumento;
        private int da_Motivo;
        
        // MTXCA
        private List<Articulo> articulos = new List<Articulo>();

        #region getters/setters
        public List<ComprobanteAsociado> ComprobantesAsociados
        {
            get 
            {
                if ( this.comprobantesAsociados == null )
                {
                    this.comprobantesAsociados = new List<ComprobanteAsociado>();
                }
                return this.comprobantesAsociados; 
            }
            set { this.comprobantesAsociados = value; }
        }
        public List<TributoComprobante> Tributos
        {
            get 
            {
                if ( this.tributos == null )
                {
                    this.tributos = new List<TributoComprobante>();
                }
                return this.tributos; 
            }
            set { this.tributos = value; }
        }
        public List<Articulo> Articulos
        {
            get { return articulos; }
            set { articulos = value; }
        }
        public List<IVA> Iva
        {
            get { return this.iva; }
            set { this.iva = value; }
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
        public double ImporteTributos
        {
            get { return this.importeTributos; }
            set { this.importeTributos = value; }
        }
        public double ImporteNetoNoGravado
        {
            get { return this.importeNetoNoGravado; }
            set { this.importeNetoNoGravado = value; }
        }

        public double ImporteTotal
        {
            get { return this.importeTotal; }
            set { this.importeTotal = value; }
        }

        public long ComprobanteHasta
        {
            get { return this.comprobanteHasta; }
            set { this.comprobanteHasta = value; }
        }
        public long ComprobanteDesde
        {
            get { return this.comprobanteDesde; }
            set { this.comprobanteDesde = value; }
        }
        public long DocumentoNumero
        {
            get { return this.documentoNumero; }
            set { this.documentoNumero = value; }
        }
        public int DocumentoTipo
        {
            get { return this.documentoTipo; }
            set { this.documentoTipo = value; }
        }
        public int Concepto
        {
            get { return this.concepto; }
            set { this.concepto = value; }
        }
        public string ComprobanteFecha
        {
            get { return this.comprobanteFecha; }
            set { this.comprobanteFecha = value; }
        }
        public double ImporteExento
        {
            get { return this.importeExento; }
            set { this.importeExento = value; }
        }
        public double ImporteIVA
        {
            get { return this.importeIVA; }
            set { this.importeIVA = value; }
        }
        public double ImporteNeto
        {
            get { return this.importeNeto; }
            set { this.importeNeto = value; }
        }
        public string FechaServicioHasta
        {
            get { return this.fechaServicioHasta; }
            set { this.fechaServicioHasta = value; }
        }
        public string FechaServicioDesde
        {
            get { return this.fechaServicioDesde; }
            set { this.fechaServicioDesde = value; }
        }
        public string FechaVencimientoDePago
        {
            get { return this.fechaVencimientoDePago; }
            set { this.fechaVencimientoDePago = value; }
        }

        public int DA_CondicionTitular
        {
            get { return this.da_CondicionTitular; }
            set { this.da_CondicionTitular = value; }
        }

        public int DA_TipoDocumento
        {
            get { return this.da_TipoDocumento; }
            set { this.da_TipoDocumento = value; }
        }

        public string DA_NumeroDocumento
        {
            get { return this.da_NumeroDocumento; }
            set { this.da_NumeroDocumento = value; }
        }

        public int DA_Motivo
        {
            get { return this.da_Motivo; }
            set { this.da_Motivo = value; }
        }


        #endregion

        public void AgregarIva( int id, double baseImponible, double importe )
        {
            this.Iva.Add( new IVA( id, baseImponible, importe ) );
        }

        public void AgregarTributo( short id, string descripcion, double baseImponible, double alicuota, double importe )
        {
            this.Tributos.Add( new TributoComprobante( id, descripcion, baseImponible, alicuota, importe ) );
        }

        public void AgregarComprobante( int tipo, int puntoDeVenta, long nroComprobante, long cuit )
        {
            this.ComprobantesAsociados.Add( new ComprobanteAsociado( tipo, puntoDeVenta, nroComprobante, cuit ) );
        }

        public void AgregarArticulo( string descripcion, int unidadMedidaCodigo, int condicionIVACodigo, double importeItem )
        {
            this.articulos.Add( new Articulo( descripcion, unidadMedidaCodigo, condicionIVACodigo, importeItem ) );
        }

        public void AgregarArticulo(Articulo articulo)
        {
            this.articulos.Add(articulo);
        }

        public string Serializar()
        {
            string retorno = "";

            retorno = "Desde comprobante: " + this.comprobanteDesde.ToString() + "\r\n";
            retorno = retorno + "Hasta comprobante: " + this.comprobanteHasta.ToString() + "\r\n";

            return retorno;
        }
    }
}
