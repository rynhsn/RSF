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
    public class GSM02500Controller : ControllerBase, IGSM02500
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
        public GetCUOMFromPropertyResultDTO GetCUOMFromProperty(GetCUOMFromPropertyParameterDTO poParam)
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
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02500_PROPERTY_ID_CONTEXT);

                _logger.LogInfo("Run GetCUOMFromProperty(Cls) || GetCUOMFromProperty(Controller)");
                loRtn.Data = loCls.GetCUOMFromProperty(poParam);
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
        public SelectedBuildingResultDTO GetSelectedBuilding(SelectedBuildingParameterDTO poParam)
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
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02500_PROPERTY_ID_CONTEXT);
                //loTemp = R_Utility.R_GetContext<string>(ContextConstant.GSM02500_BUILDING_ID_CONTEXT);
                //poParam.Data = new SelectedBuildingDTO() { CBUILDING_ID = loTemp };

                _logger.LogInfo("Run GetSelectedBuilding(Cls) || GetSelectedBuilding(Controller)");
                loRtn.Data = loReusableCls.GetSelectedBuilding(poParam);
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
        public SelectedFloorResultDTO GetSelectedFloor(SelectedFloorParameterDTO poParam)
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
                //lcTemp = R_Utility.R_GetContext<string>(ContextConstant.GSM02500_FLOOR_ID_CONTEXT);
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02500_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_BUILDING_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02500_BUILDING_ID_CONTEXT);
                //loParam.Data = new SelectedFloorDTO() { CFLOOR_ID = lcTemp };

                _logger.LogInfo("Run GetSelectedFloor(Cls) || GetSelectedFloor(Controller)");
                loRtn.Data = loReusableCls.GetSelectedFloor(poParam);
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
        public SelectedPropertyResultDTO GetSelectedProperty(SelectedPropertyParameterDTO poParam)
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
                //lcTemp = R_Utility.R_GetContext<string>(ContextConstant.GSM02500_PROPERTY_ID_CONTEXT);
                //loParam.Data = new SelectedPropertyDTO() { CPROPERTY_ID = lcTemp };

                _logger.LogInfo("Run GetSelectedProperty(Cls) || GetSelectedProperty(Controller)");
                loRtn.Data = loReusableCls.GetSelectedProperty(poParam);
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
        public SelectedUnitResultDTO GetSelectedUnit(SelectedUnitParameterDTO poParam)
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
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02500_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_BUILDING_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02500_BUILDING_ID_CONTEXT);
                //loParam.CSELECTED_FLOOR_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02500_FLOOR_ID_CONTEXT);
                //lcTemp = R_Utility.R_GetContext<string>(ContextConstant.GSM02500_UNIT_ID_CONTEXT);
                //loParam.Data = new SelectedUnitDTO() { CUNIT_ID = lcTemp };

                _logger.LogInfo("Set GetSelectedUnit(Cls) || GetSelectedUnit(Controller)");
                loRtn.Data = loReusableCls.GetSelectedUnit(poParam);
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
        public SelectedOtherUnitResultDTO GetSelectedOtherUnit(SelectedOtherUnitParameterDTO poParam)
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
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02500_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_BUILDING_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02500_BUILDING_ID_CONTEXT);
                //loParam.CSELECTED_FLOOR_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02500_FLOOR_ID_CONTEXT);
                //lcTemp = R_Utility.R_GetContext<string>(ContextConstant.GSM02500_OtherUnit_ID_CONTEXT);
                //loParam.Data = new SelectedOtherUnitDTO() { COtherUnit_ID = lcTemp };

                _logger.LogInfo("Set GetSelectedOtherUnit(Cls) || GetSelectedOtherUnit(Controller)");
                loRtn.Data = loReusableCls.GetSelectedOtherUnit(poParam);
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
        public SelectedUnitTypeResultDTO GetSelectedUnitType(SelectedUnitTypeParameterDTO poParam)
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
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02500_PROPERTY_ID_CONTEXT);
                //lcTemp = R_Utility.R_GetContext<string>(ContextConstant.GSM02500_UNIT_TYPE_ID_CONTEXT);
                //loParam.Data = new SelectedUnitTypeDTO() { CUNIT_TYPE_ID = lcTemp };

                _logger.LogInfo("Run GetSelectedUnitType(Cls) || GetSelectedUnitType(Controller)");
                loRtn.Data = loReusableCls.GetSelectedUnitType(poParam);
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
            using Activity activity = _activitySource.StartActivity("GetUnitCategoryList");
            _logger.LogInfo("Start || GetUnitCategoryList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetUnitCategoryDTO> loRtn = null;
            GetUnitCategoryParameterDTO loParam = new GetUnitCategoryParameterDTO();
            GSM02500Cls loCls = new GSM02500Cls();
            List<GetUnitCategoryDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetUnitCategoryList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetUnitCategoryList(Cls) || GetUnitCategoryList(Controller)");
                loTempRtn = loCls.GetUnitCategoryList(loParam);

                _logger.LogInfo("Run GetUnitCategoryStream(Controller) || GetUnitCategoryList(Controller)");
                loRtn = GetUnitCategoryStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUnitCategoryList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetUnitCategoryDTO> GetUnitCategoryStream(List<GetUnitCategoryDTO> poParameter)
        {
            foreach (GetUnitCategoryDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GetUnitTypeDTO> GetUnitTypeList()
        {
            using Activity activity = _activitySource.StartActivity("GetUnitTypeList");
            _logger.LogInfo("Start || GetUnitTypeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetUnitTypeDTO> loRtn = null;
            GetUnitTypeParameterDTO loParam = new GetUnitTypeParameterDTO();
            GSM02500Cls loCls = new GSM02500Cls();
            List<GetUnitTypeDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetUnitTypeList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02500_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetUnitTypeList(Cls) || GetUnitTypeList(Controller)");
                loTempRtn = loCls.GetUnitTypeList(loParam);

                _logger.LogInfo("Run GetUnitTypeStream(Controller) || GetUnitTypeList(Controller)");
                loRtn = GetUnitTypeStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUnitTypeList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetUnitTypeDTO> GetUnitTypeStream(List<GetUnitTypeDTO> poParameter)
        {
            foreach (GetUnitTypeDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GetUnitViewDTO> GetUnitViewList()
        {
            using Activity activity = _activitySource.StartActivity("GetUnitViewList");
            _logger.LogInfo("Start || GetUnitViewList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetUnitViewDTO> loRtn = null;
            GetUnitViewParameterDTO loParam = new GetUnitViewParameterDTO();
            GSM02500Cls loCls = new GSM02500Cls();
            List<GetUnitViewDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetUnitViewList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02500_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetUnitViewList(Cls) || GetUnitViewList(Controller)");
                loTempRtn = loCls.GetUnitViewList(loParam);

                _logger.LogInfo("Run GetUnitViewStream(Controller) || GetUnitViewList(Controller)");
                loRtn = GetUnitViewStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUnitViewList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetUnitViewDTO> GetUnitViewStream(List<GetUnitViewDTO> poParameter)
        {
            foreach (GetUnitViewDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
