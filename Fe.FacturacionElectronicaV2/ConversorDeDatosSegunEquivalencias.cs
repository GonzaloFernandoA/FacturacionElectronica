using System;
using System.Collections.Generic;
using Fe.FacturacionElectronicaV2.Core.Interfaces;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;

namespace Fe.FacturacionElectronicaV2
{
    public class ConversorDeDatosSegunEquivalencias
    {
        private ContenedorDatosEquivalencias contenedorDeDatosEq;

        public ConversorDeDatosSegunEquivalencias( ContenedorDatosEquivalencias contenedorDeDatosEq )
        {
            this.contenedorDeDatosEq = contenedorDeDatosEq;
        }

        public void Convertir( FeCabecera fc )
        {
            fc.TipoComprobante = this.ConvertirInt( fc.TipoComprobante, this.contenedorDeDatosEq.ComprobantesItemsEquivalencias );
            foreach ( FeDetalle detalle in fc.DetalleComprobantes )
            {
                detalle.Concepto = this.ConvertirInt( detalle.Concepto, this.contenedorDeDatosEq.ConceptosItemsEquivalencias );
                detalle.DocumentoTipo = this.ConvertirInt( detalle.DocumentoTipo, this.contenedorDeDatosEq.TiposDocumentoItemsEquivalencias );
                detalle.MonedaId = this.ConvertirString( detalle.MonedaId, this.contenedorDeDatosEq.MonedasItemsEquivalencias );
                foreach ( IVA iva in detalle.Iva )
                {
                    iva.Id = this.ConvertirInt( iva.Id, this.contenedorDeDatosEq.TiposDeIvaItemsEquivalencias );
                }
                foreach ( TributoComprobante tributo in detalle.Tributos )
                {
                    tributo.Id = this.ConvertirShort( tributo.Id, this.contenedorDeDatosEq.TiposDeTributoItemsEquivalencias );
                }
            }
        }

        private string ConvertirString( string valorActual, List<IValorRespuestaWS> equivalencias )
        {
            IValorRespuestaWS valor = equivalencias.
                Find( x => x.Equivalencia.Equals( valorActual, StringComparison.OrdinalIgnoreCase ) );

            if ( valor == null )
            {
                NotSupportedException ex = new NotSupportedException( "No se encuentra el valor '" + valorActual + "'" );
                throw ex;
            }

            return valor.ObtenerId();
        }

        private int ConvertirInt( int valorActual, List<IValorRespuestaWS> equivalencias )
        {
            string valor = this.ConvertirString( valorActual.ToString(), equivalencias );
            return int.Parse( valor );
        }

        private short ConvertirShort( int valorActual, List<IValorRespuestaWS> equivalencias )
        {
            string valor = this.ConvertirString( valorActual.ToString(), equivalencias );
            return short.Parse( valor );
        }
    }
}
