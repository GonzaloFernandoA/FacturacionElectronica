
namespace Fe.FacturacionElectronicaV2.Core.Equivalencias
{
    public class PuntoDeVenta
    {
        private int numero;
        private string emisionTipo;
        private string bloqueado;

        public string Bloqueado
        {
            get { return this.bloqueado; }
            set { this.bloqueado = value; }
        }

        public string EmisionTipo
        {
            get { return this.emisionTipo; }
            set { this.emisionTipo = value; }
        }

        public int Numero
        {
            get { return this.numero; }
            set { this.numero = value; }
        }

    }
}
