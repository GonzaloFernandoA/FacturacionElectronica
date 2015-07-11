using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public class WSAAProxy: IWSAAProxy
    {
        private WSAA wsaa;
        public WSAAProxy( WSAA wsaa )
        {
            this.wsaa = wsaa;
        }

        public IWebProxy Proxy
        {
            get { return this.wsaa.Proxy; }
            set { this.wsaa.Proxy = value; }
        }

        public int Timeout
        {
            get { return this.wsaa.Timeout; }
            set { this.wsaa.Timeout = value; }
        }

        public string loginCms( string certificado64 )
        {
            return this.wsaa.loginCms( certificado64 );
        }

        public string Url
        {
            get { return this.wsaa.Url; }
            set { this.wsaa.Url = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.wsaa.Dispose();
        }

        #endregion
    }
}
