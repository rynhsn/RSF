using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMM09000Back;
using PMM09000COMMON.Amortization_Entry_DTO;
using PMM09000COMMON.Amortization_List_DTO;
using PMM09000COMMON.Interface;
using PMM09000COMMON.Logs;
using PMM09000COMMON.UtiliyDTO;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PMM09000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMM09000ListController : ControllerBase, IPMM09000List
    {
        private LoggerPMM09000 _logger;
        private readonly ActivitySource _activitySource;

        public PMM09000ListController(ILogger<PMM09000ListController> logger)
        {
            //Initial and Get Logger
            LoggerPMM09000.R_InitializeLogger(logger);
            _logger = LoggerPMM09000.R_GetInstanceLogger();
            _activitySource = PMM09000Activity.R_InitializeAndGetActivitySource(nameof(PMM09000ListController));

        }

        [HttpPost]
        public IAsyncEnumerable<PMM09000BuildingDTO> GetBuldingList()
        {
            string lcMethodName = nameof(GetBuldingList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMM09000BuildingDTO>? loRtn = null;
            List<PMM09000BuildingDTO> loRtnTemp;
            try
            {
                var loDbParameter = new PMM09000DbParameterDTO();
                var loCls = new PMM09000ClsList();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);

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
        public IAsyncEnumerable<PMM09000AmortizationDTO> GetAmortizationList()
        {
            string lcMethodName = nameof(GetAmortizationList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMM09000AmortizationDTO>? loRtn = null;
            List<PMM09000AmortizationDTO> loRtnTemp;
            try
            {
                var loDbParameter = new PMM09000DbParameterDTO();
                var loCls = new PMM09000ClsList();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbParameter.CUNIT_OPTION = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUNIT_OPTION);
                loDbParameter.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CBUILDING_ID);
                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo("Call method on Cls");
                loRtnTemp = loCls.GetAmortizationList(loDbParameter);
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
        public IAsyncEnumerable<PMM09000AmortizationSheduleDetailDTO> GetAmortizationScheduleList()
        {
            string lcMethodName = nameof(GetAmortizationScheduleList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMM09000AmortizationSheduleDetailDTO>? loRtn = null;
            List<PMM09000AmortizationSheduleDetailDTO> loRtnTemp;
            try
            {
                var loDbParameter = new PMM09000DbParameterDTO();
                var loCls = new PMM09000ClsList();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbParameter.CUNIT_OPTION = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUNIT_OPTION);
                loDbParameter.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CBUILDING_ID);
                loDbParameter.CTRANS_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTRANS_TYPE);
                loDbParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);

                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo("Call method on Cls");
                List<PMM09000AmortizationSheduleDetailDTO> loReturnTemp = loCls.GetAmortizationScheduleList(loDbParameter);
                //    var loReturnTempConvert = R_Utility.R_ConvertCollectionToCollection<PMM09000AmortizationSheduleDetailDTO, PMM09000AmortizationSheduleDetailDTO>(loReturnTemp).ToList();
                _logger.LogInfo("Call method to streaming data");
                loRtn = HelperStream(loReturnTemp);
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
        public IAsyncEnumerable<PMM09000AmortizationChargesDTO> GetAmortizationChargesList()
        {
            string lcMethodName = nameof(GetAmortizationChargesList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMM09000AmortizationChargesDTO>? loRtn = null;
            List<PMM09000AmortizationChargesDTO> loRtnTemp;
            try
            {
                var loDbParameter = new PMM09000DbParameterDTO();
                var loCls = new PMM09000ClsList();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbParameter.CUNIT_OPTION = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUNIT_OPTION);
                loDbParameter.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CBUILDING_ID);
                loDbParameter.CTRANS_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTRANS_TYPE);
                loDbParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);

                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo("Call method on Cls");
                List<PMM09000AmortizationChargesDTO> loReturnTemp = loCls.GetAmortizationChargesList(loDbParameter);
                //    var loReturnTempConvert = R_Utility.R_ConvertCollectionToCollection<PMM09000AmortizationSheduleDetailDTO, PMM09000AmortizationSheduleDetailDTO>(loReturnTemp).ToList();
                _logger.LogInfo("Call method to streaming data");
                loRtn = HelperStream(loReturnTemp);
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
        public IAsyncEnumerable<ChargesDTO> GetChargesTypeList()
        {
            string lcMethodName = nameof(GetChargesTypeList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<ChargesDTO>? loRtn = null;
            List<ChargesDTO> loRtnTemp;
            try
            {
                var loDbParameter = new PMM09000DbParameterDTO();
                var loCls = new PMM09000ClsList();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo("Call method on Cls");
                loRtnTemp = loCls.ChargesTypeList(loDbParameter);
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
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }


    }
}
