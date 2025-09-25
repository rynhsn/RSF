using System;
using System.Collections.Generic;
using System.Text;

namespace PMM09000COMMON.UtiliyDTO
{
    public class PMM09000DbParameterDTO : BaseDTO
    {
        public string? CUNIT_OPTION { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CBUILDING_CODE { get; set; }
        public string? CTRANS_TYPE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CACTION { get; set; }
        public string? CGENERATE_MODE { get; set; }
    }
}
