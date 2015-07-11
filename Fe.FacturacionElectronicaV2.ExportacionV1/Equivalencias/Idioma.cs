
namespace ZooLogicSA.FacturacionElectronicaV2.ExportacionV1.Equivalencias
{
    public class Idioma
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