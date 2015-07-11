using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public class ValidadorDeConfiguracion : IValidadorDeConfiguracion
    {
        public ValidadorDeConfiguracion( IAccesoDisco accesoDisco )
        {
            this.accesoDisco = accesoDisco;
        }

        public void Validar( IConfiguracionWS configuracion )
        {
            if ( configuracion.RutaCertificado.Length == 0 )
                throw new ValidacionException("ValidarDatosBasicos", "Falta la ubicacion del certificado.");

            if ( !this.accesoDisco.ArchivoExiste( configuracion.RutaCertificado ) )
                throw new ValidacionException("ValidarDatosBasicos", "No se encontro el certificado.");

            if ( configuracion.Cuit == 0 )
                throw new ValidacionException("ValidarDatosBasicos", "Falta especificar el número de C.U.I.T.");

            if ( configuracion.UrlLogin.Length == 0 )
                throw new ValidacionException("ValidarDatosBasicos", "Falta especificar la URL de servidor.");

            if ( configuracion.NombreServicio.Length == 0 )
                throw new ValidacionException("ValidarDatosBasicos", "Falta especificar el servicio al cual se va a conectar.");
        }
        public IAccesoDisco accesoDisco { get; set; }
    }
}
