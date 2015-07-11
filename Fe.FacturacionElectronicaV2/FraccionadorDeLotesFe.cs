using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;

namespace Fe.FacturacionElectronicaV2
{
    public class FraccionadorDeLotesFe
    {
        public FeCabecera ObtenerProcesar( FeCabecera feCab, int ultimoNroWs )
        {
            FeCabecera feCabRetorno = new FeCabecera();
            foreach ( FeDetalle det in feCab.DetalleComprobantes )
            {
                if ( det.ComprobanteDesde > ultimoNroWs )
                {
                    feCabRetorno.DetalleComprobantes.Add( det );
                }
            }
            if ( feCabRetorno.DetalleComprobantes.Count == 0 )
            {
                feCabRetorno = null;
            }
            else
            {
                this.AsignarDatosCabecera( feCab, feCabRetorno );
            }

            return feCabRetorno;
        }

        public FeCabecera ObtenerReprocesar( FeCabecera feCab, int ultimoNroWs )
        {
            FeCabecera feCabRetorno = new FeCabecera();
            foreach ( FeDetalle det in feCab.DetalleComprobantes )
            {
                if ( det.ComprobanteDesde <= ultimoNroWs )
                {
                    feCabRetorno.DetalleComprobantes.Add( det );
                }
            }
            if ( feCabRetorno.DetalleComprobantes.Count == 0 )
            {
                feCabRetorno = null;
            }
            else
            {
                this.AsignarDatosCabecera( feCab, feCabRetorno );
            }

            return feCabRetorno;
        }

        private void AsignarDatosCabecera( FeCabecera feCabOrigen, FeCabecera feCabDestino )
        {
            feCabDestino.CantidadDeRegistros = feCabDestino.DetalleComprobantes.Count;
            feCabDestino.PuntoDeVenta = feCabOrigen.PuntoDeVenta;
            feCabDestino.TipoComprobante = feCabOrigen.TipoComprobante;
        }
    }
}
