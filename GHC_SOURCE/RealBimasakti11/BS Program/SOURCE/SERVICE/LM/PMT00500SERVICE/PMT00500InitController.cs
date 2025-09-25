using PMT00500BACK;
using PMT00500COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System.Diagnostics;

namespace PMT00500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT00500InitController : ControllerBase, IPMT00500Init
    {
        private LoggerPMT00500Init _Logger;
        private readonly ActivitySource _activitySource;
        public PMT00500InitController(ILogger<LoggerPMT00500Init> logger)
        {
            //Initial and Get Logger
            LoggerPMT00500Init.R_InitializeLogger(logger);
            _Logger = LoggerPMT00500Init.R_GetInstanceLogger();
            _activitySource = PMT00500ActivityInitSourceBase.R_InitializeAndGetActivitySource(nameof(PMT00500InitController));
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
        public PMT00500SingleResult<PMT00500TransCodeInfoGSDTO> GetTransCodeInfo()
        {
            using Activity activity = _activitySource.StartActivity("GetAllInitialProcessTabList");
            var loEx = new R_Exception();
            PMT00500SingleResult<PMT00500TransCodeInfoGSDTO> loRtn = new PMT00500SingleResult<PMT00500TransCodeInfoGSDTO>();
            _Logger.LogInfo("Start GetAllInitialProcessTabList");

            try
            {
                var loCls = new PMT00500InitCls();

                _Logger.LogInfo("Call Back Method");
                string lcEntity = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCALLER_TRANSACTION);

                loRtn.Data = loCls.GetTransCodeInfoGS(lcEntity);
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
        public IAsyncEnumerable<PMT00500AgreementChargeCalUnitDTO> GetAllAgreementChargeCallUnitList()
        {
            using Activity activity = _activitySource.StartActivity("GetAllAgreementChargeCallUnitList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT00500AgreementChargeCalUnitDTO> loRtn = null;
            _Logger.LogInfo("Start GetAllAgreementChargeCallUnitList");

            try
            {
                _Logger.LogInfo("Set Param GetAllAgreementChargeCallUnitList");
                var poParam = new PMT00500ParameterAgreementChargeCalUnitDTO();
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                poParam.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);
                poParam.CSEQ_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSEQ_NO);

                _Logger.LogInfo("Call Back Method GetAllAgreementChargesCallUnit");
                var loCls = new PMT00500InitCls();
                var loTempRtn = loCls.GetAllAgreementChargesCallUnit(poParam);

                _Logger.LogInfo("Call Stream Method Data GetAllAgreementChargeCallUnitList");
                loRtn = GetStreamData<PMT00500AgreementChargeCalUnitDTO>(loTempRtn);
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
        public IAsyncEnumerable<PMT00500PropertyDTO> GetAllPropertyList()
        {
            using Activity activity = _activitySource.StartActivity("GetAllPropertyList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT00500PropertyDTO> loRtn = null;
            _Logger.LogInfo("Start GetAllPropertyList");

            try
            {
                _Logger.LogInfo("Call Back Method GetAllProperty");
                var loCls = new PMT00500InitCls();
                var loTempRtn = loCls.GetAllProperty();

                _Logger.LogInfo("Call Stream Method Data GetAllPropertyList");
                loRtn = GetStreamData<PMT00500PropertyDTO>(loTempRtn);
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
        public IAsyncEnumerable<PMT00500UniversalDTO> GetAllUniversalList()
        {
            using Activity activity = _activitySource.StartActivity("GetAllUniversalList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT00500UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetAllUniversalList");

            try
            {
                _Logger.LogInfo("Set Param GetAllAgreementChargeCallUnitList");
                string lcParameter = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPARAMETER);

                _Logger.LogInfo("Call Back Method GetUniversalList");
                var loCls = new PMT00500InitCls();
                var loTempRtn = loCls.GetUniversalList(lcParameter);

                _Logger.LogInfo("Call Stream Method Data GetAllUniversalList");
                loRtn = GetStreamData<PMT00500UniversalDTO>(loTempRtn);
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
        public IAsyncEnumerable<PMT00500AgreementBuildingUtilitiesDTO> GetAllBuildingUtilitiesList()
        {
            using Activity activity = _activitySource.StartActivity("GetAllBuildingUtilitiesList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT00500AgreementBuildingUtilitiesDTO> loRtn = null;
            _Logger.LogInfo("Start GetAllBuildingUtilitiesList");

            try
            {
                _Logger.LogInfo("Set Param GetAllBuildingUtilitiesList");
                var poParam = new PMT00500ParameterAgreementBuildingUtilitiesDTO();
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CBUILDING_ID);
                poParam.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CFLOOR_ID);
                poParam.CUNIT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUNIT_ID);
                poParam.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_TYPE);

                _Logger.LogInfo("Call Back Method GetAllBuildingUtilities");
                var loCls = new PMT00500InitCls();
                var loTempRtn = loCls.GetAllBuildingUtilities(poParam);

                _Logger.LogInfo("Call Stream Method Data GetAllBuildingUtilitiesList");
                loRtn = GetStreamData<PMT00500AgreementBuildingUtilitiesDTO>(loTempRtn);
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

        [HttpPost]
        public IAsyncEnumerable<PMT00500BuildingDTO> GetAllBuildingList()
        {
            using Activity activity = _activitySource.StartActivity("GetAllBuildingList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT00500BuildingDTO> loRtn = null;
            _Logger.LogInfo("Start GetAllBuildingList");

            try
            {
                _Logger.LogInfo("Set Param GetAllAgreementChargeCallUnitList");
                string lcParameter = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPARAMETER);

                _Logger.LogInfo("Call Back Method GetUniversalList");
                var loCls = new PMT00500InitCls();
                var loTempRtn = loCls.GetBuildingList(lcParameter);

                _Logger.LogInfo("Call Stream Method Data GetAllBuildingList");
                loRtn = GetStreamData<PMT00500BuildingDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllBuildingList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT00500BuildingUnitDTO> GetAllBuildingUnitList()
        {
            using Activity activity = _activitySource.StartActivity("GetAllBuildingUnitList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT00500BuildingUnitDTO> loRtn = null;
            _Logger.LogInfo("Start GetAllBuildingUnitList");

            try
            {
                _Logger.LogInfo("Set Param GetAllBuildingUnitList");
                var poParam = new PMT00500BuildingUnitDTO();
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CBUILDING_ID);

                _Logger.LogInfo("Call Back Method GetAllBuildingUtilities");
                var loCls = new PMT00500InitCls();
                var loTempRtn = loCls.GetAllBuildingUnit(poParam);

                _Logger.LogInfo("Call Stream Method Data GetAllBuildingUnitList");
                loRtn = GetStreamData<PMT00500BuildingUnitDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllBuildingUnitList");

            return loRtn;
        }
    }
}