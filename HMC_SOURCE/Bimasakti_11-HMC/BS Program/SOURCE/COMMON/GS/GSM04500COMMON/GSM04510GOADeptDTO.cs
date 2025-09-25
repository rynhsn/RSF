using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM04500Common
{
    public class GSM04510GOADeptDTO
    {

        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CJRNGRP_TYPE { get; set; }
        public string CJRNGRP_CODE { get; set; }

        public string CGOA_CODE { get; set; }
        public string CGOA_NAME { get; set; }

        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; } = DateTime.Now;
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; } = DateTime.Now;

    }
    public class GSM04510GOADeptListDTO : R_APIResultBaseDTO
    {
        public List<GSM04510GOADeptDTO> ListData { get; set; }
    }
}
