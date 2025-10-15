using PMM10000COMMON.Interface;
using PMM10000COMMON.SLA_Call_Type;
using PMM10000COMMON.SLA_Category;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM10000MODEL
{
    public class PMM10000CRUDCategoryModel : R_BusinessObjectServiceClientBase<PMM10000CategoryDTO>, IPMM10000CRUDCategory
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMM10000Category";
        private const string DEFAULT_MODULE = "PM";

        public PMM10000CRUDCategoryModel(
          string pcHttpClientName = DEFAULT_HTTP,
          string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
          string pcModuleName = DEFAULT_MODULE,
          bool plSendWithContext = true,
          bool plSendWithToken = true)
          : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }
    }
}
