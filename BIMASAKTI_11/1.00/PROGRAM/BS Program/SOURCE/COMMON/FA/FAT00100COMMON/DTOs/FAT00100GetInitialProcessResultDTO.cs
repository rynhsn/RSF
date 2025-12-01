namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Result DTO for GetInitialProcess method
    /// </summary>
    public class FAT00100GetInitialProcessResultDTO
    {
        public string CTRANS_DEPT_CODE { get; set; } = string.Empty;
        public string CASSET_DEPT_CODE { get; set; } = string.Empty;
        public bool LASSET_INCREMENT_FLAG { get; set; }
        public bool LJRNGRP_MODE { get; set; }
        public bool LDEPT_MODE { get; set; }
        public string CPERIOD_MODE { get; set; } = string.Empty;
        public string CCURRENT_PERIOD { get; set; } = string.Empty;
        public string CSOFT_PERIOD { get; set; } = string.Empty;
        public string CRATETYPE_CODE { get; set; } = string.Empty;
        public string CGLLINK_DATE { get; set; } = string.Empty;
        public string CPJLINK_DATE { get; set; } = string.Empty;
        public string CSUPPLIER_ID { get; set; } = string.Empty;
        public string CTRANSACTION_PRD { get; set; } = string.Empty;
        public string CLOCAL_CURRENCY_CODE { get; set; } = string.Empty;
        public string CBASE_CURRENCY_CODE { get; set; } = string.Empty;
        public bool LCUST_PERIOD_FLAG { get; set; }
        public string CFILTER_TRANS_DESC { get; set; } = string.Empty;
        public bool LAPPROVAL_FLAG { get; set; }
        public bool LINCREMENT_FLAG { get; set; }
        public string CPJ_TRANS_DESC { get; set; } = string.Empty;
        public bool LCAN_APPROVE { get; set; }
        public bool LCAN_CLOSE { get; set; }
    }
}

