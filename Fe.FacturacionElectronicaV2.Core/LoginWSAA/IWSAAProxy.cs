using System;
using System.Net;
namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public interface IWSAAProxy : IDisposable
    {
        string loginCms( string certificado64 );
        IWebProxy Proxy { get; set; }
        int Timeout { get; set; }
        string Url { get; set; }
    }
}
