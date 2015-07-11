using System;
using System.Collections.Generic;
using System.Text;
using Fe.FacturacionElectronicaV2.Core.Interfaces;
using Fe.FacturacionElectronicaV2.Exportacion.Core;

namespace Fe.FacturacionElectronicaV2.ExportacionV0.Equivalencias
{
    public class FexItem
    {
        private string productoCodigo;
        private string productoDescripcion;
        private double productoCantidad;
        private int productoUnidadDeMedida;
        private double productoPrecioUnitario;
        private double productoImporteTotal;
        private double productoDescuento;

        #region getters/setters
        public double ProductoImporteTotal
        {
            get { return this.productoImporteTotal; }
            set { this.productoImporteTotal = value; }
        }
        public double ProductoPrecioUnitario
        {
            get { return this.productoPrecioUnitario; }
            set { this.productoPrecioUnitario = value; }
        }
        public int ProductoUnidadDeMedida
        {
            get { return this.productoUnidadDeMedida; }
            set { this.productoUnidadDeMedida = value; }
        }
        public double ProductoCantidad
        {
            get { return this.productoCantidad; }
            set { this.productoCantidad = value; }
        }
        public string ProductoDescripcion
        {
            get { return this.productoDescripcion; }
            set { this.productoDescripcion = value; }
        }
        public string ProductoCodigo
        {
            get { return this.productoCodigo; }
            set { this.productoCodigo = value; }
        }
        public double ProductoDescuento
        {
            get { return productoDescuento; }
            set { productoDescuento = value; }
        }
        #endregion

        public string Serializar()
        {
            string retorno = "";

            retorno = "Codigo: " + this.productoCodigo.ToString() + "\r\n";
            retorno = retorno + "Descripcion: " + this.productoDescripcion.ToString() + "\r\n";
            retorno = retorno + "Cantidad: " + this.productoCantidad.ToString() + "\r\n";
            retorno = retorno + "Unida de medida: " + this.productoUnidadDeMedida.ToString() + "\r\n";
            retorno = retorno + "Descuento: " + this.productoDescuento.ToString() + "\r\n";
            retorno = retorno + "Precio: " + this.productoPrecioUnitario.ToString() + "\r\n";
            retorno = retorno + "Total: " + this.productoImporteTotal.ToString() + "\r\n";

            return retorno;
        }
    }
}
