using System;
using System.Globalization;

namespace PMM04500COMMON.DTO_s
{
    public class PricingDTO : UnitTypeCategoryDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPRICE_TYPE { get; set; }
        public string CUNIT_TYPE_CATEGORY_ID { get; set; }
        public string CUNIT_TYPE_CATEGORY_NAME { get; set; }
        public string CVALID_INTERNAL_ID { get; set; }
        public string CVALID_DATE { get; set; } = "";
        public DateTime? DVALID_DATE => string.IsNullOrWhiteSpace(CVALID_DATE) ? DateTime.Now : DateTime.ParseExact(CVALID_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_TYPE_DESCR { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public string CPRICE_MODE { get; set; }
        public string CPRICE_MODE_DESCR { get; set; }
        public decimal NNORMAL_PRICE { get; set; }
        public decimal NBOTTOM_PRICE { get; set; }
        public decimal NTOTAL_PRICE { get; set; }
        public bool LOVERWRITE { get; set; }
        public string CINVOICE_PERIOD { get; set; }
        public string CINVOICE_PERIOD_DESCR { get; set; }
        public string CACTIVE_BY { get; set; }
        public DateTime DACTIVE_DATE { get; set; }
        public string CINACTIVE_BY { get; set; }
        public DateTime DINACTIVE_DATE { get; set; }
    }
}
