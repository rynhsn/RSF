using Microsoft.AspNetCore.Mvc;
using PMT01700BACK;
using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Unit___Charges;
using PMT01700COMMON.DTO.Utilities.ParamDb;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.Interface;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PMT01700COMMON.Logs;
using PMT01700COMMON.Context._1._Other_Untit_List;

namespace PMT01700SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMT01700LOO_UnitUtilities_UnitUtiliesController : ControllerBase, IPMT01700LOO_UnitUtilities_UnitUtilities
    {
        private readonly LoggerPMT01700? _logger;
        private readonly ActivitySource _activitySource;
        public PMT01700LOO_UnitUtilities_UnitUtiliesController(ILogger<PMT01700LOO_UnitUtilities_UnitUtiliesController> logger)
        {
            //Initial and Get Logger
            LoggerPMT01700.R_InitializeLogger(logger);
            _logger = LoggerPMT01700.R_GetInstanceLogger();
            _activitySource = PMT01700Activity.R_InitializeAndGetActivitySource(nameof(PMT01700LOO_UnitUtilities_UnitUtiliesController));

        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceDelete);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = null;
            PMT01700LOO_UnitUtilities_UnitUtilitiesCls loCls;

            try
            {
                loCls = new PMT01700LOO_UnitUtilities_UnitUtilitiesCls();
                loRtn = new R_ServiceDeleteResultDTO();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo("Call method R_Delete");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            };
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loRtn!;
        }
        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceGetRecord);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO>();

            try
            {
                var loCls = new PMT01700LOO_UnitUtilities_UnitUtilitiesCls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo("Call method R_GetRecord");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
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
        [HttpPost]
        public R_ServiceSaveResultDTO<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceSave);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO>? loRtn = null;
            PMT01700LOO_UnitUtilities_UnitUtilitiesCls loCls;

            try
            {
                loCls = new PMT01700LOO_UnitUtilities_UnitUtilitiesCls();
                loRtn = new R_ServiceSaveResultDTO<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo("Call method R_ServiceSave");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            };
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loRtn!;

        }
        [HttpPost]
        public PMT01700LOO_UnitCharges_UnitCharges_AgreementUnitHeaderDTO GetUnitChargesHeader(PMT01700LOO_UnitUtilities_ParameterDTO poParameter)
        {
            string lcMethodName = nameof(GetUnitChargesHeader);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            PMT01700LOO_UnitCharges_UnitCharges_AgreementUnitHeaderDTO loRtn = null;
            try
            {
                var loCls = new PMT01700LOO_UnitUtilities_UnitUtilitiesCls();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", poParameter);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtn = loCls.GetUnitUtilitiesHeader(poParameter);
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
        public IAsyncEnumerable<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO> GetUnitInfoList()
        {
            string lcMethodName = nameof(GetUnitInfoList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO> loRtn = null;
            List<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO> loRtnTemp;
            PMT01700Utilities? loUtilities = null;
            try
            {
                var loDbParameter = new PMT01700LOO_UnitUtilities_ParameterDTO();
                var loCls = new PMT01700LOO_UnitUtilities_UnitUtilitiesCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CPROPERTY_ID);
                loDbParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CDEPT_CODE);
                loDbParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CTRANS_CODE);
                loDbParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CREF_NO);
                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtnTemp = loCls.GetUnitInfoListDb(loDbParameter);

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
#pragma warning restore CS8603 // Possible null reference return.
        }
       
    }
}
