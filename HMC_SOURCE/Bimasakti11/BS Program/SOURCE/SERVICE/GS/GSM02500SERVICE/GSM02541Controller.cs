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
    public class GSM02541Controller : ControllerBase, IGSM02541
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
            using Activity activity = _activitySource.StartActivity("GetOtherUnitList");
            _logger.LogInfo("Start || GetOtherUnitList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM02541DTO> loRtn = null;
            GetOtherUnitListParameterDTO loParam = new GetOtherUnitListParameterDTO();
            GSM02540OtherUnitCls loCls = new GSM02540OtherUnitCls();
            List<GSM02541DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetOtherUnitList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02540_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetOtherUnitList(Cls) || GetOtherUnitList(Controller)");
                loTempRtn = loCls.GetOtherUnitList(loParam);

                _logger.LogInfo("Run GetOtherUnitStream(Controller) || GetOtherUnitList(Controller)");
                loRtn = GetOtherUnitStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetOtherUnitList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM02541DTO> GetOtherUnitStream(List<GSM02541DTO> poParameter)
        {
            foreach (GSM02541DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public TemplateOtherUnitDTO DownloadTemplateOtherUnit()
        {
            using Activity activity = _activitySource.StartActivity("DownloadTemplateOtherUnit");
            _logger.LogInfo("Start || DownloadTemplateOtherUnit(Controller)");
            R_Exception loException = new R_Exception();
            TemplateOtherUnitDTO loRtn = new TemplateOtherUnitDTO();

            try
            {
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
            using Activity activity = _activitySource.StartActivity("GetOtherUnitTypeList");
            _logger.LogInfo("Start || GetOtherUnitTypeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<OtherUnitTypeDTO> loRtn = null;
            OtherUnitTypeParameterDTO loParam = new OtherUnitTypeParameterDTO();
            GSM02540OtherUnitCls loCls = new GSM02540OtherUnitCls();
            List<OtherUnitTypeDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetOtherUnitTypeList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02540_PROPERTY_ID_STREAMING_CONTEXT);

                _logger.LogInfo("Run GetOtherUnitTypeList(Cls) || GetOtherUnitTypeList(Controller)");
                loTempRtn = loCls.GetOtherUnitTypeList(loParam);

                _logger.LogInfo("Run GetOtherUnitTypeStream(Controller) || GetOtherUnitTypeList(Controller)");
                loRtn = GetOtherUnitTypeStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetOtherUnitTypeList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<OtherUnitTypeDTO> GetOtherUnitTypeStream(List<OtherUnitTypeDTO> poParameter)
        {
            foreach (OtherUnitTypeDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<BuildingDTO> GetBuildingList()
        {
            using Activity activity = _activitySource.StartActivity("GetBuildingList");
            _logger.LogInfo("Start || GetBuildingList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<BuildingDTO> loRtn = null;
            BuildingParameterDTO loParam = new BuildingParameterDTO();
            GSM02540OtherUnitCls loCls = new GSM02540OtherUnitCls();
            List<BuildingDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetBuildingList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02540_PROPERTY_ID_STREAMING_CONTEXT);

                _logger.LogInfo("Run GetBuildingList(Cls) || GetBuildingList(Controller)");
                loTempRtn = loCls.GetBuildingList(loParam);

                _logger.LogInfo("Run GetBuildingStream(Controller) || GetBuildingList(Controller)");
                loRtn = GetBuildingStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetBuildingList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<BuildingDTO> GetBuildingStream(List<BuildingDTO> poParameter)
        {
            foreach (BuildingDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<FloorDTO> GetFloorList()
        {
            using Activity activity = _activitySource.StartActivity("GetFloorList");
            _logger.LogInfo("Start || GetFloorList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<FloorDTO> loRtn = null;
            FloorParameterDTO loParam = new FloorParameterDTO();
            GSM02540OtherUnitCls loCls = new GSM02540OtherUnitCls();
            List<FloorDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetFloorList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02540_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CSELECTED_BUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02540_BUILDING_ID_STREAMING_CONTEXT);

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
        private async IAsyncEnumerable<FloorDTO> GetFloorStream(List<FloorDTO> poParameter)
        {
            foreach (FloorDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethod(GSM02500ActiveInactiveParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethod");
            _logger.LogInfo("Start || RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethod(Controller)");
            R_Exception loException = new R_Exception();
            //GSM02500ActiveInactiveParameterDTO loParam = new GSM02500ActiveInactiveParameterDTO();
            GSM02500ActiveInactiveResultDTO loRtn = new GSM02500ActiveInactiveResultDTO();
            GSM02540OtherUnitCls loCls = new GSM02540OtherUnitCls();

            try
            {
                _logger.LogInfo("Set Parameter || RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethod(Controller)");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02540_PROPERTY_ID_CONTEXT);
                //loParam.COTHER_UNIT_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02540_COTHER_UNIT_ID_CONTEXT);
                //loParam.LACTIVE = R_Utility.R_GetContext<bool>(ContextConstant.GSM02540_LACTIVE_CONTEXT);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethod(Cls) || RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethod(Controller)");
                loCls.RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethod(poParam);
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02541ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM02540OtherUnitCls loCls = new GSM02540OtherUnitCls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02540_PROPERTY_ID_CONTEXT);
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
        public R_ServiceGetRecordResultDTO<GSM02541ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02541ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM02541ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM02541ParameterDTO>();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM02540OtherUnitCls loCls = new GSM02540OtherUnitCls();

                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02540_PROPERTY_ID_CONTEXT);
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
        public R_ServiceSaveResultDTO<GSM02541ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02541ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM02541ParameterDTO> loRtn = new R_ServiceSaveResultDTO<GSM02541ParameterDTO>();
            GSM02540OtherUnitCls loCls = new GSM02540OtherUnitCls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02540_PROPERTY_ID_CONTEXT);
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
    }
}
