using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public class ManagerAutorizaciones
    {
        private string archivo;
        
        public ManagerAutorizaciones(string nombreServicio)
        {
            this.archivo = Path.Combine(Environment.CurrentDirectory, "Vigencia" + nombreServicio + ".xml");
        }

        public bool VerificarVigencia( IConfiguracionWS configuracionSolicitud )
        {
            Autorizacion autorizacion = this.ObtenerAutorizacionDesdeArchivo(); ;
            bool retorno = (autorizacion.Expiracion > DateTime.Now) && ( autorizacion.Cuit == configuracionSolicitud.Cuit );

            return retorno;
        }

        public void SerializarVigencia( Autorizacion autorizacion )
        { 
            TextWriter salida = new StreamWriter( this.archivo );
            XmlSerializer serializador = new XmlSerializer( autorizacion.GetType() );
            serializador.Serialize( salida, autorizacion );
            salida.Dispose();            
        }

        public Autorizacion ObtenerAutorizacionVigente()
        {
            Autorizacion autorizacion = this.ObtenerAutorizacionDesdeArchivo();

            return autorizacion;
        }

        private Autorizacion ObtenerAutorizacionDesdeArchivo()
        {
            Autorizacion autorizacion = new Autorizacion();
            
            if (File.Exists(this.archivo))
            {
                XmlSerializer serializador = new XmlSerializer(typeof(Autorizacion));
                Stream reader = new FileStream(this.archivo, FileMode.Open);
                autorizacion = (Autorizacion)serializador.Deserialize(reader);
                reader.Close();
                reader.Dispose();
            }
            return autorizacion;
        }
    }
}
