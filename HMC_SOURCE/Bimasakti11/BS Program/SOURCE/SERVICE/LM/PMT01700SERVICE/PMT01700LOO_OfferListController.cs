using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT01700BACK;
using PMT01700COMMON.Context._1._Other_Untit_List;
using PMT01700COMMON.DTO._1._Other_Unit_List;
using PMT01700COMMON.DTO._2._LOO._1._LOO___Offer_List;
using PMT01700COMMON.DTO.Utilities.ParamDb;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.DTO.Utilities.Print;
using PMT01700COMMON.Interface;
using PMT01700COMMON.Logs;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT01700SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMT01700LOO_OfferListController : ControllerBase, IPMT01700LOO_OfferList
    {
        private readonly LoggerPMT01700? _logger;
        private readonly ActivitySource _activitySource;
        public PMT01700LOO_OfferListController(ILogger<PMT01700LOO_OfferListController> logger)
        {
            //Initial and Get Logger
            LoggerPMT01700.R_InitializeLogger(logger);
            _logger = LoggerPMT01700.R_GetInstanceLogger();
            _activitySource = PMT01700Activity.R_InitializeAndGetActivitySource(nameof(PMT01700UnitListController));

        }

        [HttpPost]
        public IAsyncEnumerable<ReportTemplateListDTO> GetDataGetReportTemplateList(ParamaterGetReportTemplateListDTO poParameter)
        {
            string lcMethodName = nameof(GetDataGetReportTemplateList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<ReportTemplateListDTO> loRtn = null;
            List<ReportTemplateListDTO> loRtnTemp;
            PMT01700Utilities? loUtilities = null;
            try
            {
                var loCls = new PMT01700LOO_OfferListCls();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogDebug("DbParameter {@Parameter} ", poParameter);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtnTemp = loCls.GetDataSign_ReportTemplate(poParameter);

                loUtilities = new();

                _logger.LogInfo("Call method to streaming data");
                loRtn = loUtilities.PMT01700GetListStream(loRtnTemp);
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
#pragma warning restore CS8603 //
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01700LOO_OfferList_OfferListDTO> GetOfferList()
        {
            string lcMethodName = nameof(GetOfferList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01700LOO_OfferList_OfferListDTO> loRtn = null;
            List<PMT01700LOO_OfferList_OfferListDTO> loRtnTemp;
            PMT01700Utilities? loUtilities = null;
            try
            {
                var loDbParameter = new PMT01700LOO_UnitUtilities_ParameterDTO();
                var loCls = new PMT01700LOO_OfferListCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CPROPERTY_ID);
                loDbParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CTRANS_CODE);
                //CR Offer List. 18/07/2024
                loDbParameter.CFROM_REF_DATE = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CFROM_REF_DATE);
                loDbParameter.CTRANS_STATUS_LIST = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CCANCELLED);
                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtnTemp = loCls.GetOfferListDb(loDbParameter);

                loUtilities = new();

                _logger.LogInfo("Call method to streaming data");
                loRtn = loUtilities.PMT01700GetListStream(loRtnTemp);
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
#pragma warning restore CS8603 //
        }
        [HttpPost]
        public IAsyncEnumerable<PMT01700LOO_OfferList_UnitListDTO> GetUnitList()
        {
            string lcMethodName = nameof(GetUnitList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01700LOO_OfferList_UnitListDTO> loRtn = null;
            List<PMT01700LOO_OfferList_UnitListDTO> loRtnTemp;
            PMT01700Utilities? loUtilities = null;
            try
            {
                var loDbParameter = new PMT01700LOO_UnitUtilities_ParameterDTO();
                var loCls = new PMT01700LOO_OfferListCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CPROPERTY_ID);
                loDbParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CDEPT_CODE);
                loDbParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CREF_NO);
                loDbParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CTRANS_CODE);
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtnTemp = loCls.GetUnitListDb(loDbParameter);

                loUtilities = new();

                _logger.LogInfo("Call method to streaming data");
                loRtn = loUtilities.PMT01700GetListStream(loRtnTemp);
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
#pragma warning restore CS8603 //
        }
        [HttpPost]
        public PMT01700LOO_OfferList_OfferListDTO UpdateAgreement(PMT01700LOO_ProcessOffer_DTO poEntity)
        {
            string lcMethodName = nameof(UpdateAgreement);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            PMT01700LOO_OfferList_OfferListDTO loRtn = new();
            try
            {
                var loCls = new PMT01700LOO_OfferListCls();

                poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poEntity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", poEntity);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                bool llReturn = loCls.ProcessAgreement(poEntity);

                loRtn.IS_PROCESS_SUCCESS = llReturn;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn;
        }
    }
}
