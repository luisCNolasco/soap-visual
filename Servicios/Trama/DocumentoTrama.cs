using Entidades;
using Utilidades;

namespace Servicios.Trama
{
    public class DocumentoTrama
    {
        public string obtenerDocumentoTrama(Documento doc)
        {
            string trama = string.Empty;

            Convertidor convertidor = new Convertidor();

            //VER
            trama = "VER|Invoice|2.1|1.0 \n";

            //HD 
            trama = trama + "HD|" + doc.serieCorrelativo + "|" + convertidor.formatearFecha(doc.fechaEmision) + "|" + doc.tipoComprobante + "|" + doc.codigoMoneda + "|" + "" + "|";

            if (doc.tipoComprobante == "01" || doc.tipoComprobante == "03")
            {
                if (doc.formaPago == "Contado Externo")
                {
                    trama = trama + "" + "\n";
                }
                else
                {
                    trama = trama + convertidor.formatearFecha(doc.fechaVencimiento) + "\n";
                }
            }
            else if (doc.tipoComprobante == "07" || doc.tipoComprobante == "08")
            {
                trama = trama + "" + "\n";
            }

            //DRS  (07)

            if (doc.tipoComprobante == "07" || doc.tipoComprobante == "08")
            {
                //DRS
                trama = trama + "DRS|" + doc.docModificaNumero + "|" + doc.motivoCodigo + "\n";

                //DRS_DS
                trama = trama + "DRS_DS|" + doc.motivoSustento + "\n";

                //BRF_ID
                trama = trama + "BRF_ID|" + doc.docModificaNumeroNC + "|" + doc.docModificaTipo + "|" + convertidor.formatearFecha(doc.docModificaFechaEmision) + "\n";

            }

            //ASP
            trama = trama + "ASP|" + "20100202396" + "|" + "6" + "|" + "" + "|" + "AUTOMOTRIZ ANDINA S.A." + "\n";

            //ASP_PA
            trama = trama + "ASP_PA|" + "040101" + "|" + "AV. PARRA NRO. 122 URB. VALLECITO" + "|" + "" + "|" + "AREQUIPA" + "|" + "AREQUIPA" + "|" + "AREQUIPA" + "|" + "PE" + "\n";

            //ACP
            trama = trama + "ACP|" + doc.clienteNumeroDoc + "|" + doc.clienteTipoDoc + "|" + doc.clienteDenominacion + "\n";

            //ACP_PA
            trama = trama + "ACP_PA|" + " " + "|" + doc.clienteDireccionNombre + "|" + "" + "|" + doc.clienteDireccionProv + "|" + doc.clienteDireccionDepa + "|" + doc.clienteDireccionDist + "|" + doc.clienteDirecPaisCodi + "\n";

            //LMT
            trama = trama + "LMT|" + doc.totalValorVent + "|" + doc.totalValorPrec + "|" + "" + "|" + "" + "|" + doc.totalPagar + "\n";

            //AMT
            trama = trama + "AMT|" + "1001" + "|" + doc.conceptoMonto + "\n";

            // ********* AMT DETRACCION *********
            if (doc.montodeDetraccionSoles > 0)
            {
                trama = trama + "AMT|" + "2003" + "|" + doc.montodeDetraccionSoles + "|" + "" + "|" + "" + "|" + doc.detraccionProcentaje + "|" + "\n";
            }

            //APP
            trama = trama + "APP|" + "1000" + "|" + doc.leyendaDesc + "\n";

            // ********* APP DETRACCION *********
            if (doc.montodeDetraccionSoles > 0)
            {
                trama = trama + "APP|" + "3000" + "|" + doc.codigoBienOServicio + "\n";
                trama = trama + "APP|" + "3001" + "|" + doc.numeroCuenta + "\n";

                //MPS
                trama = trama + "MPS|" + doc.medioPago + "\n";

                //SNT
                trama = trama + "SNT|" + doc.sunaTipoOpera + "\n";
            }
            else
            {
                //SNT 
                trama = trama + "SNT|" + "0101" + "\n";
            }

            //EST
            trama = trama + "EST|" + "0000" + "\n";


            /*****+* Detalle Documento ******/


            for (int i = 0; i < doc.detalleDocumento.Count; i++)
            {

                //DLN_HD
                trama = trama + "DLN_HD|" + doc.detalleDocumento[i].lineaNumero + "|" + doc.detalleDocumento[i].lineaCantVen + "|" + doc.detalleDocumento[i].lineaUnidMed + "|" + doc.detalleDocumento[i].lineaImporte + "\n";

                //DLN_PI
                trama = trama + "DLN_PI|" + doc.detalleDocumento[i].lineaValorUni + "\n";

                //DLN_PR_ACP
                trama = trama + "DLN_PR_ACP|" + doc.detalleDocumento[i].lineaPrecUni + "|" + "01" + "\n";

                //DLN_AC
                trama = trama + "DLN_AC|" + "false" + "|" + doc.detalleDocumento[i].montoDescuento + "|" + doc.detalleDocumento[i].importeBruto + "|" + doc.detalleDocumento[i].porcentajeDescuento + "\n";

                //DLN_TT
                trama = trama + "DLN_TT|" + doc.detalleDocumento[i].lineaTribTotal + "\n";

                //DLN_TT_TS
                trama = trama + "DLN_TT_TS|" + doc.detalleDocumento[i].lineaTribImpor + "|" + doc.detalleDocumento[i].lineaTribTipoAfec + "|" + doc.detalleDocumento[i].lineaTribPorce + "\n";

                // DLN_IT
                trama = trama + "DLN_IT|" + doc.detalleDocumento[i].lineaItemCod + "\n";

                // DLN_IT_DS
                trama = trama + "DLN_IT_DS|" + doc.detalleDocumento[i].lineaItemDesc + "\n";


            }

            //TTT
            trama = trama + "TTT|" + doc.tributoTotal + "\n";

            //TTT_TS
            trama = trama + "TTT_TS|" + "" + "|" + doc.tributoImporte + "|";

            // TTT_TS +++ tributo_identificación : 1000 si es IGV, o 2000 si es ISC, o 9999 si es Otros
            if (doc.tributoIdentif == 1000)
            {
                trama = trama + "1000|IGV|VAT" + "\n";
            }
            else if (doc.tributoIdentif == 2000)
            {
                trama = trama + "2000|ISC|VAT" + "\n";
            }
            else if (doc.tributoIdentif == 9999)
            {
                trama = trama + "9999|Otros tributos|VAT" + "\n";
            }

            //FP
            if (doc.tipoComprobante == "01" || doc.tipoComprobante == "03")
            {
                trama = trama + "FP|";
                if (doc.indicador == "Contado Externo")
                {
                    trama = trama + "Contado" + "|";
                    trama = trama + "0.00" + "\n";
                }
                else
                {
                    trama = trama + "Credito" + "|";
                    trama = trama + doc.montoNetoPendiente + "\n";
                }
            }
            /*
            else if (doc.tipoComprobante == "07" || doc.tipoComprobante == "08")
            {
                trama = trama + "" + "|";
                trama = trama + "" + "\n";
            }*/


            //FED
            trama = trama + "FED|" + "CUSTOMEREMAIL" + "|" + "" + "\n";
            trama = trama + "FED|" + "DocPrcntIGV" + "|" + doc.docPrcntIGV + "\n";
            trama = trama + "FED|" + "SERIE_VIN" + "|" + doc.serieVin + "\n";
            trama = trama + "FED|" + "MODELO" + "|" + doc.modelo + "\n";
            trama = trama + "FED|" + "PLACA" + "|" + doc.placa + "\n";
            trama = trama + "FED|" + "MOTOR" + "|" + doc.motor + "\n";
            trama = trama + "FED|" + "NUMERO_CLIENTE" + "|" + doc.numeroCliente + "\n";
            trama = trama + "FED|" + "VENDEDOR" + "|" + doc.vendedor + "\n";
            trama = trama + "FED|" + "FECHA_ORDEN" + "|" + doc.fechaOrden + "\n";
            trama = trama + "FED|" + "CLASE_ORDEN" + "|" + doc.claseOrden + "\n";
            trama = trama + "FED|" + "NUMERO_ORDEN" + "|" + doc.numeroOrden + "\n";
            trama = trama + "FED|" + "FORMA_PAGO" + "|" + doc.formaPago + "\n";
            trama = trama + "FED|" + "KILOMETRAJE" + "|" + doc.kilometraje + "\n";
            trama = trama + "FED|" + "comentarios" + "|" + doc.comentarios + "\n";
            trama = trama + "FED|" + "DIRECCION_OFICINA" + "|" + doc.direccionOficina + "\n";

            return trama;
        }

    }
}