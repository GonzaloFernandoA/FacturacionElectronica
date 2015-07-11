using System.Collections.Generic;

using System.IO;
using System.Xml.Serialization;
using ZooLogicSA.FacturacionElectronicaV2.Nacional.Equivalencias;
using ZooLogicSA.FacturacionElectronicaV2.Nacional.Interfaces;

namespace ZooLogicSA.FacturacionElectronicaV2.ExportacionV1.Equivalencias
{
    public class CAERespuestaFex : ISerializable
    {
        private long id;
        private long cuit;
        private string fechaComprobante;
        private short tipoComprobante;
        private short puntoDeVenta;
        private long comprobanteNumero;
        private string cae;
        private string fechaVencimientoCae;
        private string resultado;
        private string reproceso;
        private string motivosObservaciones;
        private List<Observacion> observaciones;

        public List<Observacion> Observaciones
        {
            get { return this.observaciones; }
            set { this.observaciones = value; }
        }

        #region getters/setters
        public string MotivosObservaciones
        {
            get { return this.motivosObservaciones; }
            set { this.motivosObservaciones = value; }
        }
        public string Reproceso
        {
            get { return this.reproceso; }
            set { this.reproceso = value; }
        }
        public string Resultado
        {
            get { return this.resultado; }
            set { this.resultado = value; }
        }
        public string FechaComprobante
        {
            get { return this.fechaComprobante; }
            set { this.fechaComprobante = value; }
        }
        public string FechaVencimientoCae
        {
            get { return this.fechaVencimientoCae; }
            set { this.fechaVencimientoCae = value; }
        }

        public long ComprobanteNumero
        {
            get { return this.comprobanteNumero; }
            set { this.comprobanteNumero = value; }
        }
        public short PuntoDeVenta
        {
            get { return this.puntoDeVenta; }
            set { this.puntoDeVenta = value; }
        }
        public short TipoComprobante
        {
            get { return this.tipoComprobante; }
            set { this.tipoComprobante = value; }
        }
        public long Cuit
        {
            get { return this.cuit; }
            set { this.cuit = value; }
        }
        public long Id
        {
            get { return this.id; }
            set { this.id = value; }
        }        
        public string Cae
        {
            get { return this.cae; }
            set { this.cae = value; }
        }
	    #endregion

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
