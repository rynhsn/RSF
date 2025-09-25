using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02120Print
{
    public class PMT02120PrintReportChecklistDTO
    {
        public string CCATEGORY { get; set; }
        public string CITEM { get; set; }
        public string CSTATUS { get; set; }
        public string CNOTES { get; set; }
        public string CQUANTITY { get; set; }
        public string CUNIT { get; set; }
        //public string CACTUAL_QUANTITY { get; set; }
        public int IDEFAULT_QUANTITY { get; set; }

        //CR05
        //public string CUNIT { get; set; }
    }
}
