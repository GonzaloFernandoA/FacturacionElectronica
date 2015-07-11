using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fe.FacturacionElectronicaMTXCA.Nacional.WebServices;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Core;

namespace Fe.FacturacionElectronicaMTXCA.Nacional.Wrappers
{
    public class WrapperArticuloMTXCA
    {
        public ItemType Convertir( Articulo articulo )
        {
            ItemType articuloAFIP = new ItemType();
            articuloAFIP.unidadesMtx = articulo.UnidadesMtx;
            articuloAFIP.codigoMtx = articulo.CodigoMtx;
            articuloAFIP.codigo = articulo.Codigo;
            articuloAFIP.descripcion = articulo.Descripcion;
            articuloAFIP.cantidad = (decimal) articulo.Cantidad;
            articuloAFIP.codigoUnidadMedida = (short) articulo.UnidadMedidaCodigo;
            articuloAFIP.precioUnitario = (decimal) Redondeo.Aplicar( articulo.PrecioUnitario );
            articuloAFIP.importeBonificacion = (decimal) Redondeo.Aplicar( articulo.ImporteBonificacion );
            articuloAFIP.codigoCondicionIVA = (short) articulo.CondicionIVACodigo;
            articuloAFIP.importeIVA = (decimal) Redondeo.Aplicar( articulo.ImporteIVA );
            articuloAFIP.importeItem = (decimal) Redondeo.Aplicar( articulo.ImporteItem );

            articuloAFIP.unidadesMtxSpecified = (articulo.UnidadesMtx>0);
            articuloAFIP.cantidadSpecified = (articulo.Cantidad!=0);
            articuloAFIP.precioUnitarioSpecified = (articulo.PrecioUnitario != 0);
            articuloAFIP.importeBonificacionSpecified = (articulo.ImporteBonificacion != 0);
            
            return articuloAFIP;
        }
    }
}
