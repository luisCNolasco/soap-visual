using Entidades;
using Servicios;
using System;
using System.Net;
using System.Xml;
using System.Net.Http;
using System.IO;
using System.Configuration;

namespace Negocio
{
    public class SoapConsumir : ISoapServicio
    {

        private string usuario =    ConfigurationManager.AppSettings["Usuario"];
        private string contrasena = ConfigurationManager.AppSettings["Contrasena"];
        private string ruc =        ConfigurationManager.AppSettings["RUC"];


        #region PropiedadesEnvio
        //Definir Propiedades de Envio
        private static HttpWebRequest getWebRequest()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://52.26.185.136:8090/ws/ws/billService");
            webRequest.ContentType = "application/soap+xml;charset=UTF-8";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";

            return webRequest;
        }
        #endregion

        #region TipoServicios
        // Tipos de servicios del servicio SOAP 
        public enum typeMethod : int
        {
            sendDoc = 0,
            getInfo,
            sendVoidedDoc,
            sendDespatchAdvice,
            sendCom = 4
        }
        #endregion

        #region FormatosCabeceraTrama
        // Formato para enviar la trama "Enviar Documento"
        private String getBodySendDoc(String doc)
        {
            String s = String.Empty;
            s += $"    <ws:sendDoc>";
            s += "       <companyIdentification>" + ruc + "</companyIdentification>";
            s += "       <username>" + usuario + "</username>";
            s += "       <password>" + contrasena + "</password>";
            s += "       <content><![CDATA[" + doc + "]]></content>";
            s += "       <returnValues>" + "5" + "</returnValues>";
            s += $"    </ws:sendDoc>";

            return s;
        }

        // Formato para enviar la trama "Consultar Documento"
        private String getBodyGetInfo(String tipoDocSunat, String folio)
        {
            String s = String.Empty;

            s += $"    <ws:getInfo>";
            s += "       <companyIdentification>" + ruc + "</companyIdentification>";
            s += "       <username>" + usuario + "</username>";
            s += "       <password>"+ contrasena + "</password>";
            s += "       <documentType>"+ tipoDocSunat + "</documentType>";
            s += "       <identifier>"+ folio + "</identifier>";
            s += "       <returnValues>" + "5" + "</returnValues>";
            s += $"    </ws:getInfo>";

            return s;
        }

        // Formato para enviar la trama "Eliminar Documento"
        private String getBodyVoidedDoc(String doc)
        {
            String s = String.Empty;

            s += $"    <ws:sendVoidedDocuments>";
            s += "       <companyIdentification>" + ruc + "</companyIdentification>";
            s += "       <username>" + usuario + "</username>";
            s += "       <password>" + contrasena + "</password>";
            s += "       <content><![CDATA[" + doc + "]]></content>";
            s += "       <returnValues>" + "5" + "</returnValues>";
            s += $"    </ws:sendVoidedDocuments>";


            return s;
        }

        // Formato para enviar la trama "Generar Guias de Forma Online"
        private String getBodySendDespatchAdvice(String doc)
        {
            String s = String.Empty;

            s += $"    <ws:sendDespatchAdvice>";
            s += "       <companyIdentification>" + ruc + "</companyIdentification>";
            s += "       <username>" + usuario + "</username>";
            s += "       <password>" + contrasena + "</password>";
            s += "       <content><![CDATA[" + doc + "]]></content>";
            s += "       <returnValues>" + "5" + "</returnValues>";
            s += $"    </ws:sendDespatchAdvice>";

            return s;
        }

        private String getBodySendCom(String doc)
        {
            String s = String.Empty;

            s += $"    <ws:sendCom>";
            s += "       <companyIdentification>" + ruc + "</companyIdentification>";
            s += "       <username>" + usuario + "</username>";
            s += "       <password>" + contrasena + "</password>";
            s += "       <content><![CDATA[" + doc + "]]></content>";
            s += "       <returnValues>" + "5" + "</returnValues>";
            s += $"    </ws:sendCom>";

            return s;
        }
        #endregion

