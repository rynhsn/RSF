using R_CommonFrontBackAPI;

namespace FAM00100Common.DTOs.FAM00100
{
    public class FAM00100SaveParameterDTO
    {
        public FAM00100DTO Entity { get; set; }
        public eCRUDMode CRUDMode { get; set; }
    }
}
