using PMM01000BACK;
using PMM01000COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace PMM01000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMM01010Controller : ControllerBase, IPMM01010
    {
        private LoggerPMM01010 _Logger;
        private readonly ActivitySource _activitySource;

        public PMM01010Controller(ILogger<LoggerPMM01010> logger)
        {
            //Initial and Get Logger
            LoggerPMM01010.R_InitializeLogger(logger);
            _Logger = LoggerPMM01010.R_GetInstanceLogger();
            _activitySource = PMM01010ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMM01010Controller));
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMM01010DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMM01010DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMM01010DTO> loRtn = new R_ServiceGetRecordResultDTO<PMM01010DTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMM01010");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord PMM01010");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01010Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord PMM01010Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord PMM01010");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMM01010DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMM01010DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMM01010DTO> loRtn = new R_ServiceSaveResultDTO<PMM01010DTO>();
            _Logger.LogInfo("Start ServiceSave PMM01010");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave PMM01010");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                }
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01010Cls();

                _Logger.LogInfo("Call Back Method R_Save PMM01010Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave PMM01010");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMM01010DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01011DTO> GetRateECList()
        {
            using Activity activity = _activitySource.StartActivity("GetRateECList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01011DTO> loRtn = null;
            List<PMM01011DTO> loTempRtn = null;
            _Logger.LogInfo("Start GetRateECList");

            try
            {
                var loCls = new PMM01010Cls();
                loTempRtn = new List<PMM01011DTO>();
                var poParam = new PMM01011DTO();

                _Logger.LogInfo("Set Param GetRateECList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_TYPE);
                poParam.CCHARGES_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_ID);
                poParam.CCHARGES_DATE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_DATE);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetAllRateECList");
                loTempRtn = loCls.GetAllRateECList(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetRateECList");
                loRtn = GetRateECListStream<PMM01011DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetRateECList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01010DTO> GetRateECDateList()
        {
            using Activity activity = _activitySource.StartActivity("GetRateECDateList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01010DTO> loRtn = null;
            List<PMM01010DTO> loTempRtn = null;
            _Logger.LogInfo("Start GetRateECDateList");

            try
            {
                var loCls = new PMM01010Cls();
                loTempRtn = new List<PMM01010DTO>();
                var poParam = new PMM01010DTO();

                _Logger.LogInfo("Set Param GetRateECDateList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_TYPE);
                poParam.CCHARGES_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_ID);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetAllRateECDateList");
                loTempRtn = loCls.GetAllRateECDateList(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetRateECDateList");
                loRtn = GetRateECListStream<PMM01010DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetRateECDateList");

            return loRtn;
        }

        private async IAsyncEnumerable<T> GetRateECListStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
    }
}