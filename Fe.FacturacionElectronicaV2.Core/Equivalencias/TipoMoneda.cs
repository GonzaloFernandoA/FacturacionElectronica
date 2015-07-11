using Fe.FacturacionElectronicaV2.Core.Interfaces;

namespace Fe.FacturacionElectronicaV2.Core.Equivalencias
{
    public class TipoMoneda : IValorRespuestaWS
    {
        private string id;
        private string descripcion;
        private string equivalencia;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Equivalencia
        {
            get { return this.equivalencia; }
            set { this.equivalencia = value; }
        }

        public string ObtenerId()
        {
            return this.id;
        }
    }
}
