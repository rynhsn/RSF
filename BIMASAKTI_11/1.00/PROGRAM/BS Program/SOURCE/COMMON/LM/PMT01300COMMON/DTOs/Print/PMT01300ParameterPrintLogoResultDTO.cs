using System;

namespace PMT01300COMMON
{
    public class PMT01300ParameterPrintLogoResultDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CREPORT_CULTURE { get; set; }
        public string CLANGUAGE_ID { get; set; }
        public string CREC_ID { get; set; }
        public string CJRN_ID { get; set; }

        //Logo
        public byte[] CLOGO { get; set; }
    }
}
