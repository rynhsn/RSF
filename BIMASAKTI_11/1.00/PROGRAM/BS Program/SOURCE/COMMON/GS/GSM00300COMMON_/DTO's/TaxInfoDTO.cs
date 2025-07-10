using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace GSM00300COMMON.DTO_s
{
    public class TaxInfoDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CTAX_TYPE { get; set; }
        public string CTAX_ID { get; set; }
        public string CTAX_NAME { get; set; }
        public string CID_TYPE { get; set; }
        public string CID_NO { get; set; }
        public string CID_EXPIRED_DATE { get; set; }
        public DateTime? DID_EXPIRED_DATE { get; set; }
        public decimal NTAX_CODE_LIMIT_AMOUNT { get; set; }
        public string CTAX_ADDRESS { get; set; }
        public string CTAX_PHONE1 { get; set; }
        public string CTAX_PHONE2 { get; set; }
        public string CTAX_EMAIL { get; set; }
        public string CTAX_EMAIL2 { get; set; }
        public string CUSER_ID { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
