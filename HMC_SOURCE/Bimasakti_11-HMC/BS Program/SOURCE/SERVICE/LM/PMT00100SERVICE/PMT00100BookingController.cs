using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT00100BACK;
using PMT00100COMMON.Booking;
using PMT00100COMMON.Interface;
using PMT00100COMMON.Logger;
using PMT00100COMMON.UnitList;
using PMT00100COMMON.UtilityDTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT00100SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMT00100BookingController : ControllerBase, IPMT00100Booking
    {
        private LoggerPMT00100 _logger;
        private readonly ActivitySource _activitySource;

        public PMT00100BookingController(ILogger<PMT00100BookingController> logger)
        {
            //Initial and Get Logger
            LoggerPMT00100.R_InitializeLogger(logger);
            _logger = LoggerPMT00100.R_GetInstanceLogger();
            _activitySource = PMT00100Activity.R_InitializeAndGetActivitySource(nameof(PMT00100BookingController));

        }
        [HttpPost]
        public PMT00100BookingDTO GetAgreementDetail(PMT00100BookingDTO poParam)
        {
            string lcMethodName = nameof(GetAgreementDetail);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            PMT00100BookingDTO loRtn = null;
            try
            {
                var loCls = new PMT00100BookingCls();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", poParam);
                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtn = loCls.GetAgreementDetailDb(poParam);
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
        public VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODE()
        {
            string lcMethodName = nameof(GetVAR_GSM_TRANSACTION_CODE);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            VarGsmTransactionCodeDTO loRtn = null;
            try
            {
                var loDbParameter = new PMT00100DbParameterDTO();
                var loCls = new PMT00100BookingCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);
                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtn = loCls.GetVAR_GSM_TRANSACTION_CODEDb(loDbParameter);
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT00100BookingDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceDelete);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = null;
            PMT00100BookingCls loCls;

            try
            {
                loCls = new PMT00100BookingCls();
                loRtn = new R_ServiceDeleteResultDTO();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
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
        public R_ServiceGetRecordResultDTO<PMT00100BookingDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT00100BookingDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceGetRecord);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<PMT00100BookingDTO>();

            try
            {
                var loCls = new PMT00100BookingCls();
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
        public R_ServiceSaveResultDTO<PMT00100BookingDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT00100BookingDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceSave);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<PMT00100BookingDTO>? loRtn = null;
            PMT00100BookingCls loCls;

            try
            {
                loCls = new PMT00100BookingCls();
                loRtn = new R_ServiceSaveResultDTO<PMT00100BookingDTO>();
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
        public AgreementProcessDTO UpdateAgreement(AgreementProcessDTO poEntity)
        {
            string lcMethodName = nameof(UpdateAgreement);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            AgreementProcessDTO loRtn = new();
            try
            {
                var loCls = new PMT00100BookingCls();

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
