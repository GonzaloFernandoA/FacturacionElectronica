
namespace Fe.FacturacionElectronicaV2.Core.Equivalencias
{
    public class ComprobanteAsociado
    {
        private int tipo;
        private int puntoDeVenta;
        private long numeroComprobante;
        private long cuit;

        #region getters/setters
        public long NumeroComprobante
        {
            get { return this.numeroComprobante; }
            set { this.numeroComprobante = value; }
        }
        public int PuntoDeVenta
        {
            get { return this.puntoDeVenta; }
            set { this.puntoDeVenta = value; }
        }
        public int Tipo
        {
            get { return this.tipo; }
            set { this.tipo = value; }
        }
        public long Cuit
        {
            get { return cuit; }
            set { cuit = value; }
        }
        #endregion

        public ComprobanteAsociado( int tipo, int puntoDeVenta, long numeroComprobante, long cuit )
        {
            this.tipo = tipo;
            this.puntoDeVenta = puntoDeVenta;
            this.numeroComprobante = numeroComprobante;
            this.cuit = cuit;
        }

        public ComprobanteAsociado()
        {

        }
    }
}
