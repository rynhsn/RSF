using GSM02500BACK;
using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM02500Controller : ControllerBase, GSM02500BACK.IGSM02500
    {
        private LoggerGSM02500 _logger;
        private readonly ActivitySource _activitySource;
        public GSM02500Controller(ILogger<GSM02500Controller> logger)
        {
            LoggerGSM02500.R_InitializeLogger(logger);
            _logger = LoggerGSM02500.R_GetInstanceLogger();
            _activitySource = GSM02500ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GSM02500Controller));
        }

        [HttpPost]
        public async Task<GetCUOMFromPropertyResultDTO> GetCUOMFromProperty(GetCUOMFromPropertyParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetCUOMFromProperty");
            _logger.LogInfo("Start || GetCUOMFromProperty(Controller)");
            R_Exception loException = new R_Exception();
            GetCUOMFromPropertyResultDTO loRtn = new GetCUOMFromPropertyResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetCUOMFromProperty(Controller)");
                GSM02500Cls loCls = new GSM02500Cls();

                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run GetCUOMFromProperty(Cls) || GetCUOMFromProperty(Controller)");
                loRtn.Data = await loCls.GetCUOMFromProperty(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetCUOMFromProperty(Controller)");
            return loRtn;
        }

        [HttpPost]
        public async Task<SelectedBuildingResultDTO> GetSelectedBuilding(SelectedBuildingParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetSelectedBuilding");
            _logger.LogInfo("Start || GetSelectedBuilding(Controller)");
            R_Exception loException = new R_Exception();
            SelectedBuildingResultDTO loRtn = new SelectedBuildingResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetSelectedBuilding(Controller)");
                GSM02500Cls loReusableCls = new GSM02500Cls();

                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run GetSelectedBuilding(Cls) || GetSelectedBuilding(Controller)");
                loRtn.Data = await loReusableCls.GetSelectedBuilding(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetSelectedBuilding(Controller)");
            return loRtn;
        }

        [HttpPost]
        public async Task<SelectedFloorResultDTO> GetSelectedFloor(SelectedFloorParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetSelectedFloor");
            _logger.LogInfo("Start || GetSelectedFloor(Controller)");
            R_Exception loException = new R_Exception();
            SelectedFloorResultDTO loRtn = new SelectedFloorResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetSelectedFloor(Controller)");
                GSM02500Cls loReusableCls = new GSM02500Cls();

                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetSelectedFloor(Cls) || GetSelectedFloor(Controller)");
                loRtn.Data = await loReusableCls.GetSelectedFloor(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetSelectedFloor(Controller)");
            return loRtn;
        }

        [HttpPost]
        public async Task<SelectedPropertyResultDTO> GetSelectedProperty(SelectedPropertyParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetSelectedProperty");
            _logger.LogInfo("Start || GetSelectedProperty(Controller)");
            R_Exception loException = new R_Exception();
            SelectedPropertyResultDTO loRtn = new SelectedPropertyResultDTO();
            
            try
            {
                _logger.LogInfo("Set Parameter || GetSelectedProperty(Controller)");
                GSM02500Cls loReusableCls = new GSM02500Cls();

                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run GetSelectedProperty(Cls) || GetSelectedProperty(Controller)");
                loRtn.Data = await loReusableCls.GetSelectedProperty(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetSelectedProperty(Controller)");
            return loRtn;
        }

        [HttpPost]
        public async Task<SelectedUnitResultDTO> GetSelectedUnit(SelectedUnitParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetSelectedUnit");
            _logger.LogInfo("Start || GetSelectedUnit(Controller)");
            R_Exception loException = new R_Exception();
            SelectedUnitResultDTO loRtn = new SelectedUnitResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetSelectedUnit(Controller)");
                GSM02500Cls loReusableCls = new GSM02500Cls();

                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Set GetSelectedUnit(Cls) || GetSelectedUnit(Controller)");
                loRtn.Data = await loReusableCls.GetSelectedUnit(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetSelectedUnit(Controller)");
            return loRtn;
        }


        [HttpPost]
        public async Task<SelectedOtherUnitResultDTO> GetSelectedOtherUnit(SelectedOtherUnitParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetSelectedOtherUnit");
            _logger.LogInfo("Start || GetSelectedOtherUnit(Controller)");
            R_Exception loException = new R_Exception();
            SelectedOtherUnitResultDTO loRtn = new SelectedOtherUnitResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetSelectedOtherUnit(Controller)");
                GSM02500Cls loReusableCls = new GSM02500Cls();

                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Set GetSelectedOtherUnit(Cls) || GetSelectedOtherUnit(Controller)");
                loRtn.Data = await loReusableCls.GetSelectedOtherUnit(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetSelectedOtherUnit(Controller)");
            return loRtn;
        }

        [HttpPost]
        public async Task<SelectedUnitTypeResultDTO> GetSelectedUnitType(SelectedUnitTypeParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetSelectedUnitType");
            _logger.LogInfo("Start || GetSelectedUnitType(Controller)");
            R_Exception loException = new R_Exception();
            SelectedUnitTypeResultDTO loRtn = new SelectedUnitTypeResultDTO();
            
            try
            {
                _logger.LogInfo("Set Parameter || GetSelectedUnitType(Controller)");
                GSM02500Cls loReusableCls = new GSM02500Cls();

                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run GetSelectedUnitType(Cls) || GetSelectedUnitType(Controller)");
                loRtn.Data = await loReusableCls.GetSelectedUnitType(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetSelectedUnitType(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GetUnitCategoryDTO> GetUnitCategoryList()
        {
            return GetUnitCategoryStream();
        }
        private async IAsyncEnumerable<GetUnitCategoryDTO> GetUnitCategoryStream()
        {
            using Activity activity = _activitySource.StartActivity("GetUnitCategoryList");
            _logger.LogInfo("Start || GetUnitCategoryList(Controller)");
            R_Exception loException = new R_Exception();
            List<GetUnitCategoryDTO> loRtn = null;
            GetUnitCategoryParameterDTO poParam = new GetUnitCategoryParameterDTO();
            GSM02500Cls loCls = new GSM02500Cls();

            try
            {
                _logger.LogInfo("Set Parameter || GetUnitCategoryList(Controller)");
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetUnitCategoryList(Cls) || GetUnitCategoryList(Controller)");
                loRtn = await loCls.GetUnitCategoryList(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUnitCategoryList(Controller)");
            foreach (GetUnitCategoryDTO item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GetUnitTypeDTO> GetUnitTypeList()
        {
            return GetUnitTypeStream();
        }
        private async IAsyncEnumerable<GetUnitTypeDTO> GetUnitTypeStream()
        {
            using Activity activity = _activitySource.StartActivity("GetUnitTypeList");
            _logger.LogInfo("Start || GetUnitTypeList(Controller)");
            R_Exception loException = new R_Exception();
            List<GetUnitTypeDTO> loRtn = null;
            GetUnitTypeParameterDTO poParam = new GetUnitTypeParameterDTO();
            GSM02500Cls loCls = new GSM02500Cls();

            try
            {
                _logger.LogInfo("Set Parameter || GetUnitTypeList(Controller)");
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02500_PROPERTY_ID_STREAMING_CONTEXT);
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetUnitTypeList(Cls) || GetUnitTypeList(Controller)");
                loRtn = await loCls.GetUnitTypeList(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUnitTypeList(Controller)");

            foreach (GetUnitTypeDTO item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GetUnitViewDTO> GetUnitViewList()
        {
            return GetUnitViewStream();
        }
        private async IAsyncEnumerable<GetUnitViewDTO> GetUnitViewStream()
        {
            using Activity activity = _activitySource.StartActivity("GetUnitViewList");
            _logger.LogInfo("Start || GetUnitViewList(Controller)");
            R_Exception loException = new R_Exception();
            List<GetUnitViewDTO> loRtn = null;
            GetUnitViewParameterDTO poParam = new GetUnitViewParameterDTO();
            GSM02500Cls loCls = new GSM02500Cls();

            try
            {
                _logger.LogInfo("Set Parameter || GetUnitViewList(Controller)");
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02500_PROPERTY_ID_STREAMING_CONTEXT);
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetUnitViewList(Cls) || GetUnitViewList(Controller)");
                loRtn = await loCls.GetUnitViewList(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUnitViewList(Controller)");

            foreach (GetUnitViewDTO item in loRtn)
            {
                yield return item;
            }
        }
    }
}
