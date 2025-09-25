using PMF00100BACK;
using PMF00100BACK.OpenTelemetry;
using PMF00100COMMON;
using PMF00100COMMON.DTOs;
using PMF00100COMMON.DTOs.PMF00100;
using PMF00100COMMON.Logger;
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

namespace PMF00100SERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMF00100Controller : ControllerBase, IPMF00100
    {
        private LoggerPMF00100 _logger;
        private readonly ActivitySource _activitySource;
        public PMF00100Controller(ILogger<PMF00100Controller> logger)
        {
            LoggerPMF00100.R_InitializeLogger(logger);
            _logger = LoggerPMF00100.R_GetInstanceLogger();
            _activitySource = PMF00100ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMF00100Controller));
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
                PMF00100Cls loCls = new PMF00100Cls();
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
                PMF00100Cls loCls = new PMF00100Cls();
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

        //[HttpPost]
        //public GetCallerTrxInfoResultDTO GetCallerTrxInfo(GetCallerTrxInfoParameterDTO poParam)
        //{
        //    using Activity activity = _activitySource.StartActivity("GetCallerTrxInfo");
        //    _logger.LogInfo("Start || GetCallerTrxInfo(Controller)");
        //    R_Exception loException = new R_Exception();
        //    GetCallerTrxInfoResultDTO loRtn = new GetCallerTrxInfoResultDTO();

        //    try
        //    {
        //        _logger.LogInfo("Set Parameter || GetCallerTrxInfo(Controller)");
        //        PMF00100Cls loCls = new PMF00100Cls();
        //        poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
        //        poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

        //        _logger.LogInfo("Run GetCallerTrxInfo(Cls) || GetCallerTrxInfo(Controller)");
        //        loRtn.Data = loCls.GetCallerTrxInfo(poParam);
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //        _logger.LogError(loException);
        //    }

        //    loException.ThrowExceptionIfErrors();
        //    _logger.LogInfo("End || GetCallerTrxInfo(Controller)");
        //    return loRtn;
        //}

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
                PMF00100Cls loCls = new PMF00100Cls();
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
        public PMF00100HeaderResultDTO GetHeader(PMF00100HeaderParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetHeader");
            _logger.LogInfo("Start || GetHeader(Controller)");
            R_Exception loException = new R_Exception();
            PMF00100HeaderResultDTO loRtn = new PMF00100HeaderResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetHeader(Controller)");
                PMF00100Cls loCls = new PMF00100Cls();
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
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
        public PMF00100HeaderResultDTO GetCAWTCustReceipt(PMF00100HeaderParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetCAWTCustReceipt");
            _logger.LogInfo("Start || GetCAWTCustReceipt(Controller)");
            R_Exception loException = new R_Exception();
            PMF00100HeaderResultDTO loRtn = new PMF00100HeaderResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetCAWTCustReceipt(Controller)");
                PMF00100Cls loCls = new PMF00100Cls();
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetCAWTCustReceipt(Cls) || GetCAWTCustReceipt(Controller)");
                loRtn.Data = loCls.GetCAWTCustReceipt(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetCAWTCustReceipt(Controller)");
            return loRtn;
        }

        [HttpPost]
        public PMF00100HeaderResultDTO GetCQCustReceipt(PMF00100HeaderParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetCQCustReceipt");
            _logger.LogInfo("Start || GetCQCustReceipt(Controller)");
            R_Exception loException = new R_Exception();
            PMF00100HeaderResultDTO loRtn = new PMF00100HeaderResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetCQCustReceipt(Controller)");
                PMF00100Cls loCls = new PMF00100Cls();
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetCQCustReceipt(Cls) || GetCQCustReceipt(Controller)");
                loRtn.Data = loCls.GetCQCustReceipt(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetCQCustReceipt(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMF00100ListDTO> GetAllocationList()
        {
            using Activity activity = _activitySource.StartActivity("GetAllocationList");
            _logger.LogInfo("Start || GetAllocationList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<PMF00100ListDTO> loRtn = null;
            PMF00100Cls loCls = new PMF00100Cls();
            List<PMF00100ListDTO> loTempRtn = null;
            PMF00100ListParameterDTO loParameter = new PMF00100ListParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetAllocationList(Controller)");
                loParameter = R_Utility.R_GetStreamingContext<PMF00100ListParameterDTO>(ContextConstant.PMF00100_GET_ALLOCATION_LIST_STREAMING_CONTEXT);
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
        private async IAsyncEnumerable<PMF00100ListDTO> GetAllocationStream(List<PMF00100ListDTO> poParameter)
        {
            foreach (PMF00100ListDTO item in poParameter)
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
            PMF00100Cls loCls = new PMF00100Cls();

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
