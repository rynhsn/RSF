using PMT01300BACK;
using PMT01300COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace PMT01300SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT01320Controller : ControllerBase, IPMT01320
    {
        private LoggerPMT01320 _Logger;
        private readonly ActivitySource _activitySource;
        public PMT01320Controller(ILogger<LoggerPMT01320> logger)
        {
            //Initial and Get Logger
            LoggerPMT01320.R_InitializeLogger(logger);
            _Logger = LoggerPMT01320.R_GetInstanceLogger();
            _activitySource = PMT01320ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT01320Controller));
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
        public R_ServiceGetRecordResultDTO<PMT01320DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT01320DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMT01320DTO> loRtn = new R_ServiceGetRecordResultDTO<PMT01320DTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMT01320");

            try
            {
                var loCls = new PMT01320Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord PMT01320Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord PMT01320");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMT01320DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT01320DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMT01320DTO> loRtn = new R_ServiceSaveResultDTO<PMT01320DTO>();
            _Logger.LogInfo("Start ServiceSave PMT01320");

            try
            {
                var loCls = new PMT01320Cls();

                _Logger.LogInfo("Call Back Method R_Save PMT01320Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave PMT01320");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT01320DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete PMT01320");

            try
            {
                var loCls = new PMT01320Cls();

                _Logger.LogInfo("Call Back Method R_Delete PMT01320Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete PMT01320");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01320DTO> GetLOIUtilitiesStream()
        {
            using Activity activity = _activitySource.StartActivity("GetLOIUtilitiesStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01320DTO> loRtn = null;
            _Logger.LogInfo("Start GetLOIUtilitiesStream");

            try
            {
                _Logger.LogInfo("Set Param GetLOIUtilitiesStream");
                PMT01320DTO loEntity = new PMT01320DTO();
                loEntity.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loEntity.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loEntity.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);
                loEntity.CUNIT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUNIT_ID);
                loEntity.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CFLOOR_ID);
                loEntity.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CBUILDING_ID);

                _Logger.LogInfo("Call Back Method GetAllLOI");
                var loCls = new PMT01320Cls();
                var loTempRtn = loCls.GetAllLOIUtilities(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetLOIUtilitiesStream");
                loRtn = GetStreamData<PMT01320DTO>(loTempRtn);
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
        public PMT01300SingleResult<PMT01320ActiveInactiveDTO> ChangeStatusLOIUtility(PMT01320ActiveInactiveDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("ChangeStatusLOIUtility");
            var loEx = new R_Exception();
            PMT01300SingleResult<PMT01320ActiveInactiveDTO> loRtn = new PMT01300SingleResult<PMT01320ActiveInactiveDTO>();
            _Logger.LogInfo("Start ChangeStatusLOIUtility");

            try
            {
                var loCls = new PMT01320Cls();

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