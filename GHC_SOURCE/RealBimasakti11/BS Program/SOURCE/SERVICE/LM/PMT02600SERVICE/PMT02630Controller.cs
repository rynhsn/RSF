using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT02600BACK;
using PMT02600BACK.OpenTelemetry;
using PMT02600COMMON;
using PMT02600COMMON.DTOs;
using PMT02600COMMON.DTOs.PMT02630;
using PMT02600COMMON.Loggers;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT02600SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT02630Controller : ControllerBase, IPMT02630
    {
        private LoggerPMT02630 _Logger;
        private readonly ActivitySource _activitySource;
        public PMT02630Controller(ILogger<LoggerPMT02630> logger)
        {
            //Initial and Get Logger
            LoggerPMT02630.R_InitializeLogger(logger);
            _Logger = LoggerPMT02630.R_GetInstanceLogger();
            _activitySource = PMT02630ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT02630Controller));
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
        public R_ServiceGetRecordResultDTO<PMT02630DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT02630DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMT02630DTO> loRtn = new R_ServiceGetRecordResultDTO<PMT02630DTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMT02630");

            try
            {
                var loCls = new PMT02630Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord PMT02630Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord PMT02630");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMT02630DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT02630DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMT02630DTO> loRtn = new R_ServiceSaveResultDTO<PMT02630DTO>();
            _Logger.LogInfo("Start ServiceSave PMT02630");

            try
            {
                var loCls = new PMT02630Cls();

                _Logger.LogInfo("Call Back Method R_Save PMT02630Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave PMT02630");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT02630DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete PMT02630");

            try
            {
                var loCls = new PMT02630Cls();

                _Logger.LogInfo("Call Back Method R_Delete PMT02630Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete PMT02630");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT02630DTO> GetAgreementDepositStream()
        {
            using Activity activity = _activitySource.StartActivity("GetAgreementDepositStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT02630DTO> loRtn = null;
            _Logger.LogInfo("Start GetAgreementDepositStream");

            try
            {
                _Logger.LogInfo("Set Param GetAgreementDepositStream");
                PMT02630DTO loEntity = new PMT02630DTO();
                loEntity = R_Utility.R_GetStreamingContext<PMT02630DTO>(ContextConstant.PMT02630_AGREEMENT_DEPOSIT_STREAMING_CONTEXT);

                _Logger.LogInfo("Call Back Method GetAllAgreementDeposit");
                var loCls = new PMT02630Cls();
                var loTempRtn = loCls.GetAllAgreementDeposit(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetAgreementDepositStream");
                loRtn = GetStreamData<PMT02630DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAgreementDepositStream");

            return loRtn;
        }

    }
}