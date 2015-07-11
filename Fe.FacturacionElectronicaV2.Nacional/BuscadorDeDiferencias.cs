using System.Collections.Generic;
using System.Globalization;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;

namespace Fe.FacturacionElectronicaV2.Nacional
{
    public class BuscadorDeDiferencias
    {
        public List<Observacion> Obtener( CAEDetalleRespuesta comprobanteAfip, FeDetalle comprobante )
        {
            List<Observacion> observaciones = new List<Observacion>();

            if (comprobanteAfip.ComprobanteFecha != comprobante.ComprobanteFecha)
            {
                observaciones.Add( new Observacion() { Mensaje = "La fecha es incorrecta." } );
                observaciones.Add( new Observacion() { Mensaje = "Afip: " + comprobanteAfip.ComprobanteFecha + " Enviado:" + comprobante.ComprobanteFecha } );
            }

            if (comprobanteAfip.ComprobanteDesde != comprobante.ComprobanteDesde)
            {
                observaciones.Add( new Observacion() { Mensaje = "El número no es el correcto." } );
                observaciones.Add( new Observacion() { Mensaje = "Afip: " + comprobanteAfip.ComprobanteDesde + " Enviado :" + comprobante.ComprobanteDesde } );
            }

            if (comprobanteAfip.DocumentoNumero != comprobante.DocumentoNumero)
            {
                observaciones.Add( new Observacion() { Mensaje = "El C.U.I.T./D.N.I. no es el correcto." } );
                observaciones.Add( new Observacion() { Mensaje = "Afip: " + comprobanteAfip.DocumentoNumero + " Enviado :" + comprobante.DocumentoNumero } );
            }

            if (comprobanteAfip.ImporteTotal != comprobante.ImporteTotal)
            {
                observaciones.Add( new Observacion() { Mensaje = "El importe total no es el correcto." } );
                observaciones.Add( new Observacion() { Mensaje = "Afip: " + comprobanteAfip.ImporteTotal.ToString( CultureInfo.InvariantCulture.NumberFormat ) + " Enviado :" + comprobante.ImporteTotal.ToString( CultureInfo.InvariantCulture.NumberFormat ) } );
            }

            return observaciones;
        }
    }
}
