using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public class Autorizacion
    {
        private string token;
        private string sign;
        private long cuit;
        private DateTime expiracion;

        #region 
        public DateTime Expiracion
        {
            get { return expiracion; }
            set { expiracion = value; }
        }
        public long Cuit
        {
            get { return this.cuit; }
            set { this.cuit = value; }
        }
        public string Sign
        {
            get { return this.sign; }
            set { this.sign = value; }
        }
        public string Token
        {
            get { return this.token; }
            set { this.token = value; }
        } 
        #endregion
    }
}