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
            this.FechaServicioDesde = Convert.ToDateTime(dr.Field<string>("fechaInicio"));
            this.FechaServicioHasta = Convert.ToDateTime(dr.Field<string>("fechaFin"));
            this.FechaVencimientoPago = Convert.ToDateTime(dr.Field<string>("fechaVto"));
            this.ImporteTotal = long.Parse(dr.Field<string>("importe").Replace(".", ""))/100;
        }
    }
}