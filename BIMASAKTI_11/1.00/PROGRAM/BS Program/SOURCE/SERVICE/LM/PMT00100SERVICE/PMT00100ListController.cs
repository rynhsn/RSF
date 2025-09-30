using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT00100BACK;
using PMT00100COMMON.Interface;
using PMT00100COMMON.Logger;
using PMT00100COMMON.UnitList;
using PMT00100COMMON.UtilityDTO;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT00100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT00100ListController : ControllerBase, IPMT00100List
    {
        private LoggerPMT00100 _logger;
        private readonly ActivitySource _activitySource;

        public PMT00100ListController(ILogger<PMT00100ListController> logger)
        {
            //Initial and Get Logger
            LoggerPMT00100.R_InitializeLogger(logger);
            _logger = LoggerPMT00100.R_GetInstanceLogger();
            _activitySource = PMT00100Activity.R_InitializeAndGetActivitySource(nameof(PMT00100ListController));

        }


        [HttpPost]
        public IAsyncEnumerable<PMT00100DTO> GetBuildingUnitList()
        {
            string lcMethodName = nameof(GetBuildingUnitList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMT00100DTO>? loRtn = null;
            List<PMT00100DTO> loRtnTemp;
            try
            {
                var loDbParameter = new PMT00100DbParameterDTO();
                var loCls = new PMT00100ListCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbParameter.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CBUILDING_ID);
                loDbParameter.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CFLOOR_ID);
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CUNIT_CATEGORY_LIST = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUNIT_CATEGORY_LIST);

                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo("Call method on Cls");
                loRtnTemp = loCls.GetBuildingUnitList(loDbParameter);
                _logger.LogInfo("Call method to streaming data");
                loRtn = HelperStream(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        [HttpPost]
        public IAsyncEnumerable<PMT00100BuildingDTO> GetBuildingList()
        {
            string lcMethodName = nameof(GetBuildingList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMT00100BuildingDTO>? loRtn = null;
            List<PMT00100BuildingDTO> loRtnTemp;
            try
            {
                var loDbParameter = new PMT00100DbParameterDTO();
                var loCls = new PMT00100ListCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo("Call method on Cls");
                loRtnTemp = loCls.GetBuildingList(loDbParameter);
                _logger.LogInfo("Call method to streaming data");
                loRtn = HelperStream(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        [HttpPost]
        public IAsyncEnumerable<PMT00100AgreementByUnitDTO> GetAgreementByUnitList()
        {
            string lcMethodName = nameof(GetAgreementByUnitList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMT00100AgreementByUnitDTO>? loRtn = null;
            List<PMT00100AgreementByUnitDTO> loRtnTemp;
            try
            {
                var loDbParameter = new PMT00100DbParameterDTO();
                var loCls = new PMT00100ListCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbParameter.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CBUILDING_ID);
                loDbParameter.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CFLOOR_ID);
                loDbParameter.CUNIT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUNIT_ID);
                loDbParameter.LOTHER_UNIT = R_Utility.R_GetStreamingContext<bool>(ContextConstant.LOTHER_UNIT);
                loDbParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTRANS_CODE);
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo("Call method on Cls");
                loRtnTemp = loCls.GetAgreementByUnitList(loDbParameter);
                _logger.LogInfo("Call method to streaming data");
                loRtn = HelperStream(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async IAsyncEnumerable<T> HelperStream<T>(List<T> poParameter)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }

        }
    }
}
