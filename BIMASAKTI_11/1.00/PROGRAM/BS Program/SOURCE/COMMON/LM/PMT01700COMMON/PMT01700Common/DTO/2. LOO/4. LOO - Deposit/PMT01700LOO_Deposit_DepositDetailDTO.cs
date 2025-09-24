namespace PMT01700COMMON.DTO._2._LOO._4._LOO___Deposit
{
    public class PMT01700LOO_Deposit_DepositDetailDTO : PMT01700LOO_Deposit_DepositListDTO
    {
        public string? CBUILDING_ID { get; set; }
        //For Db Parameter
        public string? CCOMPANY_ID { get; set; }
        public string? CCURRENCY_NAME { get; set; }
        public string? CDESCRIPTION { get; set; } = "";
    }
}
