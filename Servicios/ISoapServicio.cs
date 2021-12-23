using System;
using Entidades;


namespace Servicios
{
    public interface ISoapServicio
    {
        IntegradorRespuesta enviarRetencionPercepcion(String doc);
        IntegradorRespuesta enviarDocumento(String doc);
        IntegradorRespuesta enviarComprobanteAnular(String doc);
        IntegradorRespuesta consultarComprobanteEstado(String tipoDocSunat, String serie, String Numero);

    }
}