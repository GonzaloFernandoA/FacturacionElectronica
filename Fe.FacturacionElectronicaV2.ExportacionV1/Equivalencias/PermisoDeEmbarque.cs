using System;
using System.Collections.Generic;

using System.Text;

namespace ZooLogicSA.FacturacionElectronicaV2.ExportacionV1.Equivalencias
{
    public class PermisoDeEmbarque
    {
        private string id;
        private int destinoMercaderia;

        #region getters/setters
        public int DestinoMercaderia
        {
            get { return this.destinoMercaderia; }
            set { this.destinoMercaderia = value; }
        }
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        #endregion
    }
}
