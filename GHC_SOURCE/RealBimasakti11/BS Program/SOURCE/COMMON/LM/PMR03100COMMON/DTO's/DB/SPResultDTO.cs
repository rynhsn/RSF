using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03100COMMON.DTO_s.DB
{
    public class SPResultDTO
    {
        public decimal NINVOICE_AMT { get; set; }
        public decimal NDPP { get; set; }
        public decimal NBUKTI_POTONG_AMT { get; set; }
        public string CREF_NO { get; set; } = "";
        public decimal NPPH_AMOUNT { get; set; }
        public string CMONTH_YEAR { get; set; }
    }
}
