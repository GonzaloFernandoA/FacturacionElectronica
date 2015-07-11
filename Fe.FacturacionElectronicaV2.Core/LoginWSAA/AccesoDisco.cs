using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public class AccesoDisco : IAccesoDisco
    {
        public byte[] ObtenerBytes( string argArchivo )
        {
            return File.ReadAllBytes( argArchivo );
        }

        public bool ArchivoExiste( string rutaArchivo )
        {
            return File.Exists( rutaArchivo );
        }
    }
}
