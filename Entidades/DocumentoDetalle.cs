namespace Entidades
{
    public class DocumentoDetalle
    {

        //DLN_HD
        public int lineaNumero { get; set; }
        public double lineaCantVen { get; set; }
        public string lineaUnidMed { get; set; }
        public double lineaImporte { get; set; }

        //DLN_PI
        public double lineaValorUni { get; set; }

        //DLN_PR_ACP
        public double lineaPrecUni { get; set; } //
        public int lineaPrecUniCod { get; set; }

        
        //DLN_AC
        //public bool dlnAC1 { get; set; }
        public double montoDescuento { get; set; }//
        public double importeBruto { get; set; }//
        public double porcentajeDescuento { get; set; }//


        //DLN_TT
        public double lineaTribTotal { get; set; }

        //DLN_TT_TS
        public double lineaTribImpor { get; set; }
        public int lineaTribTipoAfec { get; set; }
        public double lineaTribPorce { get; set; } //

        // DLN_IT
        public string lineaItemCod { get; set; } 
        public int lineaItemCodSunat { get; set; }

        //DLN_IT_DS
        public string lineaItemDesc { get; set; }

    }
}