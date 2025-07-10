namespace PMT03500Common.DTOs
{
    public class PMT03500UploadUtilityExcelDTO
    {
        public string BuildingId { get; set; }
        public string Department { get; set; }
        public string AgreementNo { get; set; }
        public string UtilityType { get; set; }
        public string FloorId { get; set; }
        public string UnitId { get; set; }
        public string ChargesType { get; set; }
        public string ChargesId { get; set; }
        public string MeterNo { get; set; }
        public string SeqNo { get; set; }
        public string UtilityPeriod { get; set; }
        public string InvoicePeriod { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal BlockIStart { get; set; }
        public decimal BlockIIStart { get; set; }
        public decimal BlockIEnd { get; set; }
        public decimal BlockIIEnd { get; set; }
        public decimal BebanBersama { get; set; }
        public int MeterStart { get; set; }
        public int MeterEnd { get; set; }
    }

    public class PMT03500UploadUtilityExcelECDTO
    {
        public string DisplaySeq { get; set; }

        public string BuildingId { get; set; }
        public string Department { get; set; }
        public string AgreementNo { get; set; }
        public string UtilityType { get; set; }
        public string FloorId { get; set; }
        public string UnitId { get; set; }
        public string ChargesType { get; set; }
        public string ChargesId { get; set; }
        public string MeterNo { get; set; }
        public string SeqNo { get; set; }
        public string InvoicePeriod { get; set; }
        public string UtilityPeriod { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal BlockIStart { get; set; }
        public decimal BlockIIStart { get; set; }
        public decimal BlockIEnd { get; set; }
        public decimal BlockIIEnd { get; set; }
        public decimal BebanBersama { get; set; }
        public string Valid { get; set; }
        public string Notes { get; set; }
    }

    public class PMT03500UploadUtilityExcelWGDTO
    {
        public string DisplaySeq { get; set; }

        public string BuildingId { get; set; }
        public string Department { get; set; }
        public string AgreementNo { get; set; }
        public string UtilityType { get; set; }
        public string FloorId { get; set; }
        public string UnitId { get; set; }
        public string ChargesType { get; set; }
        public string ChargesId { get; set; }
        public string MeterNo { get; set; }
        public string SeqNo { get; set; }
        public string InvoicePeriod { get; set; }
        public string UtilityPeriod { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int MeterStart { get; set; }
        public int MeterEnd { get; set; }
        public string Valid { get; set; }
        public string Notes { get; set; }
    }

    public class PMT03500UploadUtilityRequestECDTO
    {
        public int NO { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CUTILITY_TYPE { get; set; }
        public string CUNIT_ID { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CSEQ_NO { get; set; }
        public string CINV_PRD { get; set; }
        public string CUTILITY_PRD { get; set; }
        public string CSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
        public string CMETER_NO { get; set; }
        public string CPHOTO_REC_ID_1 { get; set; } = string.Empty;
        public string CPHOTO_REC_ID_2 { get; set; } = string.Empty;
        public string CPHOTO_REC_ID_3 { get; set; } = string.Empty;
        public decimal NBLOCK1_START { get; set; }
        public decimal NBLOCK2_START { get; set; }
        public decimal NBLOCK1_END { get; set; }
        public decimal NBLOCK2_END { get; set; }
        public decimal NBEBAN_BERSAMA { get; set; }
    }

    //
    public class PMT03500UploadUtilityRequestWGDTO
    {
        public int NO { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CUTILITY_TYPE { get; set; }
        public string CUNIT_ID { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CSEQ_NO { get; set; }
        public string CINV_PRD { get; set; }
        public string CUTILITY_PRD { get; set; }
        public string CSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
        public string CMETER_NO { get; set; }
        public string CPHOTO_REC_ID_1 { get; set; } = string.Empty;
        public string CPHOTO_REC_ID_2 { get; set; } = string.Empty;
        public string CPHOTO_REC_ID_3 { get; set; } = string.Empty;
        public decimal NMETER_START { get; set; }
        public decimal NMETER_END { get; set; }
    }

    public class PMT03500UploadUtilityErrorValidateDTO
    {
        public int NO { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CUTILITY_TYPE { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CUNIT_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CINV_PRD { get; set; }
        public string CUTILITY_PRD { get; set; }
        public string CSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
        public string CTRANS_CODE { get; set; } = "";
        public string CMETER_NO { get; set; }
        public string CSEQ_NO { get; set; }

        #region EC

        public decimal NBLOCK1_START { get; set; }
        public decimal NBLOCK2_START { get; set; }
        public decimal NBLOCK1_END { get; set; }
        public decimal NBLOCK2_END { get; set; }
        public decimal NBEBAN_BERSAMA { get; set; }

        #endregion

        #region WG

        public int IMETER_START { get; set; }
        public int IMETER_END { get; set; }
        
        public decimal NMETER_START { get; set; }
        public decimal NMETER_END { get; set; }

        #endregion

        public string ErrorMessage { get; set; }
        public string ErrorFlag { get; set; }
    }

    // public class LMT03500UploadErrorValidateParamDTO
    // {
    //     public string CCOMPANY_ID { get; set; }
    //     public string CUSER_ID { get; set; }
    //     public string CKEY_GUID { get; set; }
    // }

    public class PMT03500UndoUtilityDTO
    {
        public int NO { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CUNIT_ID { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CSEQ_NO { get; set; }
        public string CINV_PRD { get; set; }
        public string CMETER_NO { get; set; }
    }
}