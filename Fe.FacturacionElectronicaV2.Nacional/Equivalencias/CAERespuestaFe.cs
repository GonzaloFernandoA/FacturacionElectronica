using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;

namespace Fe.FacturacionElectronicaV2.Nacional.Equivalencias
{
    public class CAERespuestaFe
    {
        private CAECabeceraRespuesta cabecera;
        private List<CAEDetalleRespuesta> detalle;

        #region getters/setters
        public List<CAEDetalleRespuesta> Detalle
        {
            get { return this.detalle; }
            set { this.detalle = value; }
        }

        public CAECabeceraRespuesta Cabecera
        {
            get { return this.cabecera; }
            set { this.cabecera = value; }
        }
        #endregion


        public void Unir( CAERespuestaFe caeResp )
        {
            this.cabecera.CantidadDeRegistros = this.cabecera.CantidadDeRegistros + caeResp.cabecera.CantidadDeRegistros;
            foreach ( CAEDetalleRespuesta item in caeResp.detalle )
            {
                this.detalle.Add( item );
            }
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
