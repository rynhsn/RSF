using System;
using System.Collections.Generic;
using System.Text;

namespace FAT00300Common.Requests
{
    public class FAT00300GetSystemParamResultDTO
    {
        // C = string (default string.Empty)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CTRANS_DEPT_CODE { get; set; } = string.Empty;
        public string CTRANS_DEPT_NAME { get; set; } = string.Empty;
        public string CASSET_DEPT_CODE { get; set; } = string.Empty;
        public string CRATETYPE_CODE { get; set; } = string.Empty;
        public string CASSET_JOURNAL_TYPE { get; set; } = string.Empty;
        public string CAUTO_DEPR_TYPE { get; set; } = string.Empty;
        public string CJRNGRP_DELIMITER { get; set; } = string.Empty;
        public string CDEPT_DELIMITER { get; set; } = string.Empty;
        public string CPERIOD_MODE { get; set; } = string.Empty;
        public string CPERIOD_DELIMITER { get; set; } = string.Empty;
        public string CYEAR_FORMAT { get; set; } = string.Empty;
        public string CNUMBER_DELIMITER { get; set; } = string.Empty;
        public string CSEQUENCE01 { get; set; } = string.Empty;
        public string CSEQUENCE02 { get; set; } = string.Empty;
        public string CSEQUENCE03 { get; set; } = string.Empty;
        public string CSEQUENCE04 { get; set; } = string.Empty;
        public string CGLLINK_DATE { get; set; } = string.Empty;
        public string CPJLINK_DATE { get; set; } = string.Empty;
        public string CICLINK_DATE { get; set; } = string.Empty;
        public string CSOFT_PERIOD { get; set; } = string.Empty;
        public string CCURRENT_PERIOD { get; set; } = string.Empty;
        public string CLSOFT_CLOSE_BY { get; set; } = string.Empty;
        public string CPRD_END_PROGRESS_BY { get; set; } = string.Empty;
        public string CLPRD_END_BY { get; set; } = string.Empty;
        public string CCREATE_BY { get; set; } = string.Empty;
        public string CUPDATE_BY { get; set; } = string.Empty;
        public string CSOFT_PERIOD_YY { get; set; } = string.Empty;
        public string CSOFT_PERIOD_MM { get; set; } = string.Empty;
        public string CCURRENT_PERIOD_YY { get; set; } = string.Empty;
        public string CCURRENT_PERIOD_MM { get; set; } = string.Empty;

        // L = bool
        public bool LINCREMENT_FLAG { get; set; }
        public bool LJRNGRP_MODE { get; set; }
        public bool LDEPT_MODE { get; set; }
        public bool LALLOW_CANCEL_SOFTCLOSE { get; set; }

        // I = int
        public int IJRNGRP_LENGTH { get; set; }
        public int IDEPT_LENGTH { get; set; }
        public int INUMBER_LENGTH { get; set; }

        // D = DateTime? (nullable)
        public DateTime? DLSOFT_CLOSE_DATE { get; set; }
        public DateTime? DLPRD_END_DATE { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
