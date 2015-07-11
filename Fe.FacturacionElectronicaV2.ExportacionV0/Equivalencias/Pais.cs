using System;
using System.Collections.Generic;

using System.Text;

namespace Fe.FacturacionElectronicaV2.ExportacionV0.Equivalencias
{
    public class Pais
    {
        private string id;
        private string descripcion;
        private List<CuitPais> cuits;

        public List<CuitPais> Cuits
        {
            get { return cuits; }
            set { cuits = value; }
        }

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