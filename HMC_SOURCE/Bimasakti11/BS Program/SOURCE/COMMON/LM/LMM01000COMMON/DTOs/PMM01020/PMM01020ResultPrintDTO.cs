namespace PMM01000COMMON
{
    public class PMM01020ResultPrintDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public PMM01020DTO HeaderData { get; set; }
    }

    public class PMM01020ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public PMM01020ResultPrintDTO RateWG { get; set; }
        public PMM01020ColumnPrintDTO Column { get; set; }
    }
}
