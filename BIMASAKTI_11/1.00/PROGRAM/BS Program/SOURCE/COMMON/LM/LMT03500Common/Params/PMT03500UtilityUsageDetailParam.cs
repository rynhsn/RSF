namespace PMT03500Common.Params
{
    public class PMT03500UtilityUsageDetailParam
    {
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
        
        
        #region STORAGE_ID
        public string CSTART_PHOTO1_STORAGE_ID { get; set; } = "";
        public string CSTART_PHOTO2_STORAGE_ID { get; set; } = "";
        public string CSTART_PHOTO3_STORAGE_ID { get; set; } = "";
        public string CEND_PHOTO1_STORAGE_ID { get; set; } = "";
        public string CEND_PHOTO2_STORAGE_ID { get; set; } = "";
        public string CEND_PHOTO3_STORAGE_ID { get; set; } = "";
        #endregion
    }
}