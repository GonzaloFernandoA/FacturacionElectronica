using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fe.FacturacionElectronicaMTXCA.Nacional.WebServices;

namespace Fe.FacturacionElectronicaV2.Core
{
    public class ProcesadorErrorMTXCA : IProcesadorError
    {
        private CodigoDescripcionType[] errores;

        public ProcesadorErrorMTXCA( CodigoDescripcionType[] errores )
        {
            this.errores = errores;
        }

        #region IProcesadorError Members

        public string Procesar( ref ExcepcionFe ex )
        {
            string mensaje = "";
            foreach ( CodigoDescripcionType error in this.errores )
            {
                ex.AgregarError( error );
                mensaje = mensaje + "\r\nERROR DE PROCESO: " + error.descripcion + " (" + error.codigo + ")";
            }

            return mensaje;
        }

        #endregion
    }
}
