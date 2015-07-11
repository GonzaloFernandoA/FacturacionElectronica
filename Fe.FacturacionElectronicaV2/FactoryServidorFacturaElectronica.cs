using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services.Protocols;
using Fe.FacturacionElectronicaV2.Core.Logueos;

namespace Fe.FacturacionElectronicaV2
{
    public class FactoryServidorFacturaElectronica
    {
        public static IServidorFacturaElectronica ObtenerInstancia( TipoWebService tipo, SoapHttpClientProtocol ws, LogueadorFe logueador )
        {
            IServidorFacturaElectronica retorno = null;

            switch ( tipo )
            {
                case TipoWebService.Nacional:
                    retorno = new ServidorFacturaElectronica( ws, logueador );
                    break;
                case TipoWebService.MTXCA:
                    retorno = new ServidorFacturaElectronicaMTXCA( ws, logueador );
                    break;
                default:
                    break;
            }

            return retorno;
        }
    }
}
