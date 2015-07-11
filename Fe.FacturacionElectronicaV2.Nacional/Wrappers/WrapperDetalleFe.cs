using System;
using Fe.FacturacionElectronicaV2.Core;
using Fe.FacturacionElectronicaV2.Core.Equivalencias;
using Fe.FacturacionElectronicaV2.Wrappers;
using Fe.FacturacionElectronicaV2.Nacional.Equivalencias;
using Fe.FacturacionElectronicaV2.Nacional.WebServices;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace Fe.FacturacionElectronicaV2.Nacional.Wrappers
{
    public class WrapperDetalleFe
    {
        public FECAEDetRequest Convertir( FeDetalle detalle )
        {
            return this.Convertir( detalle, true );
        }

        public FECAEDetRequest Convertir( FeDetalle detalle, bool arreglar )
        {
            FECAEDetRequest feCaeDet = new FECAEDetRequest();
            feCaeDet.Concepto = (int)detalle.Concepto;
            feCaeDet.DocTipo = (int)detalle.DocumentoTipo;
            feCaeDet.DocNro = detalle.DocumentoNumero;
            feCaeDet.CbteDesde = detalle.ComprobanteDesde;
            feCaeDet.CbteHasta = detalle.ComprobanteDesde;
            feCaeDet.CbteFch = detalle.ComprobanteFecha;

            feCaeDet.ImpTotal = Redondeo.Aplicar(detalle.ImporteTotal);
            feCaeDet.ImpTotConc = Redondeo.Aplicar(detalle.ImporteNetoNoGravado);
            feCaeDet.ImpNeto = Redondeo.Aplicar(detalle.ImporteNeto);
            feCaeDet.ImpOpEx = Redondeo.Aplicar(detalle.ImporteExento);
            feCaeDet.ImpTrib = Redondeo.Aplicar(detalle.ImporteTributos);
            
            feCaeDet.FchServDesde = detalle.FechaServicioDesde;
            feCaeDet.FchServHasta = detalle.FechaServicioHasta;
            feCaeDet.FchVtoPago = detalle.FechaVencimientoDePago;
            feCaeDet.MonId = detalle.MonedaId;
            feCaeDet.MonCotiz = detalle.MonedaCotizacion;

            this.ConvertirComprobantesAsociados( detalle, feCaeDet );
            this.ConvertirTributos( detalle, feCaeDet );
            this.ConvertirIVA( detalle, feCaeDet );

            feCaeDet.ImpIVA = this.CalcularIva( feCaeDet );
            feCaeDet.ImpTrib = this.CalcularTributos( feCaeDet );

            this.ConvertirDatosAdicionales( detalle, feCaeDet );

            if ( arreglar )
            {
                if ( !this.ValidarSumaImporteTotal( feCaeDet ) )
                {
                    this.TratarDeArreglarRedondeando( feCaeDet, detalle );
                }

                if ( !this.ValidarSumaImporteTotal( feCaeDet ) )
                {
                    this.TratarDeArreglarCocinando( feCaeDet, detalle );
                }

                if ( !this.ValidarSumaImporteTotal( feCaeDet ) )
                {
                    // fallo todo, dejo como estaba
                    feCaeDet = this.Convertir( detalle, false );
                }
            }
            return feCaeDet;
        }

        /// <summary>
        /// Cocina para que cierren los valores
        /// </summary>
        /// <param name="feCaeDet"></param>
        /// <param name="detalle"></param>
        private void TratarDeArreglarCocinando( FECAEDetRequest requestAfip, FeDetalle detalle )
        {
            Decimal diferencia = this.ObtenerDiferenciaTotales( requestAfip );

            // Tolerancia de 2 ctvos
            if ( Math.Abs( diferencia ) <= 0.02M )
            {
                // Suma De Totales Es Menor al total
                if ( diferencia > 0 )
                {
                    if ( requestAfip.ImpIVA > 0 )
                    {
                        // La ligó el IVA
                        requestAfip.ImpIVA = Redondeo.SumarDoubles( new double[]{ requestAfip.ImpIVA , (double)diferencia } );
                        requestAfip.Iva[0].Importe = Redondeo.SumarDoubles( new double[] { requestAfip.Iva[0].Importe, (double)diferencia } );
                    }
                    else if ( requestAfip.ImpTrib > 0 )
                    {
                        // sin IVA, le pego a los impuestos...
                        requestAfip.ImpTrib = Redondeo.SumarDoubles( new double[] { requestAfip.ImpTrib, (double)diferencia } );
                        requestAfip.Tributos[0].Importe = Redondeo.SumarDoubles( new double[] { requestAfip.Tributos[0].Importe, (double)diferencia } );
                    }
                    else if ( requestAfip.ImpOpEx > 0 )
                    {
                        // nada, al exento
                        requestAfip.ImpOpEx = Redondeo.SumarDoubles( new double[] { requestAfip.ImpOpEx, (double)diferencia } );
                    }
                    else if ( requestAfip.ImpTotConc > 0 )
                    {
                        // nada, al Neto NG
                        requestAfip.ImpTotConc = Redondeo.SumarDoubles( new double[] { requestAfip.ImpTotConc, (double)diferencia } );
                    }
                }
                
                // Suma De Totales Es Mayor al total
                if ( diferencia < 0 )
                {
                    if ( requestAfip.ImpOpEx > 0 )
                    {
                        // Tiene exento, lo pongo ahi
                        requestAfip.ImpOpEx = Redondeo.SumarDoubles( new double[] { requestAfip.ImpOpEx, (double)diferencia } );
                    }
                    else if ( requestAfip.ImpTotConc > 0 )
                    {
                        // Tiene Neto NG, lo pongo ahi
                        requestAfip.ImpTotConc = Redondeo.SumarDoubles( new double[] { requestAfip.ImpTotConc, (double)diferencia } );
                    }
                    else if ( requestAfip.ImpNeto > 0 )
                    {
                        // Importe Neto
                        requestAfip.ImpNeto = Redondeo.SumarDoubles( new double[] { requestAfip.ImpNeto, (double)diferencia } );
                        requestAfip.Iva[0].BaseImp = Redondeo.SumarDoubles( new double[] { requestAfip.Iva[0].BaseImp, (double)diferencia } );
                    }
                }
            }
        }

        /// <summary>
        /// Pone los valores redondeados
        /// </summary>
        /// <param name="feCaeDet">detalle del request a la ADIP</param>
        /// <param name="detalle">detalle wrappeado, con los valores de Organic/Lince</param>
        private void TratarDeArreglarRedondeando( FECAEDetRequest feCaeDet, FeDetalle detalle )
        {
            Decimal ivaOriginal = (Decimal)feCaeDet.ImpIVA;
            Decimal tributosOriginal = (Decimal)feCaeDet.ImpTrib;

            // Total no se toca
            // feCaeDet.ImpTotal = Redondeo.Aplicar( detalle.ImporteTotal );
            
            feCaeDet.ImpTotConc = Redondeo.Redondear( detalle.ImporteNetoNoGravado );
            feCaeDet.ImpNeto = Redondeo.Redondear( detalle.ImporteNeto );
            feCaeDet.ImpOpEx = Redondeo.Redondear( detalle.ImporteExento );
            feCaeDet.ImpTrib = Redondeo.Redondear( detalle.ImporteTributos );
            feCaeDet.ImpIVA = Redondeo.Redondear( detalle.ImporteIVA );

            #region Arreglo IVA. Si cambia, la suma de la coleccion debe dar lo mismo
                if ( (Decimal)feCaeDet.ImpIVA != ivaOriginal )
            {
                // le pego a algun IVA para que no falle
                // validacion 10023  La suma de los campos <importe> en  <IVA>   debe  ser igual al valor ingresado en  ImpIVA. 
                Decimal diferencia = (Decimal)feCaeDet.ImpIVA - ivaOriginal;

                try
                {
                    feCaeDet.Iva[0].Importe = Redondeo.SumarDoubles( new double[] { feCaeDet.Iva[0].Importe, (double)diferencia } );
                }
                finally
                {
                }
            } 
            #endregion

            #region Arreglo Tributos. Si cambia, la suma de la coleccion debe dar lo mismo
            if ( (Decimal)feCaeDet.ImpTrib != tributosOriginal )
            {
                // le pego a alguno para que no falle
                // validacion 10029  La suma de los importes en  <Tributo>   debe  ser igual al valor ingresado  en  ImpTrib
                Decimal diferencia = (Decimal)feCaeDet.ImpTrib - tributosOriginal;

                try
                {
                    feCaeDet.Tributos[0].Importe = Redondeo.SumarDoubles( new double[] { feCaeDet.Tributos[0].Importe, (double)diferencia } );
                }
                finally
                {
                }
            }
            #endregion
        }

        /// <summary>
        /// Validacion para anticipar error 10048 - El campo "Importe Total" (ImpTotal), debe ser igual  a la  suma de ImpTotConc + ImpNeto + ImpOpEx + ImpTrib + ImpIva 
        /// </summary>
        /// <param name="detalle">detalle del request a la ADIP</param>
        private bool ValidarSumaImporteTotal( FECAEDetRequest detalle )
        {
            return this.ObtenerDiferenciaTotales( detalle ) == 0;
        }

        private Decimal ObtenerDiferenciaTotales( FECAEDetRequest requestAfip )
        {
            double sumaTotales = Redondeo.SumarDoubles( new double[] { requestAfip.ImpTotConc, requestAfip.ImpNeto, requestAfip.ImpOpEx, requestAfip.ImpTrib, requestAfip.ImpIVA } );

            Decimal diferencia = (Decimal)requestAfip.ImpTotal - (Decimal)sumaTotales;

            return diferencia;
        }

        private double CalcularTributos( FECAEDetRequest feCaeDet )
        {
            double totalesTributos = 0;
            if ( feCaeDet.Tributos != null )
            {
                totalesTributos = Redondeo.SumarDoubles( (from x in feCaeDet.Tributos select x.Importe).ToArray() );
            }
            return totalesTributos;
        }

        private double CalcularIva( FECAEDetRequest feCaeDet )
        {
            double totalesIva = 0;
            
            if ( feCaeDet.Iva != null )
            {
                totalesIva = Redondeo.SumarDoubles( (from x in feCaeDet.Iva select x.Importe).ToArray() );
            }
            return totalesIva;
        }

        private void ConvertirComprobantesAsociados( FeDetalle detalle, FECAEDetRequest feCaeDet )
        {
            if ( detalle.ComprobantesAsociados.Count > 0 )
            {
                int i = 0;
                feCaeDet.CbtesAsoc = new CbteAsoc[detalle.ComprobantesAsociados.Count];
                WrapperComprobanteAsociadoFe wcaf = new WrapperComprobanteAsociadoFe();
                foreach ( ComprobanteAsociado comprobante in detalle.ComprobantesAsociados )
                {
                    feCaeDet.CbtesAsoc[i] = wcaf.Convertir( comprobante );
                    i++;
                }
            }
        }

        private void ConvertirTributos( FeDetalle detalle, FECAEDetRequest feCaeDet )
        {
            if ( detalle.Tributos.Count > 0 )
            {
                int i = 0;
                feCaeDet.Tributos = new Tributo[detalle.Tributos.Count];
                WrapperTributoFe wtc = new WrapperTributoFe();
                foreach ( TributoComprobante item in detalle.Tributos )
                {
                    feCaeDet.Tributos[i] = wtc.Convertir( item );
                    i++;
                }
            }
        }

        private void ConvertirIVA( FeDetalle detalle, FECAEDetRequest feCaeDet )
        {
            if ( detalle.Iva.Count > 0 )
            {
                int i = 0;
                feCaeDet.Iva = new AlicIva[detalle.Iva.Count];
                WrapperIvaFe wic = new WrapperIvaFe();
                foreach ( IVA iva in detalle.Iva )
                {
                    feCaeDet.Iva[i] = wic.Convertir( iva );
                    i++;
                }
            }
        }

        private void ConvertirDatosAdicionales(FeDetalle detalle, FECAEDetRequest feCaeDet)
        {
            if ( !String.IsNullOrEmpty( detalle.DA_NumeroDocumento ) && detalle.DA_NumeroDocumento != "0" )
            {
                feCaeDet.Opcionales = new Opcional[4];
            
                feCaeDet.Opcionales[0] = new Opcional();
                feCaeDet.Opcionales[0].Id = "7";
                feCaeDet.Opcionales[0].Valor = detalle.DA_CondicionTitular.ToString().PadLeft( 2, '0' );

                feCaeDet.Opcionales[1] = new Opcional();
                feCaeDet.Opcionales[1].Id = "61";
                feCaeDet.Opcionales[1].Valor = detalle.DA_TipoDocumento.ToString().PadLeft(2, '0');

                feCaeDet.Opcionales[2] = new Opcional();
                feCaeDet.Opcionales[2].Id = "62";
                feCaeDet.Opcionales[2].Valor = detalle.DA_NumeroDocumento.ToString();

                feCaeDet.Opcionales[3] = new Opcional();
                feCaeDet.Opcionales[3].Id = "5";
                feCaeDet.Opcionales[3].Valor = detalle.DA_Motivo.ToString().PadLeft(2, '0');
             }
        }
    }
}
