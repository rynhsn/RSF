namespace PMT04200Common.DTOs;

public class PMT04200LastCurrencyRateDTO
{
    public string CCURRENCY_CODE { get; set; }
    public string CRATETYPE_CODE { get; set; }
    public string CRATE_DATE { get; set; }
    public decimal NLBASE_RATE_AMOUNT { get; set; }
    public decimal NLCURRENCY_RATE_AMOUNT { get; set; }
    public decimal NBBASE_RATE_AMOUNT { get; set; }
    public decimal NBCURRENCY_RATE_AMOUNT { get; set; }
}