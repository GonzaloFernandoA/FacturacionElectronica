using System;
using System.Collections.Generic;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;
using Fe.FacturacionElectronicaV2.Exportacion.WebServices;
using Fe.FacturacionElectronicaMTXCA.Nacional.WebServices;

namespace Fe.FacturacionElectronicaV2.Core
{
    public class ExcepcionFe : Exception
    {
        private List<Error> errores = new List<Error>();

        public List<Error> Errores
        {
            get { return this.errores; }
            set { this.errores = value; }
        }

        public void AgregarError( Err error )
        {
            Error errorNuevo = new Error( error.Code, error.Msg );
            this.errores.Add( errorNuevo );
        }
        
        public void AgregarError( ClsFEXErr error )
        {
            Error errorNuevo = new Error( error.ErrCode, error.ErrMsg );
            this.errores.Add( errorNuevo );
        }

        public void AgregarError( CodigoDescripcionType error )
        {
            Error errorNuevo = new Error( error.codigo, error.descripcion );
            this.errores.Add( errorNuevo );
        }

        public override string Message
        {
            get { return this.ObtenerMensajes(); }
        }

        private string ObtenerMensajes()
        {
            string mensaje = "";
            foreach ( Error error in this.errores )
            {
                mensaje = mensaje + error.Mensaje + " (" + error.Codigo.ToString() + ")\r\n";
            }

            return mensaje;
        }
    }
}
