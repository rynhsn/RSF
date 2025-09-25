using APF00100BACK;
using APF00100BACK.OpenTelemetry;
using APF00100COMMON;
using APF00100COMMON.DTOs;
using APF00100COMMON.DTOs.APF00100;
using APF00100COMMON.Logger;
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

namespace APF00100SERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APF00100Controller : ControllerBase, IAPF00100
    {
        private LoggerAPF00100 _logger;
        private readonly ActivitySource _activitySource;
        public APF00100Controller(ILogger<APF00100Controller> logger)
        {
            LoggerAPF00100.R_InitializeLogger(logger);
            _logger = LoggerAPF00100.R_GetInstanceLogger();
            _activitySource = APF00100ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(APF00100Controller));
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
                APF00100Cls loCls = new APF00100Cls();
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

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
                APF00100Cls loCls = new APF00100Cls();
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
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
        public GetCallerTrxInfoResultDTO GetCallerTrxInfo(GetCallerTrxInfoParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetCallerTrxInfo");
            _logger.LogInfo("Start || GetCallerTrxInfo(Controller)");
            R_Exception loException = new R_Exception();
            GetCallerTrxInfoResultDTO loRtn = new GetCallerTrxInfoResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetCallerTrxInfo(Controller)");
                APF00100Cls loCls = new APF00100Cls();
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetCallerTrxInfo(Cls) || GetCallerTrxInfo(Controller)");
                loRtn.Data = loCls.GetCallerTrxInfo(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetCallerTrxInfo(Controller)");
            return loRtn;
        }

        [HttpPost]
        public GetPeriodResultDTO GetPeriod(GetPeriodParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetPeriod");
            _logger.LogInfo("Start || GetPeriod(Controller)");
            R_Exception loException = new R_Exception();
            GetPeriodResultDTO loRtn = new GetPeriodResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetPeriod(Controller)");
                APF00100Cls loCls = new APF00100Cls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run GetPeriod(Cls) || GetPeriod(Controller)");
                loRtn.Data = loCls.GetPeriod(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetPeriod(Controller)");
            return loRtn;
        }

        [HttpPost]
        public APF00100HeaderResultDTO GetHeader(APF00100HeaderParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetHeader");
            _logger.LogInfo("Start || GetHeader(Controller)");
            R_Exception loException = new R_Exception();
            APF00100HeaderResultDTO loRtn = new APF00100HeaderResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetHeader(Controller)");
                APF00100Cls loCls = new APF00100Cls();
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetHeader(Cls) || GetHeader(Controller)");
                loRtn.Data = loCls.GetHeader(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetHeader(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<APF00100ListDTO> GetAllocationList()
        {
            using Activity activity = _activitySource.StartActivity("GetAllocationList");
            _logger.LogInfo("Start || GetAllocationList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<APF00100ListDTO> loRtn = null;
            APF00100Cls loCls = new APF00100Cls();
            List<APF00100ListDTO> loTempRtn = null;
            APF00100ListParameterDTO loParameter = new APF00100ListParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetAllocationList(Controller)");
                loParameter = R_Utility.R_GetStreamingContext<APF00100ListParameterDTO>(ContextConstant.APF00100_GET_ALLOCATION_LIST_STREAMING_CONTEXT);
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetAllocationList(Cls) || GetAllocationList(Controller)");
                loTempRtn = loCls.GetAllocationList(loParameter);

                _logger.LogInfo("Run GetAllocationStream(Controller) || GetAllocationList(Controller)");
                loRtn = GetAllocationStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetInvoiceList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<APF00100ListDTO> GetAllocationStream(List<APF00100ListDTO> poParameter)
        {
            foreach (APF00100ListDTO item in poParameter)
            {
                yield return item;
            }
        }


        [HttpPost]
        public GetTransactionFlagResultDTO GetTransactionFlag(GetTransactionFlagParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetTransactionFlag");
            _logger.LogInfo("Start || GetTransactionFlag(Controller)");
            R_Exception loException = new R_Exception();
            GetTransactionFlagResultDTO loRtn = new GetTransactionFlagResultDTO();
            APF00100Cls loCls = new APF00100Cls();

            try
            {
                _logger.LogInfo("Run GetTransactionFlag(Cls) || GetTransactionFlag(Controller)");
                loRtn.Data = loCls.GetTransactionFlag(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetTransactionFlag(Controller)");
            return loRtn;
        }
    }
}
