using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT01400COMMON.DTOs.Helper;
using PMT02600BACK;
using PMT02600BACK.OpenTelemetry;
using PMT02600COMMON;
using PMT02600COMMON.DTOs;
using PMT02600COMMON.DTOs.PMT02620;
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
    public class PMT02620UtilitiesController : ControllerBase, IPMT02620Utilities
    {
        private LoggerPMT02620Utilities _Logger;
        private readonly ActivitySource _activitySource;
        public PMT02620UtilitiesController(ILogger<LoggerPMT02620Utilities> logger)
        {
            //Initial and Get Logger
            LoggerPMT02620Utilities.R_InitializeLogger(logger);
            _Logger = LoggerPMT02620Utilities.R_GetInstanceLogger();
            _activitySource = PMT02620UtilitiesActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT02620UtilitiesController));
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
        public R_ServiceGetRecordResultDTO<PMT02620UtilitiesDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT02620UtilitiesDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMT02620UtilitiesDTO> loRtn = new R_ServiceGetRecordResultDTO<PMT02620UtilitiesDTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMT02620");

            try
            {
                var loCls = new PMT02620UtilitiesCls();

                _Logger.LogInfo("Call Back Method R_GetRecord PMT02620Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord PMT02620");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMT02620UtilitiesDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT02620UtilitiesDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMT02620UtilitiesDTO> loRtn = new R_ServiceSaveResultDTO<PMT02620UtilitiesDTO>();
            _Logger.LogInfo("Start ServiceSave PMT02620");

            try
            {
                var loCls = new PMT02620UtilitiesCls();

                _Logger.LogInfo("Call Back Method R_Save PMT02620Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave PMT02620");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT02620UtilitiesDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete PMT02620");

            try
            {
                var loCls = new PMT02620UtilitiesCls();

                _Logger.LogInfo("Call Back Method R_Delete PMT02620Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete PMT02620");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT02620UtilitiesDTO> GetAgreementUtilitiesStream()
        {
            using Activity activity = _activitySource.StartActivity("GetAgreementUtilitiesStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT02620UtilitiesDTO> loRtn = null;
            _Logger.LogInfo("Start GetAgreementUtilitiesStream");

            try
            {
                _Logger.LogInfo("Set Param GetAgreementUtilitiesStream");
                PMT02620UtilitiesDTO loEntity = new PMT02620UtilitiesDTO();
                loEntity = R_Utility.R_GetStreamingContext<PMT02620UtilitiesDTO>(ContextConstant.PMT02620_UTITLITIES_GET_AGREEMENT_UTILITIES_STREAMING_CONTEXT);

                _Logger.LogInfo("Call Back Method GetAllAgreement");
                var loCls = new PMT02620UtilitiesCls();
                var loTempRtn = loCls.GetAllAgreementUtilities(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetAgreementUtilitiesStream");
                loRtn = GetStreamData<PMT02620UtilitiesDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAgreementUtilitiesStream");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<CodeDescDTO> GetUtilitiyChargesType()
        {
            using Activity activity = _activitySource.StartActivity("GetAllUniversalList");
            var loEx = new R_Exception();
            IAsyncEnumerable<CodeDescDTO> loRtn = null;
            _Logger.LogInfo("Start GetAllUniversalList");

            try
            {
                _Logger.LogInfo("Call Back Method GetUniversalList");
                var loCls = new PMT02620UtilitiesCls();
                var loTempRtn = loCls.GetUtilityCharges();

                _Logger.LogInfo("Call Stream Method Data GetAllUniversalList");
                loRtn = GetStreamData<CodeDescDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllUniversalList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT02620AgreementBuildingUtilitiesDTO> GetAllBuildingUtilitiesList()
        {
            using Activity activity = _activitySource.StartActivity("GetAllBuildingUtilitiesList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT02620AgreementBuildingUtilitiesDTO> loRtn = null;
            _Logger.LogInfo("Start GetAllBuildingUtilitiesList");

            try
            {
                _Logger.LogInfo("Set Param GetAllBuildingUtilitiesList");
                var poParam = new PMT02620ParameterAgreementBuildingUtilitiesDTO();
                poParam = R_Utility.R_GetStreamingContext<PMT02620ParameterAgreementBuildingUtilitiesDTO>(ContextConstant.PMT02620_UTITLITIES_METER_NO_STREAMING_CONTEXT);

                _Logger.LogInfo("Call Back Method GetAllBuildingUtilities");
                var loCls = new PMT02620UtilitiesCls();
                var loTempRtn = loCls.GetAllBuildingUtilities(poParam);

                _Logger.LogInfo("Call Stream Method Data GetAllBuildingUtilitiesList");
                loRtn = GetStreamData<PMT02620AgreementBuildingUtilitiesDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllBuildingUtilitiesList");

            return loRtn;
        }
    }
}