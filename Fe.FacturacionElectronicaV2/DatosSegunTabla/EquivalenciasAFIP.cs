using System;
using System.Collections.Generic;

using System.Text;
using Fe.FacturacionElectronicaV2.Nacional;
using Fe.FacturacionElectronicaV2.Core;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;

namespace Fe.FacturacionElectronicaV2.DatosSegunTabla
{
    public class EquivalenciasAFIP
    {
        private Dictionary<string, int> tiposDeComprobante;
        private Dictionary<string, short> tiposDeTributos;
        private Dictionary<string, int> tiposDeConcepto;
        private Dictionary<string, int> tiposDeDocumento;
        private Dictionary<int, int> tiposDeIva;
        private Dictionary<int, int> condicionesIvaArticulos;

        public EquivalenciasAFIP()
        {
            this.CompletarTiposDeComprobante();
            this.CompletarTiposDeTributos();
            this.CompletarTiposDeConcepto();
            this.CompletarTiposDeDocumento();
            this.CompletarTiposDeIva();
            this.CompletarCondicionesIvaArticulos();
        }

        #region Funciones De Llenado de datos
        private void CompletarTiposDeIva()
        {
            this.tiposDeIva = new Dictionary<int, int>();
            this.tiposDeIva.Add( 0, 3 ); // IVA 0%
            this.tiposDeIva.Add(10, 4); // IVA 10,5%
            this.tiposDeIva.Add(21, 5); // IVA 21%
            this.tiposDeIva.Add(27, 6); // IVA 27%
        }
        private void CompletarCondicionesIvaArticulos()
        {
            this.condicionesIvaArticulos = new Dictionary<int, int>();
            this.condicionesIvaArticulos.Add( 0, 1 ); // NO GRAVADO
            this.condicionesIvaArticulos.Add( 1, 2 ); // EXENTO
            //this.condicionesIvaArticulos.Add( 0, 3 ); // IVA 0%
            this.condicionesIvaArticulos.Add( 10, 4 ); // IVA 10,5%
            this.condicionesIvaArticulos.Add( 21, 5 ); // IVA 21%
            this.condicionesIvaArticulos.Add( 27, 6 ); // IVA 27%
        }

        private void CompletarTiposDeDocumento()
        {
            this.tiposDeDocumento = new Dictionary<string, int>();
            this.tiposDeDocumento.Add("CUIT", 80); // CUIT
            this.tiposDeDocumento.Add("DNI", 96); // DNI
            this.tiposDeDocumento.Add("OTRO", 99); // OTRO
        }

        private void CompletarTiposDeConcepto()
        {
            this.tiposDeConcepto = new Dictionary<string, int>();
            this.tiposDeConcepto.Add("P", 1); //Producto
            this.tiposDeConcepto.Add("S", 2); //Servicios
            this.tiposDeConcepto.Add("PS", 3); //Productos y Servicios
        }

        private void CompletarTiposDeTributos()
        {
            this.tiposDeTributos = new Dictionary<string, short>();
            this.tiposDeTributos.Add("IN", 1); //Impuestos nacionales
            this.tiposDeTributos.Add("IP", 2); //Impuestos provinciales
            this.tiposDeTributos.Add("IM", 3); //Impuestos municipales
            this.tiposDeTributos.Add("II", 4); //Impuestos Internos
            this.tiposDeTributos.Add("OTRO", 99); //Otro
        }

        private void CompletarTiposDeComprobante()
        {
            this.tiposDeComprobante = new Dictionary<string, int>();
            this.tiposDeComprobante.Add("FACTURAA", 1);
            this.tiposDeComprobante.Add("NOTADEDEBITOA", 2);
            this.tiposDeComprobante.Add("NOTADECREDITOA", 3);
            this.tiposDeComprobante.Add("FACTURAB", 6);
            this.tiposDeComprobante.Add("NOTADEDEBITOB", 7);
            this.tiposDeComprobante.Add("NOTADECREDITOB", 8);
            this.tiposDeComprobante.Add("FACTURAC", 11);
            this.tiposDeComprobante.Add("NOTADEDEBITOC", 12);
            this.tiposDeComprobante.Add("NOTADECREDITOC", 13);
            this.tiposDeComprobante.Add("FACTURAE", 19);
            this.tiposDeComprobante.Add("NOTADECREDITOE", 21);
            this.tiposDeComprobante.Add("NOTADEDEBITOE", 20);

        }
        #endregion

        #region Funciones de obtención de datos
        public int ObtenerTipoDeComprobante(string tipoLince)
        {
            return this.tiposDeComprobante[tipoLince.ToUpper()];
        }

        public int ObtenerTipoDeIva(int tipoIva)
        {
            int retorno = 0;

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
        public int ObtenerCondicionIvaArticulo( int tipoIva )
        {
            int retorno = 0;

            try
            {
                retorno = this.condicionesIvaArticulos[tipoIva];
            }
            catch ( Exception ex )
            {
                ExcepcionFe miEx = new ExcepcionFe();
                Err error = new Err();
                error.Code = 0;
                error.Msg = "El porcentaje de IVA " + tipoIva.ToString() + "% no es valido.";
                miEx.AgregarError( error );
                throw miEx;
            }

            return retorno;
        }

        public int ObtenerTipoDeDocumento(string tipoDoc)
        {
            return this.tiposDeDocumento[tipoDoc.ToUpper()];
        }

        public int ObtenerTipoDeConcepto(string concepto)
        {
            return this.tiposDeConcepto[concepto.ToUpper()];
        }

        public short ObtenerTipoDeTributo(string tributo)
        {
            return this.tiposDeTributos[tributo.ToUpper()];
        }
        #endregion
    }
}
