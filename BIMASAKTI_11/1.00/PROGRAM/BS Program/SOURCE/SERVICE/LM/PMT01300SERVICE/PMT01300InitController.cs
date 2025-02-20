using PMT01300BACK;
using PMT01300COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System.Diagnostics;

namespace PMT01300SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT01300InitController : ControllerBase, IPMT01300Init
    {
        private LoggerPMT01300Init _Logger;
        private readonly ActivitySource _activitySource;
        public PMT01300InitController(ILogger<LoggerPMT01300Init> logger)
        {
            //Initial and Get Logger
            LoggerPMT01300Init.R_InitializeLogger(logger);
            _Logger = LoggerPMT01300Init.R_GetInstanceLogger();
            _activitySource = PMT01300ActivityInitSourceBase.R_InitializeAndGetActivitySource(nameof(PMT01300InitController));
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
        public PMT01300SingleResult<PMT01300TransCodeInfoGSDTO> GetTransCodeInfo()
        {
            using Activity activity = _activitySource.StartActivity("GetAllInitialProcessTabList");
            var loEx = new R_Exception();
            PMT01300SingleResult<PMT01300TransCodeInfoGSDTO> loRtn = new PMT01300SingleResult<PMT01300TransCodeInfoGSDTO>();
            _Logger.LogInfo("Start GetAllInitialProcessTabList");

            try
            {
                var loCls = new PMT01300InitCls();

                _Logger.LogInfo("Call Back Method");
                loRtn.Data = loCls.GetTransCodeInfoGS();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllInitialProcessTabList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01300AgreementChargeCalUnitDTO> GetAllAgreementChargeCallUnitList()
        {
            using Activity activity = _activitySource.StartActivity("GetAllAgreementChargeCallUnitList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01300AgreementChargeCalUnitDTO> loRtn = null;
            _Logger.LogInfo("Start GetAllAgreementChargeCallUnitList");

            try
            {
                _Logger.LogInfo("Set Param GetAllAgreementChargeCallUnitList");
                var poParam = new PMT01300ParameterAgreementChargeCalUnitDTO();
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                poParam.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);
                poParam.CSEQ_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSEQ_NO);

                _Logger.LogInfo("Call Back Method GetAllAgreementChargesCallUnit");
                var loCls = new PMT01300InitCls();
                var loTempRtn = loCls.GetAllAgreementChargesCallUnit(poParam);

                _Logger.LogInfo("Call Stream Method Data GetAllAgreementChargeCallUnitList");
                loRtn = GetStreamData<PMT01300AgreementChargeCalUnitDTO>(loTempRtn);
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

        [HttpPost]
        public IAsyncEnumerable<PMT01300PropertyDTO> GetAllPropertyList()
        {
            using Activity activity = _activitySource.StartActivity("GetAllPropertyList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01300PropertyDTO> loRtn = null;
            _Logger.LogInfo("Start GetAllPropertyList");

            try
            {
                _Logger.LogInfo("Call Back Method GetAllProperty");
                var loCls = new PMT01300InitCls();
                var loTempRtn = loCls.GetAllProperty();

                _Logger.LogInfo("Call Stream Method Data GetAllPropertyList");
                loRtn = GetStreamData<PMT01300PropertyDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllPropertyList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01300UniversalDTO> GetAllUniversalList()
        {
            using Activity activity = _activitySource.StartActivity("GetAllUniversalList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01300UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetAllUniversalList");

            try
            {
                _Logger.LogInfo("Set Param GetAllAgreementChargeCallUnitList");
                string lcParameter = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPARAMETER);

                _Logger.LogInfo("Call Back Method GetUniversalList");
                var loCls = new PMT01300InitCls();
                var loTempRtn = loCls.GetUniversalList(lcParameter);

                _Logger.LogInfo("Call Stream Method Data GetAllUniversalList");
                loRtn = GetStreamData<PMT01300UniversalDTO>(loTempRtn);
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
        public IAsyncEnumerable<PMT01300AgreementBuildingUtilitiesDTO> GetAllBuildingUtilitiesList()
        {
            using Activity activity = _activitySource.StartActivity("GetAllBuildingUtilitiesList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01300AgreementBuildingUtilitiesDTO> loRtn = null;
            _Logger.LogInfo("Start GetAllBuildingUtilitiesList");

            try
            {
                _Logger.LogInfo("Set Param GetAllBuildingUtilitiesList");
                var poParam = new PMT01300ParameterAgreementBuildingUtilitiesDTO();
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CBUILDING_ID);
                poParam.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CFLOOR_ID);
                poParam.CUNIT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUNIT_ID);
                poParam.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_TYPE);

                _Logger.LogInfo("Call Back Method GetAllBuildingUtilities");
                var loCls = new PMT01300InitCls();
                var loTempRtn = loCls.GetAllBuildingUtilities(poParam);

                _Logger.LogInfo("Call Stream Method Data GetAllBuildingUtilitiesList");
                loRtn = GetStreamData<PMT01300AgreementBuildingUtilitiesDTO>(loTempRtn);
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