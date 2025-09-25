using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace PMM00100COMMON.DTO_s
{
    public class SystemParamDetailDTO : SystemParamDTO
    {
        public string CSOFT_PERIOD { get; set; }
        public string CCURRENT_PERIOD { get; set; }
        public string CINV_PRD { get; set; }
        public string CINVOICE_MODE { get; set; }
        public bool LGLLINK { get; set; }
        public bool LINV_PROCESS_FLAG { get; set; }
        public string CWHT_MODE { get; set; }
        public string CCUR_RATETYPE_CODE { get; set; }
        public string CCUR_RATETYPE_DESC { get; set; }
        public string CTAX_RATETYPE_CODE { get; set; }
        public string CTAX_RATETYPE_DESC { get; set; }
        public string COVER_RECEIPT { get; set; }
        public string COVER_RECEIPT_DESC { get; set; }
        public decimal NOVER_TOLERANCE_AMOUNT { get; set; }
        public string CLESS_RECEIPT { get; set; }
        public string CLESS_RECEIPT_DESC { get; set; }
        public decimal NLESS_TOLERANCE_AMOUNT { get; set; }
        public string CELECTRIC_PERIOD { get; set; }
        public string CWATER_PERIOD { get; set; }
        public string CGAS_PERIOD { get; set; }
        public bool LELECTRIC_END_MONTH { get; set; }
        public bool LWATER_END_MONTH { get; set; }
        public bool LGAS_END_MONTH { get; set; }
        public string CELECTRIC_DATE { get; set; }
        public string CWATER_DATE { get; set; }
        public string CGAS_DATE { get; set; }
        public string CCUR_RATE_TYPE_CODE { get; set; }
        public string CTAX_RATE_TYPE_CODE { get; set; }
        public bool LENABLED_PERIOD { get; set; }
        public bool LENABLED_UTILITY_PERIOD { get; set; }
        public int IMAX_DAYS { get; set; }
        public int IMAX_ATTEMPTS { get; set; }
        public string CCALL_TYPE_ID { get; set; }
        public string CCALL_TYPE_NAME { get; set; }
        public bool LALL_BUILDING { get; set; }
        public bool LPRIORITY { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public bool LDISTRIBUTE_PDF { get; set; }
        public bool LINCLUDE_IMAGE { get; set; }
    }
}
