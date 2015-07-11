using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Fe.FacturacionElectronicaV2.Core.Interfaces;

namespace Fe.FacturacionElectronicaV2.Nacional.Equivalencias
{
    public class FeCabecera : ISerializable
    {
        private int cantidadDeRegistros = 0;
        private int tipoComprobante = 0;
        private int puntoDeVenta = 0;
        private List<FeDetalle> detalleComprobantes = new List<FeDetalle>();

        #region getters/setters
        public int PuntoDeVenta
        {
            get { return this.puntoDeVenta; }
            set { this.puntoDeVenta = value; }
        }
        public int TipoComprobante
        {
            get { return this.tipoComprobante; }
            set { this.tipoComprobante = value; }
        }
        public int CantidadDeRegistros
        {
            get { return this.cantidadDeRegistros; }
            set { this.cantidadDeRegistros = value; }
        }
        public List<FeDetalle> DetalleComprobantes
        {
            get { return this.detalleComprobantes; }
            set { this.detalleComprobantes = value; }
        }
        #endregion

        public FeCabecera()
        {

        }

        public string Serializar()
        {
            string retorno;

            TextWriter salida = new StringWriter();
            XmlSerializer serializador = new XmlSerializer(this.GetType());
            serializador.Serialize(salida, this);
            retorno = salida.ToString();
            salida.Dispose();

            return retorno;
        }
    }
}
