namespace LMM02500Back.DTO
{
    public class LMM02500ListDbParameterDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CUSER_LOGIN_ID { get; set; }
    }
    public class SaveMoveTenantGroupParameterDbDTO
    {
        public string? CTENANT_ID { get; set; }
        public string? CLOGIN_COMPANY_ID { get; set; }
        public string? CSELECTED_PROPERTY_ID { get; set; }
        public string? CFROM_TENANT_CATEGORY_ID { get; set; }
        public string? CTO_TENANT_CATEGORY_ID { get; set; }
        public string? CLOGIN_USER_ID { get; set; }
    }
}

