using System;
namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public interface IAccesoDisco
    {
        byte[] ObtenerBytes( string argArchivo );
        bool ArchivoExiste( string rutaArchivo );
    }
}
