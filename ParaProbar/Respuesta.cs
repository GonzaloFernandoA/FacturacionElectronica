using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesadorCae
{
    public class Respuesta
    {
        public string Cae { get; set; }
        public List<string> Problema { get; set; }

        public Respuesta()
        {
            this.Cae = "";
            this.Problema = new List<string>();
        }

        public void AgregarProblema(string mensaje)
        {
            this.Problema.Add(mensaje);
        }

        public void ToXmlVfp(string path)
        {
            DataTable dt = new DataTable();
            dt.TableName = "Tabla1";

            DataColumn campo1 = new DataColumn();
            campo1.DataType = typeof(string);
            campo1.MaxLength = 20;
            campo1.ColumnName = "Cae";

            DataColumn campo2 = new DataColumn();
            campo2.DataType = typeof(string);
            campo2.MaxLength = 500;
            campo2.ColumnName = "Problema";

            dt.Columns.Add(campo1);
            dt.Columns.Add(campo2);


            foreach (string mensaje in this.Problema)
            {
                DataRow registro1 = dt.NewRow();
                registro1[0] = this.Cae;
                registro1[1] = mensaje;
                dt.Rows.Add(registro1);
            }

            dt.WriteXml(path);
        }
    }
  }
