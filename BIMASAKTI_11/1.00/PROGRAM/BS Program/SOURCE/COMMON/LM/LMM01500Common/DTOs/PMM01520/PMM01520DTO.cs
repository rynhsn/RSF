using R_APICommonDTO;
using System;

namespace PMM01500COMMON
{
    public class PMM01520DTO
    {
        // parameter
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; } 
        public string CINVGRP_CODE { get; set; }
        public string CUSER_ID { get; set; }
        public string CACTION { get; set; }

        // + result
        public string CPENALTY_DATE { get; set; }
        public DateTime? DPENALTY_DATE { get; set; }
        public string CINVGRP_NAME { get; set; }
        public bool LACTIVE { get; set; }
        public bool LPENALTY { get; set; }
        public string CPENALTY_UNIT_CHARGES_ID { get; set; } = "";
        public string CPENALTY_CHARGES_DESCR { get; set; } = "";
        public string CPENALTY_ADD_ID { get; set; } = "";
        public string CCHARGES_NAME { get; set; } = "";
        public string CPENALTY_ADD_NAME { get; set; } = "";

        public string CPENALTY_TYPE { get; set; } = "";

        public decimal NPENALTY_TYPE_VALUE { get; set; }
        public decimal NPENALTY_TYPE_VALUE_MonthlyAmmount { get; set; }
        public decimal NPENALTY_TYPE_VALUE_MonthlyPercentage { get; set; }
        public decimal NPENALTY_TYPE_VALUE_DailyAmmount { get; set; }
        public decimal NPENALTY_TYPE_VALUE_DailyPercentage { get; set; }
        public decimal NPENALTY_TYPE_VALUE_OneTimeAmmount { get; set; }
        public decimal NPENALTY_TYPE_VALUE_OneTimePercentage { get; set; }

        public string CROUNDING_MODE { get; set; } = "";
        public string CPENALTY_TYPE_CALC_BASEON { get; set; } = "";
        public string CPENALTY_TYPE_CALC_BASEON_CalcBaseonMonth { get; set; } = "";
        public string CPENALTY_TYPE_CALC_BASEON_CalcBaseonDays { get; set; } = "";

        public int IROUNDED { get; set; } = 0;
        public string CCUTOFDATE_BY { get; set; } = "";
        public int IGRACE_PERIOD { get; set; }
        public string CPENALTY_FEE_START_FROM { get; set; } = "";
        public bool LEXCLUDE_SPECIAL_DAY_HOLIDAY { get; set; }
        public bool LEXCLUDE_SPECIAL_DAY_SATURDAY { get; set; }
        public bool LEXCLUDE_SPECIAL_DAY_SUNDAY { get; set; }
        public bool LMIN_PENALTY_AMOUNT { get; set; }
        public bool LMAX_PENALTY_AMOUNT { get; set; }
        public decimal NMIN_PENALTY_AMOUNT { get; set; }
        public decimal NMAX_PENALTY_AMOUNT { get; set; }
        public string CACTIVE_BY { get; set; }
    }


}
