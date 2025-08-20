using GSM02500BACK;
using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02501;
using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs.GSM02503;
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
    public class GSM02503UnitTypeController : ControllerBase, GSM02500BACK.IGSM02503UnitType
    {
        private LoggerGSM02503UnitType _logger;
        private readonly ActivitySource _activitySource;
        public GSM02503UnitTypeController(ILogger<GSM02503UnitTypeController> logger)
        {
            LoggerGSM02503UnitType.R_InitializeLogger(logger);
            _logger = LoggerGSM02503UnitType.R_GetInstanceLogger();
            _activitySource = GSM02503UnitTypeActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GSM02503UnitTypeController));
        }

        [HttpPost]
        public IAsyncEnumerable<GSM02503UnitTypeDTO> GetUnitTypeList()
        {
            return GetUnitTypeListStream();
        }
        private async IAsyncEnumerable<GSM02503UnitTypeDTO> GetUnitTypeListStream()
        {
            using Activity activity = _activitySource.StartActivity("GetUnitTypeList");
            _logger.LogInfo("Start || GetUnitTypeList(Controller)");
            R_Exception loException = new R_Exception();
            List<GSM02503UnitTypeDTO> loRtn = null;
            GetUnitTypeParameterDTO loParam = new GetUnitTypeParameterDTO();
            GSM02503UnitTypeCls loCls = new GSM02503UnitTypeCls();

            try
            {
                _logger.LogInfo("Set Parameter || GetUnitTypeList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02503_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CUNIT_TYPE_CATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02503_UNIT_TYPE_CATEGORY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetUnitTypeList(Cls) || GetUnitTypeList(Controller)");
                loRtn = await loCls.GetUnitTypeList(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUnitTypeList(Controller)");

            foreach (GSM02503UnitTypeDTO item in loRtn)
            {
                yield return item;
            }
        }


        [HttpPost]
        public async Task<TemplateUnitTypeDTO> DownloadTemplateUnitType()
        {
            using Activity activity = _activitySource.StartActivity("DownloadTemplateUnitType");
            _logger.LogInfo("Start || DownloadTemplateUnitType(Controller)");
            R_Exception loException = new R_Exception();
            TemplateUnitTypeDTO loRtn = new TemplateUnitTypeDTO();

            try
            {
                await Task.CompletedTask;
                _logger.LogInfo("Read File || DownloadTemplateUnitType(Controller)");
                var loAsm = Assembly.Load("BIMASAKTI_GS_API");
                var lcResourceFile = "BIMASAKTI_GS_API.Template.Unit Type.xlsx";

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
            _logger.LogInfo("End || DownloadTemplateUnitType(Controller)");
            return loRtn;
        }

        [HttpPost]
        public async Task<GSM02500ActiveInactiveResultDTO> RSP_GS_ACTIVE_INACTIVE_UNIT_TYPEMethod(GSM02500ActiveInactiveParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_ACTIVE_INACTIVE_UNIT_TYPEMethod");
            _logger.LogInfo("Start || RSP_GS_ACTIVE_INACTIVE_UNIT_TYPEMethod(Controller)");
            R_Exception loException = new R_Exception();
            GSM02500ActiveInactiveResultDTO loRtn = new GSM02500ActiveInactiveResultDTO();
            GSM02503UnitTypeCls loCls = new GSM02503UnitTypeCls();

            try
            {
                _logger.LogInfo("Set Parameter || RSP_GS_ACTIVE_INACTIVE_UNIT_TYPEMethod(Controller)");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run RSP_GS_ACTIVE_INACTIVE_UNIT_TYPEMethod(Cls) || RSP_GS_ACTIVE_INACTIVE_UNIT_TYPEMethod(Controller)");
                await loCls.RSP_GS_ACTIVE_INACTIVE_UNIT_TYPEMethod(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || RSP_GS_ACTIVE_INACTIVE_UNIT_TYPEMethod(Controller)");
            return loRtn;
        }

        [HttpPost]
        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02503UnitTypeParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM02503UnitTypeCls loCls = new GSM02503UnitTypeCls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CACTION = "DELETE";

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
        public async Task<R_ServiceGetRecordResultDTO<GSM02503UnitTypeParameterDTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02503UnitTypeParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM02503UnitTypeParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM02503UnitTypeParameterDTO>();


            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM02503UnitTypeCls loCls = new GSM02503UnitTypeCls();

                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

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
        public async Task<R_ServiceSaveResultDTO<GSM02503UnitTypeParameterDTO>> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02503UnitTypeParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM02503UnitTypeParameterDTO> loRtn = new R_ServiceSaveResultDTO<GSM02503UnitTypeParameterDTO>();
            GSM02503UnitTypeCls loCls = new GSM02503UnitTypeCls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
                //poParameter.Entity.Data.NCOMMON_AREA_SIZE = 0;

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
