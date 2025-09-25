namespace PMM01000COMMON
{
    public class PMM01010ResultPrintDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public PMM01010DTO HeaderData { get; set; }
    }

    public class PMM01010ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public PMM01010ResultPrintDTO RateEC { get; set; }
        public PMM01010ColumnPrintDTO Column { get; set; }
    }
}
