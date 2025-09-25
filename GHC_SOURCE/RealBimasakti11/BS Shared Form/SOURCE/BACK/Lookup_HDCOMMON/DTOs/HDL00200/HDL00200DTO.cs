using System;
using System.Collections.Generic;

namespace Lookup_HDCOMMON.DTOs.HDL00200
{
    public class HDL00200DTO
    {
        public string CDEPT_CODE { get; set; } = "";
        public bool LTAXABLE { get; set; } = false;
        public string CINVGRP_CODE { get; set; }
        public DateTime? DSTART_DATE { get; set; } = null;
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CPRICELIST_ID { get; set; } = "";
        public string CVALID_ID { get; set; } = "";
        public string CPRICELIST_NAME { get; set; } = "";
        public string CCHARGES_ID { get; set; }  = "";
        public string CUNIT { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = ""; 
        public int IPRICE { get; set; } = 0;
        public string CDESCRIPTION { get; set; } = "";
        public string CSTART_DATE { get; set; } = "";
    }
    
    public class HDL00200ListDTO
    {
        public List<HDL00200DTO> Data { get; set; }
    }
}