using PMT05000BACK;
using PMT05000COMMON;
using PMT05000COMMON.DTO_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PMT05000SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMT05001Controller : ControllerBase, IPMM05000
    {
        private LogerPMT05000 _logger;

        private readonly ActivitySource _activitySource;

        public PMT05001Controller(ILogger<PMT05001Controller> logger)
        {
            //initiate
            LogerPMT05000.R_InitializeLogger(logger);
            _logger = LogerPMT05000.R_GetInstanceLogger();
            _activitySource = PMT05000Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> poList)
        {
            foreach (T loEntity in poList)
            {
                yield return loEntity;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<AgreementChrgDiscDetailDTO> GetAgreementChargesDiscountList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<AgreementChrgDiscDetailDTO> loRtnTemp = null;
            PMT05000Cls loCls;
            try
            {
                loCls = new PMT05000Cls();
                ShowLogExecute();
                var loParam = new AgreementChrgDiscListParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT05000.CPROPERTY_ID),
                    CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT05000.CCHARGES_TYPE),
                    CCHARGES_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT05000.CCHARGES_ID),
                    CDISCOUNT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT05000.CDISCOUNT_CODE),
                    CDISCOUNT_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT05000.CDISCOUNT_TYPE),
                    CINV_PRD = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT05000.CINV_PRD),
                    LALL_BUILDING = R_Utility.R_GetStreamingContext<bool>(ContextConstantPMT05000.LALL_BUILDING),
                    CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT05000.CBUILDING_ID),
                    CAGREEMENT_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT05000.CAGREEMENNT_TYPE),
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                };
                loRtnTemp = loCls.GetAgreementChargesDiscountList(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return StreamListHelper(loRtnTemp);
        }

        [HttpPost]
        public AgreementChrgDiscResultDTO ProcessAgreementChargeDiscount(AgreementChrgDiscParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            AgreementChrgDiscResultDTO loRtn = new();
            PMT05000Cls loCls;
            try
            {
                loCls = new PMT05000Cls();
                ShowLogExecute();
                poParam.CCOMPANY_ID= R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loCls.ProcessAgreementChargeDiscount(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return loRtn;
        }


        #region logger
        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);
        #endregion
    }
}
