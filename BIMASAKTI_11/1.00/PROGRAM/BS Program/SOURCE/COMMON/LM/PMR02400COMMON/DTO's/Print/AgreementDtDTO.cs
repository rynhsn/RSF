using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace PMR02400COMMON.DTO_s.Print
{
    public class AgreementDtDTO : AgreementDTO
    {
        public string CINVOICE_NO { get; set; }
        public string CINVOICE_DESCRIPTION { get; set; }
        public string CDUE_DATE { get; set; }
        public DateTime DDUE_DATE { get; set; }
        public int ILATE_DAYS { get; set; }
    }
}
