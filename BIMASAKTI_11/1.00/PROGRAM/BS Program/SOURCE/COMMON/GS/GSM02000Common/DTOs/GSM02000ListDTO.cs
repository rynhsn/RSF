using System;
using System.Collections.Generic;
using R_APICommonDTO;

namespace GSM02000Common.DTOs
{
    public class GSM02000ListDTO : R_APIResultBaseDTO
    {
        public List<GSM02000GridDTO> Data { get; set; }
    }
    
    public class GSM02000GridDTO
    {
        public bool LACTIVE { get; set; }
        public string CTAX_ID { get; set; }
        public string CTAX_NAME { get; set; }
        public decimal NTAX_PERCENTAGE { get; set; }
        public string CROUNDING_MODE_DESC { get; set; }
        public int IROUNDING { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}