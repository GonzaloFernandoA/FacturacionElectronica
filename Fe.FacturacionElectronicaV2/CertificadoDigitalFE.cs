using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace Fe.FacturacionElectronicaV2
{
    public class CertificadoDigitalFE
    {        
        public String ObtenerFechaDeVencimientoCertificadoDigital(String tcFileNameCertificadoDigital)
        {

            X509Certificate m_certificado = new X509Certificate(tcFileNameCertificadoDigital);

            String lcFechaVencimiento = "";

            if (m_certificado != null)
            {
                lcFechaVencimiento = m_certificado.GetExpirationDateString();
            }

            return lcFechaVencimiento;
        }

        public int ObtenerDiasHastaElVencimientoCertificadoDigital(String tcFileNameCertificadoDigital)
        {
            String lcFechaVencimientoString = this.ObtenerFechaDeVencimientoCertificadoDigital(tcFileNameCertificadoDigital);
            DateTime ldFechaVencimiento = Convert.ToDateTime(lcFechaVencimientoString);

            DateTime ldFechaActual = DateTime.Now;

            TimeSpan ts = ldFechaVencimiento - ldFechaActual;

            int dias = ts.Days;
            return dias;
        }

        public Boolean CertificadoDigitalVencido(String tcFileNameCertificadoDigital)
        {
            String lcFechaVencimientoString = this.ObtenerFechaDeVencimientoCertificadoDigital(tcFileNameCertificadoDigital);
            DateTime ldFechaVencimiento = Convert.ToDateTime(lcFechaVencimientoString);

            DateTime ldFechaActual = DateTime.Now;
            Boolean lbVencido = false;

            if (ldFechaVencimiento <= ldFechaActual)
                lbVencido = true;

            return lbVencido;
        }

    }
}
