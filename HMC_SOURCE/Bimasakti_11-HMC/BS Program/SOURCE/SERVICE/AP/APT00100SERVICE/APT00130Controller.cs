using APT00100BACK;
using APT00100BACK.OpenTelemetry;
using APT00100COMMON;
using APT00100COMMON.DTOs.APT00121;
using APT00100COMMON.DTOs.APT00130;
using APT00100COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APT00100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APT00130Controller : ControllerBase, IAPT00130
    {
        private LoggerAPT00130 _logger;
        private readonly ActivitySource _activitySource;

        public APT00130Controller(ILogger<APT00130Controller> logger)
        {
            LoggerAPT00130.R_InitializeLogger(logger);
            _logger = LoggerAPT00130.R_GetInstanceLogger();
            _activitySource = APT00130ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(APT00130Controller));
        }

        [HttpPost]
        public APT00130ResultDTO GetSummaryInfo(GetSummaryParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetSummaryInfo");
            _logger.LogInfo("Start || GetSummaryInfo(Controller)");
            R_Exception loException = new R_Exception();
            APT00130ResultDTO loRtn = new APT00130ResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetSummaryInfo(Controller)");
                APT00130Cls loCls = new APT00130Cls();
                poParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run RefreshInvoiceItem(Cls) || GetSummaryInfo(Controller)");
                loRtn.Data = loCls.GetSummaryInfo(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetSummaryInfo(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<APT00130ParameterDTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<APT00130ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<APT00130ParameterDTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<APT00130ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<APT00130ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<APT00130ParameterDTO> loRtn = new R_ServiceSaveResultDTO<APT00130ParameterDTO>();
            APT00130Cls loCls = new APT00130Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run R_Save || R_ServiceSave(Controller)");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceSave(Controller)");

            return loRtn;
        }
    }
}
