using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public class FactoriaHerramientasWSAA : IFactoriaHerramientasWSAA
    {
        public IWSAAProxy ObtenerWSAA( IConfiguracionWS config )
        {
            WSAA wsaa = new WSAA();
            WSAAProxy retorno = new WSAAProxy( wsaa );
            retorno.Timeout = config.TiempoDeEspera;
            return retorno;
        }

        public IGeneradorTRA ObtenerGeneradorTRA()
        {
            return new GeneradorTRA();
        }

        public IAccesoWeb ObtenerAccesoWeb()
        {
            return new AccesoWeb();
        }

        public ServidorAutenticacion ObtenerServidorAutenticacion( ConfiguracionWS config )
        {
            return new ServidorAutenticacion( this, config );
        }

        public IFirmadorDeCertificado ObtenerFirmadorDeCertificados()
        {
            IManejadorDeErroresWSAA manejadorErrores = this.ObtenerManejadorErrores();
            IAccesoDisco accesoDisco = new AccesoDisco();
            return new FirmadorDeCertificado( manejadorErrores, accesoDisco );
        }

        public IManejadorDeErroresWSAA ObtenerManejadorErrores()
        {
            return new ManejadorDeErroresWSAA();
        }

        public IValidadorDeConfiguracion ObtenerValidadorDeConfiguracion()
        {
            IAccesoDisco accesoDisco = new AccesoDisco();
            return new ValidadorDeConfiguracion( accesoDisco );
        }

        public IDeserializadorDeRespuestaLogin ObtenerDeserializadorDeRespuestaLogin( IConfiguracionWS config )
        {
            return new DeserializadorDeRespuestaLogin( config );
        }

    }
}
