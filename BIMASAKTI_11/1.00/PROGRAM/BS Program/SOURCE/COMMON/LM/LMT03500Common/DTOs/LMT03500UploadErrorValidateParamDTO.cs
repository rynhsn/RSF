namespace LMT03500Common.DTOs
{
    public class LMT03500UploadDTO
    {
        //result
        public int NO { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CCASHFLOW_GROUP_CODE { get; set; }
        public string CCASHFLOW_GROUP_NAME { get; set; }
        public string CSEQ { get; set; }
        public string CCASHFLOW_CODE { get; set; }
        public string CCASH_FLOW_NAME { get; set; }
        public string CCASHFLOW_TYPE { get; set; }  
        public string ErrorMessage { get; set; }
        public bool Var_Exists { get; set; }
        public bool Var_Overwrite { get; set; }
    }

    public class LMT03500UploadExcelECDTO
    {
        public string DisplaySeq { get; set; }
        
        public string BuildingId { get; set; }
        public string Department { get; set; }
        public string AgreementNo { get; set; }
        public string UtilityType { get; set; }
        public string FloorId { get; set; }
        public string UnitId { get; set; }
        public string MeterNo { get; set; }
        public string UtilityPeriod { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int BlockIStart { get; set; }
        public int BlockIIStart { get; set; }
        public int BlockIEnd { get; set; }
        public int BlockIIEnd { get; set; }
        
        public string Notes { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class LMT03500UploadRequestDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CUTILITY_TYPE { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CUNIT_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_TYPE_DESCRS { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public string CSEQ_NO { get; set; }
        public string CINV_PRD { get; set; }
        public string CUTILITY_PRD { get; set; }
        public string CSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }

        public string CMETER_NO { get; set; }

        #region EC

        public int IBLOCK1_START { get; set; }
        public int IBLOCK2_START { get; set; }
        public int IBLOCK1_END { get; set; }
        public int IBLOCK2_END { get; set; }
        
        // public int IBEBAN_BERSAMA { get; set; }
        // public int IBLOCK1_USAGE { get; set; }
        // public int IBLOCK2_USAGE { get; set; }
        // public int IAVG_BLOCK1_USAGE { get; set; }
        // public int IAVG_BLOCK2_USAGE { get; set; }

        #endregion

        #region WG

        public int IMETER_START { get; set; }
        public int IMETER_END { get; set; }
        // public int IMETER_USAGE { get; set; }
        // public int IAVG_METER_USAGE { get; set; }
        // public int ITOTAL_AMOUNT { get; set; }
        // public int CSTATUS { get; set; }

        #endregion
    }

    public class LMT03500UploadErrorValidateDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CUTILITY_TYPE { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CUNIT_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_TYPE_DESCRS { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public string CSEQ_NO { get; set; }
        public string CINV_PRD { get; set; }
        public string CUTILITY_PRD { get; set; }
        public string CSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }

        public string CMETER_NO { get; set; }

        #region EC

        public int IBLOCK1_START { get; set; }
        public int IBLOCK2_START { get; set; }
        public int IBLOCK1_END { get; set; }
        public int IBLOCK2_END { get; set; }
        
        // public int IBEBAN_BERSAMA { get; set; }
        // public int IBLOCK1_USAGE { get; set; }
        // public int IBLOCK2_USAGE { get; set; }
        // public int IAVG_BLOCK1_USAGE { get; set; }
        // public int IAVG_BLOCK2_USAGE { get; set; }

        #endregion

        #region WG

        public int IMETER_START { get; set; }
        public int IMETER_END { get; set; }
        // public int IMETER_USAGE { get; set; }
        // public int IAVG_METER_USAGE { get; set; }
        // public int ITOTAL_AMOUNT { get; set; }
        // public int CSTATUS { get; set; }

        #endregion

        // public string CCREATE_BY { get; set; }
        // public DateTime DCREATE_DATE { get; set; }
        // public string CUPDATE_BY { get; set; }
        // public DateTime DUPDATE_DATE { get; set; }
        
        public string ErrorMessage { get; set; }
        public bool ErrorFlag { get; set; }
        public bool Var_Exists { get; set; }
        public bool Var_Overwrite { get; set; }
    }

    public class LMT03500UploadErrorValidateParamDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CKEY_GUID { get;set;}
    }
}