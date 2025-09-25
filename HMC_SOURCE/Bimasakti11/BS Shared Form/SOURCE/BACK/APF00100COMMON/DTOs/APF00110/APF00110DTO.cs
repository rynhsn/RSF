using System;
using System.Collections.Generic;
using System.Text;

namespace APF00100COMMON.DTOs.APF00110
{
    public class APF00110DTO
    {
        //Hidden
        public string CREC_ID { get; set; } = "";
        public string CTARGET_REC_ID { get; set; } = "";
        public bool LSINGLE_CURRENCY { get; set; } = false;
        public string CGL_REF_NO { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";


        public string CTRANSACTION_NAME { get; set; } = "";
        public string CTARGET_TRANS_CODE { get; set; } = "";
        public string CTRANS_STATUS { get; set; } = "";
        public string CTRANS_STATUS_NAME { get; set; } = "";
        public string CTARGET_REF_NO { get; set; } = "";
        public string CTARGET_REF_DATE { get; set; } = "";
        public DateTime? DTARGET_REF_DATE { get; set; }
        public string CTARGET_DEPT_CODE { get; set; } = "";
        public string CTARGET_DEPT_NAME { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
        public DateTime? DREF_DATE { get; set; } 
        public string CREF_NO { get; set; } = "";

        //TARGET          

        //Amount
        public decimal NTARGET_AMOUNT { get; set; } = 0;
        public string CTARGET_CURRENCY_CODE { get; set; } = "";
        public decimal NLTARGET_AMOUNT { get; set; } = 0;
        public string CLOCAL_CURRENCY_CODE { get; set; } = "";
        public decimal NBTARGET_AMOUNT { get; set; } = 0;
        public string CBASE_CURRENCY_CODE { get; set; } = "";

        //Remaining Amount
        public decimal NTARGET_REMAINING { get; set; } = 0;
        public decimal NLTARGET_REMAINING { get; set; } = 0;
        public decimal NBTARGET_REMAINING { get; set; } = 0;

        //Tax Amount
        public decimal NTARGET_TAX_AMOUNT { get; set; } = 0;
        public decimal NLTARGET_TAX_AMOUNT { get; set; } = 0;
        public decimal NBTARGET_TAX_AMOUNT { get; set; } = 0;

        //Remaining Tax Amount
        public decimal NTARGET_TAX_REMAINING { get; set; } = 0;
        public decimal NLTARGET_TAX_REMAINING { get; set; } = 0;
        public decimal NBTARGET_TAX_REMAINING { get; set; } = 0;

        //Discount
        public decimal NTARGET_DISC_AMOUNT { get; set; } = 0;
        public decimal NLTARGET_DISC_AMOUNT { get; set; } = 0;
        public decimal NBTARGET_DISC_AMOUNT { get; set; } = 0;

        //Total Remaining
        public decimal NTARGET_TOTAL_REMAINING { get; set; } = 0;
        public decimal NLTARGET_TOTAL_REMAINING { get; set; } = 0;
        public decimal NBTARGET_TOTAL_REMAINING { get; set; } = 0;

        //Local Currency
        public decimal NLTARGET_BASE_RATE { get; set; } = 0;
        public decimal NLTARGET_CURRENCY_RATE { get; set; } = 0;

        //Base Currency
        public decimal NBTARGET_BASE_RATE { get; set; } = 0;
        public decimal NBTARGET_CURRENCY_RATE { get; set; } = 0;

        //Tax Rate
        public decimal NTARGET_TAX_BASE_RATE { get; set; } = 0;
        public decimal NTARGET_TAX_CURRENCY_RATE { get; set; } = 0;

        //Total Allocation Amount
        public decimal NTARGET_ALLOCATION { get; set; } = 0;

        //CALLER

        //Amount
        public decimal NCALLER_AMOUNT { get; set; } = 0;
        public string CCALLER_CURRENCY_CODE { get; set; } = "";
        public decimal NLCALLER_AMOUNT { get; set; } = 0;
        public decimal NBCALLER_AMOUNT { get; set; } = 0;

        //Remaining Amount
        public decimal NCALLER_REMAINING { get; set; } = 0;
        public decimal NLCALLER_REMAINING { get; set; } = 0;
        public decimal NBCALLER_REMAINING { get; set; } = 0;

        //Tax Amount
        public decimal NCALLER_TAX_AMOUNT { get; set; } = 0;
        public decimal NLCALLER_TAX_AMOUNT { get; set; } = 0;
        public decimal NBCALLER_TAX_AMOUNT { get; set; } = 0;

        //Remaining Tax Amount
        public decimal NCALLER_TAX_REMAINING { get; set; } = 0;
        public decimal NLCALLER_TAX_REMAINING { get; set; } = 0;
        public decimal NBCALLER_TAX_REMAINING { get; set; } = 0;

        //Discount
        public decimal NCALLER_DISC_AMOUNT { get; set; } = 0;
        public decimal NLCALLER_DISC_AMOUNT { get; set; } = 0;
        public decimal NBCALLER_DISC_AMOUNT { get; set; } = 0;

        //Total Remaining
        public decimal NCALLER_TOTAL_REMAINING { get; set; } = 0;
        public decimal NLCALLER_TOTAL_REMAINING { get; set; } = 0;
        public decimal NBCALLER_TOTAL_REMAINING { get; set; } = 0;

        //Local Currency
        public decimal NLCALLER_BASE_RATE { get; set; } = 0;
        public decimal NLCALLER_CURRENCY_RATE { get; set; } = 0;

        //Base Currency
        public decimal NBCALLER_BASE_RATE { get; set; } = 0;
        public decimal NBCALLER_CURRENCY_RATE { get; set; } = 0;

        //Tax Rate
        public decimal NCALLER_TAX_BASE_RATE { get; set; } = 0;
        public decimal NCALLER_TAX_CURRENCY_RATE { get; set; } = 0; //cek dulu di sql

        //Total From Amount
        public decimal NCALLER_ALLOCATION { get; set; } = 0;

        //Foreign Gain (Loss)
        public decimal NLFOREX_GAINLOSS { get; set; } = 0;
        public decimal NBFOREX_GAINLOSS { get; set; } = 0;


        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
    }
}
