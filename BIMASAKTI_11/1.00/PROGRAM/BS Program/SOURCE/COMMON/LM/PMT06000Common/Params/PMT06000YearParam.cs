using PMT06000Common.DTOs;

namespace PMT06000Common.Params
{
    public class PMT06000YearParam
    {
        public string CYEAR { get; set; } = "";
    }
    
    

    public class PMT06000ProcessSubmitParam
    {
     public string CPROPERTY_ID { get; set; } = "";
     public string CREC_ID { get; set; } = "";
     public string CNEW_STATUS { get; set; } = "";
    }

    public class PMT06000UnitParam
    {
        public PMT06000OvtDTO OVT { get; set; }
        public PMT06000OvtServiceDTO SVC { get; set; }
    }
}