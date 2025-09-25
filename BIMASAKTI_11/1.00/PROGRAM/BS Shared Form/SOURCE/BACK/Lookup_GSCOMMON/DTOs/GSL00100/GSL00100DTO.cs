using System;

namespace Lookup_GSCOMMON.DTOs
{
    public class GSL00100DTO
    {
        private string _cTAXIN_GLACCOUNT_NO_CTAXIN_GLACCOUNT_NAME;
        private string _cTAXOUT_GLACCOUNT_NO_CTAXOUT_GLACCOUNT_NAME;

        public string CCOMPANY_ID { get; set; }
        public string CTAX_ID { get; set; }
        public string CTAX_NAME { get; set; }
        public decimal NTAX_PERCENTAGE { get; set; }
        public string CDESCRIPTION { get; set; }
        public string CROUNDING_MODE { get; set; }
        public string CROUNDING_MODE_DESCR { get; set; }
        public int IROUNDING { get; set; }
        public string CTAXIN_GLACCOUNT_NO { get; set; }
        public string CTAXIN_GLACCOUNT_NAME { get; set; }
        public string CTAXIN_GLACCOUNT_NO_CTAXIN_GLACCOUNT_NAME { get => _cTAXIN_GLACCOUNT_NO_CTAXIN_GLACCOUNT_NAME; set => _cTAXIN_GLACCOUNT_NO_CTAXIN_GLACCOUNT_NAME = string.IsNullOrWhiteSpace(CTAXIN_GLACCOUNT_NO) ? "" : string.Format("{0} ({1})", CTAXIN_GLACCOUNT_NO, CTAXIN_GLACCOUNT_NAME); }
        public string CTAXOUT_GLACCOUNT_NO { get; set; }
        public string CTAXOUT_GLACCOUNT_NAME { get; set; }
        public string CTAXOUT_GLACCOUNT_NO_CTAXOUT_GLACCOUNT_NAME { get => _cTAXOUT_GLACCOUNT_NO_CTAXOUT_GLACCOUNT_NAME; set => _cTAXOUT_GLACCOUNT_NO_CTAXOUT_GLACCOUNT_NAME = string.IsNullOrWhiteSpace(CTAXOUT_GLACCOUNT_NO) ? "" : string.Format("{0} ({1})", CTAXOUT_GLACCOUNT_NO, CTAXOUT_GLACCOUNT_NAME); }
        public bool LACTIVE { get; set; }
        public string CACTIVE_BY { get; set; }
        public DateTime DACTIVE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }

    }
}
