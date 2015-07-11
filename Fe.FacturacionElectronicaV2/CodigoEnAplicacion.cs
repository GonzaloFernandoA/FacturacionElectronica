using System;
using System.Collections.Generic;

using System.Text;

namespace Fe.FacturacionElectronicaV2
{
    public class CodigoEnAplicacion // Dato que nos llega por parametros desde LINCE u ORGANIC para el combo
    {
        private string codigo;
        private string descripcion;

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

    }
}
