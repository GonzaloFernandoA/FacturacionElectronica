using System.Collections.Generic;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.Core.LoginWSAA;
using Fe.FacturacionElectronicaV2.DatosSegunTabla;
using Fe.FacturacionElectronicaV2.ExportacionV0.Equivalencias;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;

namespace Fe.FacturacionElectronicaV2
{
    public class FactoriaFE
    {
        public ComprobanteAsociado ObtenerComprobanteAsociado(int tipo, int punto, long numero, long cuit)
        {
            return new ComprobanteAsociado(tipo, punto, numero, cuit);
        }

        public FexItem ObtenerItemFEX()
        {
            return new FexItem();
        }

        public CAERespuestaFex ObtenerRespuestaFEX()
        {
            return new CAERespuestaFex();
        }

        public FexCabecera ObtenerCabeceraFEX()
        {
            return new FexCabecera();
        }

        public List<FexCabecera> ObtenerListaDeCabeceraFEX()
        {
            return new List<FexCabecera>();
        }

        public ConfiguracionWS ObtenerObjetoConfiguracion()
        {
            return new ConfiguracionWS();
        }

        public Observacion ObtenerObservacion()
        {
            return new Observacion();
        }

        public CAEDetalleRespuesta ObtenerDetalleRespuesta()
        {
            return new CAEDetalleRespuesta();
        }

        public ServidorAutenticacion ObtenerServidorAutenticacion(ConfiguracionWS config)
        {
            // llamado de Organic/Lince
            IFactoriaHerramientasWSAA factoriaHerramientasWSAA = new FactoriaHerramientasWSAA();
            return factoriaHerramientasWSAA.ObtenerServidorAutenticacion( config ); 
        }

        public TributoComprobante ObtenerTributoComprobante(int id, string descripcion, int baseImponible, int alicuota, int importe)
        {
            return new TributoComprobante((short)id, descripcion, (double)baseImponible, (double)alicuota, (double)importe);
        }

        public IVA ObtenerDetalleIva(int id, double baseImponible, double importe)
        {
            return new IVA(id, baseImponible, importe);
        }
        
        public FeCabecera ObtenerCabecera()
        {
            return new FeCabecera();
        }
        public FeDetalle ObtenerDetalle()
        {
            return new FeDetalle();
        }

        public Articulo ObtenerArticulo()
        {
            return new Articulo();
        }

        public FacturacionElectronica ObtenerFacturacionElectronica( TipoWebService tipoWebService )
        {
            return new FacturacionElectronica( tipoWebService );
        }

        public CAERespuestaFe ObtenerObjetoRespuesta()
        {
            return new CAERespuestaFe();
        }

        public EquivalenciasLince ObtenerEquivalenciasLince()
        {
            return new EquivalenciasLince();
        }

        public TributoComprobante ObtenerTributo()
        {
            return new TributoComprobante();
        }

        public List<Observacion> ObtenerListaObservacion()
        { 
            return new List<Observacion>();
        }

        public List<CAEDetalleRespuesta> ObtenerListaCAEDetalleRespuesta()
        {
            return new List<CAEDetalleRespuesta>();
        }

        public CAERespuestaFex ObtenerRespuestaVaciaExportacion()
        {
            Observacion obs = new Observacion();
            CAERespuestaFex retorno = new CAERespuestaFex();
            retorno.Observaciones = new List<Observacion>();
            retorno.Observaciones.Add(obs);

            return retorno;
        }

        public CAERespuestaFe ObtenerRespuestaVacia()
        {
            CAERespuestaFe retorno = new CAERespuestaFe();
            retorno.Detalle = new List<CAEDetalleRespuesta>();
            CAEDetalleRespuesta item = new CAEDetalleRespuesta();
            item.Observaciones = new List<Observacion>();
            Observacion obs = new Observacion();
            item.Observaciones.Add( obs );
            retorno.Detalle.Add( item );

            return retorno;
        }
    }
}
