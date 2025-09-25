namespace ICI00200Model.DTOs
{
    public class ICI00200PopupDeptWareParamDTO
    {
        public string CCATEGORY_ID { get; set; }
        public string CPRODUCT_ID { get; set; }
        public string CPRODUCT_NAME { get; set; }
    }
    
    public enum eICI00200PopupType
    {
        DEPT,
        WARE
    }
}