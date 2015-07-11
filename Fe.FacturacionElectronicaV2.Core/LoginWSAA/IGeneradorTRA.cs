using System;
using System.Xml;

namespace Fe.FacturacionElectronicaV2.Core.LoginWSAA
{
    public interface IGeneradorTRA
    {
        XmlDocument Crear( IConfiguracionWS configuracion );
    }
}
