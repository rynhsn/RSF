using System;

namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Grid display DTO for FAT00100 - Fixed Asset Transaction Grid
    /// </summary>
    public class FAT00100GridDTO
    {
        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        // Filter and period properties
        public string CFOREIGN_LANGUAGE { get; set; } = string.Empty;
        public string CPERIODFROM { get; set; } = string.Empty;
        public string CPERIODTO { get; set; } = string.Empty;
        public string CSTATUSDRAFT { get; set; } = string.Empty;
        public string CSTATUSOPEN { get; set; } = string.Empty;
        public string CSTATUSAPPROVED { get; set; } = string.Empty;
        public string CSTATUSCLOSED { get; set; } = string.Empty;
        public string CFILTER_TRANS_CODE { get; set; } = string.Empty;

        // Transaction header properties
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CTRANSACTION_CODE { get; set; } = string.Empty;
        public string CREFERENCE_NO { get; set; } = string.Empty;
        public string CTRANSACTION_DATE { get; set; } = string.Empty;
        public DateTime? DTRANSACTION_DATE { get; set; }
        public string CSTATUS { get; set; } = string.Empty;
        public string CSTATUS_DESC { get; set; } = string.Empty;
        public string CCURRENCY_CODE { get; set; } = string.Empty;
        public decimal NLBASE_RATE_AMOUNT { get; set; }
        public decimal NLCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NBBASE_RATE_AMOUNT { get; set; }
        public decimal NBCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NTRANSACTION_AMOUNT { get; set; }
        public decimal NLTRANSACTION_AMOUNT { get; set; }
        public decimal NBTRANSACTION_AMOUNT { get; set; }
        public string CDOCUMENT_DATE { get; set; } = string.Empty;
        public string CSUPPLIER_ID { get; set; } = string.Empty;
        public string CSUPPLIER_NAME { get; set; } = string.Empty;
        public string CFR_MODULE { get; set; } = string.Empty;
        public string CFR_DEPT_CODE { get; set; } = string.Empty;
        public string CFR_TRANSACTION_CODE { get; set; } = string.Empty;
        public string CFR_REFERENCE_NO { get; set; } = string.Empty;
        public string CDEPT_NAME { get; set; } = string.Empty;
        public string CCURRENCY_NAME { get; set; } = string.Empty;
        public string CTRANSACTION_NAME { get; set; } = string.Empty;

        // System and configuration properties
        public string CTRANS_DEPT_CODE { get; set; } = string.Empty;
        public string CASSET_DEPT_CODE { get; set; } = string.Empty;
        public bool LINCREMENT_FLAG { get; set; }
        public bool LJRNGRP_MODE { get; set; }
        public bool LDEPT_MODE { get; set; }
        public string CPERIOD_MODE { get; set; } = string.Empty;
        public string CCURRENT_PERIOD { get; set; } = string.Empty;
        public string CSOFT_PERIOD { get; set; } = string.Empty;
        public string CRATETYPE_CODE { get; set; } = string.Empty;
        public string CGLLINK_DATE { get; set; } = string.Empty;
        public string CPJLINK_DATE { get; set; } = string.Empty;

        // Transaction period and currency properties
        public string CTRANSACTION_PRD { get; set; } = string.Empty;
        public string CLOCAL_CURRENCY_CODE { get; set; } = string.Empty;
        public string CBASE_CURRENCY_CODE { get; set; } = string.Empty;
        public bool LCUST_PERIOD_FLAG { get; set; }
        public string CFILTER_TRANS_DESC { get; set; } = string.Empty;
        public bool LAPPROVAL_FLAG { get; set; }

        // Transaction description and approval properties
        public string CPJ_TRANS_DESC { get; set; } = string.Empty;
        public bool LCAN_APPROVE { get; set; }
        public int ISTART_YEAR { get; set; }
        public int IEND_YEAR { get; set; }
        public string CPERIOD_NO { get; set; } = string.Empty;
        public bool LCAN_CLOSE { get; set; }

        // Additional properties
        public string CINFO_SEQNO { get; set; } = string.Empty;
        public bool LCHANGE_DESC { get; set; }
        public string CMODE { get; set; } = string.Empty;
    }
}

