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
            cab.TipoComprobante = 11;

            FeDetalle detalle = factory.ObtenerDetalle();
            Fe.FacturacionElectronicaV2.DatosSegunTabla.EquivalenciasAFIP equiv = new Fe.FacturacionElectronicaV2.DatosSegunTabla.EquivalenciasAFIP();
            
            // parametro tipo de concepto P S o ambos
            detalle.Concepto = equiv.ObtenerTipoDeConcepto("S");  
            // PARAMETROS TIPO DE DOCUMENTO
            detalle.DocumentoTipo = equiv.ObtenerTipoDeDocumento("DNI");
            detalle.DocumentoNumero = this.ObtenerNumeroDocumento();
            detalle.ComprobanteDesde = this.NumeroDeComprobante();
            detalle.ComprobanteHasta = this.NumeroDeComprobante();
            detalle.ComprobanteFecha = new DateTime(2015, 7,15).ToString("yyyyMMdd");
            detalle.ImporteTotal = this.ObtenerImporteTotal();
            detalle.FechaServicioDesde = new DateTime(2015, 7, 1).ToString("yyyyMMdd");
            detalle.FechaServicioHasta = new DateTime(2015, 7, 30).ToString("yyyyMMdd");
            detalle.FechaVencimientoDePago = new DateTime(2015, 7, 30).ToString("yyyyMMdd");
            detalle.MonedaId = "PES";
            detalle.MonedaCotizacion = 1;
            detalle.ImporteNeto = 6500;
            
            cab.DetalleComprobantes.Add(detalle);

             ConfiguracionWS config = this.ObtenerAutorizacion();

            
             CAERespuestaFe respuesta = servicio.ObtenerCaeWSFE(config, cab);

             foreach (CAEDetalleRespuesta item in respuesta.Detalle)
             {
                 foreach (Observacion itemobs in item.Observaciones)
                 {
                     MessageBox.Show(itemobs.Mensaje);
                 }
             }
        }
        // Parametro Importe total
        double ObtenerImporteTotal()
        {
            double retorno = (double)6500;
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
            long retorno = (long)25239511;
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
