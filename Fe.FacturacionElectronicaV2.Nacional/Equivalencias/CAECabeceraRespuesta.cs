
namespace Fe.FacturacionElectronicaV2.Nacional.Equivalencias
{
    public class CAECabeceraRespuesta
    {

        private long cuit;
        private int puntoDeVenta;
        private int tipoComprobante;
        private string fechaProceso;
        private int cantidadDeRegistros;
        private string resultado;

        #region getters/setters
        public string Resultado
        {
            get { return this.resultado; }
            set { this.resultado = value; }
        }
        public int CantidadDeRegistros
        {
            get { return this.cantidadDeRegistros; }
            set { this.cantidadDeRegistros = value; }
        }

        public string FechaProceso
        {
            get { return this.fechaProceso; }
            set { this.fechaProceso = value; }
        }

        public int TipoComprobante
        {
            get { return this.tipoComprobante; }
            set { this.tipoComprobante = value; }
        }
        public int PuntoDeVenta
        {
            get { return this.puntoDeVenta; }
            set { this.puntoDeVenta = value; }
        }
        public long Cuit
        {
            get { return this.cuit; }
            set { this.cuit = value; }
        }
        #endregion
    }
}
