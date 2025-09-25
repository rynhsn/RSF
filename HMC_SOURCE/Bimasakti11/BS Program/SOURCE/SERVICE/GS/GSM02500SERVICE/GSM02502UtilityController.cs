using GSM02500BACK;
using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02502Charge;
using GSM02500COMMON.DTOs.GSM02502Utility;
using GSM02500COMMON.Loggers;
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

namespace GSM02500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM02502UtilityController : ControllerBase, IGSM02502Utility
    {
        private LoggerGSM02502Utility _logger;
        private readonly ActivitySource _activitySource;
        public GSM02502UtilityController(ILogger<GSM02502UtilityController> logger)
        {
            LoggerGSM02502Utility.R_InitializeLogger(logger);
            _logger = LoggerGSM02502Utility.R_GetInstanceLogger();
            _activitySource = GSM02502UtilityActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GSM02502UtilityController));
        }

        [HttpPost]
        public IAsyncEnumerable<GSM02502UtilityDTO> GetUtilityList()
        {
            using Activity activity = _activitySource.StartActivity("GetUtilityList");
            _logger.LogInfo("Start || GetUtilityList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM02502UtilityDTO> loRtn = null;
            GetUtilityListDTO loParam = new GetUtilityListDTO();
            GSM02502UtilityCls loCls = new GSM02502UtilityCls();
            List<GSM02502UtilityDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetUtilityList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02502_UTILITY_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CSELECTED_UNIT_TYPE_CATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02502_UTILITY_UNIT_TYPE_CATEGORY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetUtilityList(Cls) || GetUtilityList(Controller)");
                loTempRtn = loCls.GetUtilityList(loParam);

                _logger.LogInfo("Run GetUtilityStream(Controller) || GetUtilityList(Controller)");
                loRtn = GetUtilityStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUtilityList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM02502UtilityDTO> GetUtilityStream(List<GSM02502UtilityDTO> poParameter)
        {
            foreach (GSM02502UtilityDTO item in poParameter)
            {
                yield return item;
            }
        }
/*

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02502UtilityDTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM02502UtilityParameterDTO loParam = new GSM02502UtilityParameterDTO();
            GSM02502UtilityCls loCls = new GSM02502UtilityCls();

            try
            {
                loParam.Data = poParameter.Entity;
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02502_UTILITY_PROPERTY_ID_CONTEXT);
                loParam.CSELECTED_UNIT_TYPE_CATEGORY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02502_UTILITY_UNIT_TYPE_CATEGORY_ID_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loParam.CACTION = "DELETE";
                loCls.R_Delete(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
*/
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM02502UtilityParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02502UtilityParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM02502UtilityParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM02502UtilityParameterDTO>();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM02502UtilityCls loCls = new GSM02502UtilityCls();

                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02502_UTILITY_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_UNIT_TYPE_CATEGORY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02502_UTILITY_UNIT_TYPE_CATEGORY_ID_CONTEXT);
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

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
        public R_ServiceSaveResultDTO<GSM02502UtilityParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02502UtilityParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM02502UtilityParameterDTO> loRtn = new R_ServiceSaveResultDTO<GSM02502UtilityParameterDTO>();
            GSM02502UtilityCls loCls = new GSM02502UtilityCls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02502_UTILITY_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_UNIT_TYPE_CATEGORY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02502_UTILITY_UNIT_TYPE_CATEGORY_ID_CONTEXT);
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Set Action Based On Mode || R_ServiceSave(Controller)");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CACTION = "ADD";
                }
                else if (poParameter.CRUDMode == eCRUDMode.EditMode)
                {
                    poParameter.Entity.CACTION = "EDIT";
                }

                _logger.LogInfo("Run R_Save(Cls) || R_ServiceSave(Controller)");
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

        [HttpPost]

        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02502UtilityParameterDTO> poParameter)
        {
            throw new NotImplementedException();
        }
    }
}
