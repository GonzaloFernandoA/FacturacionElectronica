using Fe.FacturacionElectronicaV2.Core.Interfaces;

namespace Fe.FacturacionElectronicaV2.Nacional.Equivalencias
{
    public class TipoDeIva : IValorRespuestaWS
    {
        private string id;
        private string descripcion;

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

        public string ObtenerId()
        {
            return this.id;
        }

        private string equivalencia;

        public string Equivalencia
        {
            get { return this.equivalencia; }
            set { this.equivalencia = value; }
        }

    }
}