using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaMTXCA.Nacional.WebServices;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;

namespace Fe.FacturacionElectronicaMTXCA.Nacional.Wrappers
{
    public class WrapperCaeRespuestaMTXCA
    {
        public CAERespuestaFe Convertir( ResultadoSimpleType resultado, ComprobanteCAEResponseType caeResp, CodigoDescripcionType[] obs, CodigoDescripcionType[] errores )
        {
            CAERespuestaFe respuesta = new CAERespuestaFe();

            CAECabeceraRespuesta cabResp = new CAECabeceraRespuesta();
            cabResp.CantidadDeRegistros = 1;
            cabResp.Cuit = caeResp.cuit;
            cabResp.PuntoDeVenta = caeResp.numeroPuntoVenta;
            cabResp.TipoComprobante = caeResp.codigoTipoComprobante;
            cabResp.Resultado = resultado.ToString();
            cabResp.FechaProceso = caeResp.fechaEmision.ToString( "yyyyMMdd" );

            CAEDetalleRespuesta detResp = new CAEDetalleRespuesta();
            detResp.Cae = caeResp.CAE.ToString();
            detResp.CaeFechaVencimiento = caeResp.fechaVencimientoCAE.ToString( "yyyyMMdd" );
            detResp.ComprobanteDesde = caeResp.numeroComprobante;
            detResp.ComprobanteHasta = caeResp.numeroComprobante;
            detResp.Resultado = resultado.ToString();

            if ( obs != null && obs.Length > 0 )
            {
                detResp.Observaciones = this.ObtenerObservaciones( obs );
            }

            respuesta.Cabecera = cabResp;
            respuesta.Detalle = new List<CAEDetalleRespuesta>();
            respuesta.Detalle.Add( detResp );

            return respuesta;
        }

        private List<Observacion> ObtenerObservaciones( CodigoDescripcionType[] obs )
        {
            List<Observacion> observaciones = new List<Observacion>();
            Observacion observacion;
            foreach ( CodigoDescripcionType item in obs )
            {
                if ( !string.IsNullOrEmpty( item.descripcion ) )
                {
                    observacion = new Observacion();
                    observacion.Codigo = item.codigo;
                    observacion.Mensaje = item.descripcion;
                    observaciones.Add( observacion );
                }
            }

            return observaciones;
        }
    }
}
