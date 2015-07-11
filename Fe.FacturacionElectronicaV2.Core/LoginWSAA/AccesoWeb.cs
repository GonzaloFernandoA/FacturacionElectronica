using System.Net;

namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public class AccesoWeb : IAccesoWeb
    {
        public void ChequearAcceso( string url )
        {
            WebRequest request = WebRequest.Create( url );

            try
            {
                WebResponse response = request.GetResponse();
            }
            finally
            {
                request.Abort();
                request = null;
            }
        }
    }
}
