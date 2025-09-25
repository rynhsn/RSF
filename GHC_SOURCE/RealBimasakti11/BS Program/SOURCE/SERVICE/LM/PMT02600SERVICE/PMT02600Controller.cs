using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT02600BACK;
using PMT02600BACK.OpenTelemetry;
using PMT02600COMMON;
using PMT02600COMMON.DTOs;
using PMT02600COMMON.DTOs.PMT02600;
using PMT02600COMMON.Loggers;
using R_Common;
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
    public class PMT02600Controller : ControllerBase, IPMT02600
    {
        private LoggerPMT02600 _Logger;
        private readonly ActivitySource _activitySource;
        public PMT02600Controller(ILogger<LoggerPMT02600> logger)
        {
            //Initial and Get Logger
            LoggerPMT02600.R_InitializeLogger(logger);
            _Logger = LoggerPMT02600.R_GetInstanceLogger();
            _activitySource = PMT02600ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT02600Controller));
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
        public IAsyncEnumerable<PMT02600DTO> GetAgreementList()
        {
            using Activity activity = _activitySource.StartActivity("GetAgreementList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT02600DTO> loRtn = null;
            PMT02600ParameterDTO loParam = null;
            _Logger.LogInfo("Start GetAgreementList");

            try
            {
                _Logger.LogInfo("Set Param GetAgreementList");
                loParam = R_Utility.R_GetStreamingContext<PMT02600ParameterDTO>(ContextConstant.PMT20600_GET_AGREEMENT_LIST_STREAMING_CONTEXT);

                _Logger.LogInfo("Call Back Method GetAllLOI");
                var loCls = new PMT02600Cls();
                var loTempRtn = loCls.GetAgreementList(loParam);

                _Logger.LogInfo("Call Stream Method Data GetAgreementList");
                loRtn = GetStreamData<PMT02600DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAgreementList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT02600DetailDTO> GetAgreementDetailList()
        {
            using Activity activity = _activitySource.StartActivity("GetAgreementDetailList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT02600DetailDTO> loRtn = null;
            PMT02600DetailParameterDTO loParam = null;
            _Logger.LogInfo("Start GetAgreementDetailList");

            try
            {
                _Logger.LogInfo("Set Param GetAgreementDetailList");
                loParam = R_Utility.R_GetStreamingContext<PMT02600DetailParameterDTO>(ContextConstant.PMT20600_GET_AGREEMENT_DETAIL_LIST_STREAMING_CONTEXT);

                _Logger.LogInfo("Call Back Method GetAllLOI");
                var loCls = new PMT02600Cls();
                var loTempRtn = loCls.GetAgreementDetailList(loParam);

                _Logger.LogInfo("Call Stream Method Data GetAgreementDetailList");
                loRtn = GetStreamData<PMT02600DetailDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAgreementDetailList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GetPropertyListDTO> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity("GetPropertyList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GetPropertyListDTO> loRtn = null;
            GetPropertyListParameterDTO loParam = null;
            _Logger.LogInfo("Start GetPropertyList");

            try
            {
                _Logger.LogInfo("Call Back Method GetAllLOI");
                var loCls = new PMT02600Cls();
                var loTempRtn = loCls.GetPropertyList();

                _Logger.LogInfo("Call Stream Method Data GetPropertyList");
                loRtn = GetStreamData<GetPropertyListDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetPropertyList");

            return loRtn;
        }


        [HttpPost]
        public UpdateStatusResultDTO UpdateAgreementTransStatus(UpdateStatusDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("UpdateAgreementTransStatus");
            var loEx = new R_Exception();
            UpdateStatusResultDTO loRtn = new UpdateStatusResultDTO();
            _Logger.LogInfo("Start UpdateAgreementTransStatus");

            try
            {
                var loCls = new PMT02600Cls();

                _Logger.LogInfo("Call Back Method UpdateAgreementTransStatusDisplay");
                loCls.UpdateAgreementTransStatus(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End UpdateAgreementTransStatus");

            return loRtn;
        }

        [HttpPost]
        public GetTransCodeInfoResultDTO GetTransCodeInfo()
        {
            using Activity activity = _activitySource.StartActivity("GetTransCodeInfo");
            var loEx = new R_Exception();
            GetTransCodeInfoResultDTO loRtn = new GetTransCodeInfoResultDTO();
            _Logger.LogInfo("Start GetTransCodeInfo");

            try
            {
                var loCls = new PMT02600Cls();

                _Logger.LogInfo("Call Back Method GetTransCodeInfoDisplay");
                 loRtn.Data = loCls.GetTransCodeInfo();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetTransCodeInfo");

            return loRtn;
        }
    }
}
