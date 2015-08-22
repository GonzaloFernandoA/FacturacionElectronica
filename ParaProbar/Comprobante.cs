using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesadorCae
{
    public class Comprobante
    {
        public string TipoComprobante { get; set; }
        public long NumeroComprobante { get; set; }
        public int PuntoDeVenta { get; set; }
        public string Concepto { get; set; }
        public int TipoServicio { get; set; }

        public string TipoDocumento { get; set; }
        public long NumeroDeDocumento { get; set; }
     
        public DateTime Fecha { get; set; }
        public DateTime FechaServicioDesde { get; set; }
        public DateTime FechaServicioHasta { get; set; }
        public DateTime FechaVencimientoPago { get; set; }

        public long ImporteNeto { get; set; }
        public long ImporteIva { get; set; }
        public long ImporteTotal { get; set; }

        public Comprobante() { }

        public Comprobante( DataRow dr )
        {
            this.TipoComprobante = dr.Field<string>("tipocomp");
            this.TipoDocumento = dr.Field<string>("documento");
            this.NumeroDeDocumento = Convert.ToInt64( dr.Field<string>("nrodoc"));
            this.NumeroComprobante = Convert.ToInt64(dr.Field<string>("numero"));
            this.Fecha = Convert.ToDateTime(dr.Field<string>("fecha"));
            this.FechaServicioDesde = Convert.ToDateTime(dr.Field<string>("fechaInicio"));
            this.FechaServicioHasta = Convert.ToDateTime(dr.Field<string>("fechaFin"));
            this.FechaVencimientoPago = Convert.ToDateTime(dr.Field<string>("fechaVto"));
            this.ImporteTotal = long.Parse(dr.Field<string>("importe").Replace(".", ""))/100;
            this.TipoServicio = Convert.ToInt32(dr.Field<string>("tipo"));
            this.PuntoDeVenta = Convert.ToInt32(dr.Field<string>("pos"));
        }
    }
}