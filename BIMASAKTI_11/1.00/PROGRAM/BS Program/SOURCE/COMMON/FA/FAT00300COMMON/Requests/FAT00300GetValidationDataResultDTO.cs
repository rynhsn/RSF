namespace FAT00300Common.Requests
{
    public class FAT00300GetValidationDataResultDTO
    {
        public decimal NLBOOKVAL { get; set; }
        public decimal NLRESIDUAL { get; set; }
        public int IQTY { get; set; }
        public string CLAST_TRANS_DATE { get; set; } = string.Empty;
        public string CNEXT_DEPR_PERIOD { get; set; } = string.Empty;
        public string CASSET_STATUS { get; set; } = string.Empty;
        public string CDEPR_METHOD { get; set; } = string.Empty;
    }
}







