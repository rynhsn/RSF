using System;
using System.Collections.Generic;
using System.Data.Common;
using R_APICommonDTO;

namespace Lookup_HDCOMMON.DTOs.HDL00100
{
    public class HDL00100DTO : R_APIResultBaseDTO
    {
        public string CDEPT_CODE { get; set; } = "";
        public string CTAXABLE { get; set; } = "";
        public bool LTAXABLE { get; set; } = false;
        public string CINVGRP_CODE { get; set; } = "";
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CPRICELIST_ID { get; set; } = "";
        public string CPRICELIST_NAME { get; set; } = "";
        public string CCHARGES_ID { get; set; } = "";
        public string CUNIT { get; set; } = "";
        public string CDESCRIPTION { get; set; } = "";
        public string CACTIVE_START_DATE { get; set; } = "";
        public string CCURRENCY_CODE { get; set; }
        public int IPRICE { get; set; } = 0;
        public string CSTART_DATE { get; set; } = "";
        public DateTime? DSTART_DATE { get; set; } = null;
    }

    public class HDL00100ListDTO
    {
        public List<HDL00100DTO> Data { get; set; }
    }
}