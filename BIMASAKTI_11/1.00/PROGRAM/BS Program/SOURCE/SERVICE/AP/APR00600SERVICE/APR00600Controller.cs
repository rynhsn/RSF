using APR00600BACK;
using APR00600COMMON;
using APR00600COMMON.DTOs;
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

namespace APR00600SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APR00600Controller : ControllerBase, IAPR00600
    {
        private readonly LoggerAPR00600 _logger;
        private readonly ActivitySource _activitySource;

        public APR00600Controller(ILogger<APR00600Controller> logger)
        {
            LoggerAPR00600.R_InitializeLogger(logger);
            _logger = LoggerAPR00600.R_GetInstanceLogger();
            _activitySource = APR00600Activity.R_InitializeAndGetActivitySource(nameof(APR00600Controller));
        }

        [HttpPost]
        public async Task<APR00600SingleDTO<APR00600GetCompanyInfoDTO>> GetCompanyInfo()
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetCompanyInfo));
            _logger.LogInfo("Start - GetCompanyInfo");
            R_Exception loEx = new();
            APR00600Cls loCls = new();
            APR00600SingleDTO<APR00600GetCompanyInfoDTO> loReturn = new();
            APR00600ParamDbDTO loParam = new APR00600ParamDbDTO();

            try
            {
                _logger.LogInfo("Set Parameter");
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Process GetCompanyInfo");
                loReturn.Data = await loCls.GetCompanyInfo(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End - GetCompanyInfo");
            return loReturn;
        }

        [HttpPost]
        public async Task<APR00600ListDTO<APR00600GetPeriodDtListDTO>> GetPeriodDtList()
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetPeriodDtList));
            R_Exception loEx = new();
            APR00600ListDTO<APR00600GetPeriodDtListDTO> loReturn = new();
            APR00600Cls loCls = new();
            APR00600ParamDbDTO loParam = new APR00600ParamDbDTO();

            try
            {
                _logger.LogInfo("Start - GetPeriodDtList");
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CYEAR = R_BackGlobalVar.USER_ID;
                loParam.CYEAR = R_Utility.R_GetStreamingContext<string>(APR00600ContextHeaderDTO.CYEAR);
                loReturn.Data = await loCls.GetPeriodDtList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            _logger.LogInfo("End - GetPeriodDtList");
            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        [HttpPost]
        public async Task<APR00600SingleDTO<APR00600GetPeriodeYearRangeDTO>> GetPeriodeYearRange()
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetPeriodeYearRange));
            _logger.LogInfo("Start - GetPeriodeYearRange");
            R_Exception loEx = new();
            APR00600Cls loCls = new();
            APR00600SingleDTO<APR00600GetPeriodeYearRangeDTO> loReturn = new();
            APR00600ParamDbDTO loParam = new APR00600ParamDbDTO();

            try
            {
                _logger.LogInfo("Set Parameter");
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CMODE = R_Utility.R_GetStreamingContext<string>(APR00600ContextHeaderDTO.CMODE);
                loParam.CYEAR = R_Utility.R_GetStreamingContext<string>(APR00600ContextHeaderDTO.CYEAR);
                _logger.LogInfo("Process GetPeriodeYearRange");
                loReturn.Data = await loCls.GetPeriodeYearRange(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End - GetPeriodeYearRange");
            return loReturn;
        }

        [HttpPost]
        public async Task<APR00600SingleDTO<APR00600GetSystemParamDTO>> GetSystemParam()
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetSystemParam));
            _logger.LogInfo("Start - GetSystemParam");
            R_Exception loEx = new();
            APR00600Cls loCls = new();
            APR00600SingleDTO<APR00600GetSystemParamDTO> loReturn = new();
            APR00600ParamDbDTO loParam = new APR00600ParamDbDTO();

            try
            {
                _logger.LogInfo("Set Parameter");
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(APR00600ContextHeaderDTO.CPROPERTY_ID);
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                _logger.LogInfo("Process GetSystemParam");
                loReturn.Data = await loCls.GetSystemParam(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End - GetPeriodeYearRange");
            return loReturn;
        }

        [HttpPost]
        public async Task<APR00600ListDTO<APR00600PropertyDTO>> APR00600GetPropertyList()
        {
            using var loActivity = _activitySource.StartActivity(nameof(APR00600GetPropertyList));
            R_Exception loEx = new();
            APR00600ListDTO<APR00600PropertyDTO> loReturn = new();
            APR00600Cls loCls = new();
            APR00600ParamDbDTO loParam = new APR00600ParamDbDTO();

            try
            {
                _logger.LogInfo("Start - Get Property List");
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loReturn.Data = await loCls.GetPropertyList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            _logger.LogInfo("End - Get Property List");
            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }
    }
}
