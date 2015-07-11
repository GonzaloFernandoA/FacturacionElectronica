using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fe.FacturacionElectronicaV2.Nacional.Equivalencias
{
    public class Articulo
    {
        private int unidadesMtx;
        private string codigoMtx;
        private string codigo;
        private string descripcion;
        private double cantidad;
        private int unidadMedidaCodigo;
        private double precioUnitario;
        private double importeBonificacion;
        private int condicionIVACodigo;
        private double importeIVA;
        private double importeItem;

        public Articulo()
        {
        }

        public Articulo( string descripcion, int unidadMedidaCodigo, int condicionIVACodigo, double importeItem )
        {
            this.descripcion = descripcion;
            this.unidadMedidaCodigo = unidadMedidaCodigo;
            this.condicionIVACodigo = condicionIVACodigo;
            this.importeItem = importeItem;
        }

        #region set/get
        public int UnidadesMtx
        {
            get { return unidadesMtx; }
            set { unidadesMtx = value; }
        }

        public string CodigoMtx
        {
            get { return codigoMtx; }
            set { codigoMtx = value; }
        }

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

        public double Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public int UnidadMedidaCodigo
        {
            get { return unidadMedidaCodigo; }
            set { unidadMedidaCodigo = value; }
        }

        public double PrecioUnitario
        {
            get { return precioUnitario; }
            set { precioUnitario = value; }
        }

        public double ImporteBonificacion
        {
            get { return importeBonificacion; }
            set { importeBonificacion = value; }
        }

        public int CondicionIVACodigo
        {
            get { return condicionIVACodigo; }
            set { condicionIVACodigo = value; }
        }

        public double ImporteIVA
        {
            get { return importeIVA; }
            set { importeIVA = value; }
        }

        public double ImporteItem
        {
            get { return importeItem; }
            set { importeItem = value; }
        }
        #endregion
    }
}
