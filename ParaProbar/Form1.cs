using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fe.FacturacionElectronicaV2;
using Fe.FacturacionElectronicaV2.Core.LoginWSAA;
using System.Configuration;
using Newtonsoft.Json;
using System.IO;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;

namespace ParaProbar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

           // Autorizacion autoriz = this.ObtenerAutorizacion();

            FactoriaFE factory = new FactoriaFE();
            FacturacionElectronica servicio = factory.ObtenerFacturacionElectronica(TipoWebService.Nacional);
            
            FeCabecera cab = factory.ObtenerCabecera();
            cab.PuntoDeVenta = 88;
            cab.CantidadDeRegistros = 1;
            cab.TipoComprobante = 1;

            FeDetalle detalle = factory.ObtenerDetalle();
            Fe.FacturacionElectronicaV2.DatosSegunTabla.EquivalenciasAFIP equiv = new Fe.FacturacionElectronicaV2.DatosSegunTabla.EquivalenciasAFIP();

            IVA objectoIva = factory.ObtenerDetalleIva(equiv.ObtenerTipoDeIva(21), 10000, 2100);
            
            // parametro tipo de concepto P S o ambos
            detalle.Concepto = equiv.ObtenerTipoDeConcepto("PS");  
            // PARAMETROS TIPO DE DOCUMENTO
            detalle.DocumentoTipo = equiv.ObtenerTipoDeDocumento("CUIT");
            detalle.DocumentoNumero = this.ObtenerNumeroDocumento();
            detalle.ComprobanteDesde = this.NumeroDeComprobante();
            detalle.ComprobanteHasta = this.NumeroDeComprobante();
            detalle.ComprobanteFecha = new DateTime(2015, 7,25).ToString("yyyyMMdd");
            detalle.FechaServicioDesde = new DateTime(2015, 7, 1).ToString("yyyyMMdd");
            detalle.FechaServicioHasta = new DateTime(2015, 7, 30).ToString("yyyyMMdd");
            detalle.FechaVencimientoDePago = new DateTime(2015, 7, 30).ToString("yyyyMMdd");
            detalle.MonedaId = "PES";
            detalle.MonedaCotizacion = 1;
            detalle.ImporteNeto = 10000;
            detalle.ImporteIVA = 2100;
            detalle.Iva.Add(objectoIva);
            detalle.ImporteTotal = this.ObtenerImporteTotal();

            cab.DetalleComprobantes.Add(detalle);

             ConfiguracionWS config = this.ObtenerAutorizacion();

            
             CAERespuestaFe respuesta = servicio.ObtenerCaeWSFE(config, cab);

             foreach (CAEDetalleRespuesta item in respuesta.Detalle)
             {
                 if (item.Observaciones == null)
                 {
                     MessageBox.Show(item.Cae.ToString());
                 }
                 else
                 {

                     foreach (Observacion itemobs in item.Observaciones)
                     {
                         MessageBox.Show(itemobs.Mensaje);
                     }
                 }
             }
        }
        // Parametro Importe total
        double ObtenerImporteTotal()
        {
            double retorno = (double)10000 + 2100;
            return retorno;
        }

        // Parametro numero comprobante
        long NumeroDeComprobante()  
        {
            long retorno = (long)1;
            return retorno;
        }
        
        long ObtenerNumeroDocumento()
        {
            long retorno = (long)30500089624;
            return retorno;
        }

        ConfiguracionWS ObtenerAutorizacion()
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

         //   ServidorAutenticacion servidor = null;

        //    servidor = factory.ObtenerServidorAutenticacion(config);

            return config; //  servidor.ObtenerAutorizacion();
            
        
        
        }
    }
  

}
