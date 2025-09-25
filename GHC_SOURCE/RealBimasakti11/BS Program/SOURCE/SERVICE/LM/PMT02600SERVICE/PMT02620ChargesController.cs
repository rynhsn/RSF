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
    public class PMT02620ChargesController : ControllerBase, IPMT02620Charges
    {
        private LoggerPMT02620Charges _Logger;
        private readonly ActivitySource _activitySource;
        public PMT02620ChargesController(ILogger<LoggerPMT02620Charges> logger)
        {
            //Initial and Get Logger
            LoggerPMT02620Charges.R_InitializeLogger(logger);
            _Logger = LoggerPMT02620Charges.R_GetInstanceLogger();
            _activitySource = PMT02620ChargesActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT02620ChargesController));
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
        public R_ServiceGetRecordResultDTO<PMT02620ChargesDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT02620ChargesDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMT02620ChargesDTO> loRtn = new R_ServiceGetRecordResultDTO<PMT02620ChargesDTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMT02620");

            try
            {
                var loCls = new PMT02620ChargesCls();

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
        public R_ServiceSaveResultDTO<PMT02620ChargesDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT02620ChargesDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMT02620ChargesDTO> loRtn = new R_ServiceSaveResultDTO<PMT02620ChargesDTO>();
            _Logger.LogInfo("Start ServiceSave PMT02620");

            try
            {
                var loCls = new PMT02620ChargesCls();

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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT02620ChargesDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete PMT02620");

            try
            {
                var loCls = new PMT02620ChargesCls();

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
        public IAsyncEnumerable<PMT02620ChargesDTO> GetAgreementChargeStream()
        {
            using Activity activity = _activitySource.StartActivity("GetAgreementChargeStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT02620ChargesDTO> loRtn = null;
            _Logger.LogInfo("Start GetAgreementChargeStream");

            try
            {
                _Logger.LogInfo("Set Param GetAgreementChargeStream");
                PMT02620ChargesDTO loEntity = new PMT02620ChargesDTO();
                loEntity = R_Utility.R_GetStreamingContext<PMT02620ChargesDTO>(ContextConstant.PMT02620_CHARGES_GET_AGREEMENT_CHARGE_STREAMING_CONTEXT);

                _Logger.LogInfo("Call Back Method GetAllAgreementCharges");
                var loCls = new PMT02620ChargesCls();
                var loTempRtn = loCls.GetAllAgreementCharges(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetAgreementChargeStream");
                loRtn = GetStreamData<PMT02620ChargesDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAgreementChargeStream");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<CodeDescDTO> GetFeeMethod()
        {
            using Activity activity = _activitySource.StartActivity("GetFeeMethod");
            var loEx = new R_Exception();
            IAsyncEnumerable<CodeDescDTO> loRtn = null;
            _Logger.LogInfo("Start GetFeeMethod");

            try
            {
                _Logger.LogInfo("Call Back Method GetFeeMethod");
                var loCls = new PMT02620ChargesCls();
                var loTempRtn = loCls.GetFeeMethod();

                _Logger.LogInfo("Call Stream Method Data GetFeeMethod");
                loRtn = GetStreamData<CodeDescDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetFeeMethod");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT02620AgreementChargeCalUnitDTO> GetAllAgreementChargeCallUnitList()
        {
            using Activity activity = _activitySource.StartActivity("GetAllAgreementChargeCallUnitList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT02620AgreementChargeCalUnitDTO> loRtn = null;
            _Logger.LogInfo("Start GetAllAgreementChargeCallUnitList");

            try
            {
                _Logger.LogInfo("Set Param GetAllAgreementChargeCallUnitList");
                var poParam = new PMT02620ParameterAgreementChargeCalUnitDTO();
                poParam = R_Utility.R_GetStreamingContext<PMT02620ParameterAgreementChargeCalUnitDTO>(ContextConstant.PMT02620_CHARGES_FOR_TOTAL_AREA_STREAMING_CONTEXT);

                _Logger.LogInfo("Call Back Method GetAllAgreementChargesCallUnit");
                var loCls = new PMT02620ChargesCls();
                var loTempRtn = loCls.GetAllAgreementChargesCallUnit(poParam);

                _Logger.LogInfo("Call Stream Method Data GetAllAgreementChargeCallUnitList");
                loRtn = GetStreamData<PMT02620AgreementChargeCalUnitDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllAgreementChargeCallUnitList");

            return loRtn;
        }
    }
}