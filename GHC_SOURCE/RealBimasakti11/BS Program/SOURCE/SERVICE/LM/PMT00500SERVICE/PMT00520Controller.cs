using PMT00500BACK;
using PMT00500COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace PMT00500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT00520Controller : ControllerBase, IPMT00520
    {
        private LoggerPMT00520 _Logger;
        private readonly ActivitySource _activitySource;
        public PMT00520Controller(ILogger<LoggerPMT00520> logger)
        {
            //Initial and Get Logger
            LoggerPMT00520.R_InitializeLogger(logger);
            _Logger = LoggerPMT00520.R_GetInstanceLogger();
            _activitySource = PMT00520ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT00520Controller));
        }

        #region Stream Data
        private async IAsyncEnumerable<T> GetStreamData<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
        #endregion

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMT00520DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT00520DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMT00520DTO> loRtn = new R_ServiceGetRecordResultDTO<PMT00520DTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMT00520");

            try
            {
                var loCls = new PMT00520Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord PMT00520Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord PMT00520");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMT00520DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT00520DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMT00520DTO> loRtn = new R_ServiceSaveResultDTO<PMT00520DTO>();
            _Logger.LogInfo("Start ServiceSave PMT00520");

            try
            {
                var loCls = new PMT00520Cls();

                _Logger.LogInfo("Call Back Method R_Save PMT00520Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave PMT00520");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT00520DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete PMT00520");

            try
            {
                var loCls = new PMT00520Cls();

                _Logger.LogInfo("Call Back Method R_Delete PMT00520Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete PMT00520");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT00520DTO> GetLOIUtilitiesStream()
        {
            using Activity activity = _activitySource.StartActivity("GetLOIUtilitiesStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT00520DTO> loRtn = null;
            _Logger.LogInfo("Start GetLOIUtilitiesStream");

            try
            {
                _Logger.LogInfo("Set Param GetLOIUtilitiesStream");
                PMT00520DTO loEntity = new PMT00520DTO();
                loEntity.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loEntity.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loEntity.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);
                loEntity.CUNIT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUNIT_ID);
                loEntity.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CFLOOR_ID);
                loEntity.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CBUILDING_ID);

                _Logger.LogInfo("Call Back Method GetAllLOI");
                var loCls = new PMT00520Cls();
                var loTempRtn = loCls.GetAllLOIUtilities(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetLOIUtilitiesStream");
                loRtn = GetStreamData<PMT00520DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetLOIUtilitiesStream");

            return loRtn;
        }

        [HttpPost]
        public PMT00500SingleResult<PMT00520ActiveInactiveDTO> ChangeStatusLOIUtility(PMT00520ActiveInactiveDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("ChangeStatusLOIUtility");
            var loEx = new R_Exception();
            PMT00500SingleResult<PMT00520ActiveInactiveDTO> loRtn = new PMT00500SingleResult<PMT00520ActiveInactiveDTO>();
            _Logger.LogInfo("Start ChangeStatusLOIUtility");

            try
            {
                var loCls = new PMT00520Cls();

                _Logger.LogInfo("Call Back Method ChangeStatusLOIUtility");
                loCls.ChangeStatusLOIUtility(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ChangeStatusLOIUtility");

            return loRtn;
        }
    }
}