using System;
using System.Collections.Generic;
using R_APICommonDTO;

namespace PMM04700Common.DTOs;

public class OtherUnitDTO
{
    public string CCOMPANY_ID { get; set; }
    public string CPROPERTY_ID { get; set; }
    public string CBUILDING_ID { get; set; }
    public string CBUILDING_NAME { get; set; }
    public string CFLOOR_ID { get; set; }
    public string CFLOOR_NAME { get; set; }
    public string COTHER_UNIT_ID { get; set; }
    public string COTHER_UNIT_NAME { get; set; }
    public bool LACTIVE { get; set; }
    public string CDESCRIPTION { get; set; }
    public string COTHER_UNIT_TYPE_ID { get; set; }
    public string COTHER_UNIT_TYPE_NAME { get; set; }
    public string CLOCATION { get; set; }
    public decimal NGROSS_AREA_SIZE { get; set;}
    public decimal NNET_AREA_SIZE { get; set;}
    public string CLEASE_STATUS { get; set; }
    public string CTENANT_NAME { get; set; }
    public int ITOTAL_LOO { get; set; }
    public string CAGREEMENT_NO { get; set; }
    public string CSTART_DATE { get; set; }
    public string CEND_DATE { get; set; }
    public string CCREATE_BY { get; set; }
    public DateTime DCREATE_DATE { get; set; }
    public string CUPDATE_BY { get; set; }
    public DateTime DUPDATE_DATE { get; set; }
    public string CUSER_ID { get; set; }
    public string CINVOICE_PERIOD { get; set; }
}

public class PMM04700ListResult<T> : R_APIResultBaseDTO
{
    public List<T> Data { get; set; }
}

public class PMM04700RecordResult<T> : R_APIResultBaseDTO
{
    public T Data { get; set; }
}