using PMT50600BACK;
using PMT50600BACK.OpenTelemetry;
using PMT50600COMMON;
using PMT50600COMMON.DTOs;
using PMT50600COMMON.DTOs.PMT50600;
using PMT50600COMMON.Loggers;
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

namespace PMT50600SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT50600Controller : ControllerBase, IPMT50600
    {
        private LoggerPMT50600 _logger;
        private readonly ActivitySource _activitySource;
        public PMT50600Controller(ILogger<PMT50600Controller> logger)
        {
            LoggerPMT50600.R_InitializeLogger(logger);
            _logger = LoggerPMT50600.R_GetInstanceLogger();
            _activitySource = PMT50600ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT50600Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<PMT50600DetailDTO> GetInvoiceList()
        {
            using Activity activity = _activitySource.StartActivity("GetInvoiceList");
            _logger.LogInfo("Start || GetInvoiceList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<PMT50600DetailDTO> loRtn = null;
            PMT50600Cls loCls = new PMT50600Cls();
            List<PMT50600DetailDTO> loTempRtn = null;
            PMT50600ParameterDTO loParameter = new PMT50600ParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetInvoiceList(Controller)");
                loParameter = R_Utility.R_GetStreamingContext<PMT50600ParameterDTO>(ContextConstant.PMT50600_GET_INVOICE_LIST_STREAMING_CONTEXT);
                loParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetInvoiceList(Cls) || GetInvoiceList(Controller)");
                loTempRtn = loCls.GetInvoiceList(loParameter);

                _logger.LogInfo("Run GetInvoiceStream(Controller) || GetInvoiceList(Controller)");
                loRtn = GetInvoiceStream(loTempRtn);
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
        private async IAsyncEnumerable<PMT50600DetailDTO> GetInvoiceStream(List<PMT50600DetailDTO> poParameter)
        {
            foreach (PMT50600DetailDTO item in poParameter)
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
            PMT50600Cls loCls = new PMT50600Cls();
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
                PMT50600Cls loCls = new PMT50600Cls();
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
        public GetPMSystemParamResultDTO GetPMSystemParam(GetPMSystemParamParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetPMSystemParam");
            _logger.LogInfo("Start || GetPMSystemParam(Controller)");
            R_Exception loException = new R_Exception();
            GetPMSystemParamResultDTO loRtn = new GetPMSystemParamResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetPMSystemParam(Controller)");
                PMT50600Cls loCls = new PMT50600Cls();
                poParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetPMSystemParam(Cls) || GetPMSystemParam(Controller)");
                loRtn.Data = loCls.GetPMSystemParam(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetPMSystemParam(Controller)");
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
                PMT50600Cls loCls = new PMT50600Cls();
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
                PMT50600Cls loCls = new PMT50600Cls();
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
                PMT50600Cls loCls = new PMT50600Cls();
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
