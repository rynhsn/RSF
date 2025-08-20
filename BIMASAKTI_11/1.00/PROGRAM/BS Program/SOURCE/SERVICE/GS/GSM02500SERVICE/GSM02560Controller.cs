using GSM02500BACK;
using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02550;
using GSM02500COMMON.DTOs.GSM02560;
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
    public class GSM02560Controller : ControllerBase, GSM02500BACK.IGSM02560
    {
        private LoggerGSM02560 _logger;
        private readonly ActivitySource _activitySource;
        public GSM02560Controller(ILogger<GSM02560Controller> logger)
        {
            LoggerGSM02560.R_InitializeLogger(logger);
            _logger = LoggerGSM02560.R_GetInstanceLogger();
            _activitySource = GSM02560ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GSM02560Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<GSM02560DTO> GetDepartmentList()
        {
            return GetDepartmentStream();
        }
        private async IAsyncEnumerable<GSM02560DTO> GetDepartmentStream()
        {
            using Activity activity = _activitySource.StartActivity("GetDepartmentList");
            _logger.LogInfo("Start || GetDepartmentList(Controller)");
            R_Exception loException = new R_Exception();
            List<GSM02560DTO> loRtn = null;
            GetDepartmentListParameterDTO loParam = new GetDepartmentListParameterDTO();
            GSM02560Cls loCls = new GSM02560Cls();

            try
            {
                _logger.LogInfo("Set Parameter || GetDepartmentList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02560_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetDepartmentList(Cls) || GetDepartmentList(Controller)");
                loRtn = await loCls.GetDepartmentList(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetDepartmentList(Controller)");

            foreach (GSM02560DTO item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GetDepartmentLookupListDTO> GetDepartmentLookupList()
        {
            return GetDepartmentLookupStream();
        }
        private async IAsyncEnumerable<GetDepartmentLookupListDTO> GetDepartmentLookupStream()
        {
            using Activity activity = _activitySource.StartActivity("GetDepartmentLookupList");
            _logger.LogInfo("Start || GetDepartmentLookupList(Controller)");
            R_Exception loException = new R_Exception();
            List<GetDepartmentLookupListDTO> loRtn = null;
            GetDepartmentLookupListParameterDTO loParam = new GetDepartmentLookupListParameterDTO();
            GSM02560Cls loCls = new GSM02560Cls();

            try
            {
                _logger.LogInfo("Set Parameter || GetDepartmentLookupList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02560_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetDepartmentLookupList(Cls) || GetDepartmentLookupList(Controller)");
                loRtn = await loCls.GetDepartmentLookupList(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetDepartmentLookupList(Controller)");

            foreach (GetDepartmentLookupListDTO item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02560ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM02560Cls loCls = new GSM02560Cls();

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
        public async Task<R_ServiceGetRecordResultDTO<GSM02560ParameterDTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02560ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM02560ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM02560ParameterDTO>();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM02560Cls loCls = new GSM02560Cls();

                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run R)GetRecord(Cls) || R_ServiceGetRecord(Controller)");
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
        public async Task<R_ServiceSaveResultDTO<GSM02560ParameterDTO>> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02560ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM02560ParameterDTO> loRtn = new R_ServiceSaveResultDTO<GSM02560ParameterDTO>();
            GSM02560Cls loCls = new GSM02560Cls();

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
