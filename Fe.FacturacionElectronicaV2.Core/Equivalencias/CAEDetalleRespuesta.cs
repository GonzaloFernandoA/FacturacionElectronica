using System.Collections.Generic;


namespace Fe.FacturacionElectronicaV2.Core.Equivalencias
{
    public class CAEDetalleRespuesta
    {
        private int concepto;
        private int documentoTipo;
        private long documentoNumero;
        private long comprobanteDesde;
        private long comprobanteHasta;
        private string comprobanteFecha;
        private string resultado;
        private string cae;
        private string caeFechaVencimiento;
        private List<Observacion> observaciones;
        private double importeTotal;
        private double importeNeto;
        private double importeTotalConceptos;
        private double importeIVA;
        private double importeExento;
        private double importeTributos;

        #region getters/setters
        public double ImporteTributos
        {
            get { return importeTributos; }
            set { importeTributos = value; }
        }

        public double ImporteExento
        {
            get { return importeExento; }
            set { importeExento = value; }
        }

        public double ImporteIVA
        {
            get { return importeIVA; }
            set { importeIVA = value; }
        }

        public double ImporteTotalConceptos
        {
            get { return importeTotalConceptos; }
            set { importeTotalConceptos = value; }
        }

        public double ImporteNeto
        {
            get { return importeNeto; }
            set { importeNeto = value; }
        }
        public double ImporteTotal
        {
            get { return importeTotal; }
            set { importeTotal = value; }
        }
        public List<Observacion> Observaciones
        {
            get { return this.observaciones; }
            set { this.observaciones = value; }
        }

        public string CaeFechaVencimiento
        {
            get { return this.caeFechaVencimiento; }
            set { this.caeFechaVencimiento = value; }
        }
        public string Cae
        {
            get { return this.cae; }
            set { this.cae = value; }
        }
        public string Resultado
        {
            get { return this.resultado; }
            set { this.resultado = value; }
        }
        public string ComprobanteFecha
        {
            get { return this.comprobanteFecha; }
            set { this.comprobanteFecha = value; }
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

        public int TipoDocumento
        {
            get { return this.documentoTipo; }
            set { this.documentoTipo = value; }
        }

        public int Concepto
        {
            get { return this.concepto; }
            set { this.concepto = value; }
        }
        #endregion
    }
}
