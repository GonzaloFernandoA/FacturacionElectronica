using Fe.FacturacionElectronicaV2;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.Core.LoginWSAA;
using Fe.FacturacionElectronicaV2.DatosSegunTabla;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaProbar
{
    public class ManagerCae
    {
        public void ProcesarCae(Comprobante comp)
        {

            FactoriaFE factory = new FactoriaFE();
            EquivalenciasAFIP equiv = new EquivalenciasAFIP();

            FacturacionElectronica servicio = factory.ObtenerFacturacionElectronica(TipoWebService.Nacional);

            FeCabecera cab = factory.ObtenerCabecera();
            cab.PuntoDeVenta = comp.PuntoDeVenta;
            cab.CantidadDeRegistros = 1;
            cab.TipoComprobante = Convert.ToInt32(comp.TipoComprobante);

            FeDetalle detalle = factory.ObtenerDetalle();


            IVA objectoIva = factory.ObtenerDetalleIva(equiv.ObtenerTipoDeIva(21), 10000, 2100);

            // parametro tipo de concepto P S o ambos
            detalle.Concepto = equiv.ObtenerTipoDeConcepto("PS");
            // PARAMETROS TIPO DE DOCUMENTO
            detalle.DocumentoTipo = equiv.ObtenerTipoDeDocumento(comp.TipoDocumento);
            detalle.DocumentoNumero = comp.NumeroDeDocumento;
            detalle.ComprobanteDesde = comp.NumeroComprobante;
            detalle.ComprobanteHasta = comp.NumeroComprobante;
            detalle.ComprobanteFecha = comp.Fecha.ToString("yyyyMMdd");
            detalle.FechaServicioDesde = comp.FechaServicioDesde.ToString("yyyyMMdd");
            detalle.FechaServicioHasta = comp.FechaServicioHasta.ToString("yyyyMMdd");
            detalle.FechaVencimientoDePago = comp.FechaVencimientoPago.ToString("yyyyMMdd");
            detalle.MonedaId = "PES";
            detalle.MonedaCotizacion = 1;
            detalle.ImporteNeto = comp.ImporteNeto;
            detalle.ImporteIVA = comp.ImporteIva;
            detalle.Iva.Add(objectoIva);
            detalle.ImporteTotal = comp.ImporteTotal;

            cab.DetalleComprobantes.Add(detalle);

            ConfiguracionWS config = this.ObtenerAutorizacion();
            CAERespuestaFe respuesta = servicio.ObtenerCaeWSFE(config, cab);

            List<string> problemas = new List<string>();

            foreach (CAEDetalleRespuesta item in respuesta.Detalle)
            {
                if (item.Observaciones == null)
                {
                   problemas.Add(item.Cae.ToString());
                }
                else
                {

                    foreach (Observacion itemobs in item.Observaciones)
                    {
                        problemas.Add(itemobs.Mensaje);
                    }
                }
            }
        }
        private ConfiguracionWS ObtenerAutorizacion()
        {
            FactoriaFE factory = new FactoriaFE();

            ConfiguracionWS config = factory.ObtenerObjetoConfiguracion();

            string otroresultado = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Configuracion.json"));

            ConfiguracionCliente configuracionCliente = JsonConvert.DeserializeObject<ConfiguracionCliente>(otroresultado);
            config.RutaCertificado = configuracionCliente.RutaCertificado;
            config.NombreServicio = configuracionCliente.NombreServicio;
            config.UrlLogin = configuracionCliente.ServidorAutorizacion;
            config.TiempoDeEspera = configuracionCliente.TimeOut;
            config.Cuit = configuracionCliente.Cuit;
            config.UrlNegocio = configuracionCliente.UrlNegocio;

            return config;
        }
    }

}
