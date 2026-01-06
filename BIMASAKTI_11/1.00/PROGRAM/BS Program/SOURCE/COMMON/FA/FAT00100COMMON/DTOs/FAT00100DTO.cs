using System;
using System.Collections.Generic;

namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Main entity DTO for FAT00100 - Fixed Asset Transaction
    /// </summary>
    public class FAT00100DTO
    {
        // // Standard properties (ALWAYS at top)
        // public string CCOMPANY_ID { get; set; } = string.Empty;
        // public string CLANG_ID { get; set; } = string.Empty;
        // public string CUSER_ID { get; set; } = string.Empty;

        // // Filter and period properties
        // public string CFOREIGN_LANGUAGE { get; set; } = string.Empty;
        // public string CPERIODFROM { get; set; } = string.Empty;
        // public string CPERIODTO { get; set; } = string.Empty;
        // public string CSTATUSDRAFT { get; set; } = string.Empty;
        // public string CSTATUSOPEN { get; set; } = string.Empty;
        // public string CSTATUSAPPROVED { get; set; } = string.Empty;
        // public string CSTATUSCLOSED { get; set; } = string.Empty;
        // public string CFILTER_TRANS_CODE { get; set; } = string.Empty;

        // // Transaction header properties
        // public string CDEPT_CODE { get; set; } = string.Empty;
        // public string CTRANSACTION_CODE { get; set; } = string.Empty;
        // public string CREFERENCE_NO { get; set; } = string.Empty;
        // public string CTRANSACTION_DATE { get; set; } = string.Empty;
        // public DateTime? DTRANSACTION_DATE { get; set; }
        // public string CSTATUS { get; set; } = string.Empty;
        // public string CSTATUS_DESC { get; set; } = string.Empty;
        // public string CCURRENCY_CODE { get; set; } = string.Empty;
        // public decimal NLBASE_RATE_AMOUNT { get; set; }
        // public decimal NLCURRENCY_RATE_AMOUNT { get; set; }
        // public decimal NBBASE_RATE_AMOUNT { get; set; }
        // public decimal NBCURRENCY_RATE_AMOUNT { get; set; }
        // public decimal NTRANSACTION_AMOUNT { get; set; }
        // public decimal NLTRANSACTION_AMOUNT { get; set; }
        // public decimal NBTRANSACTION_AMOUNT { get; set; }
        // public string CDOCUMENT_DATE { get; set; } = string.Empty;
        // public DateTime? DDOCUMENT_DATE { get; set; }
        // public string CFR_MODULE { get; set; } = string.Empty;
        // public string CFR_DEPT_CODE { get; set; } = string.Empty;
        // public string CFR_DEPT_NAME { get; set; } = string.Empty;
        // public string CFR_TRANSACTION_CODE { get; set; } = string.Empty;
        // public string CFR_TRANSACTION_NAME { get; set; } = string.Empty;
        // public string CFR_REFERENCE_NO { get; set; } = string.Empty;
        // public string CDEPT_NAME { get; set; } = string.Empty;
        // public string CCURRENCY_NAME { get; set; } = string.Empty;
        // public string CTRANSACTION_NAME { get; set; } = string.Empty;

        // // System and configuration properties
        // public string CTRANS_DEPT_CODE { get; set; } = string.Empty;
        // public string CASSET_DEPT_CODE { get; set; } = string.Empty;
        // public bool LINCREMENT_FLAG { get; set; }
        // public bool LJRNGRP_MODE { get; set; }
        // public bool LDEPT_MODE { get; set; }
        // public string CPERIOD_MODE { get; set; } = string.Empty;
        // public string CCURRENT_PERIOD { get; set; } = string.Empty;
        // public string CSOFT_PERIOD { get; set; } = string.Empty;
        // public string CRATETYPE_CODE { get; set; } = string.Empty;
        // public string CGLLINK_DATE { get; set; } = string.Empty;
        // public string CPJLINK_DATE { get; set; } = string.Empty;

        // // Transaction period and currency properties
        // public string CTRANSACTION_PRD { get; set; } = string.Empty;
        // public string CLOCAL_CURRENCY_CODE { get; set; } = string.Empty;
        // public string CBASE_CURRENCY_CODE { get; set; } = string.Empty;
        // public bool LCUST_PERIOD_FLAG { get; set; }
        // public string CFILTER_TRANS_DESC { get; set; } = string.Empty;
        // public bool LAPPROVAL_FLAG { get; set; }

        // // Transaction description and approval properties
        // public string CPJ_TRANS_DESC { get; set; } = string.Empty;
        // public bool LCAN_APPROVE { get; set; }
        // public int ISTART_YEAR { get; set; }
        // public int IEND_YEAR { get; set; }
        // public string CPERIOD_NO { get; set; } = string.Empty;
        // public bool LCAN_CLOSE { get; set; }

        // // Transaction details
        // public string CTRANSACTION_DESCR { get; set; } = string.Empty;
        // public string CDOCUMENT_NO { get; set; } = string.Empty;
        // public bool LGLLINK { get; set; }
        // public string CGL_TRF_STATUS { get; set; } = string.Empty;
        // public string CGL_REFERENCE_NO { get; set; } = string.Empty;
        // public string CAPPROVED_BY { get; set; } = string.Empty;
        // public DateTime DAPPROVED_DATE { get; set; }
        // public string CCOMMIT_BY { get; set; } = string.Empty;
        // public DateTime DCOMMIT_DATE { get; set; }
        // public string CCANCEL_REASON_CODE { get; set; } = string.Empty;
        // public string CCANCEL_APPROVED_BY { get; set; } = string.Empty;
        // public DateTime DUPDATE_DATE { get; set; }
        // public bool LCHANGE_DESC { get; set; }

        // // Nested DTOs
        // public List<FAT00100CPDTO>? oCP { get; set; }
        // public FAT00100SuppDTO? oSupp { get; set; }
        // public string cSupplierName { get; set; } = string.Empty;
        // public string CMODE { get; set; } = string.Empty;
        // public int IRANGEYEAR { get; set; }
        // public int IRANGEPERIOD { get; set; }
        // public string CPJ_TRANS_CODE { get; set; } = string.Empty;
        // public string CDEFAULTPERIOD { get; set; } = string.Empty;

        // // Supplier properties
        // public string CSUPPLIER_ID { get; set; } = string.Empty;
        // public string CINFO_SEQNO { get; set; } = string.Empty;
        // public string CSUPPLIER_NAME { get; set; } = string.Empty;
        // public string CADDRESS { get; set; } = string.Empty;
        // public string CPOSTAL_CODE { get; set; } = string.Empty;
        // public string CCITY { get; set; } = string.Empty;
        // public string CCOUNTRY_CODE { get; set; } = string.Empty;
        // public string CSTATE_CODE { get; set; } = string.Empty;
        // public string CPHONE_1 { get; set; } = string.Empty;
        // public string CPHONE_2 { get; set; } = string.Empty;
        // public string CPHONE_3 { get; set; } = string.Empty;
        // public string CFAX_NO1 { get; set; } = string.Empty;
        // public string CFAX_NO2 { get; set; } = string.Empty;
        // public string CFAX_NO3 { get; set; } = string.Empty;
        // public string CEMAIL_1 { get; set; } = string.Empty;
        // public string CEMAIL_2 { get; set; } = string.Empty;
        // public string CEMAIL_3 { get; set; } = string.Empty;
        // public string CTAX_REG_TP { get; set; } = string.Empty;
        // public string CTAX_NAME { get; set; } = string.Empty;
        // public string CTAX_REGISTER_ID { get; set; } = string.Empty;
        // public DateTime DTAX_REGISTER_DATE { get; set; }
        // public string CTAX_BUSINESS_TYPE { get; set; } = string.Empty;
        // public string CTAX_BUSINESS_NAME { get; set; } = string.Empty;
        // public string CNPWP { get; set; } = string.Empty;
        // public string CNPKP { get; set; } = string.Empty;
        // public string CNOTES { get; set; } = string.Empty;
        // public string CCREATE_BY { get; set; } = string.Empty;
        // public DateTime DCREATE_DATE { get; set; }
        // public string CUPDATE_BY { get; set; } = string.Empty;
        // public string CRECID { get; set; } = string.Empty;

        // Stored procedure result fields (RSP_FAT00100_GET_TRANS_DETAIL)
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CDEPT_NAME { get; set; } = string.Empty;
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CGL_REF_NO { get; set; } = string.Empty;
        public string CREFERENCE_NO { get; set; } = string.Empty;
        public string CREF_DATE { get; set; } = string.Empty;
        public DateTime DREF_DATE { get; set; }
        public string CDOCUMENT_NO { get; set; } = string.Empty;
        public string CDOC_NO { get; set; } = string.Empty;
        public string CDOCUMENT_DATE { get; set; } = string.Empty;
        public string CDOC_DATE { get; set; } = string.Empty;
        public DateTime DDOC_DATE { get; set; }
        public DateTime DDOCUMENT_DATE { get; set; }
        public string CSOURCE_MODULE { get; set; } = string.Empty;
        public string CFR_DEPT_CODE { get; set; } = string.Empty;
        public string CFR_DEPT_NAME { get; set; } = string.Empty;
        public string CFR_TRANS_CODE { get; set; } = string.Empty;
        public string CFR_TRANS_NAME { get; set; } = string.Empty;
        public string CFR_REF_NO { get; set; } = string.Empty;
        public string CSUPPLIER_ID { get; set; } = string.Empty;
        public string CSUPPLIER_NAME { get; set; } = string.Empty;
        public string CSUPPLIER_ID_NAME { get; set; } = string.Empty;
        public string CSUPPLIER_SEQ_NO { get; set; } = string.Empty;
        public string CCURRENCY_CODE { get; set; } = string.Empty;
        public string CTRANS_DESC { get; set; } = string.Empty;
        public string CTRANS_STATUS { get; set; } = string.Empty;
        public string CTRANS_STATUS_NAME { get; set; } = string.Empty;
        public decimal NLBASE_RATE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }
        public int NTOTAL_AMOUNT { get; set; }
        public int NLTOTAL_AMOUNT { get; set; }
        public int NBTOTAL_AMOUNT { get; set; }
        public string CCREATE_BY { get; set; } = string.Empty;
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = string.Empty;
        public DateTime DUPDATE_DATE { get; set; }
        public string CREC_ID { get; set; } = string.Empty;

        // Filter properties for GetDataGrid (from streaming context)
        public string CTRANSACTION_CODE { get; set; } = string.Empty;
        public string CPERIODFROM { get; set; } = string.Empty;
        public string CPERIODTO { get; set; } = string.Empty;
        public string CSTATUSDRAFT { get; set; } = string.Empty;
        public string CSTATUSOPEN { get; set; } = string.Empty;
        public string CSTATUSAPPROVED { get; set; } = string.Empty;
        public string CSTATUSCLOSED { get; set; } = string.Empty;

        

        public string CLOCAL_CURRENCY_CODE { get; set; } = string.Empty;
        public string CBASE_CURRENCY_CODE { get; set; } = string.Empty;
        public string CCURRENCY_NAME { get; set; } = string.Empty;

        public string CUSER_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;



        // // Asset and approval properties
        // public bool LASSET_INCREMENT_FLAG { get; set; }
        // public bool LONETIME_FLAG { get; set; }
        // public string CAPPROVAL_CODE { get; set; } = string.Empty;
        // public byte IAPPROVAL_OPTION { get; set; }
        // public string CASSET_CODE { get; set; } = string.Empty;

        // // add from back generate
        // public string CASSET_TRANS_SEQNO { get; set; } = string.Empty;
        // public decimal NTRANSACTION_AMOUNT1 { get; set; }
        // public decimal NLTRANSACTION_AMOUNT1 { get; set; }
        // public int ITRANSACTION_QTY1 { get; set; }
        // public string CUNIT { get; set; } = string.Empty;
        // public string CASSET_DEPT_NAME { get; set; } = string.Empty;
        // public string CASSET_LOCATION { get; set; } = string.Empty;
        // public string CJRNGRP_CODE { get; set; } = string.Empty;
        // public string CJRNGRP_NAME { get; set; } = string.Empty;
        // public string CTAX_CATEGORY_CODE { get; set; } = string.Empty;
        // public string CTAX_CATEGORY_DESC { get; set; } = string.Empty;
        // public string CASSET_NAME { get; set; } = string.Empty;

        // // Book value properties
        // public decimal NOLBOOK_VALUE { get; set; }
        // public decimal NOBBOOK_VALUE { get; set; }
        // public int IOUSEFUL_LIVE { get; set; }


    }
}

