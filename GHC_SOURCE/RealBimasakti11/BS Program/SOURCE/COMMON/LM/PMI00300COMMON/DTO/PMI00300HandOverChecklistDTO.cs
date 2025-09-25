namespace PMI00300COMMON.DTO
{
    public class PMI00300HandOverChecklistDTO
    {
        public string CCHECKLIST_ITEM_NAME { get; set; } = string.Empty;
        public string CPARENT_CHECKLIST_NAME { get; set; } = string.Empty;
        public string CNOTES { get; set; } = string.Empty;
        public bool LSTATUS { get; set; }
        public string CSTATUS { get; set; } = string.Empty;
        public string CCARE_REF_NO { get; set; } = string.Empty;
        public string CQUANTITY_DISPLAY { get; set; } = string.Empty;
        public int IBASE_QUANTITY { get; set; }
        public int IACTUAL_QUANTITY { get; set; }
        public string CUNIT { get; set; } = string.Empty;
        public string CIMAGE_STORAGE_ID_01 { get; set; } = string.Empty;
        public string CIMAGE_STORAGE_ID_02 { get; set; } = string.Empty;
        public string CIMAGE_STORAGE_ID_03 { get; set; } = string.Empty;
        public byte[] OIMAGE_STORAGE_ID_01 { get; set; }
        public byte[] OIMAGE_STORAGE_ID_02 { get; set; }
        public byte[] OIMAGE_STORAGE_ID_03 { get; set; }
    }
}