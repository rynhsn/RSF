using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMB01600Back;
using PMB01600Common;
using PMB01600Common.DTOs;
using PMB01600Common.Params;
using R_BackEnd;
using R_Common;
using System.Diagnostics;

namespace PMB01600Service
{

    [ApiController]
    [Route("api/[controller]/[action]")]

    public class PMB01600Controller : ControllerBase, IPMB01600
    {
        private LoggerPMB01600 _logger;
        private readonly ActivitySource _activitySource;

        public PMB01600Controller(ILogger<PMB01600Controller> logger)
        {
            //Initial and Get Logger
            LoggerPMB01600.R_InitializeLogger(logger);
            _logger = LoggerPMB01600.R_GetInstanceLogger();
            _activitySource = PMB01600Activity.R_InitializeAndGetActivitySource(nameof(PMB01600Controller));
        }

        [HttpPost]
        public async IAsyncEnumerable<PMB01600BillingStatementDetailDTO> PMB01600GetBillingStatementDetailListStream()
        {
            using var loActivity = _activitySource.StartActivity(nameof(PMB01600GetBillingStatementDetailListStream));
            _logger.LogInfo("Start - GetBillingStatementDetailListStream");
            R_Exception loEx = new();
            PMB01600ParameterDb loDbPar;
            List<PMB01600BillingStatementDetailDTO> loRtnTmp = new();
            PMB01600Cls loCls;
            // IAsyncEnumerable<PMB01600GridDTO> loRtn = null;

            try
            {
                loDbPar = new PMB01600ParameterDb();

                _logger.LogInfo("Set Parameter");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMB01600ContextConstantDetail.CPROPERTY_ID);
                loDbPar.CTENANT_ID = R_Utility.R_GetStreamingContext<string>(PMB01600ContextConstantDetail.CTENANT_ID);
                loDbPar.CREF_PRD = R_Utility.R_GetStreamingContext<string>(PMB01600ContextConstantDetail.CREF_PRD);

                loCls = new PMB01600Cls();
                _logger.LogInfo("GetBillingStatementDetailListStream");
                loRtnTmp = await loCls.GetBillingStatementDetailList(loDbPar);

                // loRtn = GetStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End - GetBillingStatementDetailListStream");

            foreach (var loItem in loRtnTmp)
            {
                yield return loItem;
            }
        }

        [HttpPost]
        public async IAsyncEnumerable<PMB01600BillingStatementHeaderDTO> PMB01600GetBillingStatementHeaderListStream()
        {
            using var loActivity = _activitySource.StartActivity(nameof(PMB01600GetBillingStatementHeaderListStream));
            _logger.LogInfo("Start - GetBillingStatementHeaderListStream");
            R_Exception loEx = new();
            PMB01600ParameterDb loDbPar;
            List<PMB01600BillingStatementHeaderDTO> loRtnTmp = new();
            PMB01600Cls loCls;
            // IAsyncEnumerable<PMB01600GridDTO> loRtn = null;

            try
            {
                loDbPar = new PMB01600ParameterDb();

                _logger.LogInfo("Set Parameter");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMB01600ContextConstantHeader.CPROPERTY_ID);
                loDbPar.CFROM_TENANT_ID = R_Utility.R_GetStreamingContext<string>(PMB01600ContextConstantHeader.CFROM_TENANT_ID);
                loDbPar.CTO_TENANT_ID = R_Utility.R_GetStreamingContext<string>(PMB01600ContextConstantHeader.CTO_TENANT_ID);
                loDbPar.CREF_PRD = R_Utility.R_GetStreamingContext<string>(PMB01600ContextConstantHeader.CREF_PRD);

                loCls = new PMB01600Cls();
                _logger.LogInfo("GetBillingStatementHeaderListStream");
                loRtnTmp = await loCls.GetBillingStatementHeaderList(loDbPar);

                // loRtn = GetStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End - GetBillingStatementDetailListStream");

            foreach (var loItem in loRtnTmp)
            {
                yield return loItem;
            }
        }

        [HttpPost]
        public async Task<PMB01600ListDTO<PMB01600PropertyDTO>> PMB01600GetPropertyList()
        {
            using var loActivity = _activitySource.StartActivity(nameof(PMB01600GetPropertyList));
            R_Exception loEx = new();
            PMB01600ListDTO<PMB01600PropertyDTO> loReturn = new();
            PMB01600Cls loCls = new();

            try
            {
                _logger.LogInfo("Set Parameter - Get Property List");
                var loParam = new PMB01600ParameterDb
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                };

                _logger.LogInfo("Start - Get Property List");
                loReturn.Data = await loCls.GetPropertyList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            _logger.LogInfo("End - Get Property List");
            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        [HttpPost]
        public async Task<PMB01600SingleDTO<PMB01600SystemParamDTO>> PMB01600GetSystemParam(PMB01600SystemParamParam poParam)
        {
            using var loActivity = _activitySource.StartActivity(nameof(PMB01600GetSystemParam));
            R_Exception loEx = new();
            PMB01600SingleDTO<PMB01600SystemParamDTO> loReturn = new();
            PMB01600Cls loCls = new();

            try
            {
                _logger.LogInfo("Set Parameter - GetSystemParam");
                var loParam = new PMB01600ParameterDb
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLANGUAGE_ID = R_BackGlobalVar.CULTURE,
                    CPROPERTY_ID = poParam.CPROPERTY_ID
                };

                _logger.LogInfo("Start - GetSystemParam");
                loReturn.Data = await loCls.GetSystemParam(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            _logger.LogInfo("End - GetSystemParam");
            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        [HttpPost]
        public async Task<PMB01600SingleDTO<PMB01600YearRangeDTO>> PMB01600GetYearRange()
        {
            using var loActivity = _activitySource.StartActivity(nameof(PMB01600GetYearRange));
            R_Exception loEx = new();
            PMB01600SingleDTO<PMB01600YearRangeDTO> loReturn = new();
            PMB01600Cls loCls = new();

            try
            {
                _logger.LogInfo("Set Parameter - GetYearRange");
                var loParam = new PMB01600ParameterDb
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID
                };

                _logger.LogInfo("Start - GetYearRange");
                loReturn.Data = await loCls.GetYearRange(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            _logger.LogInfo("End - GetYearRange");
            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        [HttpPost]
        public async Task<PMB01600ListDTO<PMB01600PeriodDTO>> PMB01600GetPeriodList(PMB01600PeriodParam poParam)
        {
            using var loActivity = _activitySource.StartActivity(nameof(PMB01600GetPeriodList));
            R_Exception loEx = new();
            PMB01600ListDTO<PMB01600PeriodDTO> loReturn = new();
            PMB01600Cls loCls = new();

            try
            {
                //Param
                _logger.LogInfo("Set Parameter");
                var loParam = R_Utility.R_ConvertObjectToObject<PMB01600PeriodParam, PMB01600ParameterDb>(poParam);
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Start - Get Period List");
                loReturn.Data = await loCls.GetPeriodList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            _logger.LogInfo("End - Get Period List");
            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }
    }
}