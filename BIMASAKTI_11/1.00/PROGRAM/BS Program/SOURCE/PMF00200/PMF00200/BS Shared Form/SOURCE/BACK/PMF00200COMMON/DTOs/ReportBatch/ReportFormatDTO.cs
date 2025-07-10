using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PMF00200COMMON
{
    public class ReportFormatDTO
    {
        public int _DecimalPlaces { get; set; }
        public string _DecimalSeparator { get; set; }
        public string _GroupSeparator { get; set; }
        public string _ShortDate { get; set; }
        public string _ShortTime { get; set; }
        public string _LongDate { get; set; }
        public string _LongTime { get; set; }

    }
}
