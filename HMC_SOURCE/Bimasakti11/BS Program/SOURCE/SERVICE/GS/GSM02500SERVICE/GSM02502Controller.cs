using GSM02500BACK;
using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02502;
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
    public class GSM02502Controller : ControllerBase, IGSM02502
    {
        private LoggerGSM02502 _logger;
        private readonly ActivitySource _activitySource;
        public GSM02502Controller(ILogger<GSM02502Controller> logger)
        {
            LoggerGSM02502.R_InitializeLogger(logger);
            _logger = LoggerGSM02502.R_GetInstanceLogger();
            _activitySource = GSM02502ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GSM02502Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<PropertyTypeDTO> GetPropertyTypeList()
        {
            using Activity activity = _activitySource.StartActivity("GetPropertyTypeList");
            _logger.LogInfo("Start || GetPropertyTypeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<PropertyTypeDTO> loRtn = null;
            PropertyTypeParameterDTO loParam = new PropertyTypeParameterDTO();
            GSM02502Cls loCls = new GSM02502Cls();
            List<PropertyTypeDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetPropertyTypeList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetPropertyTypeList(Cls) || GetPropertyTypeList(Controller)");
                loTempRtn = loCls.GetPropertyTypeList(loParam);

                _logger.LogInfo("Run GetPropertyTypeStream(Controller) || GetPropertyTypeList(Controller)");
                loRtn = GetPropertyTypeStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetPropertyTypeList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<PropertyTypeDTO> GetPropertyTypeStream(List<PropertyTypeDTO> poParameter)
        {
            foreach (PropertyTypeDTO item in poParameter)
            {
                yield return item;
            }
        }


        [HttpPost]
        public TemplateUnitTypeCategoryDTO DownloadTemplateUnitTypeCategory()
        {
            using Activity activity = _activitySource.StartActivity("DownloadTemplateUnitTypeCategory");
            _logger.LogInfo("Start || DownloadTemplateUnitTypeCategory(Controller)");
            R_Exception loException = new R_Exception();
            TemplateUnitTypeCategoryDTO loRtn = new TemplateUnitTypeCategoryDTO();

            try
            {
                _logger.LogInfo("Read File || DownloadTemplateUnitTypeCategory(Controller)");
                var loAsm = Assembly.Load("BIMASAKTI_GS_API");
                var lcResourceFile = "BIMASAKTI_GS_API.Template.Unit Type Category.xlsx";

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
            _logger.LogInfo("End || DownloadTemplateUnitTypeCategory(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSM02502DTO> GetUnitTypeCategoryList()
        {
            using Activity activity = _activitySource.StartActivity("GetUnitTypeCategoryList");
            _logger.LogInfo("Start || GetUnitTypeCategoryList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM02502DTO> loRtn = null;
            GetUnitTypeCategoryListDTO loParam = new GetUnitTypeCategoryListDTO();
            GSM02502Cls loCls = new GSM02502Cls();
            List<GSM02502DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetUnitTypeCategoryList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02502_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetUnitTypeCategoryList(Cls) || GetUnitTypeCategoryList(Controller)");
                loTempRtn = loCls.GetUnitTypeCategoryList(loParam);

                _logger.LogInfo("Run GetUnitTypeCategoryStream(Controller) || GetUnitTypeCategoryList(Controller)");
                loRtn = GetUnitTypeCategoryStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUnitTypeCategoryList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM02502DTO> GetUnitTypeCategoryStream(List<GSM02502DTO> poParameter)
        {
            foreach (GSM02502DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02502ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM02502Cls loCls = new GSM02502Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02502_PROPERTY_ID_CONTEXT);
                poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
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
        public R_ServiceGetRecordResultDTO<GSM02502ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02502ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM02502ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM02502ParameterDTO>();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM02502Cls loCls = new GSM02502Cls();

                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02502_PROPERTY_ID_CONTEXT);

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
        public R_ServiceSaveResultDTO<GSM02502ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02502ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM02502ParameterDTO> loRtn = new R_ServiceSaveResultDTO<GSM02502ParameterDTO>();
            GSM02502Cls loCls = new GSM02502Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02502_PROPERTY_ID_CONTEXT);
                poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Set Action Based On Move || R_ServiceSave(Controller)");
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
        public GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE_CATEGORYMethod(GSM02500ActiveInactiveParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE_CATEGORYMethod");
            _logger.LogInfo("Start || RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE_CATEGORYMethod(Controller)");
            R_Exception loException = new R_Exception();
            GSM02500ActiveInactiveResultDTO loRtn = new GSM02500ActiveInactiveResultDTO();
            GSM02502Cls loCls = new GSM02502Cls();

            try
            {
                _logger.LogInfo("Set Parameter || RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE_CATEGORYMethod(Controller)");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02502_PROPERTY_ID_CONTEXT);
                //loParam.CUNIT_TYPE_CATEGORY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02502_UNIT_TYPE_CATEGORY_ID_CONTEXT);
                //loParam.LACTIVE = R_Utility.R_GetContext<bool>(ContextConstant.GSM02502_LACTIVE_CONTEXT);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE_CATEGORYMethod(Cls) || RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE_CATEGORYMethod(Controller)");
                loCls.RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE_CATEGORYMethod(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE_CATEGORYMethod(Controller)");
            return loRtn;
        }
    }
}
