using System;
using R_APICommonDTO;

namespace LMM02500Common.DTO
{

    public class LMM02500ProfileAndTaxInfoDTO : R_APIResultBaseDTO

    {
        public LMM02500ProfileDTO Profile { get; set; } = new LMM02500ProfileDTO();
        public LMM02500TaxInfoDTO TaxInfo { get; set; } = new LMM02500TaxInfoDTO();
    }

    public class LMM02500ProfileDTO : R_APIResultBaseDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CTENANT_GROUP_ID { get; set; }

        public string? CUSER_LOGIN_ID { get; set; }
        //BONUS

        #region Profile

        public string? CTENANT_GROUP_NAME { get; set; }
        public string? CADDRESS { get; set; }
        public string? CPHONE1 { get; set; }
        public string? CPHONE2 { get; set; }
        public string? CEMAIL { get; set; }
        public string? CPIC_NAME { get; set; }
        public string? CPIC_POSITION { get; set; }
        public string? CPIC_MOBILE_PHONE1 { get; set; }
        public string? CPIC_MOBILE_PHONE2 { get; set; }
        public string? CPIC_EMAIL { get; set; }
        public bool LUSE_GROUP_TAX { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }

        #endregion
    }

    public class LMM02500TaxInfoDTO
    {
        //BONUS
        public string? CTAX_TYPE { get; set; } = "";
        public bool LPPH { get; set; } = false;
        public string? CTAX_ID { get; set; } = "";
        public string? CTAX_NAME { get; set; } = "";
        public string? CID_TYPE { get; set; } = "";
        public string? CID_NO { get; set; } = "";
        public string? CID_EXPIRED_DATE { get; set; } = "";
        public string? CTAX_CODE { get; set; } = "";
        public decimal NTAX_CODE_LIMIT_AMOUNT { get; set; }
        public string? CTAX_ADDRESS { get; set; } = "";
        public string? CTAX_EMAIL { get; set; } = "";
        public string? CTAX_PHONE1 { get; set; } = "";
        public string? CTAX_PHONE2 { get; set; } = "";
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }

    public class LMM02500TabParameterDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CUSER_LOGIN_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CTENANT_GROUP_ID { get; set; }
        public string? CTENANT_GROUP_NAME { get; set; }
    }
}