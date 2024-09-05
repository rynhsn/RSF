using GSM02500BACK;
using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02530;
using GSM02500COMMON.DTOs.GSM02531;
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
    public class GSM02540UtilitiesController : ControllerBase, IGSM02540Utilities
    {
        private LoggerGSM02540Utilities _logger;
        private readonly ActivitySource _activitySource;
        public GSM02540UtilitiesController(ILogger<GSM02540UtilitiesController> logger)
        {
            LoggerGSM02540Utilities.R_InitializeLogger(logger);
            _logger = LoggerGSM02540Utilities.R_GetInstanceLogger();
            _activitySource = GSM02540UtilityActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GSM02531Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<GSM02531UtilityDTO> GetUtilitiesList()
        {
            using Activity activity = _activitySource.StartActivity("GetUtilitiesList");
            _logger.LogInfo("Start || GetUtilitiesList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM02531UtilityDTO> loRtn = null;
            GetUtilitiesListParameterDTO loParam = new GetUtilitiesListParameterDTO();
            GSM02540UtilitiesCls loCls = new GSM02540UtilitiesCls();
            List<GSM02531UtilityDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetUtilitiesList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02530_PROPERTY_ID_STREAMING_CONTEXT);
                //loParam.CSELECTED_BUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02530_BUILDING_ID_STREAMING_CONTEXT);
                //loParam.CSELECTED_FLOOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02530_FLOOR_ID_STREAMING_CONTEXT);
                loParam.CSELECTED_OTHER_UNIT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02540_OTHER_UNIT_ID_STREAMING_CONTEXT);
                loParam.CSELECTED_UTILITIES_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02530_UTILITIES_TYPE_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                /*
                loParam.CLOGIN_COMPANY_ID = "rcd";
                loParam.CSELECTED_PROPERTY_ID = "JBMPC";
                loParam.CSELECTED_BUILDING_ID = "TOWER1";
                loParam.CSELECTED_FLOOR_ID = "Lt10";
                loParam.CSELECTED_UNIT_ID = "A01";
                loParam.CSELECTED_UTILITIES_TYPE_ID = "01";
                loParam.CLOGIN_USER_ID = "admin";
                */

                _logger.LogInfo("Run GetUtilitiesList(Cls) || GetUtilitiesList(Controller)");
                loTempRtn = loCls.GetUtilitiesList(loParam);

                _logger.LogInfo("Run GetUtilitiesStream(Controller) || GetUtilitiesList(Controller)");
                loRtn = GetUtilitiesStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUtilitiesList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM02531UtilityDTO> GetUtilitiesStream(List<GSM02531UtilityDTO> poParameter)
        {
            foreach (GSM02531UtilityDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public TemplateUnitUtilityDTO DownloadTemplateUnitUtility()
        {
            using Activity activity = _activitySource.StartActivity("DownloadTemplateUnitUtility");
            _logger.LogInfo("Start || DownloadTemplateUnitUtility(Controller)");
            R_Exception loException = new R_Exception();
            TemplateUnitUtilityDTO loRtn = new TemplateUnitUtilityDTO();

            try
            {
                _logger.LogInfo("Read File || DownloadTemplateUnitUtility(Controller)");
                var loAsm = Assembly.Load("BIMASAKTI_GS_API");
                var lcResourceFile = "BIMASAKTI_GS_API.Template.Unit Utility.xlsx";

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
            _logger.LogInfo("End || DownloadTemplateUnitUtility(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSM02531UtilityTypeDTO> GetUtilityTypeList()
        {
            using Activity activity = _activitySource.StartActivity("GetUtilityTypeList");
            _logger.LogInfo("Start || GetUtilityTypeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM02531UtilityTypeDTO> loRtn = null;
            GetUtilityTypeParameterDTO loParam = new GetUtilityTypeParameterDTO();
            GSM02540UtilitiesCls loCls = new GSM02540UtilitiesCls();
            List<GSM02531UtilityTypeDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetUtilityTypeList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("GetUtilityTypeList(Cls)  || GetUtilityTypeList(Controller)");
                loTempRtn = loCls.GetUtilityTypeList(loParam);

                _logger.LogInfo("Run GetUtilityTypeStream(Controller) || GetUtilityTypeList(Controller)");
                loRtn = GetUtilityTypeStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUtilityTypeList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM02531UtilityTypeDTO> GetUtilityTypeStream(List<GSM02531UtilityTypeDTO> poParameter)
        {
            foreach (GSM02531UtilityTypeDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_OTHER_UNIT_UTILITIESMethod(GSM02500ActiveInactiveParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_ACTIVE_INACTIVE_BUILDING_UNIT_UTILITIESMethod");
            _logger.LogInfo("Start || RSP_GS_ACTIVE_INACTIVE_BUILDING_UNIT_UTILITIESMethod(Controller)");
            R_Exception loException = new R_Exception();
            GSM02500ActiveInactiveResultDTO loRtn = new GSM02500ActiveInactiveResultDTO();
            GSM02540UtilitiesCls loCls = new GSM02540UtilitiesCls();

            try
            {
                _logger.LogInfo("Set Parameter || RSP_GS_ACTIVE_INACTIVE_BUILDING_UNIT_UTILITIESMethod(Controller)");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_PROPERTY_ID_CONTEXT);
                //loParam.CBUILDING_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_BUILDING_ID_CONTEXT);
                //loParam.CFLOOR_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_FLOOR_ID_CONTEXT);
                //loParam.CUNIT_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_UNIT_ID_CONTEXT);
                //loParam.CUTILITIES_TYPE = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_UTILITIES_TYPE_CONTEXT);
                //loParam.CSEQUENCE = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_SEQUENCE_CONTEXT);
                //loParam.LACTIVE = R_Utility.R_GetContext<bool>(ContextConstant.GSM02530_LACTIVE_CONTEXT);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run RSP_GS_ACTIVE_INACTIVE_BUILDING_UNIT_UTILITIESMethod(Cls) || RSP_GS_ACTIVE_INACTIVE_BUILDING_UNIT_UTILITIESMethod(Controller)");
                loCls.RSP_GS_ACTIVE_INACTIVE_OTHER_UNIT_UTILITIESMethod(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || RSP_GS_ACTIVE_INACTIVE_BUILDING_UNIT_UTILITIESMethod(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02531UtilityParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM02540UtilitiesCls loCls = new GSM02540UtilitiesCls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_BUILDING_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_BUILDING_ID_CONTEXT);
                //loParam.CSELECTED_FLOOR_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_FLOOR_ID_CONTEXT);
                //loParam.CSELECTED_UNIT_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_UNIT_ID_CONTEXT);
                //loParam.CSELECTED_UTILITIES_TYPE_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_UTILITIES_TYPE_CONTEXT);                
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
        public R_ServiceGetRecordResultDTO<GSM02531UtilityParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02531UtilityParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM02531UtilityParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM02531UtilityParameterDTO>();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM02540UtilitiesCls loCls = new GSM02540UtilitiesCls();

                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_BUILDING_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_BUILDING_ID_CONTEXT);
                //loParam.CSELECTED_FLOOR_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_FLOOR_ID_CONTEXT);
                //loParam.CSELECTED_UNIT_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_UNIT_ID_CONTEXT);
                //loParam.CSELECTED_UTILITIES_TYPE_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_UTILITIES_TYPE_CONTEXT); 
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
        public R_ServiceSaveResultDTO<GSM02531UtilityParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02531UtilityParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM02531UtilityParameterDTO> loRtn = new R_ServiceSaveResultDTO<GSM02531UtilityParameterDTO>();
            GSM02540UtilitiesCls loCls = new GSM02540UtilitiesCls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_BUILDING_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_BUILDING_ID_CONTEXT);
                //loParam.CSELECTED_FLOOR_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_FLOOR_ID_CONTEXT);
                //loParam.CSELECTED_UNIT_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_UNIT_ID_CONTEXT);
                //loParam.CSELECTED_UTILITIES_TYPE_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_UTILITIES_TYPE_CONTEXT);
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
