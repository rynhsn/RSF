namespace Lookup_GSCOMMON.DTOs
{
    public class GSL01400DTO
    {
        private string _CGLACCOUNT_NO_NAME;

        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CGLACCOUNT_NO_NAME { get => _CGLACCOUNT_NO_NAME; set => _CGLACCOUNT_NO_NAME = string.Format("{0} - {1}", CGLACCOUNT_NO, CGLACCOUNT_NAME); }
    }
}
