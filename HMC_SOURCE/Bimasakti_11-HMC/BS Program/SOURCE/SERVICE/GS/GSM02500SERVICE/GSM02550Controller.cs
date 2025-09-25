using GSM02500BACK;
using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02540;
using GSM02500COMMON.DTOs.GSM02550;
using GSM02500COMMON.Loggers;
using Microsoft.AspNetCore.Http;
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
    public class GSM02550Controller : ControllerBase, IGSM02550
    {
        private LoggerGSM02550 _logger;
        private readonly ActivitySource _activitySource;
        public GSM02550Controller(ILogger<GSM02550Controller> logger)
        {
            LoggerGSM02550.R_InitializeLogger(logger);
            _logger = LoggerGSM02550.R_GetInstanceLogger();
            _activitySource = GSM02550ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GSM02550Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<GSM02550DTO> GetUserPropertyList()
        {
            using Activity activity = _activitySource.StartActivity("GetUserPropertyList");
            _logger.LogInfo("Start || GetUserPropertyList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM02550DTO> loRtn = null;
            GetUserPropertyListParameterDTO loParam = new GetUserPropertyListParameterDTO();
            GSM02550Cls loCls = new GSM02550Cls();
            List<GSM02550DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetUserPropertyList(Controller)");
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02550_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run GetUserPropertyList(Cls) || GetUserPropertyList(Controller)");
                loTempRtn = loCls.GetUserPropertyList(loParam);

                _logger.LogInfo("Run GetOtherUnitStream(Controller) || GetUserPropertyList(Controller)");
                loRtn = GetUserPropertyStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUserPropertyList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM02550DTO> GetUserPropertyStream(List<GSM02550DTO> poParameter)
        {
            foreach (GSM02550DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GetUserIdNameDTO> GetUserIdNameList()
        {
            using Activity activity = _activitySource.StartActivity("GetUserIdNameList");
            _logger.LogInfo("Start || GetUserIdNameList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetUserIdNameDTO> loRtn = null;
            GetUserIdNameParameterDTO loParam = new GetUserIdNameParameterDTO();
            GSM02550Cls loCls = new GSM02550Cls();
            List<GetUserIdNameDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetUserIdNameList(Controller)");
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02550_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run GetUserIdNameList(Cls) || GetUserIdNameList(Controller)");
                loTempRtn = loCls.GetUserIdNameList(loParam);

                _logger.LogInfo("Run GetUserIdNameStream(Controller) || GetUserIdNameList(Controller)");
                loRtn = GetUserIdNameStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUserIdNameList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetUserIdNameDTO> GetUserIdNameStream(List<GetUserIdNameDTO> poParameter)
        {
            foreach (GetUserIdNameDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public GetUserIdNameResultDTO GetUserIdName(GetUserIdNameParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetUserIdName");
            _logger.LogInfo("Start || GetUserIdName(Controller)");
            R_Exception loException = new R_Exception();
            GetUserIdNameResultDTO loRtn = new GetUserIdNameResultDTO();
            GSM02550Cls loCls = new GSM02550Cls();
            List<GetUserIdNameDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetUserIdName(Controller)");
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run GetUserIdName(Cls) || GetUserIdName(Controller)");
                loTempRtn = loCls.GetUserIdNameList(poParam);

                loRtn.Data = loTempRtn.Find(x => x.CUSER_ID.Trim().ToUpper() == poParam.CSEARCH_TEXT.ToUpper().Trim());
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUserIdNameList(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02550ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM02550Cls loCls = new GSM02550Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02550_PROPERTY_ID_CONTEXT);
                poParameter.Entity.CACTION = "DELETE";
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

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
        public R_ServiceGetRecordResultDTO<GSM02550ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02550ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM02550ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM02550ParameterDTO>();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM02550Cls loCls = new GSM02550Cls();

                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02550_PROPERTY_ID_CONTEXT);

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
        public R_ServiceSaveResultDTO<GSM02550ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02550ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM02550ParameterDTO> loRtn = new R_ServiceSaveResultDTO<GSM02550ParameterDTO>();
            GSM02550Cls loCls = new GSM02550Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02550_PROPERTY_ID_CONTEXT);
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
