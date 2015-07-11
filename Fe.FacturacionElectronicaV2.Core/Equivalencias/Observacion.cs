using System;
using System.Collections.Generic;

using System.Text;

namespace Fe.FacturacionElectronicaV2.Core.Equivalencias
{
    public class Observacion
    {
        private int codigo;
        private string mensaje;

        #region getters/setters
        public string Mensaje
        {
            get { return this.mensaje; }
            set { this.mensaje = value; }
        }
        public int Codigo
        {
            get { return this.codigo; }
            set { this.codigo = value; }
        }
        #endregion
    }
}
