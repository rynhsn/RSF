namespace PMM01000COMMON
{
    public class PMM01040ResultPrintDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public PMM01040DTO HeaderData { get; set; }
    }

    public class PMM01040ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public PMM01040ResultPrintDTO RateGU { get; set; }
        public PMM01040ColumnPrintDTO Column { get; set; }
    }
}
