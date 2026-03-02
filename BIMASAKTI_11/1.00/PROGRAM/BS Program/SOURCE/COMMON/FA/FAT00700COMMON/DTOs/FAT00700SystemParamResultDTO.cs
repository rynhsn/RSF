using System;
using System.Collections.Generic;
using System.Text;

namespace FAT00700Common.DTOs
{
    public  class FAT00700SystemParamResultDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CTRANS_DEPT_CODE { get; set; } = string.Empty;
        public string CTRANS_DEPT_NAME { get; set; } = string.Empty;
        public string CASSET_DEPT_CODE { get; set; } = string.Empty;
        public string CASSET_DEPT_NAME { get; set; } = string.Empty;
        public string CRATETYPE_CODE { get; set; } = string.Empty;
        public string CRATETYPE_DESCRIPTION { get; set; } = string.Empty;
        public string CSOFT_PERIOD { get; set; } = string.Empty;
        public string CSOFT_PERIOD_YY { get; set; } = string.Empty;
        public string CSOFT_PERIOD_MM { get; set; } = string.Empty;
        public string CSOFT_CLOSING_BY { get; set; } = string.Empty;
        public string CLSOFT_END_BY { get; set; } = string.Empty;
        public string CCURRENT_PERIOD { get; set; } = string.Empty;
        public string CCURRENT_PERIOD_YY { get; set; } = string.Empty;
        public string CCURRENT_PERIOD_MM { get; set; } = string.Empty;
        public string CLPRD_END_BY { get; set; } = string.Empty;
        public string CASSET_JOURNAL_TYPE { get; set; } = string.Empty;
        public string CAUTO_DEPR_TYPE { get; set; } = string.Empty;
        public string CGLLINK_DATE { get; set; } = string.Empty;
        public string CPJLINK_DATE { get; set; } = string.Empty;
        public string CICLINK_DATE { get; set; } = string.Empty;
        public string CCREATE_BY { get; set; } = string.Empty;
        public string CUPDATE_BY { get; set; } = string.Empty;
        public bool LGLLINK { get; set; }
        public bool LSOFT_CLOSING_FLAG { get; set; }
        public bool LPRD_END_FLAG { get; set; }
        public bool LINCREMENT_FLAG { get; set; }
        public bool LBY_DEPT { get; set; }
        public bool LPJLINK { get; set; }
        public bool LICLINK { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public DateTime? DLSOFT_END_DATE { get; set; }
        public DateTime? DLPRD_END_DATE { get; set; }

        public int IJRNGRP_LENGTH { get; set; }
        public int IBY_DEPT_LENGTH { get; set; }
        public int IROW_COUNT { get; set; }
    }
}
