
namespace ZooLogicSA.FacturacionElectronicaV2.ExportacionV1.Equivalencias
{
    public class CuitPais
    {
        private long id;
        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public long Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
    }
}