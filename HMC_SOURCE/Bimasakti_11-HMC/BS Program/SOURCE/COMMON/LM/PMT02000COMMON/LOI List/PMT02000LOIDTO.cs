using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02000COMMON.LOI_List
{
    public class PMT02000LOIDTO
    {
        //for Param
        public string? CPROPERTY_ID { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CBUILDING_ID { get; set; }        
        public string? CTRANS_STATUS { get; set; }
        public string? CSTATUS_NAME { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public string? CFLOOR_NAME { get; set; }
        public string? CUNIT_NAME { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string? CREF_NO { get; set; }
        public string? CHAND_OVER_DATE { get; set; }
        public DateTime? DHAND_OVER_DATE { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        //CR02 --20-02-2024
        public string? CUNIT_DESCRIPTION { get; set; }
    }
}
