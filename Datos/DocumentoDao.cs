using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System;
using Entidades;
using System.Collections.Generic;

namespace Datos
{
    public class DocumentoDao
    {
        public List<Documento> buscarDocNoEnv()
        {
            List<Documento> lista = new List<Documento>();

            Documento doc = new Documento();

            SqlCommand cmd = null;

            string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionBDPrueba"].ToString();

            // Factura o Boleta 
            try
            {
                try
                {
                    using (SqlConnection sql = new SqlConnection(cadenaConexion))
                    {
                        using (cmd = new SqlCommand("obtenerDocNoEnv", sql))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            sql.Open();

                            SqlDataReader rd = cmd.ExecuteReader();

                            if (rd.HasRows)
                            {
                                while (rd.Read())
                                {
                                    //HD
                                    string serieCorrelativo = rd["serie_y_correlativo"].ToString();
                                    DateTime fechaEmision = Convert.ToDateTime(rd["fecha_de_emision"]);
                                    string tipoComprobante = rd["tipo_de_comprobante"].ToString();
                                    string codigoMoneda = rd["codigo_de_moneda"].ToString();
                                    DateTime fechaVencimiento = Convert.ToDateTime(rd["fecha_de_vencimiento"]); //

                                    //ACP
                                    string clienteNumeroDoc = rd["cliente_numero_documento"].ToString();
                                    int clienteTipoDoc = Convert.ToInt32(rd["cliente_tipo_documento"]);
                                    string clienteDenominacion = rd["cliente_denominacion"].ToString();

                                    //ACP_PA 
                                    string clienteDireccionNombre = rd["cliente_direccion_nombre"].ToString();
                                    string clienteDireccionProv = rd["cliente_direccion_provincia"].ToString();
                                    string clienteDireccionDepa = rd["cliente_direccion_departamento"].ToString();
                                    string clienteDireccionDist = rd["cliente_direccion_distrito"].ToString();
                                    string clienteDirecPaisCodi = rd["cliente_direccion_pais_codigo"].ToString();

                                    //LMT *
                                    double totalValorVent = Convert.ToDouble(rd["total_valorventa"]); //
                                    double totalValorPrec = Convert.ToDouble(rd["total_valorprecio"]); //
                                    double totalPagar = Convert.ToDouble(rd["total_a_pagar"]);

                                    //AMT
                                    double conceptoMonto = Convert.ToDouble(rd["concepto_monto"]);

                                    //****** AMT DETRACCION *****
                                    double detraccionProcentaje = Convert.ToDouble(rd["detraccion_porcentaje"]);
                                    double montodeDetraccionSoles = Convert.ToDouble(rd["monto_de_detraccion_soles"]);
                                    //***************

                                    //APP
                                    string leyendaDesc = rd["leyenda_descripcion"].ToString();

                                    //****** APP DETRACCION *****
                                    string codigoBienOServicio = rd["codigo_bien_o_servicio"].ToString();
                                    int numeroCuenta = Convert.ToInt32(rd["numero_cuenta"]);
                                    double tipoCambio = Convert.ToDouble(rd["tipo_cambio"]);
                                    double montoDetraccion = Convert.ToDouble(rd["monto_de_detraccion"]);

                                    //***** MPS DETRACCION ******
                                    string medioPago = rd["medio_pago"].ToString();

                                    //***** SNT DETRACION ********
                                    string sunaTipoOpera = rd["tipo_operacion_sunat"].ToString();

                                    //TTT
                                    double tributoTotal = Convert.ToDouble(rd["tributo_total"]);

                                    //TTT_TS
                                    double tributoImporte = Convert.ToDouble(rd["tributo_importe"]);
                                    int tributoIdentif = Convert.ToInt32(rd["tributo_identificación"]);

                                    //FP *
                                    string indicador = rd["indicador"].ToString(); //
                                    double montoNetoPendiente = Convert.ToDouble(rd["monto_neto_pendiente"]);

                                    //FED
                                    string docPrcntIGV = rd["DocPrcntIGV"].ToString();
                                    string serieVin = rd["SERIE_VIN"].ToString();
                                    string modelo = rd["MODELO"].ToString();
                                    string placa = rd["PLACA"].ToString();
                                    string motor = rd["MOTOR"].ToString();
                                    string numeroCliente = rd["NUMERO_CLIENTE"].ToString();
                                    string vendedor = rd["VENDEDOR"].ToString(); //
                                    string fechaOrden = rd["FECHA_ORDEN"].ToString();
                                    string claseOrden = rd["CLASE_ORDEN"].ToString();
                                    string numeroOrden = rd["NUMERO_ORDEN"].ToString();
                                    string formaPago = rd["FORMA_PAGO"].ToString();
                                    string kilometraje = rd["KILOMETRAJE"].ToString();
                                    string comentarios = rd["COMENTARIOS"].ToString().Replace("\r\n", "&#10;");
                                    string direccionOficina = rd["DIRECCION_OFICINA"].ToString().Replace("\r\n", "&#10;");

                                    Documento d = new Documento(serieCorrelativo, fechaEmision, tipoComprobante, codigoMoneda, fechaVencimiento, clienteNumeroDoc, clienteTipoDoc, clienteDenominacion, clienteDireccionNombre, clienteDireccionProv, clienteDireccionDepa, clienteDireccionDist, clienteDirecPaisCodi, totalValorVent, totalValorPrec, totalPagar, conceptoMonto, detraccionProcentaje, montodeDetraccionSoles, leyendaDesc, codigoBienOServicio, numeroCuenta, tipoCambio, montoDetraccion, medioPago, sunaTipoOpera, tributoTotal, tributoImporte, tributoIdentif, indicador, montoNetoPendiente, docPrcntIGV, serieVin, modelo, placa, motor, numeroCliente, vendedor, fechaOrden, claseOrden, numeroOrden, formaPago, kilometraje, comentarios, direccionOficina);

                                    lista.Add(d);

                                }
                            }
                            rd.Close();
                            sql.Close();
                        }
                    }

                    cmd = null;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                

                //Detalle Boleta o Factura
                if (lista.Count > 0)
                {
                    for (int i = 0; i < lista.Count; i++)
                    {

                        if (lista[i].tipoComprobante == "01" || lista[i].tipoComprobante == "03")
                        {
                            try
                            {
                                using (SqlConnection sql = new SqlConnection(cadenaConexion))
                                {
                                    using (cmd = new SqlCommand("obtenerDetDocNoEnv", sql))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.Add("@Codigo", SqlDbType.Char, 50).Value = lista[i].serieCorrelativo;

                                        //DataTable dt = new DataTable();
                                        //SqlDataAdapter da = new SqlDataAdapter(cmd);

                                        sql.Open();

                                        SqlDataReader rd = cmd.ExecuteReader();


                                        lista[i].detalleDocumento = new List<DocumentoDetalle>();

                                        if (rd.HasRows)
                                        {
                                            while (rd.Read())
                                            {

                                                lista[i].detalleDocumento.Add(new DocumentoDetalle
                                                {
                                                    //DLN_HD
                                                    lineaNumero = Convert.ToInt32(rd["linea_numero"]),
                                                    lineaCantVen = Convert.ToDouble(rd["linea_cantidad_vendida"]),
                                                    lineaUnidMed = rd["linea_item_unidad_medida"].ToString(),
                                                    lineaImporte = Convert.ToDouble(rd["linea_importe"]),

                                                    //DLN_PI
                                                    lineaValorUni = Convert.ToDouble(rd["linea_valor_unitario"]),

                                                    //DLN_PR_ACP *
                                                    lineaPrecUni = Convert.ToDouble(rd["linea_precio_unitario"]), //

                                                    //DLN_AC *
                                                    montoDescuento = Convert.ToDouble(rd["monto_descuento"]),//
                                                    importeBruto = Convert.ToDouble(rd["importe_bruto"]),//
                                                    porcentajeDescuento = Convert.ToDouble(rd["porcentaje_descuento"]),//

                                                    //DLN_TT
                                                    lineaTribTotal = Convert.ToDouble(rd["linea_tributo_total"]),

                                                    //DLN_TT_TS **
                                                    lineaTribImpor = Convert.ToDouble(rd["linea_tributo_importe"]),
                                                    lineaTribTipoAfec = Convert.ToInt32(rd["linea_tributo_tipo_afectacion"]),
                                                    lineaTribPorce = Convert.ToDouble((rd["linea_tributo_porcentaje"])), //

                                                    //DLN_IT
                                                    lineaItemCod = rd["linea_item_codigo"].ToString(),

                                                    //DELN_IT_DS
                                                    lineaItemDesc = rd["linea_item_descripcion"].ToString(),

                                                });


                                            }
                                        }

                                        rd.Close();
                                        sql.Close();

                                    }
                                }

                                cmd = null;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                        }

                    }
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            
            // Nota de Credito
            try
            {

                try
                {

                    using (SqlConnection sql = new SqlConnection(cadenaConexion))
                    {
                        using (cmd = new SqlCommand("obtenerDocNoEnvNotaCredito", sql))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;

                            sql.Open();

                            SqlDataReader rd = cmd.ExecuteReader();


                            if (rd.HasRows)
                            {
                                while (rd.Read())
                                {
                                    //HD
                                    string serieCorrelativo = rd["serie_y_correlativo"].ToString();
                                    DateTime fechaEmision = Convert.ToDateTime(rd["fecha_de_emision"]);
                                    string tipoComprobante = rd["tipo_de_comprobante"].ToString();
                                    string codigoMoneda = rd["codigo_de_moneda"].ToString();

                                    //DRS
                                    string docModificaNumero = rd["doc_que_modifica_numero"].ToString();
                                    string motivoCodigo = rd["motivo_codigo"].ToString();

                                    //DRS_DS
                                    string motivoSustento = rd["motivo_sustento"].ToString();

                                    //BRF_ID
                                    string docModificaNumeroNC = rd["doc_que_modifica_numeroNC"].ToString();
                                    string docModificaTipo = rd["doc_que_modifica_tipo"].ToString();
                                    DateTime docModificaFechaEmision = Convert.ToDateTime(rd["doc_que_modifica_fecha_emision"]);

                                    //ACP
                                    string clienteNumeroDoc = rd["cliente_numero_documento"].ToString();
                                    int clienteTipoDoc = Convert.ToInt32(rd["cliente_tipo_documento"]);
                                    string clienteDenominacion = rd["cliente_denominacion"].ToString();

                                    //ACP_PA 
                                    string clienteDireccionNombre = rd["cliente_direccion_nombre"].ToString();
                                    string clienteDireccionProv = rd["cliente_direccion_provincia"].ToString();
                                    string clienteDireccionDepa = rd["cliente_direccion_departamento"].ToString();
                                    string clienteDireccionDist = rd["cliente_direccion_distrito"].ToString();
                                    string clienteDirecPaisCodi = rd["cliente_direccion_pais_codigo"].ToString();

                                    //LMT *
                                    double totalValorVent = Convert.ToDouble(rd["total_valorventa"]); //
                                    double totalValorPrec = Convert.ToDouble(rd["total_valorprecio"]); //
                                    double totalPagar = Convert.ToDouble(rd["total_a_pagar"]);

                                    //AMT
                                    double conceptoMonto = Convert.ToDouble(rd["concepto_monto"]);

                                    //APP
                                    string leyendaDesc = rd["leyenda_descripcion"].ToString();

                                    //TTT
                                    double tributoTotal = Convert.ToDouble(rd["tributo_total"]);

                                    //TTT_TS
                                    double tributoImporte = Convert.ToDouble(rd["tributo_importe"]);
                                    int tributoIdentif = Convert.ToInt32(rd["tributo_identificación"]);

                                    //FED
                                    string docPrcntIGV = rd["DocPrcntIGV"].ToString();
                                    string serieVin = rd["SERIE_VIN"].ToString();
                                    string modelo = rd["MODELO"].ToString();
                                    string placa = rd["PLACA"].ToString();
                                    string motor = rd["MOTOR"].ToString();
                                    string numeroCliente = rd["NUMERO_CLIENTE"].ToString();
                                    string vendedor = rd["VENDEDOR"].ToString(); //
                                    string fechaOrden = rd["FECHA_ORDEN"].ToString();
                                    string claseOrden = rd["CLASE_ORDEN"].ToString();
                                    string numeroOrden = rd["NUMERO_ORDEN"].ToString();
                                    string formaPago = rd["FORMA_PAGO"].ToString();
                                    string kilometraje = rd["KILOMETRAJE"].ToString();
                                    string comentarios = rd["COMENTARIOS"].ToString();
                                    string direccionOficina = rd["DIRECCION_OFICINA"].ToString();

                                    Documento d = new Documento(serieCorrelativo, fechaEmision, tipoComprobante, codigoMoneda, docModificaNumero, motivoCodigo, motivoSustento,docModificaNumeroNC, docModificaTipo, docModificaFechaEmision, clienteNumeroDoc, clienteTipoDoc, clienteDenominacion, clienteDireccionNombre, clienteDireccionProv, clienteDireccionDepa, clienteDireccionDist, clienteDirecPaisCodi, totalValorVent, totalValorPrec, totalPagar, conceptoMonto, leyendaDesc, tributoTotal, tributoImporte, tributoIdentif, docPrcntIGV, serieVin, modelo, placa, motor, numeroCliente, vendedor, fechaOrden, claseOrden, numeroOrden, formaPago, kilometraje, comentarios, direccionOficina);

                                    lista.Add(d);

                                }
                            }

                            sql.Close();
                            rd.Close();

                        }

                    }

                    cmd = null;

                }
                catch (Exception ex)
                {
                    Console.WriteLine (ex.Message);
                }


                if (lista.Count > 0)
                {
                    for (int i = 0; i < lista.Count; i++)
                    {
                        if (lista[i].tipoComprobante == "07" || lista[i].tipoComprobante == "08")
                        {

                            try
                            {

                                using (SqlConnection sql = new SqlConnection(cadenaConexion))
                                {
                                    using (cmd = new SqlCommand("obtenerDetDocNoEnvNotaCredito", sql))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.Add("@Codigo", SqlDbType.Char, 50).Value = lista[i].serieCorrelativo;

                                        sql.Open();

                                        SqlDataReader rd = cmd.ExecuteReader();


                                        lista[i].detalleDocumento = new List<DocumentoDetalle>();

                                        if (rd.HasRows)
                                        {
                                            while (rd.Read())
                                            {

                                                lista[i].detalleDocumento.Add(new DocumentoDetalle
                                                {
                                                    //DLN_HD
                                                    lineaNumero = Convert.ToInt32(rd["linea_numero"]),
                                                    lineaCantVen = Convert.ToDouble(rd["linea_cantidad_vendida"]),
                                                    lineaUnidMed = rd["linea_item_unidad_medida"].ToString(),
                                                    lineaImporte = Convert.ToDouble(rd["linea_importe"]),

                                                    //DLN_PI
                                                    lineaValorUni = Convert.ToDouble(rd["linea_valor_unitario"]),

                                                    //DLN_PR_ACP *
                                                    lineaPrecUni = Convert.ToDouble(rd["linea_precio_unitario"]), //

                                                    //DLN_AC *
                                                    montoDescuento = Convert.ToDouble(rd["monto_descuento"]),//
                                                    importeBruto = Convert.ToDouble(rd["importe_bruto"]),//
                                                    porcentajeDescuento = Convert.ToDouble(rd["porcentaje_descuento"]),//

                                                    //DLN_TT
                                                    lineaTribTotal = Convert.ToDouble(rd["linea_tributo_total"]),

                                                    //DLN_TT_TS **
                                                    lineaTribImpor = Convert.ToDouble(rd["linea_tributo_importe"]),
                                                    lineaTribTipoAfec = Convert.ToInt32(rd["linea_tributo_tipo_afectacion"]),
                                                    lineaTribPorce = Convert.ToDouble((rd["linea_tributo_porcentaje"])), //

                                                    //DLN_IT
                                                    lineaItemCod = rd["linea_item_codigo"].ToString(),

                                                    //DELN_IT_DS
                                                    lineaItemDesc = rd["linea_item_descripcion"].ToString(),

                                                });


                                            }
                                        }

                                        rd.Close();
                                        sql.Close();
                                    }

                                }

                                cmd = null;

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);

                            }

                        }

                    }

                }   

            }
            catch (Exception ex)
            {
                Console.WriteLine (ex.Message);

            }

