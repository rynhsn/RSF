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
    public class PMT00560Controller : ControllerBase, IPMT00560
    {
        private LoggerPMT00560 _Logger;
        private readonly ActivitySource _activitySource;
        public PMT00560Controller(ILogger<LoggerPMT00560> logger)
        {
            //Initial and Get Logger
            LoggerPMT00560.R_InitializeLogger(logger);
            _Logger = LoggerPMT00560.R_GetInstanceLogger();
            _activitySource = PMT00560ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT00560Controller));
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
        public R_ServiceGetRecordResultDTO<PMT00560DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT00560DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMT00560DTO> loRtn = new R_ServiceGetRecordResultDTO<PMT00560DTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMT00560");

            try
            {
                var loCls = new PMT00560Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord PMT00560Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord PMT00560");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMT00560DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT00560DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMT00560DTO> loRtn = new R_ServiceSaveResultDTO<PMT00560DTO>();
            _Logger.LogInfo("Start ServiceSave PMT00560");

            try
            {
                var loCls = new PMT00560Cls();

                _Logger.LogInfo("Call Back Method R_Save PMT00560Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave PMT00560");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT00560DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete PMT00560");

            try
            {
                var loCls = new PMT00560Cls();

                _Logger.LogInfo("Call Back Method R_Delete PMT00560Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete PMT00560");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT00560DTO> GetLOIDocumentStream()
        {
            using Activity activity = _activitySource.StartActivity("GetLOIDepositStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT00560DTO> loRtn = null;
            _Logger.LogInfo("Start GetLOIDepositStream");

            try
            {
                _Logger.LogInfo("Set Param GetLOIDepositStream");
                PMT00560DTO loEntity = new PMT00560DTO();
                loEntity.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loEntity.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loEntity.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);

                _Logger.LogInfo("Call Back Method GetAllLOIDeposit");
                var loCls = new PMT00560Cls();
                var loTempRtn = loCls.GetAllLOIDocument(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetLOIDepositStream");
                loRtn = GetStreamData<PMT00560DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetLOIDepositStream");

            return loRtn;
        }

    }
}