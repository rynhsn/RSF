using System;
using R_CommonFrontBackAPI;

namespace PMT04200Common.DTOs;

public class PMT04200ParamDTO
{
    public string CCOMPANY_ID { get; set; }
    public string CPROPERTY_ID { get; set; }
    public string CUSER_ID { get; set; }
    public string CREC_ID { get; set; }
    public string CREF_NO { get; set; }

    public string CTRANS_CODE { get; set; }
    public string CDEPT_CODE { get; set; }
    public string CDEPT_NAME { get; set; }
    public string CCUSTOMER_ID { get; set; }
    public string CCUSTOMER_NAME { get; set; }
    public string CCUSTOMER_TYPE { get; set; }
    public string CCUSTOMER_TYPE_NAME { get; set; }
    public string CPERIOD { get; set; }
    public string CSTATUS { get; set; }
    public string CSEARCH_TEXT { get; set; }
    public string CLANGUAGE_ID { get; set; }
    public string CPAYMENT_TYPE { get; set; }
   
}

public class PMT04200SaveParamDTO
{
    public PMT04200DTO Data { get; set; }
    public eCRUDMode CRUDMode { get; set; }
}