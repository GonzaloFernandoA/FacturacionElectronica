using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ProcesadorCae
{
    public class EquivalenciaTipoConcepto
    {
        public string ObtenerEquivalencia(int tipoConcepto)
        {
            string concepto = "";

            switch (tipoConcepto)
            {
                case 1: concepto = "P"; break;
                case 2: concepto = "S"; break;
                case 3: concepto = "PS"; break;
            }

            return concepto;
        }
    }
}
