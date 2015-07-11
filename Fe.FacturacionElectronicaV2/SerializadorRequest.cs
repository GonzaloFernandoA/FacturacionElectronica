using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

namespace Fe.FacturacionElectronicaV2
{
    public class SerializadorRequest
    {
        private string nombre = "request.xml";

        public void Serializar<T>( T valor )
        {
            TextWriter salida = new StringWriter();
            XmlSerializer serializador = new XmlSerializer( typeof( T ) );
            serializador.Serialize( salida, valor );
            File.WriteAllText( this.nombre, salida.ToString() );
            salida.Dispose();
        }

        public virtual void SerializadorConRuta<T>(T cabecera, String Ruta)
        {
            this.nombre = Ruta;
            this.CrearDirectorio(Ruta);
            this.Serializar(cabecera);
        }

        public void CrearDirectorio(String cRuta)
        {
            cRuta = Path.GetDirectoryName(cRuta);
            if (!Directory.Exists(cRuta))
            {
                Directory.CreateDirectory(cRuta);
            }
        }

    }
}
