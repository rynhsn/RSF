using System.Collections.Generic;

namespace GLR00300Common.GLR00300Print
{

    public class GLR00300HeaderAccountTrialBalanceDTO
    {
        //HEADER TO Fast Report
        public string CPERIOD { get; set; }
        public string CFROM_ACCOUNT_NO { get; set; }
        public string CTO_ACCOUNT_NO { get; set; }
        public string CFROM_CENTER_CODE { get; set; }
        public string CTO_CENTER_CODE { get; set; }
        public string CTB_TYPE_NAME { get; set; }
        public string CCURRENCY { get; set; }
        public string CJOURNAL_ADJ_MODE_NAME { get; set; }
        public string CPRINT_METHOD_NAME { get; set; }
        public string CBUDGET_NO { get; set; }
        public byte[]? CLOGO { get; set; }
        public string? CCOMPANY_NAME { get; set; }
        public string? CDATETIME_NOW { get; set; }
    }

    #region Format A to Format D

    public class GLR00300DataAccountTrialBalanceAD
    {
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CDBCR { get; set; }
        public string CBSIS { get; set; }
        public string CCENTER { get; set; }
        public decimal NBEGIN_BALANCE { get; set; }
        public decimal NCREDIT { get; set; }
        public decimal NDEBIT { get; set; }
        public decimal NDEBIT_ADJ { get; set; }
        public decimal NCREDIT_ADJ { get; set; }
        public decimal NEND_BALANCE { get; set; }
        public decimal NBUDGET { get; set; }
    }


    #endregion

    #region Format E to Format H
    public class GLRR00300DataAccountTrialBalance
    {
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CDBCR { get; set; }
        public string CBSIS { get; set; }
        public List<GLRR00300DataDetailAccountTrialBalance> DataDetail { get; set; }
    }
    public class GLRR00300DataDetailAccountTrialBalance
    {
        public string CCENTER { get; set; }
        public decimal NBEGIN_BALANCE { get; set; }
        public decimal NCREDIT { get; set; }
        public decimal NDEBIT { get; set; }
        public decimal NDEBIT_ADJ { get; set; }
        public decimal NCREDIT_ADJ { get; set; }
        public decimal NEND_BALANCE { get; set; }
        public decimal NBUDGET { get; set; }
    }
    #endregion


    //RAW DATA WITH HEADER FROM DB
    public class GLR00300_DataDetail_AccountTrialBalance
    {

        //HEADERFROM DB
        public string CPERIOD_NAME { get; set; }
        public string CFROM_ACCOUNT_NO { get; set; }
        public string CTO_ACCOUNT_NO { get; set; }
        public string CFROM_CENTER_CODE { get; set; }
        public string CTO_CENTER_CODE { get; set; }
        public string CTB_TYPE_NAME { get; set; }
        public string CCURRENCY { get; set; }
        public string CJOURNAL_ADJ_MODE_NAME { get; set; }
        public string CPRINT_METHOD_NAME { get; set; }
        public string CBUDGET_NO { get; set; }

        //DATA
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CDBCR { get; set; }
        public string CBSIS { get; set; }
        public string CCENTER { get; set; }
        public decimal NBEGIN_BALANCE { get; set; }
        public decimal NCREDIT { get; set; }
        public decimal NDEBIT { get; set; }
        public decimal NDEBIT_ADJ { get; set; }
        public decimal NCREDIT_ADJ { get; set; }
        public decimal NEND_BALANCE { get; set; }
        public decimal NBUDGET { get; set; }
    }
}