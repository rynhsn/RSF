using System;

namespace FAM00100Common.DTOs.FAM00100
{
    public class FAM00100DTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CACTION { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CRATETYPE_CODE { get; set; }
        public string CRATETYPE_DESCRIPTION { get; set; }
        public string CTRANS_DEPT_CODE { get; set; }
        public string CTRANS_DEPT_NAME { get; set; }
        public string CASSET_DEPT_CODE { get; set; }
        public string CASSET_DEPT_NAME { get; set; }
        public string CICLINK_DATE { get; set; }
        public string CPJLINK_DATE { get; set; }
        public string CGLLINK_DATE { get; set; }
        public bool LALLOW_EDIT_FA_LINK_DATE { get; set; }
        public bool LINPUT_CHEQUE_DATE { get; set; }
        public string CBANK_IN_MODE { get; set; }
        public string CBANK_IN_MODE_NAME { get; set; }
        public bool LALLOW_CANCEL_SOFT_END { get; set; } = false;
        public string CPRD_END_CLOSING_BY { get; set; }
        public string CCONTRA_ACCOUNT_NO { get; set; }
        public string CCONTRA_ACCOUNT_NAME { get; set; }
        public string CCRDVG_ACCOUNT_NO { get; set; }
        public string CCRDVG_ACCOUNT_NAME { get; set; }
        public string CCRDVL_ACCOUNT_NO { get; set; }
        public string CCRDVL_ACCOUNT_NAME { get; set; }
        public string CSOFT_PERIOD { get; set; }
        public string CSOFT_PERIOD_YY { get; set; }
        public int CSOFT_PERIOD_YY_INT { get; set; }
        public string CSOFT_PERIOD_MM { get; set; }
        public string CLSOFT_END_BY { get; set; }
        public DateTime DLSOFT_END_DATE { get; set; }
        public string CCURRENT_PERIOD { get; set; }
        public string CCURRENT_PERIOD_YY { get; set; }
        public int CCURRENT_PERIOD_YY_INT { get; set; }
        public string CCURRENT_PERIOD_MM { get; set; }
        public bool LPRD_END_FLAG { get; set; }
        public bool LFA_NUMBERING { get; set; }
        public string CLPRD_END_BY { get; set; }
        public DateTime DLPRD_END_DATE { get; set; }
        public string CASSET_JOURNAL_TYPE { get; set; }
        public int IJRNGRP_LENGTH { get; set; }
        public int IBY_DEPT_LENGTH { get; set; }
        public bool LSOFT_CLOSING_FLAG { get; set; }
        public string CSOFT_CLOSING_BY { get; set; }
        public bool LINCREMENT_FLAG { get; set; }
        public bool LBY_DEPT { get; set; }
        public string CAUTO_DEPR_TYPE { get; set; }

        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public bool LGLLINK { get; set; }
        public bool LPJLINK { get; set; }
        public bool LICLINK { get; set; }
    }
}