            return lista;
        }


        // Actualizar 

        // Actualizar Factura 
        public int actualizarDocEnviado(String cod, IntegradorRespuesta ir, String trama)
        {

            SqlCommand cmd = null;

            int numeroFilas = 0;

            string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionBDPrueba"].ToString();


            try
            {

                using (SqlConnection sql = new SqlConnection(cadenaConexion))
                {
                    using (cmd = new SqlCommand("actEstadoDocEnviado", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@documentID", SqlDbType.Char, 50).Value = cod;
                        cmd.Parameters.Add("@ose_estado", SqlDbType.Char, 2).Value = ir.Codigo;

                        if (ir.Codigo == "0")
                        {
                            cmd.Parameters.Add("@error_estado", SqlDbType.Char).Value = ir.oseDescripcion;
                        }
                        else
                        {
                            cmd.Parameters.Add("@error_estado", SqlDbType.Char).Value = ir.MensajeError;
                        }

                        cmd.Parameters.Add("@trama", SqlDbType.Char).Value = trama;


                        cmd.CommandType = CommandType.StoredProcedure;

                        sql.Open();

                        numeroFilas = cmd.ExecuteNonQuery();


                    }

                    sql.Close();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            return numeroFilas;

        }

        //Actualizar Nota Credito
        public int actualizarDocEnviadoNotaCredito(String cod, IntegradorRespuesta ir, String trama)
        {

            SqlCommand cmd = null;

            int numeroFilas = 0;

            string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionBDPrueba"].ToString();


            try
            {

                using (SqlConnection sql = new SqlConnection(cadenaConexion))
                {
                    using (cmd = new SqlCommand("actEstadoDocEnviadoNotaCredito", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@creditNoteID", SqlDbType.Char, 50).Value = cod;
                        cmd.Parameters.Add("@ose_estado", SqlDbType.Char, 2).Value = ir.Codigo;

                        if (ir.Codigo == "0")
                        {
                            cmd.Parameters.Add("@error_estado", SqlDbType.Char).Value = ir.oseDescripcion;
                        }
                        else
                        {
                            cmd.Parameters.Add("@error_estado", SqlDbType.Char).Value = ir.MensajeError;
                        }

                        cmd.Parameters.Add("@trama", SqlDbType.Char).Value = trama;


                        cmd.CommandType = CommandType.StoredProcedure;

                        sql.Open();

                        numeroFilas = cmd.ExecuteNonQuery();


                    }

                    sql.Close();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            return numeroFilas;

        }



    }
}
