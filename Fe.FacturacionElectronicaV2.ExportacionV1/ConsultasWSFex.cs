using System;
using System.Collections.Generic;
using ZooLogicSA.FacturacionElectronicaV2.Nacional;
using ZooLogicSA.FacturacionElectronicaV2.Nacional.Equivalencias;
using ZooLogicSA.FacturacionElectronicaV2.Nacional.LoginWSAA;
using ZooLogicSA.FacturacionElectronicaV2.Nacional.Logueos;
using ZooLogicSA.FacturacionElectronicaV2.Nacional.Wrappers;
using ZooLogicSA.FacturacionElectronicaV2.ExportacionV1.Equivalencias;
using ZooLogicSA.FacturacionElectronicaV2.ExportacionV0.Equivalencias;

namespace ZooLogicSA.FacturacionElectronicaV2.ExportacionV1
{
    public class ConsultasWSFex
    {
        private WSFEX wsfe;
        private WrapperAutorizacion wa;
        private ManagerErroresFe managerErrores;
        private LogueadorFe logueador;

        public ConsultasWSFex( WSFEX wsfe )
        {
            this.wsfe = wsfe;
            this.wa = new WrapperAutorizacion();
            this.logueador = new LogueadorFe();
            this.managerErrores = new ManagerErroresFe( this.logueador );
        }

        public ConsultasWSFex( WSFEX wsfe, LogueadorFe logueador )
        {
            this.wsfe = wsfe;
            this.wa = new WrapperAutorizacion();
            this.logueador = logueador;
            this.managerErrores = new ManagerErroresFe( this.logueador );
        }

        #region funciones de consulta para equivalencias
        public List<TipoMoneda> ObtenerTiposDeMoneda( Autorizacion aut )
        {
            ClsFEXAuthRequest feAutRequest = this.wa.ConvertirFex( aut );
            FEXResponse_Mon monedas = this.wsfe.FEXGetPARAM_MON( feAutRequest );
            this.managerErrores.CapturarError( monedas.FEXErr );
            List<TipoMoneda> tiposMoneda = new List<TipoMoneda>();
            TipoMoneda tipoMoneda;
            foreach ( ClsFEXResponse_Mon resultado in monedas.FEXResultGet )
            {
                tipoMoneda = new TipoMoneda();
                tipoMoneda.Id = resultado.Mon_Id;
                tipoMoneda.Descripcion = resultado.Mon_Ds;
                tiposMoneda.Add( tipoMoneda );
            }

            return tiposMoneda;
        }

