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
            FactoriaFE factory =new FactoriaFE();

          ConfiguracionWS config = factory.ObtenerObjetoConfiguracion();



          int cantidad = ConfigurationManager.AppSettings.Count;
          config.RutaCertificado = ConfigurationManager.AppSettings["RutaCertificado"];

          config.NombreServicio = ConfigurationManager.AppSettings["NombreServicio"];


    //        config.Cuit = ConfigurationManager.AppSettings["Cuit"];
           
            
            config.UrlLogin = System.Configuration.ConfigurationManager.AppSettings["ServidorAutorizacion"].ToString();
            config.TiempoDeEspera = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Timeout"]) ;

            ServidorAutenticacion servidor = null;
          
            servidor = factory.ObtenerServidorAutenticacion(config);
          
            Autorizacion autoriz = servidor.ObtenerAutorizacion();
            
            System.Console.WriteLine(autoriz.Expiracion );

        }
    }
}
