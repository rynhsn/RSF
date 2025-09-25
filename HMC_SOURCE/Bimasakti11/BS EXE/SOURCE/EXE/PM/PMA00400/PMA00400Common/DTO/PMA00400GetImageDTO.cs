namespace PMA00400Common.DTO
{
    public class PMA00400GetImageDTO
    {
        public byte[] OData { get; set; }
        public string? CCHARGES_TYPE_DESCRIPTION { get; set; }
        public string? CMETER_NO { get; set; }
        public string? CPARENT_CHECKLIST_ITEM_NAME { get; set; }
        public string? CCHECKLIST_ITEM_NAME { get; set; }
        public string? CIMAGE_STORAGE_ID { get; set; }
        public string? CNOTES { get; set; }
    }
}
