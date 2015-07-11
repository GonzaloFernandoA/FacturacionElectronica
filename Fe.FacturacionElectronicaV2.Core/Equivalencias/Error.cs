using System;
using System.Collections.Generic;

using System.Text;

namespace Fe.FacturacionElectronicaV2.Core.Equivalencias
{
    public class Error
    {
        private int codigo;
        private string mensaje;

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

        public Error( int codigo, string mensaje )
        {
            this.codigo = codigo;
            this.mensaje = mensaje;
        }
    }
}
