namespace PMT04200Common.DTOs;

public class PMT04200UpdateStatusDTO
{
    public string CREC_ID { get; set; }
    public string CNEW_STATUS { get; set; }
    public bool LAUTO_COMMIT { get; set; }
    public bool LUNDO_COMMIT { get; set; }
}