namespace PMR00170COMMON.Utility_Report
{
    public class PMR00170DBParamDTO : PMR00170ParameterVisibleDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CUSER_ID { get; set; }
        public string? CLANG_ID { get; set; }
        public string CPROPERTY_ID { get; set; } = "";
        public string CPROPERTY_NAME { get; set; } = "";


        public string CFROM_DEPARTMENT_ID { get; set; } = "";
        public string CFROM_DEPARTMENT_NAME { get; set; } = "";
        public string CTO_DEPARTMENT_ID { get; set; } = "";
        public string CTO_DEPARTMENT_NAME { get; set; } = "";
        public string CFROM_SALESMAN_ID { get; set; } = "";
        public string CFROM_SALESMAN_NAME { get; set; } = "";
        public string CTO_SALESMAN_ID { get; set; } = "";
        public string CTO_SALESMAN_NAME { get; set; } = "";
        public string CFROM_PERIOD { get; set; } = "";
        public string CTO_PERIOD { get; set; } = "";
        public string CREPORT_TYPE { get; set; } = "";
        public string CREPORT_NAME{ get; set; } = "";

        public string CREPORT_FILENAME { get; set; } = "";
        public string CREPORT_FILEEXT { get; set; } = "";
    }
}
