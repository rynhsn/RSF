using R_CommonFrontBackAPI;

namespace FAM00200Common.DTOs
{
    public class FAM00200SaveParameterDTO
    {
        public FAM00200DTO Entity { get; set; }
        public eCRUDMode CRUDMode { get; set; }
    }
}
