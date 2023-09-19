using System;
using System.Collections.Generic;
using R_APICommonDTO;

namespace GLM00500Common.DTOs
{
    public class GLM00500UploadFromFileDTO
    {
        public string Budget_Year { get; set; }
        public string Budget_No { get; set; }
        public string Budget_Name { get; set; }
        public string Currency_Type { get; set; }
        public string Account_Type { get; set; }
        public string Account_No { get; set; }
        public string Center { get; set; }
        public decimal Period_1 { get; set; }
        public decimal Period_2 { get; set; }
        public decimal Period_3 { get; set; }
        public decimal Period_4 { get; set; }
        public decimal Period_5 { get; set; }
        public decimal Period_6 { get; set; }
        public decimal Period_7 { get; set; }
        public decimal Period_8 { get; set; }
        public decimal Period_9 { get; set; }
        public decimal Period_10 { get; set; }
        public decimal Period_11 { get; set; }
        public decimal Period_12 { get; set; }
        public decimal Period_13 { get; set; }
        public decimal Period_14 { get; set; }
        public decimal Period_15 { get; set; }
    }

    public class GLM00500UploadCheckErrorDTO
    {
        public int IERROR_COUNT { get; set; }
    }
    
    public class GLM00500UploadParameterGetErrorDTO
    {
        public string CREC_ID { get; set; }
    }

    public class GLM00500UploadErrorReturnDTO : R_APIResultBaseDTO
    {
        public List<GLM00500UploadForSystemDTO> UploadList { get; set; }
        public List<GLM00500UploadErrorDTO> ErrorList { get; set; }
    }

    public class GLM00500UploadErrorDTO
    {
        public Int64 INO { get; set; }
        public string CERROR_ID { get; set; }
        public string CERROR_TYPE { get; set; }
        public string CERROR_MSG { get; set; }
    }

    public class GLM00500UploadToSystemDTO
    {
        public int SEQ_NO { get; set; }
        public string BUDGET_YEAR { get; set; }
        public string BUDGET_NO { get; set; }
        public string BUDGET_NAME { get; set; }
        public string CURRENCY_TYPE { get; set; }
        public string ACCOUNT_TYPE { get; set; }
        public string ACCOUNT_NO { get; set; }
        public string CENTER { get; set; }
        public decimal PERIOD_1 { get; set; }
        public decimal PERIOD_2 { get; set; }
        public decimal PERIOD_3 { get; set; }
        public decimal PERIOD_4 { get; set; }
        public decimal PERIOD_5 { get; set; }
        public decimal PERIOD_6 { get; set; }
        public decimal PERIOD_7 { get; set; }
        public decimal PERIOD_8 { get; set; }
        public decimal PERIOD_9 { get; set; }
        public decimal PERIOD_10 { get; set; }
        public decimal PERIOD_11 { get; set; }
        public decimal PERIOD_12 { get; set; }
        public decimal PERIOD_13 { get; set; }
        public decimal PERIOD_14 { get; set; }
        public decimal PERIOD_15 { get; set; }
    }
    
    public class GLM00500UploadForSystemDTO
    {
        // public string CPROCESS_ID { get; set; }
        public string CREC_ID { get; set; }
        public int SEQ_NO { get; set; }
        public string BUDGET_YEAR { get; set; }
        public string BUDGET_NO { get; set; }
        public string BUDGET_NAME { get; set; }
        public string CURRENCY_TYPE { get; set; }
        public string ACCOUNT_TYPE { get; set; }
        public string ACCOUNT_NO { get; set; }
        public string CENTER { get; set; }
        public decimal PERIOD_1 { get; set; }
        public decimal PERIOD_2 { get; set; }
        public decimal PERIOD_3 { get; set; }
        public decimal PERIOD_4 { get; set; }
        public decimal PERIOD_5 { get; set; }
        public decimal PERIOD_6 { get; set; }
        public decimal PERIOD_7 { get; set; }
        public decimal PERIOD_8 { get; set; }
        public decimal PERIOD_9 { get; set; }
        public decimal PERIOD_10 { get; set; }
        public decimal PERIOD_11 { get; set; }
        public decimal PERIOD_12 { get; set; }
        public decimal PERIOD_13 { get; set; }
        public decimal PERIOD_14 { get; set; }
        public decimal PERIOD_15 { get; set; }
        public string VALID { get; set; }
        public string NOTES { get; set; }
    }
}