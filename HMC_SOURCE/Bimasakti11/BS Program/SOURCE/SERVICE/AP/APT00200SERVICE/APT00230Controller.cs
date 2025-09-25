using APT00200BACK;
using APT00200BACK.OpenTelemetry;
using APT00200COMMON;
using APT00200COMMON.DTOs.APT00221;
using APT00200COMMON.DTOs.APT00230;
using APT00200COMMON.Loggers;
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

namespace APT00200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APT00230Controller : ControllerBase, IAPT00230
    {
        private LoggerAPT00230 _logger;
        private readonly ActivitySource _activitySource;
        public APT00230Controller(ILogger<APT00230Controller> logger)
        {
            LoggerAPT00230.R_InitializeLogger(logger);
            _logger = LoggerAPT00230.R_GetInstanceLogger();
            _activitySource = APT00230ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(APT00230Controller));
        }

        [HttpPost]
        public APT00230ResultDTO GetSummaryInfo(GetSummaryParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetSummaryInfo");
            _logger.LogInfo("Start || GetSummaryInfo(Controller)");
            R_Exception loException = new R_Exception();
            APT00230ResultDTO loRtn = new APT00230ResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetSummaryInfo(Controller)");
                APT00230Cls loCls = new APT00230Cls();
                poParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run RefreshPurchaseReturnItem(Cls) || GetSummaryInfo(Controller)");
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<APT00230ParameterDTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<APT00230ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<APT00230ParameterDTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<APT00230ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<APT00230ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<APT00230ParameterDTO> loRtn = new R_ServiceSaveResultDTO<APT00230ParameterDTO>();
            APT00230Cls loCls = new APT00230Cls();

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
