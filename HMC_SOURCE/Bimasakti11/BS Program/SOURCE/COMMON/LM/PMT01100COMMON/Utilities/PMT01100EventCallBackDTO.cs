using PMT01100Common.Utilities.Front;

namespace PMT01100Common.Utilities
{
    public class PMT01100EventCallBackDTO
    {
        public bool LUSING_PROPERTY_ID { get; set; }
        public bool LCRUD_MODE { get; set; }
        public string? CCRUD_MODE { get; set; }
        public PMT01100ParameterFrontChangePageDTO? ODATA_PARAMETER { get; set; }
    }
}
