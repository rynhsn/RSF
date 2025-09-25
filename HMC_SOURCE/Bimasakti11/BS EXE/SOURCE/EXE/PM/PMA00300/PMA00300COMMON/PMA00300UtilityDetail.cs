using System;
using System.Collections.Generic;
using System.Text;

namespace PMA00300COMMON
{
    public class PMA00300UtilityDetail
    {
        //HEADER
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CREF_NO { get; set; }
        public string CCUSTOMER_ID { get; set; }
        public string CCUSTOMER_NAME { get; set; }
        public string CTRANS_DESC { get; set; }
        public decimal NOCCUPIABLE_AREA { get; set; }
        public string CINV_PERIOD_DESC { get; set; }
        public decimal NFEE_AMT { get; set; }
        public decimal NCHARGE_AMOUNT { get; set; }
        public decimal NTOTAL_AMOUNT { get; set; }

    }
    public class PMA00300UtilityChargesDetail
    {
        //HEADER
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCUSTOMER_ID { get; set; }
        public string CREF_NO { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CMETER_NO { get; set; }
        //public decimal NBLOCK1_CHARGE { get; set; } //Tarif listrik dan air
        //public decimal NBLOCK2_CHARGE { get; set; }
        //public decimal NBLOCK1_START { get; set; } //Meter awal listrik
        //public decimal NBLOCK2_START { get; set; }
        //public decimal NBLOCK1_END { get; set; } // meter akhir listrik
        //public decimal NBLOCK2_END { get; set; }
        //public decimal NBLOCK1_USAGE { get; set; } // penggunaan listrik 
        //public decimal NBLOCK2_USAGE { get; set; }
        public decimal NMETER_START { get; set; }  //Meter awal listrik dan water
        public decimal NMETER_END { get; set; } // meter akhir listrik dan water
        public decimal NMETER_USAGE { get; set; }  // penggunaan listrik dan air
        public decimal NMETER_CHARGE { get; set; }  //Tarif listrik dan air
        public decimal NBEBAN_BERSAMA { get; set; }
        public decimal NCAPACITY { get; set; } //daya terpasang  listrik
        public decimal NCF { get; set; } // calculation factor listrik
        public decimal NUSAGE_MIN_CHARGE { get; set; } // rekening minimum  listrik
        public decimal NMIN_CHARGE_AMT { get; set; }
        public decimal NADDITIONAL_AMT { get; set; } // biaya tambahan listrik
        public decimal NTOTAL_USAGE_KVA { get; set; }
        public decimal NSTANDING_CONSUMP_AMT { get; set; } //Pemakaian listrik // biaya tetap untuk water
        public decimal NSUB_TOTAL_AMT { get; set; } //sub total listrik
        public decimal NADMIN_FEE_AMT { get; set; }
        public decimal NVAT_AMT { get; set; } // PPJU listrik
        public decimal NADMIN_FEE_TAX_AMT { get; set; }
        public decimal NTOTAL_AMT { get; set; } //sub total biaya air / listrik
        public decimal NMAINTENANCE_FEE { get; set; } //Biaya operasional
        public decimal CFROM_SEQ_NO { get; set; }
        public string CTRANS_DESC { get; set; }
    }
}
