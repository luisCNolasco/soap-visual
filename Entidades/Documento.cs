using System;
using System.Collections.Generic;

namespace Entidades
{
    public class Documento
    {
        //Ver
        //public string tipoUbl { get; set; }
        //public string versionUbl { get; set; }
        //public string versionCusto { get; set; }

        //HD
        public string serieCorrelativo { get; set; }
        public DateTime fechaEmision { get; set; }
        public string tipoComprobante { get; set; }
        public string codigoMoneda { get; set; }
        //public DateTime horaEmision { get; set; } // opcional
        public DateTime fechaVencimiento { get; set; } // opcional

        // ******** NOTA DE CREDITO ********
        //DRS
        public string docModificaNumero { get; set; }
        public string motivoCodigo { get; set; }

        //DRS_DS
        public string motivoSustento { get; set; } 
        
        //BRF_ID
        public string docModificaNumeroNC { get; set; }
        public string docModificaTipo { get; set; }
        public DateTime docModificaFechaEmision { get; set; }

        // *******************

        /*
        //DDR 
        public int guiaRemisionNumero { get; set; }
        public int guiaRemisionTipoDoc { get; set; }

        //ASP
        public int emisorNumeroDocumento { get; set; }
        public int emisorTipoDocumento { get; set; }
        public string emisorNombreComercial { get; set; } //Opcional
        public string emisorDenominacion { get; set; }

        //ASP_PA
        public int emisorDireccionUbigeo { get; set; }
        public string emisorDireccionNombre { get; set; }
        public string emisorDireccionZona { get; set; } //Opcional
        public string emisorDireccionProv { get; set; }
        public string emisorDirecciondepa { get; set; }
        public string emisorDireccionDist { get; set; }
        public string emisorDireccionPais { get; set; }
        */

        //ACP
        public string clienteNumeroDoc { get; set; }
        public int clienteTipoDoc { get; set; }
        public string clienteDenominacion { get; set; }

        //ACP_PA
        public string clienteDireccionNombre { get; set; }
        //public string clienteDireccionZona { get; set; } //Opcional
        public string clienteDireccionProv { get; set; }
        public string clienteDireccionDepa { get; set; }
        public string clienteDireccionDist { get; set; }
        public string clienteDirecPaisCodi { get; set; }

        //LMT 
        public double totalValorVent { get; set; } 
        public double totalValorPrec { get; set; } 
        //public double totalDescuento { get; set; } //Opcional
        //public double totalCargo { get; set; } // Opcional
        public double totalPagar { get; set; }
        //public double totalAnticipo { get; set; }


        //AMT 
        //public int conceptoCodigo { get; set; }
        public double conceptoMonto { get; set; }

        //****** AMT DETRACCION *****
        public double detraccionProcentaje { get; set; }
        public double montodeDetraccionSoles { get; set; }
        // *****************

        //APP 
        //public int leyendaCodigo { get; set; }
        public string leyendaDesc { get; set; }

        //****** APP DETRACCION *****
        public string codigoBienOServicio { get; set; }
        public int numeroCuenta { get; set; }
        public double tipoCambio { get; set; }
        public double montoDetraccion { get; set; }

        //****** MPS DETRACCION *****
        public string medioPago { get; set; }


        //****** SNT DETRACCION *****
        //SNT
        public string sunaTipoOpera { get; set; }

        //EST
        //public int estableciCod { get; set; }

        //Detalle Documento
        public List<DocumentoDetalle> detalleDocumento;

        //TTT
        public double tributoTotal { get; set; }

        //TTT_TS 
        public double tributoImporte { get; set; }
        public int tributoIdentif { get; set; }

        // FP 
        public string indicador { get; set; } 
        public double montoNetoPendiente { get; set; } 

        //FED
        public string docPrcntIGV { get; set; } 
        public string serieVin { get; set; }
        public string modelo { get; set; }
        public string placa { get; set; }
        public string motor { get; set; }
        public string numeroCliente { get; set; }
        public string vendedor { get; set; }
        public string fechaOrden { get; set; }
        public string claseOrden { get; set; }
        public string numeroOrden { get; set; }
        public string formaPago { get; set; }
        public string kilometraje { get; set; }
        public string comentarios { get; set; } 
        public string direccionOficina { get; set; }


        // Contructores 

        public Documento()
        {

        }

        //Para Enviar Documentos (FACTURA)

