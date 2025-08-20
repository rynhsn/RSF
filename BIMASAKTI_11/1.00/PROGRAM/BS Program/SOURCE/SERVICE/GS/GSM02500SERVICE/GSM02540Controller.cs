using GSM02500BACK;
using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02530;
using GSM02500COMMON.DTOs.GSM02540;
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
    public class GSM02540Controller : ControllerBase, GSM02500BACK.IGSM02540
    {
        private LoggerGSM02540 _logger;
        private readonly ActivitySource _activitySource;
        public GSM02540Controller(ILogger<GSM02540Controller> logger)
        {
            LoggerGSM02540.R_InitializeLogger(logger);
            _logger = LoggerGSM02540.R_GetInstanceLogger();
            _activitySource = GSM02540OtherUnitTypeActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GSM02540Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<GSM02540DTO> GetOtherUnitTypeList()
        {
            return GetOtherUnitTypeStream();
        }
        private async IAsyncEnumerable<GSM02540DTO> GetOtherUnitTypeStream()
        {
            using Activity activity = _activitySource.StartActivity("GetOtherUnitTypeList");
            _logger.LogInfo("Start || GetOtherUnitTypeList(Controller)");
            R_Exception loException = new R_Exception();
            List<GSM02540DTO> loRtn = null;
            GetOtherUnitTypeListParameterDTO loParam = new GetOtherUnitTypeListParameterDTO();
            GSM02540OtherUnitTypeCls loCls = new GSM02540OtherUnitTypeCls();

            try
            {
                _logger.LogInfo("Set Parameter || GetOtherUnitTypeList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02540_PROPERTY_ID_STREAMING_CONTEXT);

                _logger.LogInfo("Run GetOtherUnitTypeList(Cls) || GetOtherUnitTypeList(Controller)");
                loRtn = await loCls.GetOtherUnitTypeList(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetOtherUnitTypeList(Controller)");

            foreach (GSM02540DTO item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GetPropertyTypeDTO> GetPropertyTypeList()
        {
            return GetPropertyTypeStream();
        }
        private async IAsyncEnumerable<GetPropertyTypeDTO> GetPropertyTypeStream()
        {
            using Activity activity = _activitySource.StartActivity("GetPropertyTypeList");
            _logger.LogInfo("Start || GetPropertyTypeList(Controller)");
            R_Exception loException = new R_Exception();
            List<GetPropertyTypeDTO> loRtn = null;
            GSM02540OtherUnitCls loCls = new GSM02540OtherUnitCls();

            try
            {
                _logger.LogInfo("Run GetPropertyTypeList(Cls) || GetPropertyTypeList(Controller)");
                loRtn = await loCls.GetPropertyTypeList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetPropertyTypeList(Controller)");

            foreach (GetPropertyTypeDTO item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public async Task<TemplateOtherUnitTypeDTO> DownloadTemplateOtherUnitType()
        {
            using Activity activity = _activitySource.StartActivity("DownloadTemplateOtherUnitType");
            _logger.LogInfo("Start || DownloadTemplateOtherUnitType(Controller)");
            R_Exception loException = new R_Exception();
            TemplateOtherUnitTypeDTO loRtn = new TemplateOtherUnitTypeDTO();

            try
            {
                await Task.CompletedTask;
                _logger.LogInfo("Read File || DownloadTemplateOtherUnitType(Controller)");
                var loAsm = Assembly.Load("BIMASAKTI_GS_API");
                var lcResourceFile = "BIMASAKTI_GS_API.Template.Other Unit Type.xlsx";

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
            _logger.LogInfo("End || DownloadTemplateOtherUnitType(Controller)");
            return loRtn;
        }

        [HttpPost]
        public async Task<GSM02500ActiveInactiveResultDTO> RSP_GS_ACTIVE_INACTIVE_OTHER_UNIT_TYPEMethod(GSM02500ActiveInactiveParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_ACTIVE_INACTIVE_OTHER_UNIT_TYPEMethod");
            _logger.LogInfo("Start || RSP_GS_ACTIVE_INACTIVE_OTHER_UNIT_TYPEMethod(Controller)");
            R_Exception loException = new R_Exception();
            GSM02500ActiveInactiveResultDTO loRtn = new GSM02500ActiveInactiveResultDTO();
            GSM02540OtherUnitTypeCls loCls = new GSM02540OtherUnitTypeCls();

            try
            {
                _logger.LogInfo("Set Parameter || RSP_GS_ACTIVE_INACTIVE_OTHER_UNIT_TYPEMethod(Controller)");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run RSP_GS_ACTIVE_INACTIVE_OTHER_UNIT_TYPEMethod(Cls) || RSP_GS_ACTIVE_INACTIVE_OTHER_UNIT_TYPEMethod(Controller)");
                await loCls.RSP_GS_ACTIVE_INACTIVE_OTHER_UNIT_TYPEMethod(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || RSP_GS_ACTIVE_INACTIVE_OTHER_UNIT_TYPEMethod(Controller)");
            return loRtn;
        }

        [HttpPost]
        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02540ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM02540OtherUnitTypeCls loCls = new GSM02540OtherUnitTypeCls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CACTION = "DELETE";
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run R_Delete(Cls) || R_ServiceDelete(Controller)");
                await loCls.R_DeleteAsync(poParameter.Entity);
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
        public async Task<R_ServiceGetRecordResultDTO<GSM02540ParameterDTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02540ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM02540ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM02540ParameterDTO>();
            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM02540OtherUnitTypeCls loCls = new GSM02540OtherUnitTypeCls();

                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run R_GetRecord(Cls) || R_ServiceGetRecord(Controller)");
                loRtn.data = await loCls.R_GetRecordAsync(poParameter.Entity);
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
        public async Task<R_ServiceSaveResultDTO<GSM02540ParameterDTO>> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02540ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM02540ParameterDTO> loRtn = new R_ServiceSaveResultDTO<GSM02540ParameterDTO>();
            GSM02540OtherUnitTypeCls loCls = new GSM02540OtherUnitTypeCls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CACTION = "ADD";
                }
                else if (poParameter.CRUDMode == eCRUDMode.EditMode)
                {
                    poParameter.Entity.CACTION = "EDIT";
                }

                _logger.LogInfo("Run R_Save(Cls) || R_ServiceSave(Controller)");
                loRtn.data = await loCls.R_SaveAsync(poParameter.Entity, poParameter.CRUDMode);
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
