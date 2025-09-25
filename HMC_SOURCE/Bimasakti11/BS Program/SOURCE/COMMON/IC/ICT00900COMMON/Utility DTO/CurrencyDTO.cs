namespace ICT00900COMMON.Utility_DTO
{
    public class CurrencyDTO
    {
      public string? CCURRENCY_CODE { get; set; }
        public string? CCURRENCY_NAME { get; set; }
        public string CCURRENCY_DISPLAY =>
            string.IsNullOrWhiteSpace(CCURRENCY_CODE) && string.IsNullOrWhiteSpace(CCURRENCY_NAME)
                ? "-"
                : $"{CCURRENCY_CODE} - {CCURRENCY_NAME}";

    }
}
