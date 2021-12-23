using Entidades;
using Datos;
using Negocio;
using Servicios.Trama;
using System;
using System.Collections.Generic;
using Servicios;

using System.Windows.Forms;

namespace Servicio_Integrador
{
    internal static class Inicio
    {

        //private static readonly ISoapServicio soapServicio;

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            try
            {
                int nf = 0;

                string body = string.Empty;
                List<Documento> lista = new List<Documento>();
                DocumentoTrama trama = new DocumentoTrama();

                DocumentoDao dao = new DocumentoDao();

                lista = dao.buscarDocNoEnv();

                for (int i=0; i<lista.Count; i++)
                {
                    body = trama.obtenerDocumentoTrama(lista[i]);

                    SoapConsumir sp = new SoapConsumir();

                    IntegradorRespuesta ir = sp.SendMenssage((SoapConsumir.typeMethod)0, "123", body);

                    //IntegradorRespuesta ir = soapServicio.enviarDocumento(body);

                    if (lista[i].tipoComprobante == "01" || lista[i].tipoComprobante == "03")
                    {

                        nf = dao.actualizarDocEnviado(lista[i].serieCorrelativo , ir, body);

                    }
                    else if (lista[i].tipoComprobante == "07" || lista[i].tipoComprobante == "08")
                    {

                        nf = dao.actualizarDocEnviadoNotaCredito(lista[i].serieCorrelativo, ir, body);

                    }


                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmWindowsService());

        }
    }
}
