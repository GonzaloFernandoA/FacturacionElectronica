using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaProbar
{
    public class Comprobante
    {
        public string TipoComprobante { get; set; }
        public long NumeroComprobante { get; set; }
        public int PuntoDeVenta { get; set; }
        public string Concepto { get; set; }

        public string TipoDocumento { get; set; }
        public long NumeroDeDocumento { get; set; }
        public long Numero { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaServicioDesde { get; set; }
        public DateTime FechaServicioHasta { get; set; }
        public DateTime FechaVencimientoPago { get; set; }

        public long ImporteTotal { get; set; }

        public Comprobante() { }

        public Comprobante( DataRow dr )
        {
            this.TipoComprobante = dr.Field<string>("tipocomp");
            this.TipoDocumento = dr.Field<string>("documento");
            this.NumeroDeDocumento = Convert.ToInt64( dr.Field<string>("nrodoc"));
            this.Numero = Convert.ToInt64(dr.Field<string>("numero"));
            this.Fecha = Convert.ToDateTime(dr.Field<string>("fecha"));
            this.ImporteTotal = Convert.ToInt64(dr.Field<string>("importe"));
        }

        public Comprobante(string xml)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(xml);
            IList<DataRow> dr = ds.Tables[0].AsEnumerable().ToList();

          //  this.TipoComprobante = dr.Select(x => x.Field<string>("tipocomp"));

            this.TipoComprobante = dr[0].ToString();

  
        
        }

    }
}
