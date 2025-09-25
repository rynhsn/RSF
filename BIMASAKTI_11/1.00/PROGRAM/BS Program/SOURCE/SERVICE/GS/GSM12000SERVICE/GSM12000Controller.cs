using GSM12000BACK;
using GSM12000COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace GSM12000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM12000Controller : ControllerBase, IGSM12000
    {
        private LoggerGSM12000 _Logger;
        private readonly ActivitySource _activitySource;
        public GSM12000Controller(ILogger<LoggerGSM12000> logger)
        {
            //Initial and Get Logger
            LoggerGSM12000.R_InitializeLogger(logger);
            _Logger = LoggerGSM12000.R_GetInstanceLogger();
            _activitySource = GSM12000ActivityInitSourceBase.R_InitializeAndGetActivitySource(nameof(GSM12000Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<GSM12000GSBCodeDTO> GetListMessageType()
        {
            return GetListMessageTypeStreamData();
        }
        private async IAsyncEnumerable<GSM12000GSBCodeDTO> GetListMessageTypeStreamData()
        {
            using Activity activity = _activitySource.StartActivity("GetListMessageTypeStreamData");
            var loEx = new R_Exception();
            List<GSM12000GSBCodeDTO> loRtn = null;
            _Logger.LogInfo("Start GetListMessageTypeStreamData");

            try
            {
                var loCls = new GSM12000Cls();

                _Logger.LogInfo("Call Back Method GetListMessageTypeStreamData");
                loRtn = await loCls.GetListGSBCodeAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetListMessageTypeStreamData");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSM12000DTO> GetListMessage()
        {
            return GetListMessageStreamData();
        }
        private async IAsyncEnumerable<GSM12000DTO> GetListMessageStreamData()
        {
            using Activity activity = _activitySource.StartActivity("GetListMessageStreamData");
            var loEx = new R_Exception();
            List<GSM12000DTO> loRtn = null;
            _Logger.LogInfo("Start GetListMessageStreamData");

            try
            {
                var loCls = new GSM12000Cls();
                _Logger.LogInfo("Set Param GetListMessageStreamData");
                string lcMessageType = R_Utility.R_GetStreamingContext<string>(ContextConstant.CMESSAGE_TYPE);


                _Logger.LogInfo("Call Back Method GetListMessageStreamData");
                loRtn = await loCls.GetListMessageAsync(lcMessageType);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetListMessageStreamData");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public async Task<GSM12000SingleResult<GSM12000DTO>> ActiveInActiveMessageAsync(GSM12000DTO poEntity)
        {

            using Activity activity = _activitySource.StartActivity("ActiveInActiveMessageAsync");
            var loEx = new R_Exception();
            GSM12000SingleResult<GSM12000DTO> loRtn = new GSM12000SingleResult<GSM12000DTO>();
            _Logger.LogInfo("Start ActiveInActiveMessageAsync");

            try
            {
                var loCls = new GSM12000Cls();

                _Logger.LogInfo("Call Back Method ActiveInActiveMessageAsync");
                await loCls.ChangeStatusActiveClsAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ActiveInActiveMessageAsync");

            return loRtn;
        }
        
        [HttpPost]
        public GSM12000DTO GetActiveInactive(GSM12000DTO poParamDto)
        {
            using Activity activity = _activitySource.StartActivity("GetActiveInactive");
            _Logger.LogInfo("Begin || GetActiveInactive(Controller)");
            R_Exception loException = new R_Exception();
            GSM12000DTO loParam = new GSM12000DTO();
            GSM12000DTO loRtn = new GSM12000DTO();
            GSM12000Cls loCls = new GSM12000Cls();

            try
            {
                _Logger.LogInfo("Set Parameter || GetActiveInactive(Controller)");
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CMESSAGE_TYPE = poParamDto.CMESSAGE_TYPE;
                loParam.CMESSAGE_NO = poParamDto.CMESSAGE_NO;
                loParam.LACTIVE = poParamDto.LACTIVE;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                _Logger.LogInfo("Run ActiveInactiveCls || GetActiveInactive(Controller)");
                loCls.ChangeStatusActiveClsAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _Logger.LogInfo("End || GetActiveInactive(Controller)");
            return loRtn;
        }

        [HttpPost]
        public async Task<R_ServiceGetRecordResultDTO<GSM12000DTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM12000DTO> poParameter)
        {

            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM12000DTO> loRtn = new R_ServiceGetRecordResultDTO<GSM12000DTO>();
            _Logger.LogInfo("Start R_ServiceGetRecord");

            try
            {
                var loCls = new GSM12000Cls();

                _Logger.LogInfo("Call Back Method R_ServiceGetRecord");
                loRtn.data = await loCls.R_GetRecordAsync(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceGetRecord");

            return loRtn;
        }

        [HttpPost]
        public async Task<R_ServiceSaveResultDTO<GSM12000DTO>> R_ServiceSave(R_ServiceSaveParameterDTO<GSM12000DTO> poParameter)
        {

            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<GSM12000DTO> loRtn = new R_ServiceSaveResultDTO<GSM12000DTO>();
            _Logger.LogInfo("Start ServiceSave GSM12000");

            try
            {
                var loCls = new GSM12000Cls();

                _Logger.LogInfo("Call Back Method R_Save GSM12000Cls");
                loRtn.data = await loCls.R_SaveAsync(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave GSM12000");

            return loRtn;
        }

        [HttpPost]
        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM12000DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete GSM12000");

            try
            {
                var loCls = new GSM12000Cls();

                _Logger.LogInfo("Call Back Method R_Delete GSM12000Cls");
                await loCls.R_DeleteAsync(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete GSM12000");

            return loRtn;
        }
    }
}