namespace PMM01500COMMON
{
    public class PMM01501DTO
    {
        private string cINVGRP_CODE_NAME;

        public string CINVGRP_CODE { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CINVGRP_NAME { get; set; }
        public string CINVGRP_CODE_NAME { get => cINVGRP_CODE_NAME; set => cINVGRP_CODE_NAME = string.Format("{0} - {1}", CINVGRP_CODE, CINVGRP_NAME); }
    }

    public class PMM01501ParameterDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CUSER_ID { get; set; }
    }
}
