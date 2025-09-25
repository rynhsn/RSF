using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace PMM07500COMMON.DTO_s.stamp_amount
{
    public class PMM07520SaveDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CACTION { get; set; }
        public string CSTAMP_CODE { get; set; }
        public string CGRAND_PARENT_ID { get; set; }
        public string CPARENT_ID { get; set; }
        public string CREC_ID { get; set; }
        public string CDATE { get; set; }
        public decimal NMIN_AMOUNT { get; set; }
        public decimal NSTAMP_AMOUNT { get; set; }
    }
}
