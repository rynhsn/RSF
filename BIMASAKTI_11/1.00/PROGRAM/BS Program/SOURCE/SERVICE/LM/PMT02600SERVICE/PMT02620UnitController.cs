using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class PMT02620UnitController : ControllerBase, IPMT02620Unit
    {
        private LoggerPMT02620Unit _Logger;
        private readonly ActivitySource _activitySource;
        public PMT02620UnitController(ILogger<LoggerPMT02620Unit> logger)
        {
            //Initial and Get Logger
            LoggerPMT02620Unit.R_InitializeLogger(logger);
            _Logger = LoggerPMT02620Unit.R_GetInstanceLogger();
            _activitySource = PMT02620UnitActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT02620UnitController));
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
        public R_ServiceGetRecordResultDTO<PMT02620UnitDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT02620UnitDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMT02620UnitDTO> loRtn = new R_ServiceGetRecordResultDTO<PMT02620UnitDTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMT02620Unit");

            try
            {
                var loCls = new PMT02620UnitCls();

                _Logger.LogInfo("Call Back Method R_GetRecord PMT02620UnitCls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord PMT02620Unit");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMT02620UnitDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT02620UnitDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMT02620UnitDTO> loRtn = new R_ServiceSaveResultDTO<PMT02620UnitDTO>();
            _Logger.LogInfo("Start ServiceSave PMT02620Unit");

            try
            {
                var loCls = new PMT02620UnitCls();

                _Logger.LogInfo("Call Back Method R_Save PMT02620UnitCls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave PMT02620Unit");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT02620UnitDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete PMT02620Unit");

            try
            {
                var loCls = new PMT02620UnitCls();

                _Logger.LogInfo("Call Back Method R_Delete PMT02620UnitCls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete PMT02620Unit");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT02620UnitDTO> GetAgreementUnitListStream()
        {
            using Activity activity = _activitySource.StartActivity("GetAgreementUnitListStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT02620UnitDTO> loRtn = null;
            _Logger.LogInfo("Start GetAgreementUnitListStream");

            try
            {
                _Logger.LogInfo("Set Param GetAgreementUnitListStream");
                PMT02620UnitDTO loEntity = new PMT02620UnitDTO();
                loEntity = R_Utility.R_GetStreamingContext<PMT02620UnitDTO>(ContextConstant.PMT02620_UNIT_GET_AGREEMENT_UNIT_LIST_STREAMING_CONTEXT);

                _Logger.LogInfo("Call Back Method GetAllAgreement");
                var loCls = new PMT02620UnitCls();
                var loTempRtn = loCls.GetAllAgreementUnit(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetAgreementUnitListStream");
                loRtn = GetStreamData<PMT02620UnitDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAgreementUnitListStream");

            return loRtn;
        }
    }
}