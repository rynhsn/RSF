namespace HDA00100Console.MulltiTenant
{
    public class MultiTenantDbDTO
    {
        public string CTENANT_ID { get; set; } = string.Empty;
        public string CCONNECTIONSTRING { get; set; } = string.Empty;
        public string CPROVIDER_NAME { get; set; } = string.Empty;
        public string CKEYWORD_FOR_PASSWORD { get; set; } = string.Empty;
    }
}
