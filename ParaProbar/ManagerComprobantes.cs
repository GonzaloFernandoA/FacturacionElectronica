using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace ProcesadorCae
{
    public class ManagerComprobantes
    {
        public List<Comprobante> ObtenerComprobantes(string xml)
        {
            List<Comprobante> comprobantes = new List<Comprobante>();
            DataSet ds = new DataSet();
            ds.ReadXml(xml);
            IList<DataRow> dr = ds.Tables[0].AsEnumerable().ToList();
            comprobantes = dr.Select(x => new Comprobante(x)).ToList();
            return comprobantes;
        }

        public List<Iva> ObtenerIva(string xml)
        {
            List<Iva> ivas = new List<Iva>();
  
            DataSet ds = new DataSet();
            ds.ReadXml(xml);
            IList<DataRow> dr = ds.Tables[0].AsEnumerable().ToList();
            ivas = dr.Select(x => new Iva(x)).ToList();
            return ivas;
        }

    }
}
