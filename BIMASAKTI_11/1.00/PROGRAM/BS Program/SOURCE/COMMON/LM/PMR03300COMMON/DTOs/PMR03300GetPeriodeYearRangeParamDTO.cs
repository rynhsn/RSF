using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace PMR03300COMMON.DTOs
{
    public class PMR03300GetPeriodeYearRangeParamDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CMODE { get; set; } = "";
        public string CYEAR { get; set; } = "";
    }
}
