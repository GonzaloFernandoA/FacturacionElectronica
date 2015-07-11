using System.Collections.Generic;
using Fe.FacturacionElectronicaV2.Core.Interfaces;

namespace Fe.FacturacionElectronicaV2
{
    public class ContenedorDatosEquivalencias
    {
        private List<CodigoEnAplicacion> monedasCodigosApp;
        private List<IValorRespuestaWS> monedasItemsEquivalencias;

        private List<IValorRespuestaWS> comprobantesItemsEquivalencias;
        private List<IValorRespuestaWS> conceptosItemsEquivalencias;
        private List<IValorRespuestaWS> tiposDocumentoItemsEquivalencias;
        private List<IValorRespuestaWS> tiposDeIvaItemsEquivalencias;
        private List<IValorRespuestaWS> tiposDeTributoItemsEquivalencias;


        #region getters/setters
        public List<IValorRespuestaWS> TiposDeTributoItemsEquivalencias
        {
            get { return this.tiposDeTributoItemsEquivalencias; }
            set { this.tiposDeTributoItemsEquivalencias = value; }
        }
        public List<IValorRespuestaWS> TiposDeIvaItemsEquivalencias
        {
            get { return this.tiposDeIvaItemsEquivalencias; }
            set { this.tiposDeIvaItemsEquivalencias = value; }
        }
        public List<IValorRespuestaWS> TiposDocumentoItemsEquivalencias
        {
            get { return this.tiposDocumentoItemsEquivalencias; }
            set { this.tiposDocumentoItemsEquivalencias = value; }
        }
        public List<IValorRespuestaWS> ConceptosItemsEquivalencias
        {
            get { return this.conceptosItemsEquivalencias; }
            set { this.conceptosItemsEquivalencias = value; }
        }
        public List<IValorRespuestaWS> ComprobantesItemsEquivalencias
        {
            get { return comprobantesItemsEquivalencias; }
            set { comprobantesItemsEquivalencias = value; }
        }

        public List<IValorRespuestaWS> MonedasItemsEquivalencias
        {
            get { return this.monedasItemsEquivalencias; }
            set { this.monedasItemsEquivalencias = value; }
        }

        public List<CodigoEnAplicacion> MonedasCodigosApp
        {
            get { return this.monedasCodigosApp; }
            set { this.monedasCodigosApp = value; }
        }
        #endregion
    }
}
