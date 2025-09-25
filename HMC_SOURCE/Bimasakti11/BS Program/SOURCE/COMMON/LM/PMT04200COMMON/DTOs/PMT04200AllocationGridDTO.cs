namespace PMT04200Common.DTOs;

public class PMT04200AllocationGridDTO
{
    public string CALLOC_DATE { get; set; }
    public string CTRANS_NAME { get; set; }
    public string CREF_NO { get; set; }
    public string CCURRENCY_CODE { get; set; }
    public decimal NTRANS_AMOUNT { get; set; }
    public decimal NLFOREX_GAINLOSS { get; set; }
    public decimal NBFOREX_GAINLOSS { get; set; }
    public string CTRANS_DESC { get; set; }
}