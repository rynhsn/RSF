namespace PMR00460Common.DTOs.Print
{
    public class PMR00460PrintBaseHeaderLogoDTO
    {
        public string? CCOMPANY_NAME { get; set; }
        public string CDATETIME_NOW { get; set; }
        public byte[] CLOGO { get; set; }
        public string? CSTORAGE_ID { get; set; }
    }
}