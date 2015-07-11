using System;
using System.Collections.Generic;

using System.Text;
using Fe.FacturacionElectronicaV2.Core.Interfaces;

namespace Fe.FacturacionElectronicaV2.Nacional.Equivalencias
{
    public class TiposTributo : IValorRespuestaWS
    {
        private short id;
        private string descripcion;
        private string equivalencia;

        public string Equivalencia
        {
            get { return this.equivalencia; }
            set { this.equivalencia = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public short Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string ObtenerId()
        {
            return this.id.ToString();
        }
    }
}
