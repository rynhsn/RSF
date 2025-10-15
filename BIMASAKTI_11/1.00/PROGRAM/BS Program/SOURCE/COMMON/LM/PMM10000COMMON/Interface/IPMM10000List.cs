using PMM10000COMMON.Call_Type_Pricelist;
using PMM10000COMMON.SLA_Call_Type;
using PMM10000COMMON.SLA_Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM10000COMMON.Interface
{
    public interface IPMM10000List
    {
        IAsyncEnumerable<PMM10000SLACallTypeDTO> GetCallTypeList();
        IAsyncEnumerable<PMM10000CategoryDTO> GetCategoryList();
        IAsyncEnumerable<PMM10000PricelistDTO> GetAssignPricelist();
        IAsyncEnumerable<PMM10000PricelistDTO> GetPricelist();
    }
}
