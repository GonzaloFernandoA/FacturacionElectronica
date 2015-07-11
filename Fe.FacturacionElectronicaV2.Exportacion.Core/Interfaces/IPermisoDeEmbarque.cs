using System;

namespace Fe.FacturacionElectronicaV2.Exportacion.Core
{
    public interface IPermisoDeEmbarque
    {
        int DestinoMercaderia { get; set; }
        string Id { get; set; }
    }
}
