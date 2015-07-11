using Fe.FacturacionElectronicaV2.Core.Logueos;
using Fe.FacturacionElectronicaV2.Core.Interfaces;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;
using Fe.FacturacionElectronicaV2.Exportacion.WebServices;
using Fe.FacturacionElectronicaMTXCA.Nacional.WebServices;

namespace Fe.FacturacionElectronicaV2.Core
{
    public class ManagerErroresFe
    {
        private LogueadorFe logueador;

        public ManagerErroresFe( LogueadorFe logueador )
        {
            this.logueador = logueador;
        }

        private void Procesar( IProcesadorError procesadorError, ISerializable serializable )
        {
            ExcepcionFe ex = new ExcepcionFe();
            string mensaje = procesadorError.Procesar( ref ex );

            if ( serializable == null )
            {
                this.logueador.Loguear( mensaje );
            }
            else
            {
                this.logueador.LoguearError( mensaje, serializable );
            }
            throw ex;
        }
        
        public void CapturarError( Err[] errores )
        {
            if ( errores != null )
            {
                IProcesadorError procesadorError = new ProcesadorErrorFe( errores );
                this.Procesar( procesadorError, null );
            }
        }

        public void CapturarError( Err[] errores, ISerializable serializable )
        {
            if ( errores != null )
            {
                IProcesadorError procesadorError = new ProcesadorErrorFe( errores );
                this.Procesar( procesadorError, serializable );
            }            
        }

        public void CapturarError( ClsFEXErr error )
        {
            if ( error != null && !error.ErrCode.Equals(0))
            {
                ExcepcionFe ex = new ExcepcionFe();
                ex.AgregarError( error );
                this.logueador.Loguear( "ERROR DE PROCESO: " + error.ErrMsg + " (" + error.ErrCode + ")" );
                throw ex;
            }
        }

        public void CapturarError( ClsFEXErr error, ISerializable serializable )
        {
            if ( error != null && !error.ErrCode.Equals( 0 ) )
            {
                ExcepcionFe ex = new ExcepcionFe();
                ex.AgregarError( error );
                this.logueador.LoguearError( "ERROR DE PROCESO: " + error.ErrMsg + " (" + error.ErrCode + ")", serializable );
                throw ex;
            }
        }

        public void CapturarError( CodigoDescripcionType[] errores )
        {
            if ( errores != null && errores.Length > 0 )
            {
                if ( errores.Length > 1 || errores[0].codigo != 1502 )
                {   
                    IProcesadorError procesadorError = new ProcesadorErrorMTXCA( errores );
                    this.Procesar( procesadorError, null );
                }
            }
        }

        public void CapturarError( CodigoDescripcionType[] errores, ISerializable serializable )
        {
            if ( errores != null && errores.Length > 0 )
            {
                if ( errores.Length > 1 || errores[0].codigo != 1502 )
                {   
                    IProcesadorError procesadorError = new ProcesadorErrorMTXCA( errores );
                    this.Procesar( procesadorError, serializable );
                }
            }
        }
    }
}