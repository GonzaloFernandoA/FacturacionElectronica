using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fe.FacturacionElectronicaMTXCA.Nacional.WebServices;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;

namespace Fe.FacturacionElectronicaMTXCA.Nacional.Wrappers
{
    public class WrapperCabeceraMTXCA
    {
        public ComprobanteType Convertir( FeCabecera cabFe )
        {
            WrapperDetalleMTXCA wdm = new WrapperDetalleMTXCA();
            ComprobanteType comprobante = wdm.Convertir( cabFe.DetalleComprobantes[0] );

            this.SetearEnItemsEspecificacionesObligatoriasSegunComprobante( cabFe, comprobante );

            comprobante.numeroPuntoVenta = (short) cabFe.PuntoDeVenta;
            comprobante.codigoTipoComprobante = (short) cabFe.TipoComprobante;

            return comprobante;
        }

        private void SetearEnItemsEspecificacionesObligatoriasSegunComprobante( FeCabecera cabFe, ComprobanteType comprobante )
        {
            foreach ( ItemType item in comprobante.arrayItems )
            {
                item.importeIVASpecified = (cabFe.TipoComprobante >= 1 && cabFe.TipoComprobante <= 3);
            }
        }

        //public Obs[] Comparar( FECompConsResponse comprobanteAfip, FeDetalle comprobante )
        //{
        //    BuscadorDeDiferencias buscador = new BuscadorDeDiferencias();
        //    return buscador.Obtener( comprobanteAfip, comprobante );
        //}
    }
}
