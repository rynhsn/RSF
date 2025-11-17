using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR00100Back;
using PMR00100Common;
using PMR00100Common.DTOs;
using PMR00100Common.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMR00100Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMR00100Controller : ControllerBase, PMR00100Back.IPMR00100
    {
        private LoggerPMR00100? _loggerPMR00100;
        private ActivitySource _activitySource;

        public PMR00100Controller(ILogger<PMR00100Controller> logger)
        {
            LoggerPMR00100.R_InitializeLogger(logger);
            _loggerPMR00100 = LoggerPMR00100.R_GetInstanceLogger();
            _activitySource = PMR00100Activity.R_InitializeAndGetActivitySource(nameof(PMR00100Controller));
        }

        #region CRUD
        [HttpPost]
        public Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<PMR00100DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public Task<R_ServiceGetRecordResultDTO<PMR00100DTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMR00100DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public Task<R_ServiceSaveResultDTO<PMR00100DTO>> R_ServiceSave(R_ServiceSaveParameterDTO<PMR00100DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region GetList
        [HttpPost]
        public IAsyncEnumerable<PMR00100DTO> GetLOOPrintList(PrintParamDTO poData)
        {
            R_Exception loEx = new R_Exception();
            _loggerPMR00100.LogInfo("Start GetLOOPrintList PMR00100");
            IAsyncEnumerable<PMR00100DTO> loRtn = null;
            List<PMR00100DTO> loRtnTmp;
            PrintParamDTO loDbPar;
            PMR00100Cls loCls;

            try
            {
                loCls = new PMR00100Cls();
                poData.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poData.CLANG_ID = R_BackGlobalVar.CULTURE;
                _loggerPMR00100.LogDebug("Go To PMR00100Cls.GetLOOPrintList");
                loRtnTmp = loCls.GetPrintLOOList(poData);
                _loggerPMR00100.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);
                loRtn = GetLOOPrintListStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMR00100.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerPMR00100.LogInfo("End GetLOOPrintList PMR00100");
            return loRtn;
        }

        [HttpPost]
        public async Task<LOOStatusDataDTO> GetLOOStatusList()
        {
            R_Exception loEx = new R_Exception();
            _loggerPMR00100.LogInfo("Start GetLOOStatusList PMR00100");
            LOOStatusDataDTO loRtn = null;
            List<LOOStatusDTO> loResult;
            PMR00100ParamDTO loDbPar;
            PMR00100Cls loCls;

            try
            {
                loDbPar = new PMR00100ParamDTO();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CLANG_ID = R_BackGlobalVar.CULTURE;
                loCls = new PMR00100Cls();
                _loggerPMR00100.LogDebug("Go To PMR00100Cls.GetStatus");
                loResult = await loCls.GetLOOStatus(loDbPar);
                loRtn = new LOOStatusDataDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMR00100.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerPMR00100.LogInfo("End GetLOOStatusList PMR00100");
            return loRtn;
        }
        [HttpPost]
        public async Task<PeriodDT_DataDTO> GetPeriodDTList(PMR00100ParamDTO poData)
        {
            var loEx = new R_Exception();
            _loggerPMR00100.LogInfo("Start GetPeriodDTList PMR00100");
            PeriodDT_DataDTO loRtn = null;
            PMR00100ParamDTO loDbPar;
            PMR00100Cls loCls;
            try
            {
                loDbPar = new PMR00100ParamDTO();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loCls = new PMR00100Cls();

                loRtn = new PeriodDT_DataDTO();
                _loggerPMR00100.LogDebug("Go To PMR00100Cls.GetPeriodDTList");
                loRtn.Data = await loCls.GetPeriodDTList(loDbPar, poData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMR00100.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerPMR00100.LogInfo("End GetPeriodDTList PMR00100");
            return loRtn;
        }
        [HttpPost]
        public async Task<PeriodYearRangeDTO> GetPeriodYear()
        {
            R_Exception loEx = new R_Exception();
            _loggerPMR00100.LogInfo("Start GetPeriodYear PMR00100");
            PMR00100ParamDTO loDbPar = new();
            PeriodYearRangeDTO loReturn = null;
            PMR00100Cls loCls;
            try
            {
                loCls = new PMR00100Cls();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                _loggerPMR00100.LogDebug("Go To PMR00100Cls.GetPeriodYear");
                loReturn = await loCls.GetPeriodYear(loDbPar);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMR00100.LogError(loEx);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();

            _loggerPMR00100.LogInfo("End GetPeriodYear PMR00100");
            return loReturn;
        }
        [HttpPost]
        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            return GetPropertyStream();
        }
        private async IAsyncEnumerable<PropertyDTO> GetPropertyStream()
        {
            R_Exception loEx = new R_Exception();
            _loggerPMR00100.LogInfo("Start GetPropertyList PMR00100");
            List<PropertyDTO> loRtn = null;
            PMR00100Cls loCls;
            PMR00100ParamDTO loDbPar;

            try
            {
                loDbPar = new PMR00100ParamDTO();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loCls = new PMR00100Cls();
                _loggerPMR00100.LogInfo("Go To PMR00100Cls.GetPropertyList");
                loRtn = await loCls.GetPropertyList(loDbPar);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMR00100.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _loggerPMR00100.LogInfo("End GetPropertyList PMR00100");

            foreach (PropertyDTO item in loRtn)
            {
                yield return item;
            }
        }
        #endregion

        #region Helper
        private async IAsyncEnumerable<PMR00100DTO> GetLOOPrintListStream(List<PMR00100DTO> poParameter)
        {
            foreach (PMR00100DTO item in poParameter)
            {
                yield return item;
            }
        }
        #endregion

    }
}
