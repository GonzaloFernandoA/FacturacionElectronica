using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services.Protocols;

namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public class ManejadorDeErroresWSAA : Fe.FacturacionElectronicaV2.Core.LoginWSAA.IManejadorDeErroresWSAA
    {
        public void ManejarError( Exception ex, string metodo, string mensajeLoco )
        {
            if ( ex is SoapException )
            {
                SoapException excepcionSoap = (SoapException)ex;

                switch ( excepcionSoap.Code.Name )
                {
                    case "xml.generationTime.invalid":
                        mensajeLoco = "Hay diferencias entre la fecha y hora del equipo y la informada por el servicio de facturación electrónica. " + ex.Message;
                        break;
                    case "xml.expirationTime.invalid":
                        mensajeLoco = "Hay diferencias entre la fecha y hora del equipo y la informada por el servicio de facturación electrónica. " + ex.Message;
                        break;
                    default:
                        break;
                }
            }

            Exception miEx = new ValidacionException( metodo, mensajeLoco );
            throw miEx;
        }
    }


    [Serializable]
    public class ValidacionException : Exception
    {
        private string nombreProceso;
        private string mensaje;

        public ValidacionException( string nombreProceso, string mensaje )
            : base( mensaje )
        {
            this.nombreProceso = nombreProceso;
            if ( mensaje == "Se excedió el tiempo de espera de la operación" )
            {
                mensaje = mensaje + ".\r\n" + "Verifique la conexión de internet y la fecha de la máquina.";
            }
            this.mensaje = mensaje;
        }

        public string NombreProceso { get { return this.nombreProceso; } }

        public override string Message
        {
            get
            {
                return "Ocurrio el siguiente error: " + this.mensaje + "\r\nProceso " + this.nombreProceso;
            }
        }
    }
}
