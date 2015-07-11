using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fe.FacturacionElectronicaV2.Nacional.Equivalencias
{
    public class UltimoNumeroComprobante
    {
        private int tipoComprobante;
        private int ultimoNumero;

        public int UltimoNumero
        {
            get { return this.ultimoNumero; }
            set { this.ultimoNumero = value; }
        }
        public int TipoComprobante
        {
            get { return this.tipoComprobante; }
            set { this.tipoComprobante = value; }
        }
    }
}
