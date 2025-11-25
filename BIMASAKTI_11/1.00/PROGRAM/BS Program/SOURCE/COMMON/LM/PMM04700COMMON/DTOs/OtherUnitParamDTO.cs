using System;
using System.Collections.Generic;
using R_CommonFrontBackAPI;

namespace PMM04700Common.DTOs;

public class OtherUnitParamDTO : PricingDTO
{
 
    public string CACTION { get; set; }
    public string CTYPE { get; set; }
    public string CVALID_FROM_DATE { get; set; }
    public string COTHER_UNIT_TYPE_ID { get; set; }
    public bool LACTIVE { get; set; }
}
