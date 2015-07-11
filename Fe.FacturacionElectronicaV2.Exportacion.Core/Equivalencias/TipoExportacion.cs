
namespace Fe.FacturacionElectronicaV2.ExportacionV0.Equivalencias
{
    public class TipoExportacion
    {
        private int id;
        private string descripcion;

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
    }
}
