using System;
using System.IO;
using ZooLogicSA.Core.Logueo;

// : IConfiguracionLogueo

namespace Fe.FacturacionElectronicaV2.Core
{
    public class ConfiguracionLogueo 
    {
        #region IConfiguracionLogueo Members

        public string ArchivoAppenders
        {
            get { return "AppendersFE.xml"; }
        }

        public string ArchivoLoggers
        {
            get { return "LoggersFE.xml"; }
        }

        public IInformacionCabecera InformacionCabecera
        {
            get
            {
                IInformacionCabecera infoCab = new InformacionCabecera();
                infoCab.Aplicacion = "Facturación Electrónica V2";

                return infoCab;
            }
        }

        public string RutaAppenders
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Recursos"); }
        }

        public string RutaLoggers
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Recursos"); }
        }

        public string NombreLogger
        {
            get { return "FE"; }
        }

        public string NombreLoggerSinCabecera
        {
            get { return "FESINCABECERA"; }
        }

        public bool ConCabecera
        {
            get { return true; }
        }

        #endregion
    }
}