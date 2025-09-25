namespace PMA00400Common.DTO
{
    public class PMA00400UtilityDTO
    {
        public string? CUNIT_ID { get; set; }
        public string? CUTILITY_TYPE { get; set; }
        public string? CMETER_NO { get; set; }
        public decimal NCAPACITY { get; set; }
        public decimal NCALCULATION_FACTOR { get; set; }
        public string? CCHARGES_ID { get; set; }
        public string? CCHARGES_NAME { get; set; }
        public string? CSTART_INV_PRD { get; set; }
        public string? CSTART_INV_PRD_CONVERT { get; set; }
        public decimal NMETER_START { get; set; }
        //public string CMETER_START => NMETER_START == 0 ? "-" :  NMETER_START.ToString("F2");
        public decimal NBLOCK1_START { get; set; }
        //public string CBLOCK1_START => NBLOCK1_START == 0 ? "-" :  NBLOCK1_START.ToString("F2");
        public decimal NBLOCK2_START { get; set; }
        //public string CBLOCK2_START => NBLOCK2_START == 0 ? "-" : NBLOCK2_START.ToString("F2");
        public string? CFLOOR_ID { get; set; }
        public string? CCHARGES_TYPE { get; set; }
        public string? CSEQ_NO { get; set; }
    }
}
