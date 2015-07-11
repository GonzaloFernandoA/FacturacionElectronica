
namespace Fe.FacturacionElectronicaV2.Nacional.Equivalencias
{
    public class CAEConsulta
    {
        private string caea;
        private int periodo;
        private short orden;
        private string fechaVigenciaDesde;
        private string fechaVigenciaHasta;
        private string fechaTopeParaInformar;
        private string fechaProceso;

        #region getters/setters
        public string FechaProceso
        {
            get { return this.fechaProceso; }
            set { this.fechaProceso = value; }
        }
        public string FechaTopeParaInformar
        {
            get { return this.fechaTopeParaInformar; }
            set { this.fechaTopeParaInformar = value; }
        }
        public string FechaVigenciaHasta
        {
            get { return this.fechaVigenciaHasta; }
            set { this.fechaVigenciaHasta = value; }
        }
        public string FechaVigenciaDesde
        {
            get { return this.fechaVigenciaDesde; }
            set { this.fechaVigenciaDesde = value; }
        }
        public short Orden
        {
            get { return this.orden; }
            set { this.orden = value; }
        }
        public int Periodo
        {
            get { return this.periodo; }
            set { this.periodo = value; }
        }
        public string CAEA
        {
            get { return this.caea; }
            set { this.caea = value; }
        }
        #endregion
    }
}
