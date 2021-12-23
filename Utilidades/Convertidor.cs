using System;

namespace Utilidades
{
    public class Convertidor
    {
        public string formatearFecha(DateTime fecha)
        {

            if (fecha == null)
            {
                return "";
            }
            else
            {
                return fecha.ToString("yyyy-MM-dd");
            }
        }
    }
}
