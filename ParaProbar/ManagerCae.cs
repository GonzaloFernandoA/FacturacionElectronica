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

namespace ProcesadorCae
{
    public class ManagerCae
    {
        public void ProcesarCae(Comprobante comprobante)
        {
            FactoriaFE factory = new FactoriaFE();
            EquivalenciasAFIP equiv = new EquivalenciasAFIP();
            EquivalenciaTipoConcepto equivalenciaConcepto = new EquivalenciaTipoConcepto();

            FacturacionElectronica servicio = factory.ObtenerFacturacionElectronica(TipoWebService.Nacional);

            FeCabecera cabecera = factory.ObtenerCabecera();
            cabecera.PuntoDeVenta = comprobante.PuntoDeVenta;
            cabecera.CantidadDeRegistros = 1;
            cabecera.TipoComprobante = Convert.ToInt32(comprobante.TipoComprobante);

            FeDetalle detalle = factory.ObtenerDetalle();
            detalle.Concepto = equiv.ObtenerTipoDeConcepto(equivalenciaConcepto.ObtenerEquivalencia(comprobante.TipoServicio));
            detalle.DocumentoTipo = equiv.ObtenerTipoDeDocumento(comprobante.TipoDocumento);
            detalle.DocumentoNumero = comprobante.NumeroDeDocumento;
            detalle.ComprobanteDesde = comprobante.NumeroComprobante;
            detalle.ComprobanteHasta = comprobante.NumeroComprobante;
            detalle.ComprobanteFecha = comprobante.Fecha.ToString("yyyyMMdd");
            detalle.FechaServicioDesde = comprobante.FechaServicioDesde.ToString("yyyyMMdd");
            detalle.FechaServicioHasta = comprobante.FechaServicioHasta.ToString("yyyyMMdd");
            detalle.FechaVencimientoDePago = comprobante.FechaVencimientoPago.ToString("yyyyMMdd");
            detalle.MonedaId = "PES";
            detalle.MonedaCotizacion = 1;
            detalle.ImporteNeto = comprobante.ImporteTotal;
            if (comprobante.ImporteIva > 0)
            {
                IVA objectoIva = factory.ObtenerDetalleIva(equiv.ObtenerTipoDeIva(21), comprobante.ImporteNeto, comprobante.ImporteIva);
                detalle.ImporteIVA = comprobante.ImporteIva;
                detalle.Iva.Add(objectoIva);
            }
            detalle.ImporteTotal = comprobante.ImporteTotal;
            cabecera.DetalleComprobantes.Add(detalle);

            ConfiguracionWS config = this.ObtenerAutorizacion();
            Respuesta respuesta = new Respuesta() { Cae = "0"};
            List<string> problemas = new List<string>();
           try 
	        {	        
                CAERespuestaFe respuestaFe = servicio.ObtenerCaeWSFE(config, cabecera);
                foreach (CAEDetalleRespuesta item in respuestaFe.Detalle)
                {
                    if (item.Observaciones == null)
                    {
                        respuesta.AgregarProblema("");
                        respuesta.Cae = item.Cae.ToString();
                    }
                    else
                    {
                        foreach (Observacion itemobs in item.Observaciones)
                        {
                            respuesta.AgregarProblema(itemobs.Mensaje);
                        }
                    }
                }
             }
           catch (Exception ex)
           {
               respuesta.AgregarProblema(ex.Message);
           }
           respuesta.ToXmlVfp( Path.Combine( Environment.CurrentDirectory,"Respuesta.xml"));
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
