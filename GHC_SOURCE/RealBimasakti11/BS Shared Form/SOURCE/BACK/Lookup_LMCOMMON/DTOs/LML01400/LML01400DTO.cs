using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.LML01400
{
    public class LML01400DTO
    {
        public string? COMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CCHARGES_TYPE { get; set; }
        public string? CCHARGES_TYPE_DESCR { get; set; }
        
        public string? CDESCRIPTION { get; set; }
        public string? CCHARGES_ID { get; set; }
        public string? CCHARGES_NAME { get; set; }
        public string? CSEQ_NO { get; set; }
        public string? CTAX_ID { get; set; }
        public string? CTAX_NAME { get; set; }
        public string? CSTART_DATE { get; set; }
        public DateTime? DSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public DateTime? DEND_DATE { get; set; }
        public string? CBILLING_MODE { get; set; }
        public string? CFEE_METHOD { get; set; }
        public decimal NFEE_AMT { get; set; }
        public string? CINVOICE_PERIOD { get; set; }
        public string? CINVOICE_PERIOD_DESCR { get; set; }
        public int IINTERVAL { get; set; }
        public decimal NINVOICE_AMT { get; set; }
        public bool LCAL_UNIT { get; set; }
        public string? CCURRENCY_CODE { get; set; }
        public int IYEARS { get; set; }
        public int IMONTHS { get; set; }
        public int IDAYS { get; set; }
        public decimal? NVGRP_CODE { get; set; }
        public decimal? NVGRP_NAME { get; set; }
        public bool LPRORATE { get; set; }
        public bool LBASED_OPEN_DATE { get; set; }
        public bool LTOTAL_PRICE { get; set; }
        public bool LOVERWRITE { get; set; }
        public decimal? NBOTTOM_PRICE { get; set; }
        public bool LACTIVE { get; set; }
        public string? CACTIVE_BY { get; set; }
        public DateTime DACTIVE_DATE { get; set; }
        public string? CINACTIVE_BY { get; set; }
        public DateTime DINACTIVE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }




}
