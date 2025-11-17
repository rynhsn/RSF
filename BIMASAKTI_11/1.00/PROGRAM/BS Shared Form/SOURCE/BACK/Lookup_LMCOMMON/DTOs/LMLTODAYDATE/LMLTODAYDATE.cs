using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.LMLTODAYDATE
{
    public class LMLTODAYDATEDTO
    {
        public DateTime? DTODAY_DATE_TIME { get; set; }

        public string CYEAR { get; set; }
        public string CMONTH { get; set; }
        public string DDAY_DATE { get; set; }

        public int IYEAR { get; set; }
        public int IMONTH { get; set; }
        public int IDAY_DATE { get; set; }
    }
}
