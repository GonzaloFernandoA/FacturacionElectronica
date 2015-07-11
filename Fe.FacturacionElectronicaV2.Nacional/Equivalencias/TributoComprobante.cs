using System;
using System.Collections.Generic;

using System.Text;

namespace Fe.FacturacionElectronicaV2.Nacional.Equivalencias
{
    public class TributoComprobante
    {
        private int id;
        private string descripcion;
        private double baseImponible;
        private double alicuota;
        private double importe;

        #region getters/setters
        public double Alicuota
        {
            get { return this.alicuota; }
            set { this.alicuota = value; }
        }

        public double BaseImponible
        {
            get { return this.baseImponible; }
            set { this.baseImponible = value; }
        }

        public string Descripcion
        {
            get { return this.descripcion; }
            set { this.descripcion = value; }
        }
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public double Importe
        {
            get { return this.importe; }
            set { this.importe = value; }
        }
        #endregion

        public TributoComprobante( short id, string descripcion, double baseImponible, double alicuota, double importe )
        {
            this.id = id;
            this.descripcion = descripcion;
            this.baseImponible = baseImponible;
            this.alicuota = alicuota;
            this.importe = importe;
        }

        public TributoComprobante()
        {

        }
    }
}
