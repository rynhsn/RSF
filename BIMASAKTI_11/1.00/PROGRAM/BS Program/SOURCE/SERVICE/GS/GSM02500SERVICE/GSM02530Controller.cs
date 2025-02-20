using GSM02500BACK;
using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02520;
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
    public class GSM02530Controller : ControllerBase, IGSM02530
    {
        private LoggerGSM02530 _logger;
        private readonly ActivitySource _activitySource;
        public GSM02530Controller(ILogger<GSM02530Controller> logger)
        {
            LoggerGSM02530.R_InitializeLogger(logger);
            _logger = LoggerGSM02530.R_GetInstanceLogger();
            _activitySource = GSM02530ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GSM02530Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<GSM02530DTO> GetUnitInfoList()
        {
            using Activity activity = _activitySource.StartActivity("GetUnitInfoList");
            _logger.LogInfo("Start || GetUnitInfoList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM02530DTO> loRtn = null;
            GetUnitInfoListParameterDTO loParam = new GetUnitInfoListParameterDTO();
            GSM02530Cls loCls = new GSM02530Cls();
            List<GSM02530DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetUnitInfoList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02530_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CSELECTED_BUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02530_BUILDING_ID_STREAMING_CONTEXT);
                loParam.CSELECTED_FLOOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02530_FLOOR_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetUnitInfoList(Cls) || GetUnitInfoList(Controller)");
                loTempRtn = loCls.GetUnitInfoList(loParam);

                _logger.LogInfo("Run GetUnitInfoStream(Controller) || GetUnitInfoList(Controller)");
                loRtn = GetUnitInfoStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUnitInfoList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM02530DTO> GetUnitInfoStream(List<GSM02530DTO> poParameter)
        {
            foreach (GSM02530DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public TemplateUnitDTO DownloadTemplateUnit()
        {
            using Activity activity = _activitySource.StartActivity("DownloadTemplateUnit");
            _logger.LogInfo("Start || DownloadTemplateUnit(Controller)");
            R_Exception loEx = new R_Exception();
            TemplateUnitDTO loRtn = new TemplateUnitDTO();

            try
            {
                _logger.LogInfo("Read File || DownloadTemplateUnit(Controller)");
                var loAsm = Assembly.Load("BIMASAKTI_GS_API");
                var lcResourceFile = "BIMASAKTI_GS_API.Template.Unit.xlsx";

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
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End || DownloadTemplateUnit(Controller)");
            return loRtn;
        }


        [HttpPost]
        public IAsyncEnumerable<GetStrataLeaseDTO> GetStrataLeaseList()
        {
            using Activity activity = _activitySource.StartActivity("GetStrataLeaseList");
            _logger.LogInfo("Start || GetStrataLeaseList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetStrataLeaseDTO> loRtn = null;
            GetStrataLeaseParameterDTO loParam = null;
            GSM02530Cls loCls = new GSM02530Cls();
            List<GetStrataLeaseDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetStrataLeaseList(Controller)");
                loParam = R_Utility.R_GetStreamingContext<GetStrataLeaseParameterDTO>(ContextConstant.GSM02530_STRATA_LEASE_STREAMING_CONTEXT);
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetStrataLeaseList(Cls) || GetStrataLeaseList(Controller)");
                loTempRtn = loCls.GetStrataLeaseList(loParam);

                _logger.LogInfo("Run GetStrataLeaseStream(Controller) || GetStrataLeaseList(Controller)");
                loRtn = GetStrataLeaseStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetStrataLeaseList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetStrataLeaseDTO> GetStrataLeaseStream(List<GetStrataLeaseDTO> poParameter)
        {
            foreach (GetStrataLeaseDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_BUILDING_UNITMethod(GSM02500ActiveInactiveParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_ACTIVE_INACTIVE_BUILDING_UNITMethod");
            _logger.LogInfo("Start || RSP_GS_ACTIVE_INACTIVE_BUILDING_UNITMethod(Controller)");
            R_Exception loException = new R_Exception();
            GSM02500ActiveInactiveResultDTO loRtn = new GSM02500ActiveInactiveResultDTO();
            GSM02530Cls loCls = new GSM02530Cls();

            try
            {
                _logger.LogInfo("Set Parameter || RSP_GS_ACTIVE_INACTIVE_BUILDING_UNITMethod(Controller)");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_PROPERTY_ID_CONTEXT);
                //loParam.CBUILDING_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_BUILDING_ID_CONTEXT);
                //loParam.CFLOOR_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_FLOOR_ID_CONTEXT);
                //loParam.CUNIT_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_UNIT_ID_CONTEXT);
                //loParam.LACTIVE = R_Utility.R_GetContext<bool>(ContextConstant.GSM02530_LACTIVE_CONTEXT);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run RSP_GS_ACTIVE_INACTIVE_BUILDING_UNITMethod(Cls) || RSP_GS_ACTIVE_INACTIVE_BUILDING_UNITMethod(Controller)");
                loCls.RSP_GS_ACTIVE_INACTIVE_BUILDING_UNITMethod(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || RSP_GS_ACTIVE_INACTIVE_BUILDING_UNITMethod(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02530ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM02530Cls loCls = new GSM02530Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_BUILDING_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_BUILDING_ID_CONTEXT);
                //loParam.CSELECTED_FLOOR_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_FLOOR_ID_CONTEXT);
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CACTION = "DELETE";

                _logger.LogInfo("Run R_Delete(Cls) || R_ServiceDelete(Controller)");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceDelete(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM02530ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02530ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM02530ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM02530ParameterDTO>();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM02530Cls loCls = new GSM02530Cls();

                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_BUILDING_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_BUILDING_ID_CONTEXT);
                //loParam.CSELECTED_FLOOR_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_FLOOR_ID_CONTEXT);
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run R_GetRecord(Cls) || R_ServiceGetRecord(Controller)");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceGetRecord(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GSM02530ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02530ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM02530ParameterDTO> loRtn = new R_ServiceSaveResultDTO<GSM02530ParameterDTO>();
            GSM02530Cls loCls = new GSM02530Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_BUILDING_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_BUILDING_ID_CONTEXT);
                //loParam.CSELECTED_FLOOR_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02530_FLOOR_ID_CONTEXT);
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
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceSave(Controller)");
            return loRtn;
        }
    }
}
