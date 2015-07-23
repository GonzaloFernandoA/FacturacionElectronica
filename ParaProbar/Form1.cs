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

            Autorizacion autoriz = this.ObtenerAutorizacion();

            FactoriaFE factory = new FactoriaFE();

            FeCabecera cab = factory.ObtenerCabecera();
            cab.PuntoDeVenta = 88;
            cab.CantidadDeRegistros = 1;
            cab.TipoComprobante = 1;

            FeDetalle detalle = factory.ObtenerDetalle();
            Fe.FacturacionElectronicaV2.DatosSegunTabla.EquivalenciasAFIP equiv = new Fe.FacturacionElectronicaV2.DatosSegunTabla.EquivalenciasAFIP();
            
            // parametro tipo de concepto P S o ambos
            detalle.Concepto = equiv.ObtenerTipoDeConcepto("P");  
            // PARAMETROS TIPO DE DOCUMENTO
            detalle.DocumentoTipo = equiv.ObtenerTipoDeDocumento("CUIT");
            detalle.DocumentoNumero = this.ObtenerNumeroDocumento();
            detalle.ComprobanteDesde = this.NumeroDeComprobante();
            detalle.ComprobanteHasta = this.NumeroDeComprobante();
            detalle.ComprobanteFecha = DateTime.Today.ToString();
            detalle.ImporteTotal = this.ObtenerImporteTotal();
        
        
        }
        // Parametro Importe total
        double ObtenerImporteTotal()
        {
            double retorno = (double)234234;
            return retorno;
        }

        // Parametro numero comprobante
        long NumeroDeComprobante()  
        {
            long retorno = (long)234234;
            return retorno;
        }
        
        long ObtenerNumeroDocumento()
        {
            long retorno = (long)234234;
            return retorno;
        }

        Autorizacion ObtenerAutorizacion()
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

            ServidorAutenticacion servidor = null;

            servidor = factory.ObtenerServidorAutenticacion(config);

            return servidor.ObtenerAutorizacion();
            
        
        
        }
    }
  

}
