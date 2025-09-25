using System;
using System.Collections.Generic;

namespace PMT01500Common.DTO._4._Charges_Info
{
    public class PMT01500ChargesInfoDetailDTO
    {
        
        //External Parameter
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CUSER_ID { get; set; }
        //Updated 18 Apr 2024
        public string? CCHARGE_MODE { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CUNIT_ID { get; set; }
        //REAL DTOs
        public string CSEQ_NO { get; set; } = "";
        public string? CCHARGES_TYPE { get; set; }
        public string? CCHARGES_TYPE_DESCR { get; set; }
        public string? CCHARGES_ID { get; set; }
        public string? CCHARGES_NAME { get; set; }
        public Boolean LACTIVE { get; set; }
        public string CTAX_ID { get; set; } = "";
        public string CTAX_NAME { get; set; } = "";
        public string? CSTART_DATE { get; set; }
        public string? CYEAR { get; set; }
        public int IYEAR { get; set; }
        public string? CMONTH { get; set; }
        public string? CDAY { get; set; }
        public string? CEND_DATE { get; set; }
        public string? CBILLING_MODE { get; set; }
        public string? CFEE_METHOD { get; set; }
        public decimal NFEE_AMT { get; set; }
        public string? CINVOICE_PERIOD { get; set; }
        public decimal NINVOICE_AMT { get; set; }
        public string? CINVGRP_CODE { get; set; }
        public string? CINVGRP_NAME { get; set; }
        public string? CDESCRIPTION { get; set; } = "";
        public bool LCAL_UNIT { get; set; }
        public List<PMT01500FrontChargesInfo_FeeCalculationDetailDTO>? ODATA_FEE_CALCULATION { get; set; }
        //For Front
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }

    }
}
