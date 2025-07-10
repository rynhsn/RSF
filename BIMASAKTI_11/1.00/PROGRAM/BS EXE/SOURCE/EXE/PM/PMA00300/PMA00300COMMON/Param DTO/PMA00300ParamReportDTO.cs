using System;
using System.Collections.Generic;
using System.Text;

namespace PMA00300COMMON.Param_DTO
{
    public class PMA00300ParamReportDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CLANG_ID { get; set; } = "";
        public string CTENANT_ID { get; set; } = "";
        public string CPERIOD { get; set; } = "";
        public string CREPORT_TYPE { get; set; } = ""; //@CSERVICE_TYPE
        public string CSERVICE_TYPE { get; set; } = "";

    }
}
