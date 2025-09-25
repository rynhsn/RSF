using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02000COMMON.DTO_s.Print.Grouping
{
    public class SubtotalCurrenciesDTO
    {
        public string CDEPT_CODE { get; set; }
        public string CCUSTOMER_ID { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NBEGINNING_APPLY_AMOUNT { get; set; }
        public decimal NREMAINING_AMOUNT { get; set; }
        public decimal NTAX_AMOUNT { get; set; }
        public decimal NGAINLOSS_AMOUNT { get; set; }
        public decimal NCASHBANK_AMOUNT { get; set; }
    }
}
