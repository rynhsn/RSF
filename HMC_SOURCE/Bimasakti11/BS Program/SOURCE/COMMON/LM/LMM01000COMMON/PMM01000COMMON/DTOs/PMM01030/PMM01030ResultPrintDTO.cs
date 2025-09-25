namespace PMM01000COMMON
{
    public class PMM01030ResultPrintDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public PMM01030DTO HeaderData { get; set; }
    }

    public class PMM01030ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public PMM01030ResultPrintDTO RatePG { get; set; }
        public PMM01030ColumnPrintDTO Column { get; set; }
    }
}