        public Documento(string serieCorrelativo, DateTime fechaEmision, string tipoComprobante, string codigoMoneda, DateTime fechaVencimiento, string clienteNumeroDoc, int clienteTipoDoc, string clienteDenominacion, string clienteDireccionNombre, string clienteDireccionProv, string clienteDireccionDepa, string clienteDireccionDist, string clienteDirecPaisCodi, double totalValorVent, double totalValorPrec, double totalPagar, double conceptoMonto, double detraccionProcentaje, double montodeDetraccionSoles, string leyendaDesc, string codigoBienOServicio, int numeroCuenta, double tipoCambio, double montoDetraccion, string medioPago, string sunaTipoOpera, double tributoTotal, double tributoImporte, int tributoIdentif, string indicador, double montoNetoPendiente, string docPrcntIGV, string serieVin, string modelo, string placa, string motor, string numeroCliente, string vendedor, string fechaOrden, string claseOrden, string numeroOrden, string formaPago, string kilometraje, string comentarios, string direccionOficina)
        {
            this.serieCorrelativo = serieCorrelativo;
            this.fechaEmision = fechaEmision;
            this.tipoComprobante = tipoComprobante;
            this.codigoMoneda = codigoMoneda;
            this.fechaVencimiento = fechaVencimiento;
            this.clienteNumeroDoc = clienteNumeroDoc;
            this.clienteTipoDoc = clienteTipoDoc;
            this.clienteDenominacion = clienteDenominacion;
            this.clienteDireccionNombre = clienteDireccionNombre;
            this.clienteDireccionProv = clienteDireccionProv;
            this.clienteDireccionDepa = clienteDireccionDepa;
            this.clienteDireccionDist = clienteDireccionDist;
            this.clienteDirecPaisCodi = clienteDirecPaisCodi;
            this.totalValorVent = totalValorVent;
            this.totalValorPrec = totalValorPrec;
            this.totalPagar = totalPagar;
            this.conceptoMonto = conceptoMonto;
            this.detraccionProcentaje = detraccionProcentaje;
            this.montodeDetraccionSoles = montodeDetraccionSoles;
            this.leyendaDesc = leyendaDesc;
            this.codigoBienOServicio = codigoBienOServicio;
            this.numeroCuenta = numeroCuenta;
            this.tipoCambio = tipoCambio;
            this.montoDetraccion = montoDetraccion;
            this.medioPago = medioPago;
            this.sunaTipoOpera = sunaTipoOpera;
            this.tributoTotal = tributoTotal;
            this.tributoImporte = tributoImporte;
            this.tributoIdentif = tributoIdentif;
            this.indicador = indicador;
            this.montoNetoPendiente = montoNetoPendiente;
            this.docPrcntIGV = docPrcntIGV;
            this.serieVin = serieVin;
            this.modelo = modelo;
            this.placa = placa;
            this.motor = motor;
            this.numeroCliente = numeroCliente;
            this.vendedor = vendedor;
            this.fechaOrden = fechaOrden;
            this.claseOrden = claseOrden;
            this.numeroOrden = numeroOrden;
            this.formaPago = formaPago;
            this.kilometraje = kilometraje;
            this.comentarios = comentarios;
            this.direccionOficina = direccionOficina;
        }



        //Para Enviar Documentos (NOTA DE CREDITO)

        public Documento(string serieCorrelativo, DateTime fechaEmision, string tipoComprobante, string codigoMoneda, string docModificaNumero, string motivoCodigo, string motivoSustento, string docModificaNumeroNC, string docModificaTipo, DateTime docModificaFechaEmision, string clienteNumeroDoc, int clienteTipoDoc, string clienteDenominacion, string clienteDireccionNombre, string clienteDireccionProv, string clienteDireccionDepa, string clienteDireccionDist, string clienteDirecPaisCodi, double totalValorVent, double totalValorPrec, double totalPagar, double conceptoMonto, string leyendaDesc, double tributoTotal, double tributoImporte, int tributoIdentif, string docPrcntIGV, string serieVin, string modelo, string placa, string motor, string numeroCliente, string vendedor, string fechaOrden, string claseOrden, string numeroOrden, string formaPago, string kilometraje, string comentarios, string direccionOficina)
        {
            this.serieCorrelativo = serieCorrelativo;
            this.fechaEmision = fechaEmision;
            this.tipoComprobante = tipoComprobante;
            this.codigoMoneda = codigoMoneda;
            this.docModificaNumero = docModificaNumero;
            this.motivoCodigo = motivoCodigo;
            this.motivoSustento = motivoSustento;
            this.docModificaNumeroNC = docModificaNumeroNC;
            this.docModificaTipo = docModificaTipo;
            this.docModificaFechaEmision = docModificaFechaEmision;
            this.clienteNumeroDoc = clienteNumeroDoc;
            this.clienteTipoDoc = clienteTipoDoc;
            this.clienteDenominacion = clienteDenominacion;
            this.clienteDireccionNombre = clienteDireccionNombre;
            this.clienteDireccionProv = clienteDireccionProv;
            this.clienteDireccionDepa = clienteDireccionDepa;
            this.clienteDireccionDist = clienteDireccionDist;
            this.clienteDirecPaisCodi = clienteDirecPaisCodi;
            this.totalValorVent = totalValorVent;
            this.totalValorPrec = totalValorPrec;
            this.totalPagar = totalPagar;
            this.conceptoMonto = conceptoMonto;
            this.leyendaDesc = leyendaDesc;
            this.tributoTotal = tributoTotal;
            this.tributoImporte = tributoImporte;
            this.tributoIdentif = tributoIdentif;
            this.docPrcntIGV = docPrcntIGV;
            this.serieVin = serieVin;
            this.modelo = modelo;
            this.placa = placa;
            this.motor = motor;
            this.numeroCliente = numeroCliente;
            this.vendedor = vendedor;
            this.fechaOrden = fechaOrden;
            this.claseOrden = claseOrden;
            this.numeroOrden = numeroOrden;
            this.formaPago = formaPago;
            this.kilometraje = kilometraje;
            this.comentarios = comentarios;
            this.direccionOficina = direccionOficina;
        }

        
    }




    


}

