using System;
using System.Collections.Generic;
using Fe.FacturacionElectronicaV2.Nacional;
using Fe.FacturacionElectronicaV2.Core;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;

namespace Fe.FacturacionElectronicaV2.DatosSegunTabla
{
    public class EquivalenciasLince
    {
        private Dictionary<string, int> tiposDeComprobante;
        private Dictionary<string, short> tiposDeTributos;
        private Dictionary<string, int> tiposDeConcepto;
        private Dictionary<string, int> tiposDeDocumento;
        private Dictionary<int, int> tiposDeIva;

        public EquivalenciasLince()
        {
            this.CompletarTiposDeComprobante();
            this.CompletarTiposDeTributos();
            this.CompletarTiposDeConcepto();
            this.CompletarTiposDeDocumento();
            this.CompletarTiposDeIva();
        }

        #region Funciones De Llenado de datos
        private void CompletarTiposDeIva()
        {
            this.tiposDeIva = new Dictionary<int, int>();
            this.tiposDeIva.Add( 0, 3 ); // IVA 0%
            this.tiposDeIva.Add( 10, 4 ); // IVA 10,5%
            this.tiposDeIva.Add( 21, 5 ); // IVA 21%
            this.tiposDeIva.Add( 27, 6 ); // IVA 27%
        }

        private void CompletarTiposDeDocumento()
        {
            this.tiposDeDocumento = new Dictionary<string, int>();
            this.tiposDeDocumento.Add( "CUIT", 80 ); // CUIT
            this.tiposDeDocumento.Add( "DNI", 96 ); // DNI
            this.tiposDeDocumento.Add( "OTRO", 99 ); // OTRO
        }

        private void CompletarTiposDeConcepto()
        {
            this.tiposDeConcepto = new Dictionary<string, int>();
            this.tiposDeConcepto.Add( "P", 1 ); //Producto
            this.tiposDeConcepto.Add( "S", 2 ); //Servicios
            this.tiposDeConcepto.Add( "PS", 3 ); //Productos y Servicios
        }

        private void CompletarTiposDeTributos()
        {
            this.tiposDeTributos = new Dictionary<string, short>();
            this.tiposDeTributos.Add( "IN", 1 ); //Impuestos nacionales
            this.tiposDeTributos.Add( "IP", 2 ); //Impuestos provinciales
            this.tiposDeTributos.Add( "IM", 3 ); //Impuestos municipales
            this.tiposDeTributos.Add( "II", 4 ); //Impuestos Internos
            this.tiposDeTributos.Add( "OTRO", 99 ); //Otro
        }

        private void CompletarTiposDeComprobante()
        {
            this.tiposDeComprobante = new Dictionary<string, int>();
            this.tiposDeComprobante.Add( "1A", 1 ); // Factura A
            this.tiposDeComprobante.Add( "4A", 2 ); // Nota de débito A
            this.tiposDeComprobante.Add( "3A", 3 ); // Nota de crédito A
            this.tiposDeComprobante.Add( "1B", 6 ); // Factura B
            this.tiposDeComprobante.Add( "4B", 7 ); // Nota de débito B
            this.tiposDeComprobante.Add( "3B", 8 ); // Nota de crédito B

            this.tiposDeComprobante.Add("1C", 11 ); // Factura C
            this.tiposDeComprobante.Add("4C", 12); // Nota de Débito C
            this.tiposDeComprobante.Add("3C", 13 ); // Nota de Crédito C

        }
        #endregion

        #region Funciones de obtención de datos
        public int ObtenerTipoDeComprobante( string tipoLince )
        {
            return this.tiposDeComprobante[tipoLince.ToUpper()];
        }

        public int ObtenerTipoDeIva( int tipoIva )
        {
            int retorno=0;

            try
            {
                retorno = this.tiposDeIva[tipoIva];
            }
            catch (Exception ex)
            {
                ExcepcionFe miEx = new ExcepcionFe();
                Err error = new Err();
                error.Code = 0;
                error.Msg = "El porcentaje de IVA " + tipoIva.ToString() + "% no es valido.";
                miEx.AgregarError(error);
                throw miEx;
            }

            return retorno;
        }

        public int ObtenerTipoDeDocumento( string tipoDoc )
        {
            return this.tiposDeDocumento[tipoDoc.ToUpper()];
        }

        public int ObtenerTipoDeConcepto( string concepto )
        {
            return this.tiposDeConcepto[concepto.ToUpper()];
        }

        public short ObtenerTipoDeTributo( string tributo )
        {
            return this.tiposDeTributos[tributo.ToUpper()];
        }
        #endregion
    }
}
