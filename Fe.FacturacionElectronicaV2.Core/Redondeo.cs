using System;
using System.Collections.Generic;
using System.Text;

namespace Fe.FacturacionElectronicaV2.Core
{    public static class Redondeo
    {
        public static double Aplicar(double valor)
        {
            return (double)Math.Truncate( (decimal)valor * 100 ) / 100;
        }

        public static decimal AplicarDecimal( double valor )
        {
            return Math.Truncate( (decimal) valor * 100 ) / 100;
        }

        public static double Redondear( double valor )
        {
            return (double)Math.Round( (decimal)valor, 2 );
        }

        public static double SumarDoubles( double[] valores )
        {
            Decimal retorno = 0;

            foreach ( double valor in valores )
            {
                retorno += (Decimal)valor;
            }

            return Decimal.ToDouble( retorno );
        }
    }
}
