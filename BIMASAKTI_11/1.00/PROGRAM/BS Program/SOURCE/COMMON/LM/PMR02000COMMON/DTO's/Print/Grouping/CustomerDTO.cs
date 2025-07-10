using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02000COMMON.DTO_s.Print.Grouping
{
    public class CustomerDTO
    {
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CCUSTOMER_ID { get; set; }
        public string CCUSTOMER_NAME { get; set; }
        public List<TrxTypeDTO> Transactions { get; set; }
        public List<ProductDTO> ProductList { get; set; }
        public List<SubtotalCurrenciesDTO> CustSubtotalCurr { get; set; }
        public string CTRX_TYPE_NAME { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime DREF_DATE { get; set; }
        public string CCUSTOMER_TYPE_NAME { get; set; }
        public string CLOI_AGRMT_NO { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NBEGINNING_APPLY_AMOUNT { get; set; }
        public decimal NREMAINING_AMOUNT { get; set; }
        public decimal NTAX_AMOUNT { get; set; }
        public decimal NGAINLOSS_AMOUNT { get; set; }
        public decimal NCASHBANK_AMOUNT { get; set; }
    }
}
