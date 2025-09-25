using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT01600BACK;
using PMT01600COMMON.DTO.CRUDBase;
using PMT01600COMMON.Interface;
using PMT01600COMMON.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace PMT01600Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMT01600Unit_UnitInfoDetailController : ControllerBase, IPMT01600Unit_UnitInfoDetail
    {
        private readonly LoggerPMT01600? _loggerPMT01100;
        private readonly ActivitySource _activitySource;

        public PMT01600Unit_UnitInfoDetailController(ILogger<PMT01600Unit_UnitInfoDetailController> logger)
        {
            LoggerPMT01600.R_InitializeLogger(logger);
            _loggerPMT01100 = LoggerPMT01600.R_GetInstanceLogger();
            _activitySource = PMT01600Activity.R_InitializeAndGetActivitySource(nameof(PMT01600Unit_UnitInfoDetailController));
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMT01600Unit_UnitInfoDetailDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT01600Unit_UnitInfoDetailDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceGetRecord);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<PMT01600Unit_UnitInfoDetailDTO>();

            try
            {
                _loggerPMT01100.LogInfo(string.Format("Initialize the loCls in method {0}", lcMethod));
                var loCls = new PMT01600Unit_UnitInfoDetailCls();
                _loggerPMT01100.LogDebug("{@PMT01500UnitInfo_UtilitiesCls}", loCls);

                _loggerPMT01100.LogInfo(string.Format("Set the property of poParameter.Entity Value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(poParameter.Entity.CCOMPANY_ID))
                    poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerPMT01100.LogDebug("{@ObjectParameter}", poParameter.Entity);
                poParameter.Entity.CTRANS_CODE = !string.IsNullOrEmpty(poParameter.Entity.CTRANS_CODE) ? poParameter.Entity.CTRANS_CODE : "802041";

                _loggerPMT01100.LogInfo(string.Format("Call the R_GetRecord method of loCls with poParameter.Entity and assign the result to loRtn.data in method {0}", lcMethod));
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
                _loggerPMT01100.LogDebug("{@ObjectReturn}", loRtn.data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerPMT01100.LogError(loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMT01600Unit_UnitInfoDetailDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT01600Unit_UnitInfoDetailDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceSave);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            R_ServiceSaveResultDTO<PMT01600Unit_UnitInfoDetailDTO> loReturn = new();
            try
            {
                _loggerPMT01100.LogInfo(string.Format("Initialize the loCls object as a new instance of Cls in method {0}", lcMethod));
                PMT01600Unit_UnitInfoDetailCls? loCls = new();
                _loggerPMT01100.LogDebug("{@ObjectPMT01500UnitInfo_UtilitiesCls}", loCls);

                _loggerPMT01100.LogInfo(string.Format("Set the property of poParameter.Entity value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (poParameter.Entity.CCOMPANY_ID != null)
                    poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerPMT01100.LogDebug("{@ObjectParameter}", poParameter.Entity);

                _loggerPMT01100.LogInfo(string.Format("Checking Data From Profile, and edit if Profile has empty string or null in method {0}", lcMethod));
                loReturn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
                _loggerPMT01100.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01100.LogError(loException);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT01600Unit_UnitInfoDetailDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceDelete);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            R_ServiceDeleteResultDTO? loReturn = new();
            try
            {
                _loggerPMT01100.LogInfo(string.Format("Set the property of poParameter.Entity value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (poParameter.Entity.CCOMPANY_ID != null)
                    poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerPMT01100.LogDebug("{@ObjectParameter}", poParameter.Entity);

                _loggerPMT01100.LogInfo(string.Format("Initialize the loCls object as a new instance of Cls in method {0}", lcMethod));
                PMT01600Unit_UnitInfoDetailCls? loCls = new();
                _loggerPMT01100.LogDebug("{@ObjectPMT01500UnitInfo_UtilitiesCls}", loCls);

                _loggerPMT01100.LogInfo(string.Format("Perform the delete operation using the R_Delete method of Cls in method {0}", lcMethod));
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01100.LogError(loException);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));
            return loReturn;
        }


    }
}
