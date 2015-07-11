
namespace Fe.FacturacionElectronicaV2.ExportacionV0.Equivalencias
{
    public class Incoterms
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
    }
}