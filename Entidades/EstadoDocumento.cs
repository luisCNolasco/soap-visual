namespace Entidades
{
    public class EstadoDocumento
    {
        public int NoRegistrado { get; set; } = -1;
        public int NoConsultado { get; set; } = 0;
        public int Aceptado { get; set; } = 1;
        public int AceptadoConObservaciones { get; set; } = 2;
        public int Rechazado { get; set; } = 3;
        public int Anulado { get; set; } = 4;

    }
}