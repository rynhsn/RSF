namespace PMR02600Common.Print
{
    public class PMR02600PrintBaseHeaderLogoDTO
    {
        public string? CCOMPANY_NAME { get; set; }
        public string CDATETIME_NOW { get; set; }
        public byte[] BLOGO { get; set; }
        public string CSTORAGE_ID { get; set; } = "";
    }
}