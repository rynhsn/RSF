namespace PMT03500Common.DTOs
{
    
    public class PMT03500UploadCutOffExcelDTO
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
        public string InvoicePeriod { get; set; }
        public string UtilityPeriod { get; set; }
        public string StartDate { get; set; }
        // public string EndDate { get; set; }
        public decimal BlockIStart { get; set; }
        public decimal BlockIIStart { get; set; }
        public decimal MeterStart { get; set; }
    }

    public class PMT03500UploadCutOffExcelECDTO
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
        // public string EndDate { get; set; }
        public decimal BlockIStart { get; set; }
        public decimal BlockIIStart { get; set; }
        public string Valid { get; set; }
        public string Notes { get; set; }
    }

    public class PMT03500UploadCutOffExcelWGDTO
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
        // public string EndDate { get; set; }
        public decimal MeterStart { get; set; }
        public string Valid { get; set; }
        public string Notes { get; set; }
    }

    public class PMT03500UploadCutOffRequestECDTO
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
        public decimal NBLOCK1_START { get; set; }
        public decimal NBLOCK2_START { get; set; }
    }
    
    public class PMT03500UploadCutOffRequestWGDTO
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
        public decimal NMETER_START { get; set; }
    }

    public class PMT03500UploadCutOffErrorValidateDTO
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
        public string CINV_PRD { get; set; } = "";
        public string CUTILITY_PRD { get; set; }
        public string CSTART_DATE { get; set; } = "";
        public string CEND_DATE { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CMETER_NO { get; set; } 
        public string CSEQ_NO { get; set; } = "";
        
        #region EC

        public decimal NBLOCK1_START { get; set; }
        public decimal NBLOCK2_START { get; set; }

        #endregion

        #region WG

        public decimal NMETER_START { get; set; }

        #endregion

        public string ErrorMessage { get; set; }
        public string ErrorFlag { get; set; }
    }
    
    public class PMT03500UtilityExcelECDTO
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
        public string InvoicePeriod { get; set; }
        public string UtilityPeriod { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal BlockIStart { get; set; }
        public decimal BlockIIStart { get; set; }
        public decimal BlockIEnd { get; set; }
        public decimal BlockIIEnd { get; set; }
        public decimal BebanBersama { get; set; }
    }
    
    public class PMT03500UtilityExcelWGDTO
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
        public string InvoicePeriod { get; set; }
        public string UtilityPeriod { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal MeterStart { get; set; }
        public decimal MeterEnd { get; set; }
    }
    
    public class PMT03500CutOffExcelECDTO
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
        public string InvoicePeriod { get; set; }
        public string UtilityPeriod { get; set; }
        public string StartDate { get; set; }
        // public string EndDate { get; set; }
        public decimal BlockIStart { get; set; }
        public decimal BlockIIStart { get; set; }
    }
    
    public class PMT03500CutOffExcelWGDTO
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
        public string InvoicePeriod { get; set; }
        public string UtilityPeriod { get; set; }
        public string StartDate { get; set; }
        // public string EndDate { get; set; }
        public decimal MeterStart { get; set; }
    }
}