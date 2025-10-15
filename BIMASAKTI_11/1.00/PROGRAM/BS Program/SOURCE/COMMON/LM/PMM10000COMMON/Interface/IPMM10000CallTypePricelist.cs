using PMM10000COMMON.Call_Type_Pricelist;
using PMM10000COMMON.UtilityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM10000COMMON.Interface
{
    public interface IPMM10000CallTypePricelist
    {
        AssignUnassignPricelistDTO AssignPricelist(PMM10000DbParameterDTO poParameter);
        AssignUnassignPricelistDTO UnassignPricelist(PMM10000DbParameterDTO poParameter);
    }
}
