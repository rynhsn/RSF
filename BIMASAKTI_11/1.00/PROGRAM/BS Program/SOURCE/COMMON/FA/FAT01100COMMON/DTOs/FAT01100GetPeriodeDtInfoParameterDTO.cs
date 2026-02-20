namespace FAT01100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT01100GetPeriodeDtInfo method (RSP_GS_GET_PERIOD_DT_INFO)
    /// </summary>
    public class FAT01100GetPeriodeDtInfoParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CYEAR { get; set; } = string.Empty;
        public string CPERIOD_NO { get; set; } = string.Empty;
    }
}
