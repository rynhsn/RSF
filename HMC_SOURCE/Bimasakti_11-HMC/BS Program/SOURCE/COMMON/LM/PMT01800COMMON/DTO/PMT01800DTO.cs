using System;
using System.Collections.Generic;
using R_APICommonDTO;

namespace PMT01800COMMON.DTO
{
    public class PMT01800DTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CTENANT_ID { get; set; }
        public string CSALESMAN_ID { get; set; }
        public string CTRANS_STATUS { get; set; }
        public string CAGREEMENT_STATUS { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CREF_NO { get; set; } = "";
        public string CBUILDING_ID { get; set; } = "";
        public string CALLER_ACTION { get; set; } = "";
        public string CREF_DATE { get; set; }
        public DateTime? CREF_DATE_DISPLAY { get; set; }
        public DateTime? CFOLLOW_UP_DATE_DISPLAY { get; set; }
        public string CFOLLOW_UP_DATE { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CSALESMAN_NAME { get; set; }
        public string CORIGINAL_REF_NO { get; set; }
        public string CTRANS_STATUS_DESC { get; set; }
        public string CAGREEMENT_STATUS_DESC { get; set; }

        public string CBUILDING_NAME { get; set; } = "";
        public string CFLOOR_NAME { get; set; }
        public string CUNIT_NAME { get; set; }
        public string CUNIT_TYPE_NAME { get; set; }
        public decimal NGROSS_AREA_SIZE { get; set; }
        public decimal NNET_AREA_SIZE { get; set; }
    }

    public class PMT01800ListDTO : R_APIResultBaseDTO
    {
        public List<PMT01800DTO> Data { get; set; }
    }
}