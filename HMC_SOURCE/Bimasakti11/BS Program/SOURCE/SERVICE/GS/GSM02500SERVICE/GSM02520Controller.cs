﻿using GSM02500BACK;
using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02510;
using GSM02500COMMON.DTOs.GSM02520;
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM02520Controller : ControllerBase, IGSM02520
    {
        private LoggerGSM02520 _logger;
        private readonly ActivitySource _activitySource;
        public GSM02520Controller(ILogger<GSM02520Controller> logger)
        {
            LoggerGSM02520.R_InitializeLogger(logger);
            _logger = LoggerGSM02520.R_GetInstanceLogger();
            _activitySource = GSM02520ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GSM02520Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<GSM02520DTO> GetFloorList()
        {
            using Activity activity = _activitySource.StartActivity("GetFloorList");
            _logger.LogInfo("Start || GetFloorList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM02520DTO> loRtn = null;
            GetFloorListParameterDTO loParam = new GetFloorListParameterDTO();
            GSM02520Cls loCls = new GSM02520Cls();
            List<GSM02520DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetFloorList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02520_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02520_BUILDING_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetFloorList(Cls) || GetFloorList(Controller)");
                loTempRtn = loCls.GetFloorList(loParam);

                _logger.LogInfo("Run GetFloorStream(Controller) || GetFloorList(Controller)");
                loRtn = GetFloorStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetFloorList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM02520DTO> GetFloorStream(List<GSM02520DTO> poParameter)
        {
            foreach (GSM02520DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public TemplateFloorDTO DownloadTemplateFloor()
        {
            using Activity activity = _activitySource.StartActivity("DownloadTemplateFloor");
            _logger.LogInfo("Start || DownloadTemplateFloor(Controller)");
            R_Exception loException = new R_Exception();
            TemplateFloorDTO loRtn = new TemplateFloorDTO();

            try
            {
                _logger.LogInfo("Read File || DownloadTemplateFloor(Controller)");
                var loAsm = Assembly.Load("BIMASAKTI_GS_API");
                var lcResourceFile = "BIMASAKTI_GS_API.Template.Floor.xlsx";

                using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                {
                    var ms = new MemoryStream();
                    resFilestream.CopyTo(ms);
                    var bytes = ms.ToArray();

                    loRtn.FileBytes = bytes;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || DownloadTemplateFloor(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02520ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM02520Cls loCls = new GSM02520Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02520_PROPERTY_ID_CONTEXT);
                //loParam.CBUILDING_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02520_BUILDING_ID_CONTEXT);
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CACTION = "DELETE";

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
        public R_ServiceGetRecordResultDTO<GSM02520ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02520ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM02520ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM02520ParameterDTO>();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM02520Cls loCls = new GSM02520Cls();

                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02520_PROPERTY_ID_CONTEXT);
                //loParam.CBUILDING_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02520_BUILDING_ID_CONTEXT);

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
        public R_ServiceSaveResultDTO<GSM02520ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02520ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM02520ParameterDTO> loRtn = new R_ServiceSaveResultDTO<GSM02520ParameterDTO>();
            GSM02520Cls loCls = new GSM02520Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02520_PROPERTY_ID_CONTEXT);
                //loParam.CBUILDING_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02520_BUILDING_ID_CONTEXT);
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
        public GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_FLOORMethod(GSM02500ActiveInactiveParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_ACTIVE_INACTIVE_FLOORMethod");
            _logger.LogInfo("Start || RSP_GS_ACTIVE_INACTIVE_FLOORMethod(Controller)");
            R_Exception loException = new R_Exception();
            GSM02500ActiveInactiveResultDTO loRtn = new GSM02500ActiveInactiveResultDTO();
            GSM02520Cls loCls = new GSM02520Cls();

            try
            {
                _logger.LogInfo("Set Parameter || RSP_GS_ACTIVE_INACTIVE_FLOORMethod(Controller)");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02520_PROPERTY_ID_CONTEXT);
                //loParam.CBUILDING_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02520_BUILDING_ID_CONTEXT);
                //loParam.CFLOOR_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02520_FLOOR_ID_CONTEXT);
                //loParam.LACTIVE = R_Utility.R_GetContext<bool>(ContextConstant.GSM02520_LACTIVE_CONTEXT);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("RSP_GS_ACTIVE_INACTIVE_FLOORMethod(Cls) || RSP_GS_ACTIVE_INACTIVE_FLOORMethod(Controller)");
                loCls.RSP_GS_ACTIVE_INACTIVE_FLOORMethod(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || RSP_GS_ACTIVE_INACTIVE_FLOORMethod(Controller)");
            return loRtn;
        }
    }
}
