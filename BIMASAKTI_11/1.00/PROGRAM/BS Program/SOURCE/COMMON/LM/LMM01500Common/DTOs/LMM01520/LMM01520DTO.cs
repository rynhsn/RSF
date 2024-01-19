using R_APICommonDTO;

namespace LMM01500COMMON
{
    public class LMM01520DTO
    {
        // parameter
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; } 
        public string CINVGRP_CODE { get; set; }
        public string CUSER_ID { get; set; }
        public string CACTION { get; set; }

        // + result
        public string CINVGRP_NAME { get; set; }
        public bool LACTIVE { get; set; } = false;
        public bool LPENALTY { get; set; } = false;
        public string CPENALTY_ADD_ID { get; set; } = "";
        public string CCHARGES_NAME { get; set; } = "";

        public string CPENALTY_TYPE { get; set; } = "";

        public int NPENALTY_TYPE_VALUE { get; set; }
        public int NPENALTY_TYPE_VALUE_MonthlyAmmount { get; set; }
        public int NPENALTY_TYPE_VALUE_MonthlyPercentage { get; set; }
        public int NPENALTY_TYPE_VALUE_DailyAmmount { get; set; }
        public int NPENALTY_TYPE_VALUE_DailyPercentage { get; set; }
        public int NPENALTY_TYPE_VALUE_OneTimeAmmount { get; set; }
        public int NPENALTY_TYPE_VALUE_OneTimePercentage { get; set; }

        public string CPENALTY_TYPE_CALC_BASEON { get; set; } = "";
        public string CPENALTY_TYPE_CALC_BASEON_CalcBaseonMonth { get; set; } = "";
        public string CPENALTY_TYPE_CALC_BASEON_CalcBaseonDays { get; set; } = "";

        public int IROUNDED { get; set; } = 0;
        public string CCUTOFDATE_BY { get; set; } = "";
        public int IGRACE_PERIOD { get; set; }
        public string CPENALTY_FEE_START_FROM { get; set; } = "";
        public bool LEXCLUDE_SPECIAL_DAY_HOLIDAY { get; set; } = false;
        public bool LEXCLUDE_SPECIAL_DAY_SATURDAY { get; set; } = false;
        public bool LEXCLUDE_SPECIAL_DAY_SUNDAY { get; set; } = false;
        public int NMIN_PENALTY_AMOUNT { get; set; }
        public int NMAX_PENALTY_AMOUNT { get; set; }
        public string CACTIVE_BY { get; set; }
    }
}
