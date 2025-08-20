using GSM02500BACK;
using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02540;
using GSM02500COMMON.DTOs.GSM02541;
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
    public class GSM02541Controller : ControllerBase, GSM02500BACK.IGSM02541
    {
        private LoggerGSM02541 _logger;
        private readonly ActivitySource _activitySource;
        public GSM02541Controller(ILogger<GSM02541Controller> logger)
        {
            LoggerGSM02541.R_InitializeLogger(logger);
            _logger = LoggerGSM02541.R_GetInstanceLogger();
            _activitySource = GSM02540OtherUnitActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GSM02540Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<GSM02541DTO> GetOtherUnitList()
        {
            return GetOtherUnitStream();
        }
        private async IAsyncEnumerable<GSM02541DTO> GetOtherUnitStream()
        {
            using Activity activity = _activitySource.StartActivity("GetOtherUnitList");
            _logger.LogInfo("Start || GetOtherUnitList(Controller)");
            R_Exception loException = new R_Exception();
            List<GSM02541DTO> loRtn = null;
            GetOtherUnitListParameterDTO loParam = new GetOtherUnitListParameterDTO();
            GSM02540OtherUnitCls loCls = new GSM02540OtherUnitCls();

            try
            {
                _logger.LogInfo("Set Parameter || GetOtherUnitList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02540_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetOtherUnitList(Cls) || GetOtherUnitList(Controller)");
                loRtn = await loCls.GetOtherUnitList(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetOtherUnitList(Controller)");

            foreach (GSM02541DTO item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public async Task<TemplateOtherUnitDTO> DownloadTemplateOtherUnit()
        {
            using Activity activity = _activitySource.StartActivity("DownloadTemplateOtherUnit");
            _logger.LogInfo("Start || DownloadTemplateOtherUnit(Controller)");
            R_Exception loException = new R_Exception();
            TemplateOtherUnitDTO loRtn = new TemplateOtherUnitDTO();

            try
            {
                await Task.CompletedTask;
                _logger.LogInfo("Read File || DownloadTemplateOtherUnit(Controller)");
                var loAsm = Assembly.Load("BIMASAKTI_GS_API");
                var lcResourceFile = "BIMASAKTI_GS_API.Template.Other Unit.xlsx";

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
            _logger.LogInfo("End || DownloadTemplateOtherUnit(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<OtherUnitTypeDTO> GetOtherUnitTypeList()
        {
            return GetOtherUnitTypeStream();
        }
        private async IAsyncEnumerable<OtherUnitTypeDTO> GetOtherUnitTypeStream()
        {
            using Activity activity = _activitySource.StartActivity("GetOtherUnitTypeList");
            _logger.LogInfo("Start || GetOtherUnitTypeList(Controller)");
            R_Exception loException = new R_Exception();
            List<OtherUnitTypeDTO> loRtn = null;
            OtherUnitTypeParameterDTO loParam = new OtherUnitTypeParameterDTO();
            GSM02540OtherUnitCls loCls = new GSM02540OtherUnitCls();

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

            foreach (OtherUnitTypeDTO item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<BuildingDTO> GetBuildingList()
        {
            return GetBuildingStream();
        }
        private async IAsyncEnumerable<BuildingDTO> GetBuildingStream()
        {
            using Activity activity = _activitySource.StartActivity("GetBuildingList");
            _logger.LogInfo("Start || GetBuildingList(Controller)");
            R_Exception loException = new R_Exception();
            List<BuildingDTO> loRtn = null;
            BuildingParameterDTO loParam = new BuildingParameterDTO();
            GSM02540OtherUnitCls loCls = new GSM02540OtherUnitCls();

            try
            {
                _logger.LogInfo("Set Parameter || GetBuildingList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02540_PROPERTY_ID_STREAMING_CONTEXT);

                _logger.LogInfo("Run GetBuildingList(Cls) || GetBuildingList(Controller)");
                loRtn = await loCls.GetBuildingList(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetBuildingList(Controller)");

            foreach (BuildingDTO item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<FloorDTO> GetFloorList()
        {
            return GetFloorStream();
        }
        private async IAsyncEnumerable<FloorDTO> GetFloorStream()
        {
            using Activity activity = _activitySource.StartActivity("GetFloorList");
            _logger.LogInfo("Start || GetFloorList(Controller)");
            R_Exception loException = new R_Exception();
            List<FloorDTO> loRtn = null;
            FloorParameterDTO loParam = new FloorParameterDTO();
            GSM02540OtherUnitCls loCls = new GSM02540OtherUnitCls();

            try
            {
                _logger.LogInfo("Set Parameter || GetFloorList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02540_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CSELECTED_BUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02540_BUILDING_ID_STREAMING_CONTEXT);

                _logger.LogInfo("Run GetFloorList(Cls) || GetFloorList(Controller)");
                loRtn = await loCls.GetFloorList(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetFloorList(Controller)");

            foreach (FloorDTO item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public async Task<GSM02500ActiveInactiveResultDTO> RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethod(GSM02500ActiveInactiveParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethod");
            _logger.LogInfo("Start || RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethod(Controller)");
            R_Exception loException = new R_Exception();
            GSM02500ActiveInactiveResultDTO loRtn = new GSM02500ActiveInactiveResultDTO();
            GSM02540OtherUnitCls loCls = new GSM02540OtherUnitCls();

            try
            {
                _logger.LogInfo("Set Parameter || RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethod(Controller)");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethod(Cls) || RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethod(Controller)");
                await loCls.RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethod(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethod(Controller)");
            return loRtn;
        }

        [HttpPost]
        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02541ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM02540OtherUnitCls loCls = new GSM02540OtherUnitCls();

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
        public async Task<R_ServiceGetRecordResultDTO<GSM02541ParameterDTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02541ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM02541ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM02541ParameterDTO>();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM02540OtherUnitCls loCls = new GSM02540OtherUnitCls();

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
        public async Task<R_ServiceSaveResultDTO<GSM02541ParameterDTO>> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02541ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM02541ParameterDTO> loRtn = new R_ServiceSaveResultDTO<GSM02541ParameterDTO>();
            GSM02540OtherUnitCls loCls = new GSM02540OtherUnitCls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
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
