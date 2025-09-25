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
    public class PMM01040Controller : ControllerBase, IPMM01040
    {
        private LoggerPMM01040 _Logger;
        private readonly ActivitySource _activitySource;

        public PMM01040Controller(ILogger<LoggerPMM01040> logger)
        {
            //Initial and Get Logger
            LoggerPMM01040.R_InitializeLogger(logger);
            _Logger = LoggerPMM01040.R_GetInstanceLogger();
            _activitySource = PMM01040ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMM01040Controller));
        }
        private async IAsyncEnumerable<T> GetListStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMM01040DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMM01040DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMM01040DTO> loRtn = new R_ServiceGetRecordResultDTO<PMM01040DTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMM01040");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord PMM01040");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01040Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord PMM01040Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord PMM01040");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMM01040DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMM01040DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMM01040DTO> loRtn = new R_ServiceSaveResultDTO<PMM01040DTO>();
            _Logger.LogInfo("Start ServiceSave PMM01040");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave PMM01040");

                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                }

                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01040Cls();

                _Logger.LogInfo("Call Back Method R_Save PMM01040Cls");

                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave PMM01040");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMM01040DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete PMM01040");

            try
            {
                _Logger.LogInfo("Set Param Entity R_ServiceDelete PMM01040");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01040Cls();

                _Logger.LogInfo("Call Back Method R_Delete PMM01040Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete PMM01040");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01040DTO> GetRateGUDateList()
        {
            using Activity activity = _activitySource.StartActivity("GetRateGUDateList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01040DTO> loRtn = null;
            List<PMM01040DTO> loTempRtn = null;
            _Logger.LogInfo("Start GetRateGUDateList");

            try
            {
                var loCls = new PMM01040Cls();
                loTempRtn = new List<PMM01040DTO>();
                var poParam = new PMM01040DTO();

                _Logger.LogInfo("Set Param GetRateGUDateList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_TYPE);
                poParam.CCHARGES_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_ID);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetAllRateGUDateList");
                loTempRtn = loCls.GetAllRateGUDateList(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetRateGUDateList");
                loRtn = GetListStream<PMM01040DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetRateGUDateList");

            return loRtn;
        }
    }
}