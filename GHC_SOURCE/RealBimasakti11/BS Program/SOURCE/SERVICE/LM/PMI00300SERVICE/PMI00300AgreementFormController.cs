using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMI00300BACK;
using PMI00300BACK.OpenTelemetry;
using PMI00300COMMON;
using PMI00300COMMON.DTO;
using PMI00300COMMON.Loggers;
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

namespace PMI00300SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMI00300AgreementFormController : ControllerBase, IPMI00300AgreementForm
    {
        private LoggerPMI00300AgreementForm _logger;
        private readonly ActivitySource _activitySource;

        public PMI00300AgreementFormController(ILogger<PMI00300AgreementFormController> logger)
        {
            LoggerPMI00300AgreementForm.R_InitializeLogger(logger);
            _logger = LoggerPMI00300AgreementForm.R_GetInstanceLogger();
            _activitySource = PMI00300AgreementFormActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMI00300Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<PMI00300GetAgreementFormListDTO> GetAgreementFormList()
        {
            using Activity activity = _activitySource.StartActivity("GetAgreementFormList");
            _logger.LogInfo("Start || GetAgreementFormList(Controller)");

            R_Exception loEx = new R_Exception();
            IAsyncEnumerable<PMI00300GetAgreementFormListDTO>? loRtn = null;
            PMI00300AgreementFormCls loCls = new PMI00300AgreementFormCls();

            try
            {
                _logger.LogInfo("Set Parameter || GetAgreementFormList(Controller)");
                var loParameter = new PMI00300GetAgreementFormListParameterDTO();
                loParameter = R_Utility.R_GetStreamingContext<PMI00300GetAgreementFormListParameterDTO>(ContextConstant.PMI00300_GET_AGREEMENT_FORM_LIST_STREAMING_CONTEXT);

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLANG_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetAgreementFormListDb(Cls) || GetAgreementFormList(Controller)");
                var loTemp = loCls.GetAgreementFormListDb(loParameter);
                loRtn = GetListStream(loTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAgreementFormList(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMI00300HandOverChecklistDTO> GetList_HandOverChecklist(PMI00300HandOverChecklistParamDTO poParam)
        {
            Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            _logger.LogInfo($"Starting {MethodBase.GetCurrentMethod().Name} in {GetType().Name}");
            R_Exception loException = new R_Exception();
            List<PMI00300HandOverChecklistDTO> loRtnTemp = null;
            PMI00300AgreementFormCls loCls;
            try
            {
                loCls = new();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                _logger.LogInfo($"Executing cls method in {GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
                loRtnTemp = loCls.GetList_HandOverChecklist(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo($"End {MethodBase.GetCurrentMethod().Name} in {GetType().Name}");
            return GetListStream(loRtnTemp);
        }


        [HttpPost]
        public PMI00300GetPeriodYearRangeResultDTO GetPeriodYearRange()
        {
            using Activity activity = _activitySource.StartActivity("GetPeriodYearRange");
            _logger.LogInfo("Start || GetPeriodYearRange(Controller)");

            R_Exception loEx = new R_Exception();
            PMI00300GetPeriodYearRangeParameterDTO loParameter = new PMI00300GetPeriodYearRangeParameterDTO();
            PMI00300GetPeriodYearRangeResultDTO loRtn = new PMI00300GetPeriodYearRangeResultDTO();
            PMI00300AgreementFormCls loCls = new PMI00300AgreementFormCls();

            try
            {
                _logger.LogInfo("Set Parameter || GetPeriodYearRange(Controller)");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run GetPeriodYearRangeDb(Cls) || GetPeriodYearRange(Controller)");
                loRtn.Data = loCls.GetPeriodYearRangeDb(loParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetPeriodYearRange(Controller)");
            return loRtn;
        }

        #region UTILITIES
        private async IAsyncEnumerable<T> GetListStream<T>(IEnumerable<T> poParameter)
        {
            foreach (T item in poParameter)
            {
                yield return item;
            }
        }
        #endregion
    }
}
