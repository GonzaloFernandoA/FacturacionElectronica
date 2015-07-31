using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaProbar
{
    public class Iva
    {
        public int Tipo { get; set; }
        public long MontoGrabado { get; set; }
        public long MontoIva { get; set; }
    
       public Iva( DataRow dr )
        {
            this.Tipo = Convert.ToInt32(dr.Field<string>("Tipo"));
            this.MontoGrabado = long.Parse(dr.Field<string>("MontoGrabado").Replace(".", "")) / 100;
            this.MontoIva = long.Parse(dr.Field<string>("MontoIva").Replace(".", "")) / 100; 
        }
    }

}
