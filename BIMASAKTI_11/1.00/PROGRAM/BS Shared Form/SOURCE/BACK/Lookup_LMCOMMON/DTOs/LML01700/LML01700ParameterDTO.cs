using Lookup_PMCOMMON.DTOs.UtilityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.LML01700
{
    public class LML01700ParameterDTO : BaseDTO
    {
        public string? CPERIOD { get; set; } = "";
        public string? CCUSTOMER_ID { get; set; } = "";
        public string? CCUSTOMER_NAME { get; set; } = "";
        public string? CLOI_AGRMT_ID { get; set; } = "";
        public string? CRECEIPT_ID { get; set; } = "";
        public string? CREC_ID { get; set; } = "";
        public string? CDEPT_CODE { get; set; } = "";
        public string CSEARCH_TEXT { get; set; } = "";

    }
}
