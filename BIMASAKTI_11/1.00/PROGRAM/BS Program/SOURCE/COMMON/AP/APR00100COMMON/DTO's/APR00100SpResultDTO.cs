using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace APR00100COMMON.DTO_s
{
    public class APR00100SpResultDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime DREF_DATE { get; set; }
        public string CREF_PRD { get; set; }
        public string CREF_NO { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRANS_NAME { get; set; }
        public string CSUPPLIER_ID { get; set; }
        public string CSUPPLIER_NAME { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CBASE_CURRENCY_CODE { get; set; }
        public string CLOCAL_CURRENCY_CODE { get; set; }
        public decimal NBEGINNING_APPLY_AMOUNT { get; set; }
        public decimal NLBEGINNING_APPLY_AMOUNT { get; set; }
        public decimal NBBEGINNING_APPLY_AMOUNT { get; set; }
        public decimal NTOTAL_REMAINING { get; set; }
        public decimal NLTOTAL_REMAINING { get; set; }
        public decimal NBTOTAL_REMAINING { get; set; }
        public decimal NTAXABLE_AMOUNT { get; set; }
        public decimal NBTAXABLE_AMOUNT { get; set; }
        public decimal NLTAXABLE_AMOUNT { get; set; }
        public decimal NGAIN_LOSS_AMOUNT { get; set; }
        public decimal NLGAIN_LOSS_AMOUNT { get; set; }
        public decimal NBGAIN_LOSS_AMOUNT { get; set; }
        public decimal NCASH_BANK_AMOUNT { get; set; }
        public decimal NLCASH_BANK_AMOUNT { get; set; }
        public decimal NBCASH_BANK_AMOUNT { get; set; }
    }
}
