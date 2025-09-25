using System;
using System.Collections.Generic;
using System.Text;

namespace PMT04200Common.DTOs
{
    public class PMT04200CBSystemParamDTO
    {
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CRATETYPE_CODE { get; set; }
        public string CRATETYPE_DESCRIPTION { get; set; }
        public string CCB_LINK_DATE { get; set; }
        public string LALLOW_EDIT_CB_LINK_DATE { get; set; }
        public string LINPUT_CHEQUE_DATE { get; set; }
        public string CBANK_IN_MODE { get; set; }
        public string CBANK_IN_MODE_NAME { get; set; }
        public string LALLOW_CANCEL_SOFT_END { get; set; }
        public string CPRD_END_CLOSING_BY { get; set; }
        public string CCONTRA_ACCOUNT_NO { get; set; }
        public string CCONTRA_ACCOUNT_NAME { get; set; }
        public string CCRDVG_ACCOUNT_NAME { get; set; }
        public string CCRDVL_ACCOUNT_NO { get; set; }
        public string CCRDVL_ACCOUNT_NAME { get; set; }
        public string CSOFT_PERIOD { get; set; }
        public string CSOFT_PERIOD_YY { get; set; }
        public string CSOFT_PERIOD_MM { get; set; }
        public string CLSOFT_END_BY { get; set; }
        public string DLSOFT_END_DATE { get; set; }
        public string CCURRENT_PERIOD { get; set; }
        public string CCURRENT_PERIOD_YY { get; set; }
        public string CCURRENT_PERIOD_MM { get; set; }
        public bool LCB_NUMBERING { get; set; }
        public bool LPRD_END_FLAG { get; set; }
        public string CLPRD_END_BY { get; set; }
        public DateTime DLPRD_END_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
}
