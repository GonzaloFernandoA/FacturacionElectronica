using System.Collections.Generic;
using System.Globalization;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.ExportacionV0.Equivalencias;
using Fe.FacturacionElectronicaV2.Core.Interfaces;
using Fe.FacturacionElectronicaV2.Exportacion;
using Fe.FacturacionElectronicaV2.Exportacion.WebServices;

namespace Fe.FacturacionElectronicaV2.ExportacionV0
{
    public class BuscadorDeDiferencias
    {
        public List<Observacion> Obtener(ClsFEXGetCMPR comprobanteAfip, FexCabecera cabFex)
        {
            List<string> diferencias = new List<string>();

            if ( comprobanteAfip.Fecha_cbte != cabFex.FechaComprobante )
            {
                diferencias.Add("La fecha no es la correcta.");
                diferencias.Add("Afip: " + comprobanteAfip.Fecha_cbte + " Enviado :" + cabFex.FechaComprobante);
            }

            if ( comprobanteAfip.Cbte_nro != cabFex.ComprobanteNumero )
            {
                diferencias.Add("El número no es el correcto.");
                diferencias.Add("Afip: " + comprobanteAfip.Cbte_nro + " Enviado :" + cabFex.ComprobanteNumero);
            }

            if ( comprobanteAfip.Incoterms !=  cabFex.ClausulaDeVenta )
            {
                diferencias.Add("El incoterms no es el correcto.");
                diferencias.Add("Afip: " + comprobanteAfip.Incoterms + " Enviado :" + cabFex.ClausulaDeVenta);
            }

            if (comprobanteAfip.Cuit_pais_cliente != cabFex.CuitPaisCliente)
            {
                diferencias.Add("El C.U.I.T. no es el correcto.");
                diferencias.Add("Afip: " + comprobanteAfip.Cuit_pais_cliente + " Enviado :" + cabFex.CuitPaisCliente);
            }

            if (comprobanteAfip.Imp_total != (decimal) cabFex.ImporteTotal)
            {
                diferencias.Add("El total no es correcto.");
                diferencias.Add("Afip: " + comprobanteAfip.Imp_total.ToString(CultureInfo.InvariantCulture.NumberFormat) + " Enviado :" + cabFex.ImporteTotal.ToString(CultureInfo.InvariantCulture.NumberFormat));
            }

            List<Observacion> observaciones = new List<Observacion>();
            Observacion observacion;

            for (int i = 0; i < diferencias.Count; i++)
            {
                observacion = new Observacion();
                observacion.Mensaje = diferencias[i];
                observaciones.Add( observacion );
            }

            return observaciones;
        }
    }
}
