using System;
using System.Collections.Generic;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.ExportacionV0.Equivalencias;
using Fe.FacturacionElectronicaV2.Core.Interfaces;
using Fe.FacturacionElectronicaV2.Exportacion;
using Fe.FacturacionElectronicaV2.Exportacion.WebServices;

namespace Fe.FacturacionElectronicaV2.ExportacionV0.Wrappers
{
    public class WrapperCabeceraFex
    {
        private double diferenciaPermitida = 0.02;
        private decimal importeTotalCalculado;

        public ClsFEXRequest Convertir( FexCabecera cabFex )
        {
            ClsFEXRequest cab = new ClsFEXRequest();
            cab.Id = cabFex.Id;
            cab.Cbte_Tipo = (short)cabFex.TipoComprobante;
            cab.Fecha_cbte = cabFex.FechaComprobante;
            cab.Punto_vta = (short)cabFex.PuntoDeVenta;
            cab.Cbte_nro = cabFex.ComprobanteNumero;
            cab.Tipo_expo = (short) cabFex.TipoExportacion;
            cab.Permiso_existente = cabFex.PermisoExistente;
            cab.Dst_cmp = (short)cabFex.PaisDestinoComprobante;
            cab.Cliente = cabFex.Cliente;
            cab.Cuit_pais_cliente = cabFex.CuitPaisCliente;
            cab.Domicilio_cliente = cabFex.DomicilioCliente;
            cab.Id_impositivo = cabFex.IdImpositivo;
            cab.Moneda_Id = cabFex.MonedaId;
            cab.Moneda_ctz = (decimal) cabFex.MonedaCotizacion;
            cab.Obs_comerciales = cabFex.ObservacionesComerciales;
            cab.Obs = cabFex.Observaciones;
            cab.Forma_pago = cabFex.FormaDePagoDescripcion;
            cab.Incoterms = cabFex.ClausulaDeVenta;
            cab.Incoterms_Ds = cabFex.ClausulaDeVentaInformacionComplementaria;
            cab.Idioma_cbte = (short) cabFex.IdiomaComprobante;
            cab.Permisos = this.ConvertirPermisos( cabFex.Permisos );
            cab.Cmps_asoc = this.ConvertirComprobantesAsociados( cabFex.ComprobantesAsociados );
            cab.Items = this.ConvertirItems( cabFex.Items );
            cab.Imp_total = (decimal) this.ObtenerValorImporteTotal( cabFex.ImporteTotal ); 

            return cab;
        }

        private double ObtenerValorImporteTotal( double importeTotalComprobante )
        {
            decimal diferencia = Math.Abs( (decimal) importeTotalComprobante - this.importeTotalCalculado );
            double retorno = importeTotalComprobante;
            if ( diferencia <= (decimal) this.diferenciaPermitida )
            {
                retorno = (double) this.importeTotalCalculado;
            }

            return retorno;
        }

        private Item[] ConvertirItems( List<FexItem> detalleItems )
        {
            Item[] items = new Item[detalleItems.Count];
            Item item;
            for ( int i = 0; i < detalleItems.Count; i++ )
            {
                item = new Item();
                item.Pro_codigo = detalleItems[i].ProductoCodigo;
                item.Pro_ds = detalleItems[i].ProductoDescripcion;
                item.Pro_qty = (decimal) detalleItems[i].ProductoCantidad;
                item.Pro_umed = detalleItems[i].ProductoUnidadDeMedida;
                item.Pro_bonificacion = (decimal) detalleItems[i].ProductoDescuento;
                item.Pro_precio_uni = (decimal) detalleItems[i].ProductoPrecioUnitario;
                item.Pro_total_item = (decimal) detalleItems[i].ProductoImporteTotal;
                items[i] = item;
                this.importeTotalCalculado += (decimal) item.Pro_total_item;
            }

            return items;
        }

        private Cmp_asoc[] ConvertirComprobantesAsociados( List<ComprobanteAsociado> comprobantesAsociados )
        {
            Cmp_asoc[] compAsocs = null;
            if ( comprobantesAsociados.Count > 0 )
            {
                compAsocs = new Cmp_asoc[comprobantesAsociados.Count];
                Cmp_asoc comprobanteAsoc;
                for ( int i = 0; i < comprobantesAsociados.Count; i++ )
                {
                    comprobanteAsoc = new Cmp_asoc();
                    comprobanteAsoc.Cbte_tipo = (short)comprobantesAsociados[i].Tipo;
                    comprobanteAsoc.Cbte_punto_vta = (short)comprobantesAsociados[i].PuntoDeVenta;
                    comprobanteAsoc.Cbte_nro = comprobantesAsociados[i].NumeroComprobante;
                    comprobanteAsoc.Cbte_cuit = comprobantesAsociados[i].Cuit;
                    compAsocs[i] = comprobanteAsoc;
                }
            }

            return compAsocs;
        }

        private Permiso[] ConvertirPermisos( List<PermisoDeEmbarque> permisosDeEmbarque )
        {
            Permiso[] permisos = null;
            if ( permisosDeEmbarque.Count > 0 )
            {
                permisos = new Permiso[permisosDeEmbarque.Count];
                Permiso permiso;
                for ( int i = 0; i < permisosDeEmbarque.Count; i++ )
                {
                    permiso = new Permiso();
                    permiso.Id_permiso = permisosDeEmbarque[i].Id;
                    permiso.Dst_merc = permisosDeEmbarque[i].DestinoMercaderia;
                    permisos[i] = permiso;
                }
            }
            return permisos;
        }

        public List<Observacion> Comparar(ClsFEXGetCMPR comprobanteAfip, FexCabecera cabFex)
        {
            BuscadorDeDiferencias buscadorDiferencias = new BuscadorDeDiferencias();
            return buscadorDiferencias.Obtener(comprobanteAfip, cabFex);
        }
    }
}
