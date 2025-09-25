using R_APICommonDTO;
using System;

namespace PMM01500COMMON
{
    public class PMM01500DTO
    {
        private int _CSCSEQUENCEInt;
        // parameter
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CINVGRP_CODE { get; set; }
        public string CUSER_ID { get; set; }
        public string CACTION { get; set; }
        public bool LTabEnalbleDept { get; set; }
        public bool DeleteAllTabDept { get; set; } = false;

        // + result
        public string CINVGRP_NAME { get; set; }
        public bool LACTIVE { get; set; } = true;
        public string CSEQUENCE { get; set; }

        public int CSEQUENCEInt { get; set; }

        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileNameExtension { get; set; }
        public Byte[] Data { get; set; }
        public string CINVOICE_TEMPLATE { get; set; } = "";
        public string CSTORAGE_ID { get; set; } = "";


        public string CSTAMP_CODE { get; set; } = "";
        public string CINVOICE_GROUP_MODE { get; set; } = "";
        public string CINVOICE_DUE_MODE { get; set; } = "01";
        public int IDUE_DAYS { get; set; }
        public int IFIXED_DUE_DATE { get; set; }
        public int ILIMIT_INVOICE_DATE { get; set; }
        public int IBEFORE_LIMIT_INVOICE_DATE { get; set; }
        public int IAFTER_LIMIT_INVOICE_DATE { get; set; }
        public bool LDUE_DATE_TOLERANCE_HOLIDAY { get; set; } = false;
        public bool LDUE_DATE_TOLERANCE_SATURDAY { get; set; } = false;
        public bool LDUE_DATE_TOLERANCE_SUNDAY { get; set; } = false;
        public bool LUSE_STAMP { get; set; } = false;
        public bool LGENERAL_TEMPLATE { get; set; } = false;
        public bool LTAX_EXEMPTION { get; set; } = false;
        public string CSTAMP_ADD_ID { get; set; } = "";
        public string CTAX_EXEMPTION_CODE { get; set; } = "";
        public string CTAX_ADD_DESCR { get; set; } = "";
        public string CSTAMP_ADD_NAME { get; set; }
        public string CDESCRIPTION { get; set; } = "";
        public bool LBY_DEPARTMENT { get; set; } = false;
        public string CDEPT_CODE { get; set; } = "";
        public string CINV_DEPT_CODE { get; set; } = "";
        public string CINV_DEPT_NAME { get; set; } = "";
        public string CDEPT_NAME { get; set; }
        public string CBANK_CODE { get; set; } = "";
        public string CCB_NAME { get; set; }
        public string CBANK_ACCOUNT { get; set; } = "";
        public string CACTIVE_BY { get; set; }
        public DateTime DACTIVE_DATE { get; set; }
        public string CINACTIVE_BY { get; set; }
        public DateTime DINACTIVE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
