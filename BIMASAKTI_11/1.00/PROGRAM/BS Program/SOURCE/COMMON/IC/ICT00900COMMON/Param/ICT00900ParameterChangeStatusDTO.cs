using ICT00900COMMON.Utility_DTO;

namespace ICT00900COMMON.Param
{
    public class ICT00900ParameterChangeStatusDTO : BaseDTO
    {
        public string? CDEPT_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CSTATUS { get; set; }
        public string? CALLOC_ID { get; set; }
    }
}
