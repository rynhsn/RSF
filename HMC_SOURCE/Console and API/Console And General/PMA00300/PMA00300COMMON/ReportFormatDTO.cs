using System.Collections.Generic;
using System.Text;

namespace PMA00300COMMON
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
