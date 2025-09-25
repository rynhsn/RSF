using APT00200BACK;
using APT00200BACK.OpenTelemetry;
using APT00200COMMON;
using APT00200COMMON.DTOs;
using APT00200COMMON.DTOs.APT00200;
using APT00200COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APT00200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APT00200Controller : ControllerBase, IAPT00200
    {
        private LoggerAPT00200 _logger;
        private readonly ActivitySource _activitySource;
        public APT00200Controller(ILogger<APT00200Controller> logger)
        {
            LoggerAPT00200.R_InitializeLogger(logger);
            _logger = LoggerAPT00200.R_GetInstanceLogger();
            _activitySource = APT00200ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(APT00200Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<APT00200DetailDTO> GetPurchaseReturnList()
        {
            using Activity activity = _activitySource.StartActivity("GetPurchaseReturnList");
            _logger.LogInfo("Start || GetPurchaseReturnList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<APT00200DetailDTO> loRtn = null;
            APT00200Cls loCls = new APT00200Cls();
            List<APT00200DetailDTO> loTempRtn = null;
            APT00200ParameterDTO loParameter = new APT00200ParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetPurchaseReturnList(Controller)");
                loParameter = R_Utility.R_GetStreamingContext<APT00200ParameterDTO>(ContextConstant.APT00200_GET_PURCHASE_RETURN_LIST_STREAMING_CONTEXT);
                loParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetPurchaseReturnList(Cls) || GetPurchaseReturnList(Controller)");
                loTempRtn = loCls.GetPurchaseReturnList(loParameter);

                _logger.LogInfo("Run GetPurchaseReturnStream(Controller) || GetPurchaseReturnList(Controller)");
                loRtn = GetPurchaseReturnStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetPurchaseReturnList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<APT00200DetailDTO> GetPurchaseReturnStream(List<APT00200DetailDTO> poParameter)
        {
            foreach (APT00200DetailDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GetPropertyListDTO> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity("GetPropertyList");
            _logger.LogInfo("Start || GetPropertyList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetPropertyListDTO> loRtn = null;
            APT00200Cls loCls = new APT00200Cls();
            List<GetPropertyListDTO> loTempRtn = null;
            GetPropertyListParameterDTO loParameter = new GetPropertyListParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetPropertyList(Controller)");
                loParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetPropertyList(Cls) || GetPropertyList(Controller)");
                loTempRtn = loCls.GetPropertyList(loParameter);

                _logger.LogInfo("Run GetPropertyStream(Controller) || GetPropertyList(Controller)");
                loRtn = GetPropertyStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetPropertyList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetPropertyListDTO> GetPropertyStream(List<GetPropertyListDTO> poParameter)
        {
            foreach (GetPropertyListDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public GetGLSystemParamResultDTO GetGLSystemParam()
        {
            using Activity activity = _activitySource.StartActivity("GetGLSystemParam");
            _logger.LogInfo("Start || GetGLSystemParam(Controller)");
            R_Exception loException = new R_Exception();
            GetGLSystemParamParameterDTO loParameter = new GetGLSystemParamParameterDTO();
            GetGLSystemParamResultDTO loRtn = new GetGLSystemParamResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetGLSystemParam(Controller)");
                APT00200Cls loCls = new APT00200Cls();
                loParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetGLSystemParam(Cls) || GetGLSystemParam(Controller)");
                loRtn.Data = loCls.GetGLSystemParam(loParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetGLSystemParam(Controller)");
            return loRtn;
        }

        [HttpPost]
        public GetAPSystemParamResultDTO GetAPSystemParam()
        {
            using Activity activity = _activitySource.StartActivity("GetAPSystemParam");
            _logger.LogInfo("Start || GetAPSystemParam(Controller)");
            R_Exception loException = new R_Exception();
            GetAPSystemParamParameterDTO loParameter = new GetAPSystemParamParameterDTO();
            GetAPSystemParamResultDTO loRtn = new GetAPSystemParamResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetAPSystemParam(Controller)");
                APT00200Cls loCls = new APT00200Cls();
                loParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetAPSystemParam(Cls) || GetAPSystemParam(Controller)");
                loRtn.Data = loCls.GetAPSystemParam(loParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAPSystemParam(Controller)");
            return loRtn;
        }

        [HttpPost]
        public GetCompanyInfoResultDTO GetCompanyInfo()
        {
            using Activity activity = _activitySource.StartActivity("GetCompanyInfo");
            _logger.LogInfo("Start || GetCompanyInfo(Controller)");
            R_Exception loException = new R_Exception();
            GetCompanyInfoParameterDTO loParameter = new GetCompanyInfoParameterDTO();
            GetCompanyInfoResultDTO loRtn = new GetCompanyInfoResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetCompanyInfo(Controller)");
                APT00200Cls loCls = new APT00200Cls();
                loParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run GetCompanyInfo(Cls) || GetCompanyInfo(Controller)");
                loRtn.Data = loCls.GetCompanyInfo(loParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetCompanyInfo(Controller)");
            return loRtn;
        }

        [HttpPost]
        public GetPeriodYearRangeResultDTO GetPeriodYearRange()
        {
            using Activity activity = _activitySource.StartActivity("GetPeriodYearRange");
            _logger.LogInfo("Start || GetPeriodYearRange(Controller)");
            R_Exception loException = new R_Exception();
            GetPeriodYearRangeParameterDTO loParameter = new GetPeriodYearRangeParameterDTO();
            GetPeriodYearRangeResultDTO loRtn = new GetPeriodYearRangeResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetPeriodYearRange(Controller)");
                APT00200Cls loCls = new APT00200Cls();
                loParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run GetPeriodYearRange(Cls) || GetPeriodYearRange(Controller)");
                loRtn.Data = loCls.GetPeriodYearRange(loParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetPeriodYearRange(Controller)");
            return loRtn;
        }

        [HttpPost]
        public GetTransCodeInfoResultDTO GetTransCodeInfo()
        {
            using Activity activity = _activitySource.StartActivity("GetTransCodeInfo");
            _logger.LogInfo("Start || GetTransCodeInfo(Controller)");
            R_Exception loException = new R_Exception();
            GetTransCodeInfoParameterDTO loParameter = new GetTransCodeInfoParameterDTO();
            GetTransCodeInfoResultDTO loRtn = new GetTransCodeInfoResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetTransCodeInfo(Controller)");
                APT00200Cls loCls = new APT00200Cls();
                loParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run GetTransCodeInfo(Cls) || GetTransCodeInfo(Controller)");
                loRtn.Data = loCls.GetTransCodeInfo(loParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetTransCodeInfo(Controller)");
            return loRtn;
        }
    }
}
