using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT00100BACK;
using PMT00100COMMON.Booking;
using PMT00100COMMON.ChangeUnit;
using PMT00100COMMON.Interface;
using PMT00100COMMON.Logger;
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
    public class PMT00100ChangeUnitController : ControllerBase, IPM00100ChangeUnit
    {
        private LoggerPMT00100 _logger;
        private readonly ActivitySource _activitySource;

        public PMT00100ChangeUnitController(ILogger<PMT00100ChangeUnitController> logger)
        {
            //Initial and Get Logger
            LoggerPMT00100.R_InitializeLogger(logger);
            _logger = LoggerPMT00100.R_GetInstanceLogger();
            _activitySource = PMT00100Activity.R_InitializeAndGetActivitySource(nameof(PMT00100ChangeUnitController));

        }
        [HttpPost]
        public PMT00100ChangeUnitDTO GetAgreementUnitInfoDetail(PMT00100ChangeUnitDTO poParam)
        {
            string lcMethodName = nameof(GetAgreementUnitInfoDetail);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            PMT00100ChangeUnitDTO loRtn = null;
            try
            {
                var loCls = new PMT00100ChangeUnitCls();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", poParam);
                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtn = loCls.GetAgreementUnitInfoDetailDb(poParam);
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
        public R_ServiceGetRecordResultDTO<PMT00100ChangeUnitDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT00100ChangeUnitDTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<PMT00100ChangeUnitDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT00100ChangeUnitDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceSave);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<PMT00100ChangeUnitDTO>? loRtn = null;
            PMT00100ChangeUnitCls loCls;

            try
            {
                loCls = new PMT00100ChangeUnitCls();
                loRtn = new R_ServiceSaveResultDTO<PMT00100ChangeUnitDTO>();
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT00100ChangeUnitDTO> poParameter)
        {
            throw new NotImplementedException();
        }
    }
}
