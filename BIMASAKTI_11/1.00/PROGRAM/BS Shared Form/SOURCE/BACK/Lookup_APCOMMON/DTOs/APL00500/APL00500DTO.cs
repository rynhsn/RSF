using System;
using R_APICommonDTO;

namespace Lookup_APCOMMON.DTOs.APL00500
{
    public class APL00500DTO
    {
        public string CPROPERTY_NAME { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public decimal NTOTAL_REMAINING { get; set; }
        public string CREC_ID { get; set; }
        public string CREF_DATE { get; set; }
        public string CREF_PRD { get; set; }
        public string CSUPPLIER_ID { get; set; } = "";
        public string CSUPPLIER_NAME { get; set; }
        public string CDOC_NO { get; set; }
        public string CDOC_DATE { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public string CTRANS_STATUS { get; set; }
        public string CTRANS_STATUS_NAME { get; set; }
        public string CTRANS_DESC { get; set; }
        public string CDUE_DATE { get; set; }

        public decimal NLTOTAL_REMAINING { get; set; }
        public decimal NBTOTAL_REMAINING { get; set; }
        public decimal NAP_REMAINING { get; set; }
        public decimal NLAP_REMAINING { get; set; }
        public decimal NBAP_REMAINING { get; set; }
        public decimal NTAX_REMAINING { get; set; }
        public decimal NLTAX_REMAINING { get; set; }
        public decimal NBTAX_REMAINING { get; set; }
        public decimal NLBASE_RATE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }
        public decimal NTAX_BASE_RATE { get; set; }
        public decimal NTAX_CURRENCY_RATE { get; set; }


        public string CSUPPLIER
        {
            get => CSUPPLIER_ID + " - " + CSUPPLIER_NAME;
        }

        public string Code { get; set; }
        public string Desc { get; set; }
        public string RadioButton { get; set; } = "A";
        public string Month { get; set; }

        public string CPERIOD { get; set; }

        public int VAR_GSM_PERIOD { get; set; }
    }

    public class APL00500PeriodDTO : R_APIResultBaseDTO
    {
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }
    }
}