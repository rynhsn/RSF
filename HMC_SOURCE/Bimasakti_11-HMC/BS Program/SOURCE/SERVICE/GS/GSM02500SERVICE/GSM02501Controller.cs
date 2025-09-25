using GSM02500BACK;
using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02501;
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
    public class GSM02501Controller : ControllerBase, IGSM02501
    {
        private LoggerGSM02501 _logger;
        private readonly ActivitySource _activitySource;
        public GSM02501Controller(ILogger<GSM02501Controller> logger)
        {
            LoggerGSM02501.R_InitializeLogger(logger);
            _logger = LoggerGSM02501.R_GetInstanceLogger();
            _activitySource = GSM02501ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GSM02501Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<GSM02501PropertyDTO> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity("GetPropertyList");
            _logger.LogInfo("Start || GetPropertyList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM02501PropertyDTO> loRtn = null;
            GetPropertyListDTO loParam = new GetPropertyListDTO();
            GSM02501Cls loCls = new GSM02501Cls();
            List<GSM02501PropertyDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetPropertyList(Controller)");
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("RunGetPropertyList(Cls) || GetPropertyList(Controller)");
                loTempRtn = loCls.GetPropertyList(loParam);

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
        private async IAsyncEnumerable<GSM02501PropertyDTO> GetPropertyStream(List<GSM02501PropertyDTO> poParameter)
        {
            foreach (GSM02501PropertyDTO item in poParameter)
            {
                yield return item;
            }
        }


        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02501DetailDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM02501ParameterDTO loParam = new GSM02501ParameterDTO();
            GSM02501Cls loCls = new GSM02501Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                loParam.Data = poParameter.Entity;
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParam.CACTION = "DELETE";

                _logger.LogInfo("Run R_Delete(Cls) || R_ServiceDelete(Controller)");
                loCls.R_Delete(loParam);
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
        public R_ServiceGetRecordResultDTO<GSM02501DetailDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02501DetailDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM02501DetailDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM02501DetailDTO>();
            GSM02501ParameterDTO loParam = new GSM02501ParameterDTO();
            R_ServiceGetRecordResultDTO<GSM02501ParameterDTO> loTempRtn = new R_ServiceGetRecordResultDTO<GSM02501ParameterDTO>();


            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM02501Cls loCls = new GSM02501Cls();

                loParam.Data = poParameter.Entity;
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run R_GetRecord(Cls) || R_ServiceGetRecord(Controller)");
                loTempRtn.data = loCls.R_GetRecord(loParam);

                loRtn.data = loTempRtn.data.Data;
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
        public R_ServiceSaveResultDTO<GSM02501DetailDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02501DetailDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM02501DetailDTO> loRtn = new R_ServiceSaveResultDTO<GSM02501DetailDTO>();
            R_ServiceSaveResultDTO<GSM02501ParameterDTO> loTempRtn = new R_ServiceSaveResultDTO<GSM02501ParameterDTO>();
            GSM02501Cls loCls = new GSM02501Cls();
            GSM02501ParameterDTO loParam = new GSM02501ParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                loParam.Data = poParameter.Entity;
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Set Action Based On Mode || R_ServiceSave(Controller)");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    loParam.CACTION = "ADD";
                }
                else if (poParameter.CRUDMode == eCRUDMode.EditMode)
                {
                    loParam.CACTION = "EDIT";
                }

                _logger.LogInfo("Run R_Save(Cls) || R_ServiceSave(Controller)");
                loTempRtn.data = loCls.R_Save(loParam, poParameter.CRUDMode);
                loRtn.data = loTempRtn.data.Data;
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
        public GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_PROPERTYMethod(GSM02500ActiveInactiveParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_ACTIVE_INACTIVE_PROPERTYMethod");
            _logger.LogInfo("Start || RSP_GS_ACTIVE_INACTIVE_PROPERTYMethod(Controller)");
            R_Exception loException = new R_Exception();
            GSM02500ActiveInactiveResultDTO loRtn = new GSM02500ActiveInactiveResultDTO();
            GSM02501Cls loCls = new GSM02501Cls();

            try
            {
                _logger.LogInfo("Set Parameter || RSP_GS_ACTIVE_INACTIVE_PROPERTYMethod(Controller)");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02501_PROPERTY_ID_CONTEXT);
                //loParam.LACTIVE = R_Utility.R_GetContext<bool>(ContextConstant.GSM02501_LACTIVE_CONTEXT);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run RSP_GS_ACTIVE_INACTIVE_PROPERTYMethod(Cls) || RSP_GS_ACTIVE_INACTIVE_PROPERTYMethod(Controller)");
                loCls.RSP_GS_ACTIVE_INACTIVE_PROPERTYMethod(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || RSP_GS_ACTIVE_INACTIVE_PROPERTYMethod(Controller)");
            return loRtn;
        }
    }
}