        public XmlDocument getXML(String body)
        {
            XmlDocument soapEnvelopeXml = new XmlDocument();
            String s = String.Empty;
            s = $"<soapenv:Envelope xmlns:soapenv = \"http://www.w3.org/2003/05/soap-envelope" + "\"" + " xmlns:ws = \"http://ws.feds/" + "\"" + ">";
            s += $"<soapenv:Body>";
            s += body;
            s += $"</soapenv:Body>";
            s += $"</soapenv:Envelope>";

            //Carga el String a la variable soapEnvelopeXml, este lo transforma a un archivo xml
            soapEnvelopeXml.LoadXml(s);

            return soapEnvelopeXml;
        }

        public IntegradorRespuesta SendMenssage(typeMethod Metodo, String tipoDocSunat, String doc_o_Folio)
        {
            String soapResult;
            HttpWebRequest webRequest;
            // define un documento XML que esta en una sercuencia de bytes
            XmlDocument soapEnveloXml = new XmlDocument();
            String body = String.Empty;

            // se asigna las propiedades de envio 
            webRequest = getWebRequest();

            // Se asigna el formato correcto deacuerdo al servicio que se realizara
            switch (Metodo)
            {
                case typeMethod.sendDoc:
                    body = getBodySendDoc(doc_o_Folio);
                    break;
                case typeMethod.getInfo:
                    body = getBodyGetInfo(tipoDocSunat, doc_o_Folio);
                    break;
                case typeMethod.sendVoidedDoc:
                    body = getBodyVoidedDoc(doc_o_Folio);
                    break;
                case typeMethod.sendDespatchAdvice:
                    body = getBodySendDespatchAdvice(doc_o_Folio);
                    break;
                case typeMethod.sendCom:
                    body = getBodySendCom(doc_o_Folio);
                    break;

            }

            // Se asigna todo el body a un documento XML
            soapEnveloXml = getXML(body);

            // obtiene las propieades de envio en una cadena de bytes
            using (Stream stream = webRequest.GetRequestStream())
            {
                // se asigna las propiedades de envio en bytes al documento XML
                soapEnveloXml.Save(stream);
            }

            using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {

                    soapResult = rd.ReadToEnd();
                }
            }

            XmlDocument x1 = new XmlDocument();
            IntegradorRespuesta ir = new IntegradorRespuesta();
            EstadoDocumento ed = new EstadoDocumento();

            // LoadXml carga la cadena en la variable tipo XmlDocument, transformandolo en un documento html
            x1.LoadXml(soapResult.Replace("&", "&amp"));
            //NameTable implementa el XmlNameTable, y hace que si un elemento o atributo aparece varias veces en el documento solo se alamcena una vez
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(x1.NameTable);
            String xpath = String.Empty;
            String action = String.Empty;
            String statustag = String.Empty;
            String status_val = String.Empty;

            nsmgr.AddNamespace("soap", "http://www.w3.org/2003/05/soap-envelope");
            nsmgr.AddNamespace("ns2", "http://ws.feds/");

            switch (Metodo)
            {
                case typeMethod.sendDoc:
                    action = "sendDocResponse";
                    statustag = "";
                    break;
                case typeMethod.getInfo:
                    action = "sendInfoResponse";
                    statustag = "info";
                    break;
                case typeMethod.sendVoidedDoc:
                    action = "sendVoidedDocumentResponse";
                    statustag = "responseCode";
                    break;
                case typeMethod.sendDespatchAdvice:
                    action = "sendDespatchAdviceResponse";
                    statustag = "";
                    break;
                case typeMethod.sendCom:
                    action = "sendComResponse";
                    statustag = "";
                    break;
            }

            xpath = $"soap:Envelope/soap:Body/ns2:" + action + "/return";

