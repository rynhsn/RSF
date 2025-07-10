namespace PMA00400Common.DTO
{
    public class PMA00400GetImageDTO
    {
        public byte[]? OData { get; set; }
        public string CCHARGES_TYPE_DESCRIPTION { get; set; } = string.Empty;
        public string CMETER_NO { get; set; } = string.Empty;
        public string CPARENT_CHECKLIST_ITEM_NAME { get; set; } = string.Empty;
        public string CCHECKLIST_ITEM_NAME { get; set; } = string.Empty;
        public string CIMAGE_STORAGE_ID { get; set; } = string.Empty;
        public string CNOTES { get; set; } = string.Empty;
    }
}
