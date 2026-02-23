using System;

namespace FAT01100Common.DTOs
{
    /// <summary>
    /// Main entity DTO for FAT01100 - Change Asset Data Transaction
    /// Aligned with RSP_FAT01100_GET_TRANS_DETAIL result and RSP_FAT01100_SAVE_TRANS parameters.
    /// </summary>
    public class FAT01100DTO
    {
        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        // Header / Ref - GET_TRANS_DETAIL / SAVE_TRANS
        public string CREC_ID { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CDEPT_NAME { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CREF_DATE { get; set; } = string.Empty;
        public DateTime DREF_DATE { get; set; }
        public string CGL_REF_NO { get; set; } = string.Empty;
        public string CACTION { get; set; } = string.Empty;

        // Currency / rates
        public string CCURRENCY_CODE { get; set; } = string.Empty;
        public string CCURRENCY_NAME { get; set; } = string.Empty;
        public string CLOCAL_CURRENCY_CODE { get; set; } = string.Empty;
        public string CBASE_CURRENCY_CODE { get; set; } = string.Empty;
        public DateTime DINSERVICE_DATE { get; set; }
        public decimal NLBASE_RATE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }

        // Transaction description / status
        public string CTRANS_DESC { get; set; } = string.Empty;
        public string CTRANS_STATUS { get; set; } = string.Empty;
        public string CTRANS_STATUS_NAME { get; set; } = string.Empty;

        // Asset - GET_TRANS_DETAIL / SAVE_TRANS
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CASSET_NAME { get; set; } = string.Empty;
        public string CASSET_TRANS_SEQ_NO { get; set; } = string.Empty;
        public int IQTY { get; set; }
        public string CUNIT { get; set; } = string.Empty;

        // SAVE_TRANS - Change Asset Data (old/new pairs)
        public string CASSET_NAME_OLD { get; set; } = string.Empty;
        public string CASSET_DEPT_CODE_OLD { get; set; } = string.Empty;
        public string CASSET_DEPT_NAME_OLD { get; set; } = string.Empty;
        public string CASSET_DEPT_CODE { get; set; } = string.Empty;
        public string CASSET_DEPT_NAME { get; set; } = string.Empty;
        public string CJRNGRP_CODE_OLD { get; set; } = string.Empty;
        public string CJRNGRP_NAME_OLD { get; set; } = string.Empty;
        public string CJRNGRP_CODE { get; set; } = string.Empty;
        public string CJRNGRP_NAME { get; set; } = string.Empty;
        public string CCATEGORY_ID_OLD { get; set; } = string.Empty;
        public string CCATEGORY_NAME_OLD { get; set; } = string.Empty;
        public string CCATEGORY_ID { get; set; } = string.Empty;
        public string CCATEGORY_NAME { get; set; } = string.Empty;
        public string CTAX_CATEGORY_ID_OLD { get; set; } = string.Empty;
        public string CTAX_CATEGORY_NAME_OLD { get; set; } = string.Empty;
        public string CTAX_CATEGORY_ID { get; set; } = string.Empty;
        public string CTAX_CATEGORY_NAME { get; set; } = string.Empty;
        public int IQTY_OLD { get; set; }
        public string CUNIT_OLD { get; set; } = string.Empty;
        public string CASSET_OWNER_OLD { get; set; } = string.Empty;
        public string CASSET_OWNER { get; set; } = string.Empty;
        public string CSERIAL_NO_OLD { get; set; } = string.Empty;
        public string CSERIAL_NO { get; set; } = string.Empty;
        public string CASSET_DESC_OLD { get; set; } = string.Empty;
        public string CASSET_DESC { get; set; } = string.Empty;
        public string CSTORAGE_ID_OLD { get; set; } = string.Empty;
        public string CSTORAGE_NAME_OLD { get; set; } = string.Empty;
        public string CSTORAGE_ID { get; set; } = string.Empty;
        public string CSTORAGE_NAME { get; set; } = string.Empty;
        public string CPROPERTY_ID { get; set; } = string.Empty;
        public string CPROPERTY_NAME { get; set; } = string.Empty;
        public string CBUILDING_ID { get; set; } = string.Empty;
        public string CBUILDING_NAME { get; set; } = string.Empty;
        public string CFLOOR_ID { get; set; } = string.Empty;
        public string CFLOOR_NAME { get; set; } = string.Empty;
        public byte[] OASSET_IMAGE_OLD { get; set; } = Array.Empty<byte>();
        public byte[] OASSET_IMAGE { get; set; } = Array.Empty<byte>();
        public string CPROPERTY_ID_OLD { get; set; } = string.Empty;
        public string CPROPERTY_NAME_OLD { get; set; } = string.Empty;
        public string CBUILDING_ID_OLD { get; set; } = string.Empty;
        public string CBUILDING_NAME_OLD { get; set; } = string.Empty;
        public string CFLOOR_ID_OLD { get; set; } = string.Empty;
        public string CFLOOR_NAME_OLD { get; set; } = string.Empty;
        public string CDEPR_METHOD_OLD { get; set; } = string.Empty;
        public string CDEPR_METHOD { get; set; } = string.Empty;
        public string CSTART_DATE_OLD { get; set; } = string.Empty;
        public string CSTART_DATE { get; set; } = string.Empty;
        public DateTime DSTART_DATE_OLD { get; set; }
        public DateTime DSTART_DATE { get; set; }
        public int IUSEFUL_LIFE_YY_OLD { get; set; }
        public int IUSEFUL_LIFE_MM_OLD { get; set; }
        public int IUSEFUL_LIFE_YY { get; set; }
        public int IUSEFUL_LIFE_MM { get; set; }
        public decimal NYEAR_DEPR_PCT_OLD { get; set; }
        public decimal NYEAR_DEPR_PCT { get; set; }
        public decimal NBOOK_VALUE { get; set; }
        public decimal NLBOOK_VALUE { get; set; }
        public decimal NBBOOK_VALUE { get; set; }
        public decimal NBOOK_VALUE_OLD { get; set; }
        public decimal NLBOOK_VALUE_OLD { get; set; }
        public decimal NBBOOK_VALUE_OLD { get; set; }
        public decimal NRESIDUAL_VALUE_OLD { get; set; }
        public decimal NLRESIDUAL_VALUE_OLD { get; set; }
        public decimal NBRESIDUAL_VALUE_OLD { get; set; }
        public decimal NYEAR_DEPR_OLD { get; set; }
        public decimal NLYEAR_DEPR_OLD { get; set; }
        public decimal NBYEAR_DEPR_OLD { get; set; }
        public decimal NRESIDUAL_VALUE { get; set; }
        public decimal NLRESIDUAL_VALUE { get; set; }
        public decimal NBRESIDUAL_VALUE { get; set; }
        public decimal NYEAR_DEPR { get; set; }
        public decimal NLYEAR_DEPR { get; set; }
        public decimal NBYEAR_DEPR { get; set; }

        public string CLOCATION_ID { get; set; } = string.Empty;
        public string CLOCATION_NAME { get; set; } = string.Empty;

        public string CLOCATION_ID_OLD { get; set; } = string.Empty;
        public string CLOCATION_NAME_OLD { get; set; } = string.Empty;
        // Doc
        public string CDOC_NO { get; set; } = string.Empty;
        public string CDOC_DATE { get; set; } = string.Empty;
        public string CSOURCE_MODULE { get; set; } = string.Empty;

        // Audit
        public string CCREATE_BY { get; set; } = string.Empty;
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = string.Empty;
        public DateTime DUPDATE_DATE { get; set; }

        // addtional
        public string CINSERVICE_DATE { get; set; } = string.Empty;
        public string CASSET_UNIT_OLD { get; set; } = string.Empty;
        public string CASSET_UNIT { get; set; } = string.Empty;
        public int IREMAINING_LIFE_YY { get; set; }
        public int IREMAINING_LIFE_MM { get; set; }
    }
}
