using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMM00500Back;
using PMM00500Common;
using PMM00500Common.DTOs;
using PMM00500Common.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace PMM00500Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMM00500Controller : ControllerBase, IPMM00500
    {
        private LoggerPMM00500 _loggerPMM00500;
        private readonly ActivitySource _activitySource;

        public PMM00500Controller(ILogger<PMM00500Controller> logger)
        {
            LoggerPMM00500.R_InitializeLogger(logger);
            _loggerPMM00500 = LoggerPMM00500.R_GetInstanceLogger();
            _activitySource = PMM00500Activity.R_InitializeAndGetActivitySource(nameof(PMM00500Controller));
        }

        #region CRUD

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMM00500DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMM00500DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMM00500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMM00500DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMM00500DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        #endregion CRUD

        [HttpPost]
        public IAsyncEnumerable<ChargesTypeDTO> GetAllChargesType()
        {
            R_Exception loEx = new R_Exception();
            _loggerPMM00500.LogInfo("Start GetAllChargesType PMM00500");
            IAsyncEnumerable<ChargesTypeDTO> loRtn = null;
            PMM00500ParameterDB loDbPar;
            PMM00500Cls loCls;
            List<ChargesTypeDTO> loRtnTmp;

            try
            {
                loDbPar = new PMM00500ParameterDB();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);

                loCls = new PMM00500Cls();
                _loggerPMM00500.LogInfo("Go To PMM00500Cls.ChargesTypeDB");
                loRtnTmp = loCls.ChargesTypeDB(loDbPar);
                _loggerPMM00500.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);
                loRtn = GetChargesTypeStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00500.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        #region Helper

        private async IAsyncEnumerable<ChargesTypeDTO> GetChargesTypeStream(List<ChargesTypeDTO> poParameter)
        {
            foreach (ChargesTypeDTO item in poParameter)
            {
                yield return item;
            }
        }

        #endregion Helper
    }
}