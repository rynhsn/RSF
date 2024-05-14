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
            _activitySource = GSM02540UnitPromotionActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GSM02540Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<GSM02541DTO> GetUnitPromotionList()
        {
            using Activity activity = _activitySource.StartActivity("GetUnitPromotionList");
            _logger.LogInfo("Start || GetUnitPromotionList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM02541DTO> loRtn = null;
            GetUnitPromotionListParameterDTO loParam = new GetUnitPromotionListParameterDTO();
            GSM02540UnitPromotionCls loCls = new GSM02540UnitPromotionCls();
            List<GSM02541DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetUnitPromotionList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02540_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetUnitPromotionList(Cls) || GetUnitPromotionList(Controller)");
                loTempRtn = loCls.GetUnitPromotionList(loParam);

                _logger.LogInfo("Run GetUnitPromotionStream(Controller) || GetUnitPromotionList(Controller)");
                loRtn = GetUnitPromotionStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUnitPromotionList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM02541DTO> GetUnitPromotionStream(List<GSM02541DTO> poParameter)
        {
            foreach (GSM02541DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public TemplateUnitPromotionDTO DownloadTemplateUnitPromotion()
        {
            using Activity activity = _activitySource.StartActivity("DownloadTemplateUnitPromotion");
            _logger.LogInfo("Start || DownloadTemplateUnitPromotion(Controller)");
            R_Exception loException = new R_Exception();
            TemplateUnitPromotionDTO loRtn = new TemplateUnitPromotionDTO();

            try
            {
                _logger.LogInfo("Read File || DownloadTemplateUnitPromotion(Controller)");
                var loAsm = Assembly.Load("BIMASAKTI_GS_API");
                var lcResourceFile = "BIMASAKTI_GS_API.Template.Unit Promotion.xlsx";

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
            _logger.LogInfo("End || DownloadTemplateUnitPromotion(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<UnitPromotionTypeDTO> GetUnitPromotionTypeList()
        {
            using Activity activity = _activitySource.StartActivity("GetUnitPromotionTypeList");
            _logger.LogInfo("Start || GetUnitPromotionTypeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<UnitPromotionTypeDTO> loRtn = null;
            UnitPromotionTypeParameterDTO loParam = new UnitPromotionTypeParameterDTO();
            GSM02540UnitPromotionCls loCls = new GSM02540UnitPromotionCls();
            List<UnitPromotionTypeDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetUnitPromotionTypeList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02540_PROPERTY_ID_STREAMING_CONTEXT);

                _logger.LogInfo("Run GetUnitPromotionTypeList(Cls) || GetUnitPromotionTypeList(Controller)");
                loTempRtn = loCls.GetUnitPromotionTypeList(loParam);

                _logger.LogInfo("Run GetUnitPromotionTypeStream(Controller) || GetUnitPromotionTypeList(Controller)");
                loRtn = GetUnitPromotionTypeStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUnitPromotionTypeList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<UnitPromotionTypeDTO> GetUnitPromotionTypeStream(List<UnitPromotionTypeDTO> poParameter)
        {
            foreach (UnitPromotionTypeDTO item in poParameter)
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
            GSM02540UnitPromotionCls loCls = new GSM02540UnitPromotionCls();
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
            GSM02540UnitPromotionCls loCls = new GSM02540UnitPromotionCls();
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
        public GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_UNIT_PROMOTIONMethod(GSM02500ActiveInactiveParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_ACTIVE_INACTIVE_UNIT_PROMOTIONMethod");
            _logger.LogInfo("Start || RSP_GS_ACTIVE_INACTIVE_UNIT_PROMOTIONMethod(Controller)");
            R_Exception loException = new R_Exception();
            //GSM02500ActiveInactiveParameterDTO loParam = new GSM02500ActiveInactiveParameterDTO();
            GSM02500ActiveInactiveResultDTO loRtn = new GSM02500ActiveInactiveResultDTO();
            GSM02540UnitPromotionCls loCls = new GSM02540UnitPromotionCls();

            try
            {
                _logger.LogInfo("Set Parameter || RSP_GS_ACTIVE_INACTIVE_UNIT_PROMOTIONMethod(Controller)");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02540_PROPERTY_ID_CONTEXT);
                //loParam.CUNIT_PROMOTION_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02540_CUNIT_PROMOTION_ID_CONTEXT);
                //loParam.LACTIVE = R_Utility.R_GetContext<bool>(ContextConstant.GSM02540_LACTIVE_CONTEXT);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run RSP_GS_ACTIVE_INACTIVE_UNIT_PROMOTIONMethod(Cls) || RSP_GS_ACTIVE_INACTIVE_UNIT_PROMOTIONMethod(Controller)");
                loCls.RSP_GS_ACTIVE_INACTIVE_UNIT_PROMOTIONMethod(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || RSP_GS_ACTIVE_INACTIVE_UNIT_PROMOTIONMethod(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02541ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM02540UnitPromotionCls loCls = new GSM02540UnitPromotionCls();

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
                GSM02540UnitPromotionCls loCls = new GSM02540UnitPromotionCls();

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
            GSM02540UnitPromotionCls loCls = new GSM02540UnitPromotionCls();

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
