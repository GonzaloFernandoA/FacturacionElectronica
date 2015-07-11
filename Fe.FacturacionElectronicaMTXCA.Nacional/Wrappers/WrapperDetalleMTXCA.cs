using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fe.FacturacionElectronicaMTXCA.Nacional.WebServices;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using System.Globalization;
using Fe.FacturacionElectronicaV2.Core;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;

namespace Fe.FacturacionElectronicaMTXCA.Nacional.Wrappers
{
    public class WrapperDetalleMTXCA
    {
        public ComprobanteType Convertir( FeDetalle detalle )
        {
            /*
             * sacar temas de redondeo a clase generica para ambos WS
             * Refactoring de ambas clases para consumir eso
             */
            DateTime fechaVacia = new DateTime();
            ComprobanteType comprobante = new ComprobanteType();
            decimal importeNetoNoGravado = (decimal) Redondeo.Aplicar( detalle.ImporteNetoNoGravado );
            decimal gravado = (decimal)Redondeo.Aplicar(detalle.ImporteNeto);
            comprobante.codigoConcepto = (short) detalle.Concepto;
            comprobante.codigoTipoDocumento = (short) detalle.DocumentoTipo;
            comprobante.numeroDocumento = detalle.DocumentoNumero;
            comprobante.numeroComprobante = detalle.ComprobanteDesde;
            comprobante.fechaEmision = DateTime.ParseExact( detalle.ComprobanteFecha, "yyyyMMdd", CultureInfo.InvariantCulture );

            comprobante.importeTotal = Redondeo.AplicarDecimal( detalle.ImporteTotal );
            comprobante.importeNoGravado = importeNetoNoGravado;
            comprobante.importeGravado = gravado;
            comprobante.importeExento = Redondeo.AplicarDecimal( detalle.ImporteExento );
            comprobante.importeOtrosTributos = Redondeo.AplicarDecimal( detalle.ImporteTributos );
            comprobante.importeSubtotal = importeNetoNoGravado + gravado + comprobante.importeExento;
         
            comprobante.codigoMoneda = detalle.MonedaId;
            comprobante.cotizacionMoneda = (decimal) detalle.MonedaCotizacion;

            comprobante.importeGravadoSpecified = detalle.Articulos.Exists( x => x.CondicionIVACodigo >= 3 && x.CondicionIVACodigo <= 6  );
            comprobante.importeNoGravadoSpecified = detalle.Articulos.Exists( x => x.CondicionIVACodigo == 1 );
            comprobante.importeExentoSpecified = detalle.Articulos.Exists( x => x.CondicionIVACodigo == 2 );
            comprobante.importeOtrosTributosSpecified = detalle.Tributos.Count > 0;
            comprobante.codigoTipoDocumentoSpecified = (comprobante.codigoTipoDocumento > 0);

            comprobante.fechaServicioDesde = this.ConvertirFecha(detalle.FechaServicioDesde);
            comprobante.fechaServicioHasta = this.ConvertirFecha(detalle.FechaServicioHasta);
            comprobante.fechaVencimientoPago = this.ConvertirFecha(detalle.FechaVencimientoDePago);
            
            comprobante.fechaEmisionSpecified = (comprobante.fechaEmision != fechaVacia);
            comprobante.fechaServicioDesdeSpecified = (comprobante.fechaServicioDesde != fechaVacia);
            comprobante.fechaServicioHastaSpecified = (comprobante.fechaServicioHasta != fechaVacia);
            comprobante.fechaVencimientoSpecified = (comprobante.fechaVencimiento != fechaVacia);
            comprobante.fechaVencimientoPagoSpecified = (comprobante.fechaVencimientoPago != fechaVacia);
            comprobante.numeroDocumentoSpecified = (comprobante.numeroDocumento > 0);

            this.ConvertirComprobantesAsociados( detalle, comprobante );
            this.ConvertirTributos( detalle, comprobante );
            this.ConvertirIVA( detalle, comprobante );
            this.ConvertirArticulos( detalle, comprobante );

            return comprobante;
        }

        private void ConvertirComprobantesAsociados( FeDetalle detalle, ComprobanteType comprobanteAFIP )
        {
            if ( detalle.ComprobantesAsociados.Count > 0 )
            {
                int i = 0;
                comprobanteAFIP.arrayComprobantesAsociados = new ComprobanteAsociadoType[detalle.ComprobantesAsociados.Count];
                WrapperComprobanteAsociadoMTXCA wcam = new WrapperComprobanteAsociadoMTXCA();
                foreach ( ComprobanteAsociado comprobante in detalle.ComprobantesAsociados )
                {
                    comprobanteAFIP.arrayComprobantesAsociados[i] = wcam.Convertir( comprobante );
                    i++;
                }
            }
        }

        private void ConvertirTributos( FeDetalle detalle, ComprobanteType comprobanteAFIP )
        {
            if ( detalle.Tributos.Count > 0 )
            {
                int i = 0;
                comprobanteAFIP.arrayOtrosTributos = new OtroTributoType[detalle.Tributos.Count];
                WrapperTributoMTXCA wtm = new WrapperTributoMTXCA();
                foreach ( TributoComprobante item in detalle.Tributos )
                {
                    comprobanteAFIP.arrayOtrosTributos[i] = wtm.Convertir( item );
                    i++;
                }
            }
        }

        private void ConvertirIVA( FeDetalle detalle, ComprobanteType comprobanteAFIP )
        {
            if ( detalle.Iva.Count > 0 )
            {
                int i = 0;
                comprobanteAFIP.arraySubtotalesIVA = new SubtotalIVAType[detalle.Iva.Count];
                WrapperIvaMTXCA wim = new WrapperIvaMTXCA();
                foreach ( IVA iva in detalle.Iva )
                {
                    comprobanteAFIP.arraySubtotalesIVA[i] = wim.Convertir( iva );
                    i++;
                }
            }
        }

        private void ConvertirArticulos( FeDetalle detalle, ComprobanteType comprobanteAFIP )
        {
            if ( detalle.Articulos.Count > 0 )
            {
                int i = 0;
                comprobanteAFIP.arrayItems = new ItemType[detalle.Articulos.Count];
                WrapperArticuloMTXCA wim = new WrapperArticuloMTXCA();
                foreach ( Articulo articulo in detalle.Articulos )
                {
                    comprobanteAFIP.arrayItems[i] = wim.Convertir( articulo );
                    i++;
                }
            }
        }

        private DateTime ConvertirFecha(string fecha)
        {
            DateTime retorno;

            if (!DateTime.TryParseExact(fecha, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out retorno))
            {
                retorno = new DateTime();
            }

            return retorno;
        }
    }
}
