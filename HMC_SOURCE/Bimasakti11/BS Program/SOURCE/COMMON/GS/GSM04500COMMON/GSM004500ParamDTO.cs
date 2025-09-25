using System;
using System.Collections.Generic;
using System.Text;

namespace GSM04500Common
{
    public class GSM004500ParamDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_ID { get; set; } = "";
        public string CPROPERTY_NAME { get; set; } = "";
        public string CJRNGRP_TYPE { get; set; } = "";
    }
}
