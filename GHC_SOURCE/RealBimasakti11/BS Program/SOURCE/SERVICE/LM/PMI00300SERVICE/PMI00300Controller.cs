using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMI00300BACK;
using PMI00300BACK.OpenTelemetry;
using PMI00300COMMON;
using PMI00300COMMON.DTO;
using PMI00300COMMON.Loggers;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMI00300SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMI00300Controller : ControllerBase, IPMI00300
    {
        private LoggerPMI00300 _logger;
        private readonly ActivitySource _activitySource;
        public PMI00300Controller(ILogger<PMI00300Controller> logger)
        {
            LoggerPMI00300.R_InitializeLogger(logger);
            _logger = LoggerPMI00300.R_GetInstanceLogger();
            _activitySource = PMI00300ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMI00300Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<PMI00300GetPropertyListDTO> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity("GetPropertyList");
            _logger.LogInfo("Start || GetPropertyList(Controller)");

            R_Exception loEx = new R_Exception();
            IAsyncEnumerable<PMI00300GetPropertyListDTO>? loRtn = null;
            PMI00300Cls loCls = new PMI00300Cls();

            try
            {
                _logger.LogInfo("Set Parameter || GetPropertyList(Controller)");
                var loParameter = new PMI00300GetPropertyListParameterDTO();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetPropertyListDb(Cls) || GetPropertyList(Controller)");
                var loTemp = loCls.GetPropertyListDb(loParameter);
                loRtn = R_NetCoreUtility.R_GetAsyncStream<PMI00300GetPropertyListDTO>(loTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetPropertyList(Controller)");
            return loRtn;
        }
        
        [HttpPost]
        public IAsyncEnumerable<PMI00300DTO> GetUnitInquiryHeaderList()
        {
            using Activity activity = _activitySource.StartActivity("GetUnitInquiryHeaderList");
            _logger.LogInfo("Start || GetUnitInquiryHeaderList(Controller)");

            R_Exception loEx = new R_Exception();
            IAsyncEnumerable<PMI00300DTO>? loRtn = null;
            PMI00300Cls loCls = new PMI00300Cls();

            try
            {
                _logger.LogInfo("Set Parameter || GetUnitInquiryHeaderList(Controller)");
                PMI00300ParameterDTO loParameter = R_Utility.R_GetStreamingContext<PMI00300ParameterDTO>(ContextConstant.PMI00300_GET_UNIT_INQUIRY_HEADER_LIST_STREAMING_CONTEXT);

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLANG_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetUnitInquiryHeaderListDb(Cls) || GetUnitInquiryHeaderList(Controller)");
                var loTemp = loCls.GetUnitInquiryHeaderListDb(loParameter);
                loRtn = R_NetCoreUtility.R_GetAsyncStream<PMI00300DTO>(loTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUnitInquiryHeaderList(Controller)");
            return loRtn;
        }
        
        [HttpPost]
        public IAsyncEnumerable<PMI00300DetailLeftDTO> GetUnitInquiryDetailLeftList()
        {
            using Activity activity = _activitySource.StartActivity("GetUnitInquiryDetailLeftList");
            _logger.LogInfo("Start || GetUnitInquiryDetailLeftList(Controller)");

            R_Exception loEx = new R_Exception();
            IAsyncEnumerable<PMI00300DetailLeftDTO>? loRtn = null;
            PMI00300Cls loCls = new PMI00300Cls();

            try
            {
                _logger.LogInfo("Set Parameter || GetUnitInquiryDetailLeftList(Controller)");
                PMI00300DetailParameterDTO loParameter = R_Utility.R_GetStreamingContext<PMI00300DetailParameterDTO>(ContextConstant.PMI00300_GET_UNIT_INQUIRY_LEFT_DETAIL_LIST_STREAMING_CONTEXT);

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLANG_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetUnitInquiryDetailLeftListDb(Cls) || GetUnitInquiryDetailLeftList(Controller)");
                var loTemp = loCls.GetUnitInquiryDetailLeftListDb(loParameter);
                loRtn = R_NetCoreUtility.R_GetAsyncStream<PMI00300DetailLeftDTO>(loTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUnitInquiryDetailLeftList(Controller)");
            return loRtn;
        }
        
        [HttpPost]
        public IAsyncEnumerable<PMI00300DetailRightDTO> GetUnitInquiryDetailRightList()
        {
            using Activity activity = _activitySource.StartActivity("GetUnitInquiryDetailRightList");
            _logger.LogInfo("Start || GetUnitInquiryDetailRightList(Controller)");

            R_Exception loEx = new R_Exception();
            IAsyncEnumerable<PMI00300DetailRightDTO>? loRtn = null;
            PMI00300Cls loCls = new();

            try
            {
                _logger.LogInfo("Set Parameter || GetUnitInquiryDetailRightList(Controller)");
                PMI00300DetailParameterDTO loParameter = R_Utility.R_GetStreamingContext<PMI00300DetailParameterDTO>(ContextConstant.PMI00300_GET_UNIT_INQUIRY_RIGHT_DETAIL_LIST_STREAMING_CONTEXT);

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLANG_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetUnitInquiryDetailRightListDb(Cls) || GetUnitInquiryDetailRightList(Controller)");
                var loTemp = loCls.GetUnitInquiryDetailRightListDb(loParameter);
                loRtn = R_NetCoreUtility.R_GetAsyncStream<PMI00300DetailRightDTO>(loTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUnitInquiryDetailRightList(Controller)");
            return loRtn;
        }
        
        [HttpPost]
        public IAsyncEnumerable<PMI00300GetGSBCodeListDTO> GetLeaseStatusList()
        {
            using Activity activity = _activitySource.StartActivity("GetLeaseStatusList");
            _logger.LogInfo("Start || GetLeaseStatusList(Controller)");

            R_Exception loEx = new R_Exception();
            IAsyncEnumerable<PMI00300GetGSBCodeListDTO> loRtn = null;
            PMI00300Cls loCls = new PMI00300Cls();

            try
            {
                _logger.LogInfo("Set Parameter || GetLeaseStatusList(Controller)");

                var loParameter = new PMI00300GetLSStatusListParameterDTO();

                loParameter = R_Utility.R_GetStreamingContext<PMI00300GetLSStatusListParameterDTO>(ContextConstant.PMI00300_GET_LS_STATUS_LIST_STREAMING_CONTEXT);
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.STATUS_TYPE = "L";
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetLSStatusListDb(Cls) || GetLeaseStatusList(Controller)");
                var loResult = loCls.GetLSStatusListDb(loParameter);

                _logger.LogInfo("Run R_GetAsyncStream(Controller) || GetLeaseStatusList(Controller)");
                loRtn = R_NetCoreUtility.R_GetAsyncStream(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetLeaseStatusList(Controller)");

            return loRtn;
        }
       
        [HttpPost]
        public IAsyncEnumerable<PMI00300GetGSBCodeListDTO> GetStrataStatusList()
        {
            using Activity activity = _activitySource.StartActivity("GetStrataStatusList");
            _logger.LogInfo("Start || GetStrataStatusList(Controller)");

            R_Exception loEx = new R_Exception();
            IAsyncEnumerable<PMI00300GetGSBCodeListDTO> loRtn = null;
            PMI00300Cls loCls = new PMI00300Cls();

            try
            {
                _logger.LogInfo("Set Parameter || GetStrataStatusList(Controller)");

                var loParameter = new PMI00300GetLSStatusListParameterDTO();

                loParameter = R_Utility.R_GetStreamingContext<PMI00300GetLSStatusListParameterDTO>(ContextConstant.PMI00300_GET_LS_STATUS_LIST_STREAMING_CONTEXT);
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.STATUS_TYPE = "S";
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetLSStatusListDb(Cls) || GetStrataStatusList(Controller)");
                var loResult = loCls.GetLSStatusListDb(loParameter);

                _logger.LogInfo("Run R_GetAsyncStream(Controller) || GetStrataStatusList(Controller)");
                loRtn = R_NetCoreUtility.R_GetAsyncStream(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetStrataStatusList(Controller)");

            return loRtn;
        }
    }
}
