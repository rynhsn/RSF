using System;
using R_APICommonDTO;

namespace GLR00300Common
{
    public class GLR00300DTO
    {
        public string CCODE { get; set; }
        public string CNAME { get; set; }
    }
    public class GLR00300InitialProcess : R_APIResultBaseDTO
    {
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }
        public string? CSOFT_PERIOD_YY { get; set; }
        public string CMIN_GLACCOUNT_NO { get; set; }
        public string CMIN_GLACCOUNT_NAME { get; set; }
        public string CMAX_GLACCOUNT_NO { get; set; }
        public string CMAX_GLACCOUNT_NAME { get; set; }
    }

    public class GLR00300GetPeriod
    {
        public string CPERIOD_NO { get; set; }
    }

    public class GLR00300BudgetNoDTO 
    {
        public string CBUDGET_NO { get; set; }
        public string CBUDGET_NAME_DISPLAY { get; set; }
    }

    public class GLR00300GetAllCenter
    {
        public string CCENTER_CODE { get; set; } = "";
        public string CCENTER_NAME { get; set; } = "";
    }
    public class GLR00300GetAccountCOA
    {
        public string CGLACCOUNT_NO { get; set; } 
        public string CGLACCOUNT_NAME { get; set; } = "Please Select Account Name.!";
    }
}
