namespace FAT01100Common.DTOs
{
    /// <summary>
    /// Result DTO for FAT01100GetPeriodeDtInfo method (RSP_GS_GET_PERIOD_DT_INFO)
    /// </summary>
    public class FAT01100GetPeriodeDtInfoResultDTO
    {
        public string CCYEAR { get; set; } = string.Empty;
        public string CPERIOD_NO { get; set; } = string.Empty;
        public string CSTART_DATE { get; set; } = string.Empty;
        public string CEND_DATE { get; set; } = string.Empty;
    }
}
