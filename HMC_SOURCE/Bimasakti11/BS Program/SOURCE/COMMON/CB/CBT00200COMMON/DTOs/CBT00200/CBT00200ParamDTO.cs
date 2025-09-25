using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CBT00200COMMON
{
    public class CBT00200ParamDTO
    {
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CPERIOD { get; set; }
        public string CSTATUS { get; set; } = "";
        public string CSEARCH_TEXT { get; set; } = "";
    }

    public class CBT00200SaveParamDTO
    {
        public CBT00200DTO Data { get; set; }
        public eCRUDMode CRUDMode { get; set; }
        public ePARAM_CALLER PARAM_CALLER { get; set; } = ePARAM_CALLER.TRANSACTION;
    }
}
