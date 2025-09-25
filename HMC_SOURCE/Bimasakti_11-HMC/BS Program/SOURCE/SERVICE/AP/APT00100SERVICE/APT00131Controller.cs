using APT00100BACK;
using APT00100BACK.OpenTelemetry;
using APT00100COMMON;
using APT00100COMMON.DTOs;
using APT00100COMMON.DTOs.APT00110;
using APT00100COMMON.DTOs.APT00131;
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
    public class APT00131Controller : ControllerBase, IAPT00131
    {
        private LoggerAPT00131 _logger;
        private readonly ActivitySource _activitySource;

        public APT00131Controller(ILogger<APT00131Controller> logger)
        {
            LoggerAPT00131.R_InitializeLogger(logger);
            _logger = LoggerAPT00131.R_GetInstanceLogger();
            _activitySource = APT00131ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(APT00131Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<APT00131DTO> GetAdditionalList()
        {
            using Activity activity = _activitySource.StartActivity("GetAdditionalList");
            _logger.LogInfo("Start || GetAdditionalList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<APT00131DTO> loRtn = null;
            APT00131Cls loCls = new APT00131Cls();
            List<APT00131DTO> loTempRtn = null;
            GetAdditionalListParameterDTO loParameter = new GetAdditionalListParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetAdditionalList(Controller)");
                loParameter = R_Utility.R_GetStreamingContext<GetAdditionalListParameterDTO>(ContextConstant.APT00131_GET_ADDITIONAL_LIST_STREAMING_CONTEXT);
                loParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetAdditionalList(Cls) || GetAdditionalList(Controller)");
                loTempRtn = loCls.GetAdditionalList(loParameter);

                _logger.LogInfo("Run GetAdditionalStream(Controller) || GetAdditionalList(Controller)");
                loRtn = GetAdditionalStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAdditionalList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<APT00131DTO> GetAdditionalStream(List<APT00131DTO> poParameter)
        {
            foreach (APT00131DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<APT00131ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            APT00131Cls loCls = new APT00131Cls();

            try
            {
                _logger.LogInfo("Run R_Delete(Cls) || R_ServiceDelete(Controller)");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceDelete(Controller)");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<APT00131ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<APT00131ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<APT00131ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<APT00131ParameterDTO>();
            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                APT00131Cls loCls = new APT00131Cls();
                poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run R_GetRecord(Cls) || R_ServiceGetRecord(Controller)");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceGetRecord(Controller)");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<APT00131ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<APT00131ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<APT00131ParameterDTO> loRtn = new R_ServiceSaveResultDTO<APT00131ParameterDTO>();
            APT00131Cls loCls = new APT00131Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Set Action Based On Mode || R_ServiceSave(Controller)");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CACTION = "NEW";
                    poParameter.Entity.Data.CREC_ID = "";
                }
                else if (poParameter.CRUDMode == eCRUDMode.EditMode)
                {
                    poParameter.Entity.CACTION = "EDIT";
                }

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
