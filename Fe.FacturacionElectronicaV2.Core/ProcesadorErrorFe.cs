using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;
using Fe.FacturacionElectronicaV2.Core;

namespace Fe.FacturacionElectronicaV2.Core
{
    public class ProcesadorErrorFe : IProcesadorError
    {
        private Err[] errores;

        public ProcesadorErrorFe( Err[] errores )
        {
            this.errores = errores;
        }

        public string Procesar( ref ExcepcionFe ex )
        {
            string mensaje = "";
            foreach ( Err error in this.errores )
            {
                ex.AgregarError( error );
                mensaje = mensaje + "\r\nERROR DE PROCESO: " + error.Msg + " (" + error.Code + ")";
            }

            return mensaje;
        }
    }
}
