using CBT02200BACK;
using CBT02200BACK.OpenTelemetry;
using CBT02200COMMON;
using CBT02200COMMON.DTO;
using CBT02200COMMON.DTO.CBT02200;
using CBT02200COMMON.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBT02200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CBT02200Controller : ControllerBase, ICBT02200
    {
        private LoggerCBT02200 _logger;
        private readonly ActivitySource _activitySource;
        public CBT02200Controller(ILogger<CBT02200Controller> logger)
        {
            LoggerCBT02200.R_InitializeLogger(logger);
            _logger = LoggerCBT02200.R_GetInstanceLogger();
            _activitySource = CBT02200ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(CBT02200Controller));
        }

        [HttpPost]
        public InitialProcessResultDTO InitialProcess()
        {
            using Activity activity = _activitySource.StartActivity("InitialProcess");
            _logger.LogInfo("Start || InitialProcess(Controller)");
            R_Exception loException = new R_Exception();
            InitialProcessResultDTO loRtn = new InitialProcessResultDTO();
            InitialProcessParameterDTO loParam = new InitialProcessParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || InitialProcess(Controller)");
                CBT02200Cls loCls = new CBT02200Cls();
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                loRtn.Data = new InitialProcessDTO();

                _logger.LogInfo("Run GetCompanyInfo(Cls) || InitialProcess(Controller)");
                loRtn.Data.CompanyInfo = loCls.GetCompanyInfo(new GetCompanyInfoParameterDTO()
                {
                    CLOGIN_COMPANY_ID = loParam.CLOGIN_COMPANY_ID
                });

                _logger.LogInfo("Run GetGLSystemParam(Cls) || InitialProcess(Controller)");
                loRtn.Data.GLSystemParam = loCls.GetGLSystemParam(new GetGLSystemParamParameterDTO()
                {
                    CLOGIN_COMPANY_ID = loParam.CLOGIN_COMPANY_ID,
                    CLANGUAGE_ID = loParam.CLANGUAGE_ID
                });

                _logger.LogInfo("Run GetCBSystemParam(Cls) || InitialProcess(Controller)");
                loRtn.Data.CBSystemParam = loCls.GetCBSystemParam(new GetCBSystemParamParameterDTO()
                {
                    CLOGIN_COMPANY_ID = loParam.CLOGIN_COMPANY_ID,
                    CLANGUAGE_ID = loParam.CLANGUAGE_ID
                });

                _logger.LogInfo("Run GetSoftPeriodStartDate(Cls) || InitialProcess(Controller)");
                loRtn.Data.SoftPeriodStartDate = loCls.GetSoftPeriodStartDate(new GetSoftPeriodStartDateParameterDTO()
                {
                    CLOGIN_COMPANY_ID = loParam.CLOGIN_COMPANY_ID,
                    CSOFT_PERIOD_YY = loRtn.Data.CBSystemParam.CSOFT_PERIOD_YY,
                    CSOFT_PERIOD_MM = loRtn.Data.CBSystemParam.CSOFT_PERIOD_MM
                });

                _logger.LogInfo("Run GetTransCodeInfo(Cls) || InitialProcess(Controller)");
                loRtn.Data.TransCodeInfo = loCls.GetTransCodeInfo(new GetTransCodeInfoParameterDTO()
                {
                    CLOGIN_COMPANY_ID = loParam.CLOGIN_COMPANY_ID,
                    CTRANS_CODE = "190020"
                });

                _logger.LogInfo("Run GetPeriodYearRange(Cls) || InitialProcess(Controller)");
                loRtn.Data.PeriodYearRange = loCls.GetPeriodYearRange(new GetPeriodYearRangeParameterDTO()
                {
                    CLOGIN_COMPANY_ID = loParam.CLOGIN_COMPANY_ID
                });

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || InitialProcess(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GetDeptLookupListDTO> GetDeptLookupList()
        {
            using Activity activity = _activitySource.StartActivity("GetDeptLookupList");
            _logger.LogInfo("Start || GetDeptLookupList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetDeptLookupListDTO> loRtn = null;
            GetDeptLookupListParameterDTO loParam = new GetDeptLookupListParameterDTO();
            CBT02200Cls loCls = new CBT02200Cls();
            List<GetDeptLookupListDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetDeptLookupList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetDeptLookupList(Cls) || GetDeptLookupList(Controller)");
                loTempRtn = loCls.GetDeptLookupList(loParam);

                _logger.LogInfo("Run GetDeptLookupListStream(Controller) || GetDeptLookupList(Controller)");
                loRtn = GetDeptLookupListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetDeptLookupList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetDeptLookupListDTO> GetDeptLookupListStream(List<GetDeptLookupListDTO> poParameter)
        {
            foreach (GetDeptLookupListDTO item in poParameter)
            {
                yield return item;
            }
        }


        [HttpPost]
        public IAsyncEnumerable<CBT02200GridDTO> GetChequeHeaderList()
        {
            using Activity activity = _activitySource.StartActivity("GetChequeHeaderList");
            _logger.LogInfo("Start || GetChequeHeaderList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<CBT02200GridDTO> loRtn = null;
            CBT02200GridParameterDTO loParam = null;
            CBT02200Cls loCls = new CBT02200Cls();
            List<CBT02200GridDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetChequeHeaderList(Controller)");
                loParam = R_Utility.R_GetStreamingContext<CBT02200GridParameterDTO>(ContextConstant.CBT02200_GRID_HEADER_STREAMING_CONTEXT);
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetChequeHeaderList(Cls) || GetChequeHeaderList(Controller)");
                loTempRtn = loCls.GetChequeHeaderList(loParam);

                _logger.LogInfo("Run GetChequeHeaderListStream(Controller) || GetChequeHeaderList(Controller)");
                loRtn = GetChequeHeaderListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetChequeHeaderList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<CBT02200GridDTO> GetChequeHeaderListStream(List<CBT02200GridDTO> poParameter)
        {
            foreach (CBT02200GridDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<CBT02200GridDetailDTO> GetChequeDetailList()
        {
            using Activity activity = _activitySource.StartActivity("GetChequeDetailList");
            _logger.LogInfo("Start || GetChequeDetailList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<CBT02200GridDetailDTO> loRtn = null;
            CBT02200GridDetailParameterDTO loParam = new CBT02200GridDetailParameterDTO();
            CBT02200Cls loCls = new CBT02200Cls();
            List<CBT02200GridDetailDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetChequeDetailList(Controller)");
                loParam.CREC_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CBT02200_GRID_DETAIL_REC_ID_STREAMING_CONTEXT);
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetChequeDetailList(Cls) || GetChequeDetailList(Controller)");
                loTempRtn = loCls.GetChequeDetailList(loParam);

                _logger.LogInfo("Run GetChequeDetailListStream(Controller) || GetChequeDetailList(Controller)");
                loRtn = GetChequeDetailListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetChequeDetailList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<CBT02200GridDetailDTO> GetChequeDetailListStream(List<CBT02200GridDetailDTO> poParameter)
        {
            foreach (CBT02200GridDetailDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public UpdateStatusResultDTO UpdateStatus(UpdateStatusParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("UpdateStatus");
            _logger.LogInfo("Start || UpdateStatus(Controller)");
            R_Exception loException = new R_Exception();
            UpdateStatusResultDTO loRtn = new UpdateStatusResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || UpdateStatus(Controller)");
                CBT02200Cls loCls = new CBT02200Cls();
                poParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run UpdateStatus(Cls) || UpdateStatus(Controller)");
                loCls.UpdateStatus(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || UpdateStatus(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GetStatusDTO> GetStatusList()
        {
            using Activity activity = _activitySource.StartActivity("GetStatusList");
            _logger.LogInfo("Start || GetStatusList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetStatusDTO> loRtn = null;
            GetStatusParameterDTO loParam = new GetStatusParameterDTO();
            CBT02200Cls loCls = new CBT02200Cls();
            List<GetStatusDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetStatusList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetStatusList(Cls) || GetStatusList(Controller)");
                loTempRtn = loCls.GetStatusList(loParam);

                _logger.LogInfo("Run GetStatusListStream(Controller) || GetStatusList(Controller)");
                loRtn = GetStatusListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetStatusList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetStatusDTO> GetStatusListStream(List<GetStatusDTO> poParameter)
        {
            foreach (GetStatusDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