            if (x1.SelectNodes(xpath, nsmgr).Count == 0)
            {
                ir.Codigo = "-1000";
                ir.MensajeError = "Tag" + statustag + "no encontrado";
            }
            else
            {
                status_val = x1.SelectNodes(xpath, nsmgr).Item(0).InnerText;
                if (status_val.Length == 0)
                {
                    ir.Codigo = "-1000";
                    ir.MensajeError = "Contenido retornado del webservice se encuentra vacio";
                }
                else
                {
                    string[] secciones = status_val.Split('\n');
                    string code = secciones[0].Split('|')[1];
                    string mensaje = secciones[1].Split('|')[1];

                    // Enviar Documento
                    if (Metodo == typeMethod.sendDoc)
                    {
                        if (code == "0")
                        {
                            ir.Codigo = "0";
                            ir.DocID = secciones[2].Split('|')[1];
                            string estadoose = secciones[6].Split('|')[1];

                            switch (estadoose)
                            {
                                case "-1":
                                    ir.estadoDocumento = ed.NoConsultado;
                                    break;
                                case "1":
                                    ir.estadoDocumento = ed.Aceptado;
                                    break;
                                case "2":
                                    ir.estadoDocumento = ed.AceptadoConObservaciones;
                                    break;
                                case "3":
                                    ir.estadoDocumento = ed.Rechazado;
                                    break;
                                case "4":
                                    ir.estadoDocumento = ed.Anulado;
                                    break;
                            }

                            ir.oseDescripcion = secciones[8].Split('|')[1];


                        }
                        else
                        {
                            ir.Codigo = code;
                            ir.MensajeError = mensaje;
                        }
                    }

                    else if (Metodo == typeMethod.sendCom)
                    {
                        if (code == "0")
                        {
                            ir.Codigo = "0";
                            ir.DocID = secciones[2].Split('|')[1];
                            string estadoose = secciones[6].Split('|')[1];

                            switch (estadoose)
                            {
                                case "-1":
                                    ir.estadoDocumento = ed.NoConsultado;
                                    break;
                                case "1":
                                    ir.estadoDocumento = ed.Aceptado;
                                    break;
                                case "2":
                                    ir.estadoDocumento = ed.AceptadoConObservaciones;
                                    break;
                                case "3":
                                    ir.estadoDocumento = ed.Rechazado;
                                    break;
                                case "4":
                                    ir.estadoDocumento = ed.Anulado;
                                    break;
                            }

                        }
                        else
                        {
                            ir.Codigo = code;
                            ir.MensajeError = mensaje;
                        }

                    }
                    //Eliminar Documento    
                    else if (Metodo == typeMethod.sendVoidedDoc)
                    {
                        if (code == "0")
                        {
                            ir.Codigo = "0";
                            ir.estadoDocumento = ed.Aceptado;
                            ir.DocID = secciones[2].Split('|')[1];
                        }
                        else
                        {
                            ir.Codigo = code;
                            ir.MensajeError = mensaje;
                        }
                    }
                    //Consultar Doducmento
                    else if (Metodo == typeMethod.getInfo)
                    {
                        string estadoose = secciones[6].Split('|')[1];
                        ir.Codigo = code;
                        ir.MensajeError = mensaje;

                        switch (estadoose)
                        {
                            case "-1":
                                ir.estadoDocumento = ed.NoConsultado;
                                break;
                            case "1":
                                ir.estadoDocumento = ed.Aceptado;
                                break;
                            case "2":
                                ir.estadoDocumento = ed.AceptadoConObservaciones;
                                break;
                            case "3":
                                ir.estadoDocumento = ed.Rechazado;
                                ir.MensajeError = "Rechazado";
                                break;
                            case "4":
                                ir.estadoDocumento = ed.Anulado;
                                ir.MensajeError = "Anulado";
                                break;
                        }

                    }
                }

            }

            return ir;
        }

        public IntegradorRespuesta consultarComprobanteEstado(string tipoDocSunat, string serie, string Numero)
        {
            return SendMenssage(typeMethod.getInfo, tipoDocSunat , serie + Numero);
        }

        public IntegradorRespuesta enviarComprobanteAnular(string doc)
        {
            return SendMenssage(typeMethod.sendVoidedDoc, null, doc);
        }

        public IntegradorRespuesta enviarDocumento(string doc)
        {
            return SendMenssage(typeMethod.sendDoc, null, doc);
        }

        public IntegradorRespuesta enviarRetencionPercepcion(string doc)
        {
            return SendMenssage(typeMethod.sendCom, null, doc);
        }
    }

}