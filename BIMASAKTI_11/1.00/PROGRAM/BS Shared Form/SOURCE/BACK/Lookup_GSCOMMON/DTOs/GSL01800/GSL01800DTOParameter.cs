namespace Lookup_GSCOMMON.DTOs
{
    public class GSL01800DTOParameter
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CCATEGORY_TYPE { get; set; } = "";

        // Add CR 7-Oct-2025
        public string CPROPERTY_ID { get; set; } = "";
        public string CPARENT_ID { get; set; } = "";
        public bool LCHILD_ONLY { get; set; }

        public string CSEARCH_TEXT { get; set; } = "";

    }
}
