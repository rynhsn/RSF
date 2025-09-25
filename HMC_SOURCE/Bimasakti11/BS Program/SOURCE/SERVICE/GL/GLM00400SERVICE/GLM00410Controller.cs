using GLM00400BACK;
using GLM00400COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLM00400SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLM00410Controller : ControllerBase, IGLM00410
    {
        private LoggerGLM00410 _Logger;
        public GLM00410Controller(ILogger<LoggerGLM00410> logger)
        {
            //Initial and Get Logger
            LoggerGLM00410.R_InitializeLogger(logger);
            _Logger = LoggerGLM00410.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<GLM00411DTO> GetAllocationAccountList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00411DTO> loRtn = null;
            List<GLM00411DTO> loTempRtn = null;
            _Logger.LogInfo("Start GetAllocationAccountList");

            try
            {
                var loCls = new GLM00410Cls();
                var poParam = new GLM00411DTO();

                _Logger.LogInfo("Set Param GetAllocationAccountList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;
                poParam.CREC_ID_ALLOCATION_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID_ALLOCATION_ID);

                _Logger.LogInfo("Call Back Method GetAllAllocationAccount");
                loTempRtn = loCls.GetAllAllocationAccount(poParam);

                _Logger.LogInfo("Call Stream Method Data GetAllocationAccountList");
                loRtn = GetAllocationAccountListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllocationAccountList");

            return loRtn;
        }

        private async IAsyncEnumerable<GLM00411DTO> GetAllocationAccountListStream(List<GLM00411DTO> poParameter)
        {
            foreach (GLM00411DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GLM00415DTO> GetAllocationPeriodByTargetCenterList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00415DTO> loRtn = null;
            List<GLM00415DTO> loTempRtn = null;
            _Logger.LogInfo("Start GetAllocationPeriodByTargetCenterList");

            try
            {
                var loCls = new GLM00410Cls();

                _Logger.LogInfo("Set Param GetAllocationPeriodByTargetCenterList");
                var poParam = new GLM00415DTO();
                poParam.CREC_ID_ALLOCATION_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID_ALLOCATION_ID);
                poParam.CPERIOD_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPERIOD_NO);
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;
                poParam.CYEAR = R_Utility.R_GetStreamingContext<string>(ContextConstant.CYEAR);

                _Logger.LogInfo("Call Back Method GetAllAllocationPeriodByTargetCenter");
                loTempRtn = loCls.GetAllAllocationPeriodByTargetCenter(poParam);

                _Logger.LogInfo("Call Stream Method Data GetAllocationPeriodByTargetCenterList");
                loRtn = GetAllocationPeriodByTargetCenterListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllocationPeriodByTargetCenterList");

            return loRtn;
        }

        private async IAsyncEnumerable<GLM00415DTO> GetAllocationPeriodByTargetCenterListStream(List<GLM00415DTO> poParameter)
        {
            foreach (GLM00415DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GLM00414DTO> GetAllocationPeriodList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00414DTO> loRtn = null;
            List<GLM00414DTO> loTempRtn = null;
            _Logger.LogInfo("Start GetAllocationPeriodList");

            try
            {
                var loCls = new GLM00410Cls();
                var poParam = new GLM00414DTO();

                _Logger.LogInfo("Set Param GetAllocationPeriodList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CCYEAR = R_Utility.R_GetStreamingContext<string>(ContextConstant.CYEAR);

                _Logger.LogInfo("Call Back Method GetAllAllocationPeriod");
                loTempRtn = loCls.GetAllAllocationPeriod(poParam);

                _Logger.LogInfo("Call Stream Method Data GetAllocationPeriodList");
                loRtn = GetAllocationPeriodListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllocationPeriodList");

            return loRtn;
        }

        private async IAsyncEnumerable<GLM00414DTO> GetAllocationPeriodListStream(List<GLM00414DTO> poParameter)
        {
            foreach (GLM00414DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GLM00413DTO> GetAllocationTargetCenterByPeriodList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00413DTO> loRtn = null;
            List<GLM00413DTO> loTempRtn = null;
            _Logger.LogInfo("Start GetAllocationTargetCenterByPeriodList");

            try
            {
                var loCls = new GLM00410Cls();
                var poParam = new GLM00413DTO();

                _Logger.LogInfo("Set Param GetAllocationTargetCenterByPeriodList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;
                poParam.CREC_ID_CENTER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID_CENTER_ID);
                poParam.CYEAR = R_Utility.R_GetStreamingContext<string>(ContextConstant.CYEAR);

                _Logger.LogInfo("Call Back Method GetAllAllocationTargetCenterByPeriod");
                loTempRtn = loCls.GetAllAllocationTargetCenterByPeriod(poParam);

                _Logger.LogInfo("Call Stream Method Data GetAllocationTargetCenterByPeriodList");
                loRtn = GetAllocationTargetCenterByPeriodListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllocationTargetCenterByPeriodList");

            return loRtn;
        }

        private async IAsyncEnumerable<GLM00413DTO> GetAllocationTargetCenterByPeriodListStream(List<GLM00413DTO> poParameter)
        {
            foreach (GLM00413DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GLM00412DTO> GetAllocationTargetCenterList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00412DTO> loRtn = null;
            List<GLM00412DTO> loTempRtn = null;
            _Logger.LogInfo("Start GetAllocationTargetCenterList");

            try
            {
                var loCls = new GLM00410Cls();
                var poParam = new GLM00412DTO();

                _Logger.LogInfo("Set Param GetAllocationTargetCenterList");
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;
                poParam.CREC_ID_ALLOCATION_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID_ALLOCATION_ID);

                _Logger.LogInfo("Call Back Method GetAllAllocationTargetCenter");
                loTempRtn = loCls.GetAllAllocationTargetCenter(poParam);

                _Logger.LogInfo("Call Stream Method Data GetAllocationTargetCenterList");
                loRtn = GetAllocationTargetCenterStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllocationTargetCenterList");

            return loRtn;
        }

        private async IAsyncEnumerable<GLM00412DTO> GetAllocationTargetCenterStream(List<GLM00412DTO> poParameter)
        {
            foreach (GLM00412DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GLM00410DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete GLM00410");

            try
            {
                var loCls = new GLM00410Cls();

                _Logger.LogInfo("Call Back Method R_Delete GLM00410Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            _Logger.LogInfo("End R_ServiceDelete GLM00410");
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GLM00410DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GLM00410DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<GLM00410DTO> loRtn = new R_ServiceGetRecordResultDTO<GLM00410DTO>();
            _Logger.LogInfo("Start ServiceGetRecord GLM04100");

            try
            {
                var loCls = new GLM00410Cls();

                _Logger.LogInfo("Set Param Entity R_ServiceGetRecord GLM00410");
                poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method R_GetRecord GLM00410Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord GLM04100");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GLM00410DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GLM00410DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<GLM00410DTO> loRtn = new R_ServiceSaveResultDTO<GLM00410DTO>();
            _Logger.LogInfo("Start ServiceSave GLM04100");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave GLM00410");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                }
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new GLM00410Cls();

                _Logger.LogInfo("Call Back Method R_Save GLM00410Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave GLM00410");

            return loRtn;
        }

        [HttpPost]
        public GLM00400Result<GLM00413DTO> GetAllocationTargetCenterByPeriod(GLM00413DTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400Result<GLM00413DTO> loRtn = new GLM00400Result<GLM00413DTO>();
            _Logger.LogInfo("Start GetAllocationTargetCenterByPeriod");

            try
            {
                var loCls = new GLM00410Cls();

                _Logger.LogInfo("Call Back Method GetAllocationTargetCenterByPeriod");
                loRtn.Data = loCls.GetAllocationTargetCenterByPeriod(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllocationTargetCenterByPeriod");

            return loRtn;
        }

        [HttpPost]
        public GLM00400Result<GLM00413DTO> SaveAllocationTargetCenterByPeriod(GLM00413DTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400Result<GLM00413DTO> loRtn = new GLM00400Result<GLM00413DTO>();
            _Logger.LogInfo("Start SaveAllocationTargetCenterByPeriod");

            try
            {
                var loCls = new GLM00410Cls();

                _Logger.LogInfo("Call Back Method SavingAllocationTargetCenterByPeriod");
                loRtn.Data = loCls.SavingAllocationTargetCenterByPeriod(poParam); ;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End SaveAllocationTargetCenterByPeriod");

            return loRtn;
        }
    }
}
