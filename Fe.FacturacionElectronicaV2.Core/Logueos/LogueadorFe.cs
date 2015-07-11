using System.Collections.Generic;
// using ZoologicSA.Core.Logueo;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.Core.Interfaces;

namespace Fe.FacturacionElectronicaV2.Core.Logueos
{
    public class LogueadorFe
    {
        public LogueadorFe()
        {
         //    Logueador.Registrador = new RegistradorDeLogueos(new ConfiguracionLogueo());            
        }
        
        public void Loguear( string mensaje )
        {
//            Logueador.Loguear(mensaje);
            System.IO.File.WriteAllText( "FeLogueo.txt", mensaje );
        }

        public void LoguearObservaciones( List<CAEDetalleRespuesta> caeDetResp, ISerializable serializable )
        {
            string mensaje = "";
            foreach ( CAEDetalleRespuesta detalle in caeDetResp )
            {
                if ( detalle.Observaciones != null )
                {
                    foreach ( Observacion observacion in detalle.Observaciones )
                    {
                        mensaje = mensaje + "\r\n" +
                                    observacion.Mensaje + " (" + observacion.Codigo + ")";
                    }
                }
            }
            if ( !mensaje.Equals( "" ) )
            {
                this.Loguear( serializable.Serializar() + "\r\n" + mensaje );
            }
        }

        public void LoguearObservaciones( string mensaje, ISerializable serializable )
        {
            if ( !string.IsNullOrEmpty( mensaje ) )
            {
                this.Loguear( serializable.Serializar() + "\r\n" + mensaje );
            }
        }

        public void LoguearError( string mensaje, ISerializable serializable )
        {
            this.Loguear( serializable.Serializar() + "\r\n\r\n" + mensaje );

        }
    }
}
