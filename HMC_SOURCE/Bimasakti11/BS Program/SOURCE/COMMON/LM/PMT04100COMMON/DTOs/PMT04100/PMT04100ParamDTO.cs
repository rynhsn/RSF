using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PMT04100COMMON
{
    public class PMT04100ParamDTO
    {
        public string CDEPT_CODE { get; set; }
        public string CCUSTOMER_ID { get; set; }
        public string CCUSTOMER_NAME { get; set; }
        public string CCUSTOMER_TYPE_NAME { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CPERIOD { get; set; }
        public string CSTATUS { get; set; } = "";
        public string CSEARCH_TEXT { get; set; } = "";
    }

    public class PMT04100SaveParamDTO
    {
        public PMT04100DTO Data { get; set; }
        public eCRUDMode CRUDMode { get; set; }
    }
}
