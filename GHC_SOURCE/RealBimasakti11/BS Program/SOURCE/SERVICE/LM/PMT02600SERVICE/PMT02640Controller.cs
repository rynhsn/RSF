using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT02600BACK.OpenTelemetry;
using PMT02600BACK;
using PMT02600COMMON.DTOs.PMT02640;
using PMT02600COMMON.Loggers;
using PMT02600COMMON;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT02600COMMON.DTOs;

namespace PMT02600SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT02640Controller : ControllerBase, IPMT02640
    {
        private LoggerPMT02640 _Logger;
        private readonly ActivitySource _activitySource;
        public PMT02640Controller(ILogger<LoggerPMT02640> logger)
        {
            //Initial and Get Logger
            LoggerPMT02640.R_InitializeLogger(logger);
            _Logger = LoggerPMT02640.R_GetInstanceLogger();
            _activitySource = PMT02640ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT02640Controller));
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
        public R_ServiceGetRecordResultDTO<PMT02640DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT02640DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMT02640DTO> loRtn = new R_ServiceGetRecordResultDTO<PMT02640DTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMT02640");

            try
            {
                var loCls = new PMT02640Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord PMT02640Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord PMT02640");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMT02640DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT02640DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMT02640DTO> loRtn = new R_ServiceSaveResultDTO<PMT02640DTO>();
            _Logger.LogInfo("Start ServiceSave PMT02640");

            try
            {
                var loCls = new PMT02640Cls();

                _Logger.LogInfo("Call Back Method R_Save PMT02640Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave PMT02640");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT02640DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete PMT02640");

            try
            {
                var loCls = new PMT02640Cls();

                _Logger.LogInfo("Call Back Method R_Delete PMT02640Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete PMT02640");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT02640DTO> GetAgreementDocumentStream()
        {
            using Activity activity = _activitySource.StartActivity("GetAgreementDocumentStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT02640DTO> loRtn = null;
            _Logger.LogInfo("Start GetAgreementDocumentStream");

            try
            {
                _Logger.LogInfo("Set Param GetAgreementDocumentStream");
                PMT02640DTO loEntity = new PMT02640DTO();
                loEntity = R_Utility.R_GetStreamingContext<PMT02640DTO>(ContextConstant.PMT02640_AGREEMENT_DOCUMENT_STREAMING_CONTEXT);

                _Logger.LogInfo("Call Back Method GetAllAgreementDocument");
                var loCls = new PMT02640Cls();
                var loTempRtn = loCls.GetAllAgreementDocument(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetAgreementDocumentStream");
                loRtn = GetStreamData<PMT02640DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAgreementDocumentStream");

            return loRtn;
        }

    }
}