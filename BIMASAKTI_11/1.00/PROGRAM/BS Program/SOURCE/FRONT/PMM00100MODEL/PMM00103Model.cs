using PMM00100COMMON;
using PMM00100COMMON.DTO_s;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMM00100MODEL
{
    public class PMM00103Model : R_BusinessObjectServiceClientBase<SystemParamBillingDTO>, IPMM00103
    {
        private const string DEFAULT_CHECKPOINT_NAME = "api/PMM00102";
        public PMM00103Model(
            string pcHttpClientName = PMM00100ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                PMM00100ContextConstant.DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        //implement only
    }
}