        public List<Comprobante> ObtenerTiposDeComprobante( Autorizacion aut )
        {
            List<Comprobante> tiposComprobante = new List<Comprobante>();

            try
            {
                ClsFEXAuthRequest feAutRequest = this.wa.ConvertirFex(aut);
                FEXResponse_Tipo_Cbte comprobantes = this.wsfe.FEXGetPARAM_Tipo_Cbte(feAutRequest);
                this.managerErrores.CapturarError(comprobantes.FEXErr);
                Comprobante comprobante;

                foreach (ClsFEXResponse_Tipo_Cbte comp in comprobantes.FEXResultGet)
                {
                    comprobante = new Comprobante();
                    comprobante.Id = comp.Cbte_Id;
                    comprobante.Descripcion = comp.Cbte_Ds;
                    tiposComprobante.Add(comprobante);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tiposComprobante;
        }

        public List<TipoExportacion> ObtenerTiposDeExportacion( Autorizacion aut )
        {
            ClsFEXAuthRequest feAutRequest = this.wa.ConvertirFex( aut );
            
            FEXResponse_Tex conceptos = this.wsfe.FEXGetPARAM_Tipo_Expo( feAutRequest );
            this.managerErrores.CapturarError( conceptos.FEXErr );
            List<TipoExportacion> tiposExportacion = new List<TipoExportacion>();
            TipoExportacion exportacion;
            foreach ( ClsFEXResponse_Tex comp in conceptos.FEXResultGet )
            {
                exportacion = new TipoExportacion();
                exportacion.Id = comp.Tex_Id;
                exportacion.Descripcion = comp.Tex_Ds;
                tiposExportacion.Add( exportacion );
            }
            return tiposExportacion;
        }

        public List<UnidadDeMedida> ObtenerTiposDeUnidadDeMedida( Autorizacion aut )
        {
            ClsFEXAuthRequest feAutRequest = this.wa.ConvertirFex( aut );
            FEXResponse_Umed unidadesMedida = this.wsfe.FEXGetPARAM_UMed( feAutRequest );
            this.managerErrores.CapturarError( unidadesMedida.FEXErr );
            List<UnidadDeMedida> tiposunidadesMedida = new List<UnidadDeMedida>();
            UnidadDeMedida unidadMedida;
            foreach ( ClsFEXResponse_UMed umed in unidadesMedida.FEXResultGet )
            {
                if (umed != null)
                {
                    unidadMedida = new UnidadDeMedida();
                    unidadMedida.Id = umed.Umed_Id;
                    unidadMedida.Descripcion = umed.Umed_Ds;
                    tiposunidadesMedida.Add(unidadMedida);
                }
            }
            return tiposunidadesMedida;
        }

        public List<Idioma> ObtenerIdiomas( Autorizacion aut )
        {
            ClsFEXAuthRequest feAutRequest = this.wa.ConvertirFex( aut );
            FEXResponse_Idi respuesta = this.wsfe.FEXGetPARAM_Idiomas( feAutRequest );
            this.managerErrores.CapturarError( respuesta.FEXErr );
            List<Idioma> idiomas = new List<Idioma>();
            Idioma idioma;
            foreach ( ClsFEXResponse_Idi idi in respuesta.FEXResultGet )
            {
                idioma = new Idioma();
                idioma.Id = idi.Idi_Id;
                idioma.Descripcion = idi.Idi_Ds;
                idiomas.Add( idioma );
            }
            return idiomas;
        }

        public List<Pais> ObtenerCodigosPaises( Autorizacion aut )
        {
            ClsFEXAuthRequest feAutRequest = this.wa.ConvertirFex( aut );
            FEXResponse_DST_pais respuesta = this.wsfe.FEXGetPARAM_DST_pais( feAutRequest );
            this.managerErrores.CapturarError( respuesta.FEXErr );
            List<Pais> paises = new List<Pais>();
            Pais pais;
            foreach ( ClsFEXResponse_DST_pais respuPais in respuesta.FEXResultGet )
            {
                pais = new Pais();
                pais.Id = respuPais.DST_Codigo;
                pais.Descripcion = respuPais.DST_Ds;
                paises.Add( pais );
            }
            return paises;
        }

        public List<CuitPais> ObtenerCuitDePaises( Autorizacion aut )
        {
            ClsFEXAuthRequest feAutRequest = this.wa.ConvertirFex( aut );
            FEXResponse_DST_cuit respuesta = this.wsfe.FEXGetPARAM_DST_CUIT( feAutRequest );
            this.managerErrores.CapturarError( respuesta.FEXErr );
            List<CuitPais> cuitpaises = new List<CuitPais>();
            CuitPais cuit;
            foreach ( ClsFEXResponse_DST_cuit respuPais in respuesta.FEXResultGet )
            {
                cuit = new CuitPais();
                cuit.Id = respuPais.DST_CUIT;
                cuit.Descripcion = respuPais.DST_Ds;
                cuitpaises.Add( cuit );
            }
            return cuitpaises;
        }

        public double ConsultarCotizacion( Autorizacion aut, string idMoneda )
        {
            ClsFEXAuthRequest feAutRequest = this.wa.ConvertirFex( aut );
            FEXResponse_Ctz respuesta = this.wsfe.FEXGetPARAM_Ctz( feAutRequest, idMoneda );
            this.managerErrores.CapturarError( respuesta.FEXErr );

            return respuesta.FEXResultGet.Mon_ctz;
        }
        
        public List<Incoterms> ObtenerIncoterms( Autorizacion aut )
        {
            ClsFEXAuthRequest feAutRequest = this.wa.ConvertirFex( aut );
            FEXResponse_Inc respuesta = this.wsfe.FEXGetPARAM_Incoterms( feAutRequest );
            this.managerErrores.CapturarError( respuesta.FEXErr );
            List<Incoterms> valoresIncoterms = new List<Incoterms>();
            Incoterms incoterms;
            foreach ( ClsFEXResponse_Inc punto in respuesta.FEXResultGet )
            {
                incoterms = new Incoterms();
                incoterms.Id = punto.Inc_Id;
                incoterms.Descripcion = punto.Inc_Ds;
                valoresIncoterms.Add( incoterms );
            }
            return valoresIncoterms;
        }
        #endregion

        #region Otras Consultas
        public long UltimoComprobante( Autorizacion aut, int ptovta, int tipo )
        {
            ClsFEXAuthRequest feAutRequest = this.wa.ConvertirFex( aut );

            ClsFEX_LastCMP x = new ClsFEX_LastCMP();
            x.Token = feAutRequest.Token;
            x.Sign = feAutRequest.Sign;
            x.Cuit = feAutRequest.Cuit;
            x.Pto_venta = (short)ptovta;
            x.Tipo_cbte = (short)tipo;

            FEXResponseLast_CMP respuesta = this.wsfe.FEXGetLast_CMP( x );
            this.managerErrores.CapturarError( respuesta.FEXErr );

            return respuesta.FEXResult_LastCMP.Cbte_nro;
        }

        /// <summary>
        /// Recibe credenciales de autenticacion, codigo de despacho y pais de destino y verifica la existencia en la base aduanera
        /// </summary>
        /// <param name="aut">Autorizacion en el WSAA</param>
        /// <param name="permisoEmbarque">Codigo de permiso de embarque</param>
        /// <param name="paisDestino">Pais de destino de la mercaderia</param>
        /// <returns></returns>
        public bool PermisoEnAduana( Autorizacion aut, string permisoEmbarque, int paisDestino )
        {
            ClsFEXAuthRequest feAutRequest = this.wa.ConvertirFex( aut );

            FEXResponse_CheckPermiso respuesta = this.wsfe.FEXCheck_Permiso( feAutRequest, permisoEmbarque, paisDestino );
            this.managerErrores.CapturarError( respuesta.FEXErr );

            return respuesta.FEXResultGet.Status.Equals("OK");
        }

        /// <summary>
        /// Recuperador de los puntos de venta asignados a Facturacion Electronica de comprobantes de Exportacion
        /// </summary>
        /// <param name="aut">Autorizacion WSAA</param>
        /// <returns>Listado de puntos de venta registrados para la operacion de comprobantes electronicos para exportacion via web services</returns>
        public List<PuntoDeVenta> ObtenerPuntosDeVenta( Autorizacion aut )
        {
            ClsFEXAuthRequest feAutRequest = this.wa.ConvertirFex( aut );
            FEXResponse_PtoVenta respuesta = this.wsfe.FEXGetPARAM_PtoVenta( feAutRequest );
            this.managerErrores.CapturarError( respuesta.FEXErr );
            List<PuntoDeVenta> puntosDeVenta = new List<PuntoDeVenta>();
            PuntoDeVenta puntoDeVenta;
            foreach ( ClsFEXResponse_PtoVenta punto in respuesta.FEXResultGet )
            {
                puntoDeVenta = new PuntoDeVenta();
                puntoDeVenta.Numero = punto.Pve_Nro;
                puntoDeVenta.Bloqueado = punto.Pve_Bloqueado;
                puntosDeVenta.Add( puntoDeVenta );
            }
            return puntosDeVenta;
        }

        /// <summary>
        /// Obtiene informacion de respuesta de autorizacion para un comprobante
        /// </summary>
        /// <param name="aut">Autorizacion</param>
        /// <param name="tipoComprobante">Tipo de Comprobante</param>
        /// <param name="nroComprobante">Nro de Comprobante</param>
        /// <param name="ptoVta">Punto de Venta</param>
        /// <returns>Respuesta simil a la de solicitud de CAE</returns>
        public ClsFEXGetCMPR DatosDeComprobante( Autorizacion aut, int tipoComprobante, long nroComprobante, int ptoVta )
        {
            ClsFEXAuthRequest feAutRequest = this.wa.ConvertirFex( aut );
            ClsFEXGetCMP solicitud = new ClsFEXGetCMP();
            solicitud.Cbte_nro = nroComprobante;
            solicitud.Tipo_cbte = (short)tipoComprobante;
            solicitud.Punto_vta = (short)ptoVta;
            FEXGetCMPResponse  respuesta = this.wsfe.FEXGetCMP( feAutRequest, solicitud );
            this.managerErrores.CapturarError( respuesta.FEXErr );

            return respuesta.FEXResultGet;
        }

        public long UltimoId( Autorizacion aut )
        {
            ClsFEXAuthRequest feAutRequest = this.wa.ConvertirFex( aut );

            FEXResponse_LastID respuesta = this.wsfe.FEXGetLast_ID( feAutRequest );
            this.managerErrores.CapturarError( respuesta.FEXErr );

            return respuesta.FEXResultGet.Id;
        }

        #endregion

    }
}