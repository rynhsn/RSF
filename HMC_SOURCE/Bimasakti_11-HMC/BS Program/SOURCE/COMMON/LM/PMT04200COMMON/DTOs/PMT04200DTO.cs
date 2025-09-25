using System;

namespace PMT04200Common.DTOs;

public class PMT04200DTO
{
    public string CACTION { get; set; }
    public string CPROPERTY_ID { get; set; }
    public int INO { get; set; }
    public string CCOMPANY_ID { get; set; }
    public string CUSER_ID { get; set; }
    public string CLANGUAGE_ID { get; set; }
    public string CDEPT_CODE { get; set; }
    public string CLOI_AGRMT_NO { get; set; }
    public string CLOI_DEPT_CODE { get; set; }
    public string CLOI_DEPT_NAME { get; set; }
    public string CDEPT_NAME { get; set; }

    public string CTRANS_CODE { get; set; }
    public string CREF_NO { get; set; }
    public string CREF_DATE { get; set; }
    public DateTime? DREF_DATE { get; set; }
    public string CDOC_NO { get; set; }
    public string CDOC_DATE { get; set; }
    public DateTime? DDOC_DATE { get; set; }
    public string CREF_PRD { get; set; }
    public string CTRANS_DESC { get; set; }
    public string CCURRENCY_CODE { get; set; }

    public decimal NTRANS_AMOUNT { get; set; }
    public decimal NLTRANS_AMOUNT { get; set; }
    public decimal NBTRANS_AMOUNT { get; set; }
    public decimal NBANK_CHARGES { get; set; }
    public decimal NLBANK_CHARGES { get; set; }
    public decimal NBBANK_CHARGES { get; set; }
    
    public string CSTATUS { get; set; }
    public string CSTATUS_NAME { get; set; }
    public string CREC_ID { get; set; }
    public string CCREATE_BY { get; set; }
    public DateTime? DCREATE_DATE { get; set; }
    public string CUPDATE_BY { get; set; }
    public DateTime? DUPDATE_DATE { get; set; }
    public string CCB_CODE { get; set; }
    public string CCB_NAME { get; set; }
    public string CCB_ACCOUNT_NO { get; set; }
    public string CCB_ACCOUNT_NAME { get; set; }
    public string CUNIT_DESCRIPTION { get; set; }
    public string CCUST_SUPP_ID_NAME { get; set; }
    public decimal NLBASE_RATE { get; set; }
    public string CCUST_SUPP_ID { get; set; }
    public string CCUST_SUPP_NAME { get; set; }
    public string CLOI_AGRMT_ID { get; set; }
    public string CCASH_FLOW_GROUP_CODE { get; set; }
    public string CCASH_FLOW_CODE { get; set; }
    public string CCASH_FLOW_NAME { get; set; }
    public string CCUSTOMER_TYPE { get; set; }
    public string CCUSTOMER_TYPE_NAME { get; set; }
    public decimal NAR_REMAINING { get; set; }
    public decimal NLAR_REMAINING { get; set; }
    public decimal NBAR_REMAINING { get; set; }
    public decimal NTAX_REMAINING { get; set; }
    public decimal NLTAX_REMAINING { get; set; }
    public decimal NBTAX_REMAINING { get; set; }
    public decimal NTOTAL_REMAINING { get; set; }
    public decimal NLTOTAL_REMAINING { get; set; }
    public decimal NBTOTAL_REMAINING { get; set; }
    public decimal NLCURRENCY_RATE { get; set; }
    public decimal NTAX_BASE_RATE { get; set; }
    public decimal NTAX_RATE { get; set; }
    public decimal NBBASE_RATE { get; set; }
    public decimal NBCURRENCY_RATE { get; set; }

}