namespace PMA00400Common.DTO
{
    public class PMA00400HandoverChecklistDTO
    {
        public string? CCHECKLIST_ITEM_NAME { get; set; }
        public string? CPARENT_CHECKLIST_NAME { get; set; }
        public string? CNOTES { get; set; }
        public bool LSTATUS { get; set; }
        public string CSTATUS => LSTATUS ? "OK" : "Not OK";
        public string? CCARE_REF_NO { get; set; }
        public string? CUNIT { get; set; }
        public int IBASE_QUANTITY { get; set; } = 0;
        //public string CBASE_QUANTITY => IBASE_QUANTITY == 0 ? "-" : IBASE_QUANTITY.ToString();
        // Auto-convert to string
        public int IACTUAL_QUANTITY { get; set; } = 0;
        //public string CACTUAL_QUANTITY => IACTUAL_QUANTITY == 0 ? "-" : IACTUAL_QUANTITY.ToString();
        public string? CIMAGE_STORAGE_ID_01 { get; set; }
        public string? CIMAGE_STORAGE_ID_02 { get; set; }
        public string? CIMAGE_STORAGE_ID_03 { get; set; }
    }
}
