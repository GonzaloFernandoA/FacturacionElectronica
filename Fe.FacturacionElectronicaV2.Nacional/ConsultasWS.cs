using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fe.FacturacionElectronicaV2.Core.Interfaces;
using Fe.FacturacionElectronicaV2.Core.LoginWSAA;
using System.Web.Services.Protocols;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaMTXCA.Nacional.WebServices;

namespace Fe.FacturacionElectronicaV2.Nacional
{
    public abstract class ConsultasWS
    {
        public string ObtenerCodigoComprobante( Autorizacion aut, int idTipoComprobante )
        {
            List<IValorRespuestaWS> comprobantes = this.ObtenerTiposDeComprobante( aut );
            IEnumerable<IValorRespuestaWS> retorno = from p in comprobantes
                where p.ObtenerId().Equals( idTipoComprobante.ToString() )
                    select p;

            return retorno.ElementAt( 0 ).Descripcion;
        }

        public abstract List<IValorRespuestaWS> ObtenerTiposDeComprobante( Autorizacion aut );
        public abstract int UltimoComprobante( Autorizacion aut, int ptovta, int tipo );
        public abstract SoapHttpClientProtocol ObtenerWS();
        public abstract CAEDetalleRespuesta DatosDeComprobante( Autorizacion aut, int tipoComprobante, int nroComprobante, int ptoVta );
        public abstract CodigoDescripcionType[] ObtenerUnidadesDeMedida( Autorizacion aut );
    }
}
