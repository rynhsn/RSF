namespace FAT00700Common.DTOs
{
    public class GetTransactionListParameterDTO
    {
        // Required standard properties (ALWAYS include)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANGUAGE_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        // SP Parameters for RSP_FA_GET_TRANS_HD_LIST
        public string CTRANS_CODE { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CFROM_PERIOD { get; set; } = string.Empty;
        public string CTO_PERIOD { get; set; } = string.Empty;
        public string CASSET_CODE { get; set; } = string.Empty;

        // Legacy properties for backward compatibility
        public string CLANGID
        {
            get => CLANGUAGE_ID;
            set => CLANGUAGE_ID = value;
        }
        public string CPERIOD_FROM
        {
            get => CFROM_PERIOD;
            set => CFROM_PERIOD = value;
        }
        public string CPERIOD_TO
        {
            get => CTO_PERIOD;
            set => CTO_PERIOD = value;
        }
        public string CTRANSACTION_CODE
        {
            get => CTRANS_CODE;
            set => CTRANS_CODE = value;
        }
    }
}

