using System;

namespace Entidades
{
    public class IntegradorRespuesta
    {
        public String Codigo { get; set; } = String.Empty;
        public String DocID { get; set; } = String.Empty;
        public String PDF { get; set; } = String.Empty;
        public String PDFBase64 { get; set; } = String.Empty;
        public String MensajeError { get; set; } = String.Empty;
        public Int32 OseStatus { get; set; }
        public String RespuestaEstadoSunatNoConsultado { get; set; } = "0";
        public int estadoDocumento { get; set; }
        public String oseDescripcion { get; set; } = String.Empty;



    }
}
