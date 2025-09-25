using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace BMM00500COMMON.DTO
{
    public class BMM00500DTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CPROGRAM_ID { get; set; } = string.Empty;
        public string CPROGRAM_NAME { get; set; } = string.Empty;
        public string CICON_NAME { get; set; } = string.Empty;
        public bool LTENANT { get; set; } = false;
        public bool LPORTAL { get; set; } = false;
        public bool LPRIORITY { get; set; } = false;
        public bool LACTIVE { get; set; } = true;
        public string CCREATE_BY { get; set; } = string.Empty;
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = string.Empty;
        public DateTime DUPDATE_DATE { get; set; }
    }

    public class BMM00500ParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CPROGRAM_ID { get; set; } = string.Empty;
    }

    public class BMM00500CRUDParameterDTO : CRUDParameterDTO<BMM00500DTO>
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CACTION { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
    }
}
