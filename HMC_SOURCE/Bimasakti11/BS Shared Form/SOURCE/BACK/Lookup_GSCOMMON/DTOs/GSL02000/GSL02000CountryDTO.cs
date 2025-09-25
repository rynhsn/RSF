using System;

namespace Lookup_GSCOMMON.DTOs
{
    public class GSL02000CountryDTO
    {
        // Param
        public string CUSER_ID { get; set; }

        // Result
        public string CCODE { get; set; }
        public string CNAME { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
