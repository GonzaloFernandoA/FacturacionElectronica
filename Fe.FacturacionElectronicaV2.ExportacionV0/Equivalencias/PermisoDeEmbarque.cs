using System;
using System.Collections.Generic;
using System.Text;
using Fe.FacturacionElectronicaV2.Core.Interfaces;
using Fe.FacturacionElectronicaV2.Exportacion.Core;

namespace Fe.FacturacionElectronicaV2.ExportacionV0.Equivalencias
{
    public class PermisoDeEmbarque : IPermisoDeEmbarque
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
