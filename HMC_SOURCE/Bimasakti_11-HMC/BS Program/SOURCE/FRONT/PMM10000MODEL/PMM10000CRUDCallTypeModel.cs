using PMM10000COMMON.Interface;
using PMM10000COMMON.SLA_Call_Type;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM10000MODEL
{
    public class PMM10000CRUDCallTypeModel : R_BusinessObjectServiceClientBase<PMM10000SLACallTypeDTO>, IPMM10000CRUDCallType
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMM10000CallType";
        private const string DEFAULT_MODULE = "PM";
        public PMM10000CRUDCallTypeModel(
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
