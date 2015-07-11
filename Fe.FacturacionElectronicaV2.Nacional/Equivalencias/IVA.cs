using System;
using System.Collections.Generic;

using System.Text;

namespace Fe.FacturacionElectronicaV2.Nacional.Equivalencias
{
    public class IVA
    {
        private int id;
        private double baseImponible;
        private double importe;

        #region getters/setters
        public double Importe
        {
            get { return this.importe; }
            set { this.importe = value; }
        }

        public double BaseImponible
        {
            get { return this.baseImponible; }
            set { this.baseImponible = value; }
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        #endregion

        public IVA( int id, double baseImponible, double importe )
        {
            this.id = id;
            this.baseImponible = baseImponible;
            this.importe = importe;
        }

        public IVA()
        {

        }
    }
}
