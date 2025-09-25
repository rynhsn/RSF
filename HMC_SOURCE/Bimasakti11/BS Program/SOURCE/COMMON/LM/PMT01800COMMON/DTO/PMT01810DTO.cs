using System;
using System.Collections.Generic;
using R_APICommonDTO;

namespace PMT01800COMMON.DTO
{
    public class PMT01810DTO : R_APIResultBaseDTO
    {
        public DateTime? CREF_DATE_DISPLAY { get; set; }
        public string CCOMPANY_ID { get; set; } = ""; 
        public string CPROPERTY_ID { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
        public string CBUILDING_ID { get; set; } = "";
        public string CDOC_NO { get; set; } = "";
        public string CDOC_DATE { get; set; } = "";
        public string CSTART_DATE { get; set; } = "";
        public string CEND_DATE { get; set; } = "";
        public int IDAYS { get; set; } = 0;
        public int IMONTHS { get; set; } = 0;
        public int IYEARS { get; set; } = 0;
        public string CSALESMAN_ID { get; set; } = "";
        public string CTENANT_ID { get; set; } = "";
        public string CUNIT_DESCRIPTION { get; set; } = "";
        public string CNOTES { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public string CLEASE_MODE { get; set; } = "";
        public string CCHARGE_MODE { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CTRANS_STATUS { get; set; }
        public string CAGREEMENT_STATUS { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CSALESMAN_NAME { get; set; }
        public string CTRANS_STATUS_DESC { get; set; }
        public string CAGREEMENT_STATUS_DESC { get; set; }
        
        public string CBUILDING_NAME { get; set; }
        public string CFLOOR_NAME { get; set; }
        public string CUNIT_NAME { get; set; }
        public string CUNIT_TYPE_NAME { get; set; }
        public decimal NGROSS_AREA_SIZE { get; set; }
        public decimal NNET_AREA_SIZE { get; set; }
    }

    public class PMT01810ListDTO : R_APIResultBaseDTO
    {
        public List<PMT01810DTO> Data { get; set; }
    }
}