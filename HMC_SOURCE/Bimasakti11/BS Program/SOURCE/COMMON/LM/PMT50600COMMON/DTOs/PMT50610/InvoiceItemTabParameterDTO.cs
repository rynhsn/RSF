using PMT50600COMMON.DTOs.PMT50600;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON.DTOs.PMT50610
{
    public class InvoiceItemTabParameterDTO
    {
        public string CREC_ID { get; set; }
        public bool LIS_NEW { get; set; } = false;
    }
}
