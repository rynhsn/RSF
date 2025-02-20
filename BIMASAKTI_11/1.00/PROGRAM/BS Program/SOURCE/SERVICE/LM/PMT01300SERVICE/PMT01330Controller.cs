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
    public class PMT01330Controller : ControllerBase, IPMT01330
    {
        private LoggerPMT01330 _Logger;
        private readonly ActivitySource _activitySource;
        public PMT01330Controller(ILogger<LoggerPMT01330> logger)
        {
            //Initial and Get Logger
            LoggerPMT01330.R_InitializeLogger(logger);
            _Logger = LoggerPMT01330.R_GetInstanceLogger();
            _activitySource = PMT01330ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT01330Controller));
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
        public R_ServiceGetRecordResultDTO<PMT01330DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT01330DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMT01330DTO> loRtn = new R_ServiceGetRecordResultDTO<PMT01330DTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMT01330");

            try
            {
                var loCls = new PMT01330Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord PMT01330Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord PMT01330");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMT01330DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT01330DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMT01330DTO> loRtn = new R_ServiceSaveResultDTO<PMT01330DTO>();
            _Logger.LogInfo("Start ServiceSave PMT01330");

            try
            {
                var loCls = new PMT01330Cls();

                _Logger.LogInfo("Call Back Method R_Save PMT01330Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave PMT01330");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT01330DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete PMT01330");

            try
            {
                var loCls = new PMT01330Cls();

                _Logger.LogInfo("Call Back Method R_Delete PMT01330Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete PMT01330");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01330DTO> GetLOIChargeStream()
        {
            using Activity activity = _activitySource.StartActivity("GetLOIChargeStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01330DTO> loRtn = null;
            _Logger.LogInfo("Start GetLOIChargeStream");

            try
            {
                _Logger.LogInfo("Set Param GetLOIChargeStream");
                PMT01330DTO loEntity = new PMT01330DTO();
                loEntity.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loEntity.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loEntity.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);
                loEntity.CUNIT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUNIT_ID);
                loEntity.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CFLOOR_ID);
                loEntity.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CBUILDING_ID);
                loEntity.CCHARGE_MODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGE_MODE);

                _Logger.LogInfo("Call Back Method GetAllLOICharges");
                var loCls = new PMT01330Cls();
                var loTempRtn = loCls.GetAllLOICharges(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetLOIChargeStream");
                loRtn = GetStreamData<PMT01330DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetLOIChargeStream");

            return loRtn;
        }

        [HttpPost]
        public PMT01300SingleResult<PMT01330ActiveInactiveDTO> ChangeStatusLOICharges(PMT01330ActiveInactiveDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("ChangeStatusLOICharges");
            var loEx = new R_Exception();
            PMT01300SingleResult<PMT01330ActiveInactiveDTO> loRtn = new PMT01300SingleResult<PMT01330ActiveInactiveDTO>();
            _Logger.LogInfo("Start ChangeStatusLOICharges");

            try
            {
                var loCls = new PMT01330Cls();

                _Logger.LogInfo("Call Back Method ChangeStatusLOICharge");
                loCls.ChangeStatusLOICharge(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ChangeStatusLOICharges");

            return loRtn;
        }
    }
}