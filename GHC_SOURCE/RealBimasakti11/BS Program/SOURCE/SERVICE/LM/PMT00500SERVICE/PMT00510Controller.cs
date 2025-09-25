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
    public class PMT00510Controller : ControllerBase, IPMT00510
    {
        private LoggerPMT00510 _Logger;
        private readonly ActivitySource _activitySource;
        public PMT00510Controller(ILogger<LoggerPMT00510> logger)
        {
            //Initial and Get Logger
            LoggerPMT00510.R_InitializeLogger(logger);
            _Logger = LoggerPMT00510.R_GetInstanceLogger();
            _activitySource = PMT00510ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT00510Controller));
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
        public R_ServiceGetRecordResultDTO<PMT00510DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT00510DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMT00510DTO> loRtn = new R_ServiceGetRecordResultDTO<PMT00510DTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMT00510");

            try
            {
                var loCls = new PMT00510Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord PMT00510Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord PMT00510");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMT00510DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT00510DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMT00510DTO> loRtn = new R_ServiceSaveResultDTO<PMT00510DTO>();
            _Logger.LogInfo("Start ServiceSave PMT00510");

            try
            {
                var loCls = new PMT00510Cls();

                _Logger.LogInfo("Call Back Method R_Save PMT00510Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave PMT00510");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT00510DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete PMT00510");

            try
            {
                var loCls = new PMT00510Cls();

                _Logger.LogInfo("Call Back Method R_Delete PMT00510Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete PMT00510");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT00510DTO> GetLOIUnitListStream()
        {
            using Activity activity = _activitySource.StartActivity("GetLOIUnitListStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT00510DTO> loRtn = null;
            _Logger.LogInfo("Start GetLOIUnitListStream");

            try
            {
                _Logger.LogInfo("Set Param GetLOIUnitListStream");
                PMT00510DTO loEntity = new PMT00510DTO();
                loEntity.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loEntity.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loEntity.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);

                _Logger.LogInfo("Call Back Method GetAllLOIUnit");
                var loCls = new PMT00510Cls();
                var loTempRtn = loCls.GetAllLOIUnit(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetLOIUnitListStream");
                loRtn = GetStreamData<PMT00510DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetLOIUnitListStream");

            return loRtn;
        }
    }
}