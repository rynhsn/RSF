namespace Lookup_GSCOMMON.DTOs
{
    public class GSL01800DTO
    {
        public string CCATEGORY_ID { get; set; }
        public string CCATEGORY_NAME { get; set; }
        public int ILEVEL { get; set; }
        public string ILEVEL_CCATEGORY_ID_CCATEGORY_NAME_DISPLAY { get; set; }
        public string CCATEGORY_TYPE { get; set; }
        public string CCATEGORY_TYPE_DESCR { get; set; }
        public string CPARENT { get; set; }
        public string CPARENT_NAME { get; set; }
        public bool LHAS_CHILD { get; set; }
    }
}
