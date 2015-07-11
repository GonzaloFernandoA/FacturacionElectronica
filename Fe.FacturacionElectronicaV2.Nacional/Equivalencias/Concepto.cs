using Fe.FacturacionElectronicaV2.Core.Interfaces;

namespace Fe.FacturacionElectronicaV2.Nacional.Equivalencias
{
    public class Concepto : IValorRespuestaWS
    {
        private int id;
        private string descripcion;
        private string fechaDesde;
        private string fechaHasta;
        private string equivalencia;

        #region getters/setters
        public string Equivalencia
        {
            get { return this.equivalencia; }
            set { this.equivalencia = value; }
        }

        public string FechaHasta
        {
            get { return this.fechaHasta; }
            set { this.fechaHasta = value; }
        }

        public string FechaDesde
        {
            get { return this.fechaDesde; }
            set { this.fechaDesde = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        #endregion

        public string ObtenerId()
        {
            return this.id.ToString();
        }
    }
}
