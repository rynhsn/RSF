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
    public class PMT01310Controller : ControllerBase, IPMT01310
    {
        private LoggerPMT01310 _Logger;
        private readonly ActivitySource _activitySource;
        public PMT01310Controller(ILogger<LoggerPMT01310> logger)
        {
            //Initial and Get Logger
            LoggerPMT01310.R_InitializeLogger(logger);
            _Logger = LoggerPMT01310.R_GetInstanceLogger();
            _activitySource = PMT01310ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT01310Controller));
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
        public R_ServiceGetRecordResultDTO<PMT01310DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT01310DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMT01310DTO> loRtn = new R_ServiceGetRecordResultDTO<PMT01310DTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMT01310");

            try
            {
                var loCls = new PMT01310Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord PMT01310Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord PMT01310");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMT01310DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT01310DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMT01310DTO> loRtn = new R_ServiceSaveResultDTO<PMT01310DTO>();
            _Logger.LogInfo("Start ServiceSave PMT01310");

            try
            {
                var loCls = new PMT01310Cls();

                _Logger.LogInfo("Call Back Method R_Save PMT01310Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave PMT01310");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT01310DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete PMT01310");

            try
            {
                var loCls = new PMT01310Cls();

                _Logger.LogInfo("Call Back Method R_Delete PMT01310Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete PMT01310");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01310DTO> GetLOIUnitListStream()
        {
            using Activity activity = _activitySource.StartActivity("GetLOIUnitListStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01310DTO> loRtn = null;
            _Logger.LogInfo("Start GetLOIUnitListStream");

            try
            {
                _Logger.LogInfo("Set Param GetLOIUnitListStream");
                PMT01310DTO loEntity = new PMT01310DTO();
                loEntity.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loEntity.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loEntity.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);

                _Logger.LogInfo("Call Back Method GetAllLOIUnit");
                var loCls = new PMT01310Cls();
                var loTempRtn = loCls.GetAllLOIUnit(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetLOIUnitListStream");
                loRtn = GetStreamData<PMT01310DTO>(loTempRtn);
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