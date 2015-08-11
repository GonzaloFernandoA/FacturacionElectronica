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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParaProbar
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ManagerComprobantes manager = new ManagerComprobantes();
            List<Comprobante> comprobantes = manager.ObtenerComprobantes( Path.Combine( Environment.CurrentDirectory , "Factura.xml"));
            ManagerCae managerCae = new ManagerCae();
            comprobantes.ForEach(x => managerCae.ProcesarCae(x));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
