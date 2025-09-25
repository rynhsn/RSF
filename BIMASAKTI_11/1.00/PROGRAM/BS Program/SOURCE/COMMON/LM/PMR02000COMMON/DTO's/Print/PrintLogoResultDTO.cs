namespace PMR02000COMMON.DTO_s.Print
{
    public class PrintLogoResultDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CCOMPANY_NAME { get; set; }
        public string CDATETIME_NOW { get; set; }
        public byte[] CLOGO { get; set; }
        public string CSTORAGE_ID { get; set; } = "";
    }
}