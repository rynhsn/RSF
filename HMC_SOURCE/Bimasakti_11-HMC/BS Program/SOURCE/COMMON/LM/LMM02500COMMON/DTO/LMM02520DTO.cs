using System;

namespace LMM02500Common.DTO
{
    public class LMM02520GridDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CUSER_LOGIN_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CTENANT_CATEGORY_NAME { get; set; }
        public string? CTENANT_GROUP_NAME { get; set; }
        public string? CTENANT_TYPE_NAME { get; set; }
        public string? CUNIT_NAME { get; set; }
        public string? CPHONE1 { get; set; }
        public string? CEMAIL { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
    
    public class ObjectParameterLMM02500MoveTenantGroup
    {
        public string? CPROPERTY_ID { get; set; }
        public string? CFROM_TENANT_GROUP { get; set; }
        public string? CTO_TENANT_GROUP { get; set; }
        public string? CTENANT_ID { get; set; }
    }

    public class LMM02520DTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CUSER_LOGIN_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CTENANT_GROUP_ID { get; set; }
        public string? CTENANT_GROUP_NAME { get; set; }
        public string? CTENANT_CATEGORY_NAME { get; set; }
        public string? CTENANT_TYPE_NAME { get; set; }
        public string? CUNIT_NAME { get; set; }
        public string? CPHONE1 { get; set; }
        public string? CEMAIL { get; set; }
        public string? CADDRESS { get; set; }
        public string? CATTENTION1_NAME { get; set; }
        public string? CATTENTION1_POSITION { get; set; }
        public string? CATTENTION1_EMAIL { get; set; }
        public string? CATTENTION1_MOBILE_PHONE1 { get; set; }
        public string? CATTENTION1_MOBILE_PHONE2 { get; set; }
        public string? CATTENTION2_NAME { get; set; }
        public string? CATTENTION2_POSITION { get; set; }
        public string? CATTENTION2_EMAIL { get; set; }
        public string? CATTENTION2_MOBILE_PHONE1 { get; set; }
        public string? CATTENTION2_MOBILE_PHONE2 { get; set; }
        public string? CJRNGRP_CODE { get; set; }
        public string? CJRNGRP_NAME { get; set; }
        public string? CPAYMENT_TERM_CODE { get; set; }
        public string? CPAYMENT_TERM_NAME { get; set; }
        public string? CCURRENCY_CODE { get; set; }
        public string? CCURRENCY_NAME { get; set; }
        public string? CSALESMAN_ID { get; set; }
        public string? CSALESMAN_NAME { get; set; }
        public string? CLOB_CODE { get; set; }
        public string? CLOB_DESCRIPTION { get; set; }
        public string? CFAMILY_CARD { get; set; }
        public string? CTAX_TYPE { get; set; }
        public string? CTAX_TYPE_DESCR { get; set; }
        public string? CTAX_ID { get; set; }
        public string? CTAX_NAME { get; set; }
        public string? CID_TYPE { get; set; }
        public string? CID_TYPE_DESCR { get; set; }
        public string? CID_NO { get; set; }
        public string? CID_EXPIRED_DATE { get; set; }
        public string? CTAX_CODE { get; set; }
        public decimal NTAX_CODE_LIMIT_AMOUNT { get; set; }
        public string? CTAX_ADDRESS { get; set; }
        public string? CTAX_EMAIL { get; set; }
        public string? CTAX_PHONE1 { get; set; }
        public string? CTAX_PHONE2 { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
}