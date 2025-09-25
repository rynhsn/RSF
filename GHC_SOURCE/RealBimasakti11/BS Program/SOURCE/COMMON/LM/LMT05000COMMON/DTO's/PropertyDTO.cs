using System;
using System.Collections.Generic;
using System.Text;

namespace PMT05000COMMON
{
    public class PropertyDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CUSER_ID { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public string CCURRENCY { get; set; }
    }

    public class PropertyParamDTO : PropertyDTO { }
}
