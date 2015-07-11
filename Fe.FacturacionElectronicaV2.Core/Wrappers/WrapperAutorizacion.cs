using Fe.FacturacionElectronicaV2.Core.LoginWSAA;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;
using Fe.FacturacionElectronicaV2.Exportacion.WebServices;
using Fe.FacturacionElectronicaMTXCA.Nacional.WebServices;

namespace Fe.FacturacionElectronicaV2.Core.Wrappers
{
    public class WrapperAutorizacion
    {
        public FEAuthRequest ConvertirFe( Autorizacion aut )
        {
            FEAuthRequest feAutReq = new FEAuthRequest();
            feAutReq.Sign = aut.Sign;
            feAutReq.Token = aut.Token;
            feAutReq.Cuit = aut.Cuit;

            return feAutReq;
        }

        public ClsFEXAuthRequest ConvertirFex( Autorizacion aut )
        {
            ClsFEXAuthRequest feAutReq = new ClsFEXAuthRequest();
            feAutReq.Sign = aut.Sign;
            feAutReq.Token = aut.Token;
            feAutReq.Cuit = aut.Cuit;

            return feAutReq;
        }

        public AuthRequestType ConvertirMTXCA( Autorizacion aut )
        {
            AuthRequestType feAutReq = new AuthRequestType();
            feAutReq.sign = aut.Sign;
            feAutReq.token = aut.Token;
            feAutReq.cuitRepresentada = aut.Cuit;

            return feAutReq;
        }
    }
}
