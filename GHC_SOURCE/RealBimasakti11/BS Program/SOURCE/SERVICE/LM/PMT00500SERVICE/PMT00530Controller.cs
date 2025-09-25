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
    public class PMT00530Controller : ControllerBase, IPMT00530
    {
        private LoggerPMT00530 _Logger;
        private readonly ActivitySource _activitySource;
        public PMT00530Controller(ILogger<LoggerPMT00530> logger)
        {
            //Initial and Get Logger
            LoggerPMT00530.R_InitializeLogger(logger);
            _Logger = LoggerPMT00530.R_GetInstanceLogger();
            _activitySource = PMT00530ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT00530Controller));
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
        public R_ServiceGetRecordResultDTO<PMT00530DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT00530DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMT00530DTO> loRtn = new R_ServiceGetRecordResultDTO<PMT00530DTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMT00530");

            try
            {
                var loCls = new PMT00530Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord PMT00530Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord PMT00530");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMT00530DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT00530DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMT00530DTO> loRtn = new R_ServiceSaveResultDTO<PMT00530DTO>();
            _Logger.LogInfo("Start ServiceSave PMT00530");

            try
            {
                var loCls = new PMT00530Cls();

                _Logger.LogInfo("Call Back Method R_Save PMT00530Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave PMT00530");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT00530DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete PMT00530");

            try
            {
                var loCls = new PMT00530Cls();

                _Logger.LogInfo("Call Back Method R_Delete PMT00530Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete PMT00530");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT00530DTO> GetLOIChargeStream()
        {
            using Activity activity = _activitySource.StartActivity("GetLOIChargeStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT00530DTO> loRtn = null;
            _Logger.LogInfo("Start GetLOIChargeStream");

            try
            {
                _Logger.LogInfo("Set Param GetLOIChargeStream");
                PMT00530DTO loEntity = new PMT00530DTO();
                loEntity.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loEntity.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loEntity.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);
                loEntity.CUNIT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUNIT_ID);
                loEntity.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CFLOOR_ID);
                loEntity.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CBUILDING_ID);
                loEntity.CCHARGE_MODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGE_MODE);

                _Logger.LogInfo("Call Back Method GetAllLOICharges");
                var loCls = new PMT00530Cls();
                var loTempRtn = loCls.GetAllLOICharges(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetLOIChargeStream");
                loRtn = GetStreamData<PMT00530DTO>(loTempRtn);
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
        public PMT00500SingleResult<PMT00530ActiveInactiveDTO> ChangeStatusLOICharges(PMT00530ActiveInactiveDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("ChangeStatusLOICharges");
            var loEx = new R_Exception();
            PMT00500SingleResult<PMT00530ActiveInactiveDTO> loRtn = new PMT00500SingleResult<PMT00530ActiveInactiveDTO>();
            _Logger.LogInfo("Start ChangeStatusLOICharges");

            try
            {
                var loCls = new PMT00530Cls();

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