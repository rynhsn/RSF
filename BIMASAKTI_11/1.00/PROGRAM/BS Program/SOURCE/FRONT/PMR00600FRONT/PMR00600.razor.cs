using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00400;
using Lookup_PMModel.ViewModel.LML00600;
using Microsoft.AspNetCore.Components;
using PMR00600COMMON.DTO_s;
using PMR00600COMMON.DTO_s.Print;
using PMR00600COMMON.DTO_s.PrintDetail;
using PMR00600FrontResources;
using PMR00600MODEL.View_Models;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System.Globalization;

namespace PMR00600FRONT
{
    public partial class PMR00600 : R_Page
    {
        private PMR00600ViewModel _viewModel = new();

        private R_Conductor _conductorRef;
        [Inject] IClientHelper _clientHelper { get; set; }
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        private R_RadioGroup<ReportTypeDTO, string> _radioReportType;
        private R_RadioGroup<GroupTypeDTO, string> _radioGroupingType;
        protected override async Task R_Init_From_Master(object poParameter)
        {

            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.InitProcess(_localizer);
                await _setDefaultBuilding();
                await _setDefaultDepartment();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task _setDefaultBuilding()
        {
            var loEx = new R_Exception();

            try
            {
                
                if (string.IsNullOrEmpty(_viewModel._ReportParam.CPROPERTY_ID)) return;

                var loLookupViewModel = new LookupGSL02200ViewModel();
                var param = new GSL02200ParameterDTO
                {
                    CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID
                };

                await loLookupViewModel.GetBuildingList(param);
                if (loLookupViewModel.BuildingGrid.Count > 0)
                {   
                    _viewModel._ReportParam.CFROM_BUILDING_ID = loLookupViewModel.BuildingGrid.FirstOrDefault()?.CBUILDING_ID;
                    _viewModel._ReportParam.CFROM_BUILDING_NAME = loLookupViewModel.BuildingGrid
                        .Where(x => x.CBUILDING_ID == _viewModel._ReportParam.CFROM_BUILDING_ID)
                        .Select(x => x.CBUILDING_NAME).FirstOrDefault() ?? string.Empty;
                    _viewModel._ReportParam.CTO_BUILDING_ID = loLookupViewModel.BuildingGrid.LastOrDefault()?.CBUILDING_ID;
                    _viewModel._ReportParam.CTO_BUILDING_NAME = loLookupViewModel.BuildingGrid
                        .Where(x => x.CBUILDING_ID == _viewModel._ReportParam.CTO_BUILDING_ID)
                        .Select(x => x.CBUILDING_NAME).FirstOrDefault() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task _setDefaultDepartment()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(_viewModel._ReportParam.CPROPERTY_ID)) return;

                var loLookupViewModel = new LookupGSL00710ViewModel();

                var param = new GSL00710ParameterDTO
                {
                    CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID
                };
                
                await loLookupViewModel.GetDepartmentPropertyList(param);
                if (loLookupViewModel.DepartmentPropertyGrid.Count > 0)
                {
                    _viewModel._ReportParam.CFROM_DEPT_CODE = loLookupViewModel.DepartmentPropertyGrid.FirstOrDefault()?.CDEPT_CODE;
                    _viewModel._ReportParam.CFROM_DEPT_NAME = loLookupViewModel.DepartmentPropertyGrid
                        .Where(x => x.CDEPT_CODE == _viewModel._ReportParam.CFROM_DEPT_CODE)
                        .Select(x => x.CDEPT_NAME).FirstOrDefault() ?? string.Empty;
                    _viewModel._ReportParam.CTO_DEPT_CODE = loLookupViewModel.DepartmentPropertyGrid.LastOrDefault()?.CDEPT_CODE;
                    _viewModel._ReportParam.CTO_DEPT_NAME = loLookupViewModel.DepartmentPropertyGrid
                        .Where(x => x.CDEPT_CODE == _viewModel._ReportParam.CTO_DEPT_CODE)
                        .Select(x => x.CDEPT_NAME).FirstOrDefault() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        #region PropertyDropdown
        public async Task ComboboxPropertyValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel._ReportParam.CPROPERTY_ID = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;
                _viewModel._ReportParam.CTO_DEPT_CODE = "";
                _viewModel._ReportParam.CTO_DEPT_NAME = "";
                _viewModel._ReportParam.CFROM_DEPT_CODE = "";
                _viewModel._ReportParam.CFROM_DEPT_NAME = "";
                _viewModel._ReportParam.CFROM_BUILDING_ID = "";
                _viewModel._ReportParam.CTO_BUILDING_ID = "";
                _viewModel._ReportParam.CFROM_BUILDING_NAME = "";
                _viewModel._ReportParam.CTO_BUILDING_NAME = "";
                _viewModel._ReportParam.CFROM_TENANT_ID = "";
                _viewModel._ReportParam.CFROM_TENANT_NAME = "";
                _viewModel._ReportParam.CTO_TENANT_ID = "";
                _viewModel._ReportParam.CTO_TENANT_NAME = "";
                _viewModel._ReportParam.CFROM_SERVICE_ID = "";
                _viewModel._ReportParam.CFROM_SERVICE_NAME = "";
                _viewModel._ReportParam.CTO_SERVICE_ID = "";
                _viewModel._ReportParam.CTO_SERVICE_NAME = "";
                await _setDefaultBuilding();
                await _setDefaultDepartment();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region lookupFromDept
        private void BeforeOpen_lookupFromDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL00710ParameterDTO() { CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID };
            eventArgs.TargetPageType = typeof(GSL00710);
        }
        private void AfterOpen_lookupFromDeptAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel._ReportParam.CFROM_DEPT_CODE = loTempResult.CDEPT_CODE;
                _viewModel._ReportParam.CFROM_DEPT_NAME = loTempResult.CDEPT_CODE;
            }
            else
            {
                _viewModel._ReportParam.CFROM_DEPT_CODE = "";
                _viewModel._ReportParam.CFROM_DEPT_NAME = "";
            }
        }
        private async Task OnLostFocus_LookupFromDept()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CFROM_DEPT_CODE))
                {

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel(); //use GSL's model
                    var loParam = new GSL00700ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CSEARCH_TEXT = _viewModel._ReportParam.CFROM_DEPT_CODE, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetDepartment(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CFROM_DEPT_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel._ReportParam.CFROM_DEPT_CODE = loResult.CDEPT_CODE;
                    _viewModel._ReportParam.CFROM_DEPT_NAME = loResult.CDEPT_NAME; //assign bind textbox name kalo ada
                }
                else
                {
                    _viewModel._ReportParam.CFROM_DEPT_NAME = ""; //kosongin bind textbox name kalo gaada

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }
        private void BeforeOpen_lookupToDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL00710ParameterDTO() { CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID };
            eventArgs.TargetPageType = typeof(GSL00710);
        }
        private void AfterOpen_lookupToDeptAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel._ReportParam.CTO_DEPT_CODE = loTempResult.CDEPT_CODE;
                _viewModel._ReportParam.CTO_DEPT_NAME = loTempResult.CDEPT_CODE;
            }
            else
            {
                _viewModel._ReportParam.CTO_DEPT_CODE = "";
                _viewModel._ReportParam.CTO_DEPT_NAME = "";
            }

        }
        private async Task OnLostFocus_LookupToDept()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CTO_DEPT_CODE))
                {

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel(); //use GSL's model
                    var loParam = new GSL00700ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CSEARCH_TEXT = _viewModel._ReportParam.CTO_DEPT_CODE, // property that bindded to search textbox
                    };

                    var loResult = await loLookupViewModel.GetDepartment(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CTO_DEPT_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel._ReportParam.CTO_DEPT_CODE = loResult.CDEPT_CODE;
                    _viewModel._ReportParam.CTO_DEPT_NAME = loResult.CDEPT_NAME; //assign bind textbox name kalo ada
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }
        #endregion

        #region lookupBuilding
        private void BeforeOpen_lookupFromBuilding(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL02200ParameterDTO()
            { CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID };
            eventArgs.TargetPageType = typeof(GSL02200);
        }
        private void AfterOpen_lookupFromBuildingAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL02200DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel._ReportParam.CFROM_BUILDING_ID = loTempResult.CBUILDING_ID;
                _viewModel._ReportParam.CFROM_BUILDING_NAME = loTempResult.CBUILDING_NAME;
            }
            else
            {
                _viewModel._ReportParam.CFROM_BUILDING_ID = "";
                _viewModel._ReportParam.CFROM_BUILDING_NAME = "";
            }
        }
        private async Task OnLostFocus_LookupFromBuilding()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CFROM_BUILDING_ID))
                {

                    LookupGSL02200ViewModel loLookupViewModel = new LookupGSL02200ViewModel(); //use GSL's model
                    var loParam = new GSL02200ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {

                        CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID,
                        CSEARCH_TEXT = _viewModel._ReportParam.CFROM_BUILDING_ID, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetBuilding(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CFROM_BUILDING_NAME = ""; //kosongin bind textbox name kalo gaada
                        _viewModel._ReportParam.CFROM_BUILDING_ID = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel._ReportParam.CFROM_BUILDING_ID = loResult.CBUILDING_ID;
                    _viewModel._ReportParam.CFROM_BUILDING_NAME = loResult.CBUILDING_NAME; //assign bind textbox name kalo ada
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }
        private void BeforeOpen_lookupToBuilding(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL02200ParameterDTO()
            { CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID };
            eventArgs.TargetPageType = typeof(GSL02200);
        }
        private void AfterOpen_lookupToBuildingAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL02200DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel._ReportParam.CTO_BUILDING_ID = loTempResult.CBUILDING_ID;
                _viewModel._ReportParam.CTO_BUILDING_NAME = loTempResult.CBUILDING_NAME;
            }
            else
            {
                _viewModel._ReportParam.CTO_BUILDING_ID = "";
                _viewModel._ReportParam.CTO_BUILDING_NAME = "";
            }
        }
        private async Task OnLostFocus_LookupToBuilding()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CTO_BUILDING_ID))
                {

                    LookupGSL02200ViewModel loLookupViewModel = new LookupGSL02200ViewModel(); //use GSL's model
                    var loParam = new GSL02200ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID,
                        CSEARCH_TEXT = _viewModel._ReportParam.CTO_BUILDING_ID, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetBuilding(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CTO_BUILDING_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel._ReportParam.CTO_BUILDING_ID = loResult.CBUILDING_ID;
                    _viewModel._ReportParam.CTO_BUILDING_NAME = loResult.CBUILDING_NAME; //assign bind textbox name kalo ada
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }
        #endregion

        #region lookupTenant
        private void BeforeOpen_lookupFromTenant(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00600ParameterDTO()
            { CCOMPANY_ID = _clientHelper.CompanyId, CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID, CCUSTOMER_TYPE = "01", CUSER_ID = _clientHelper.UserId, CSEARCH_TEXT = "" };
            eventArgs.TargetPageType = typeof(LML00600);
        }
        private void AfterOpen_lookupFromTenantAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00600DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel._ReportParam.CFROM_TENANT_ID = loTempResult.CTENANT_ID;
                _viewModel._ReportParam.CFROM_TENANT_NAME = loTempResult.CTENANT_NAME;
            }
            else
            {
                _viewModel._ReportParam.CFROM_TENANT_ID = "";
                _viewModel._ReportParam.CFROM_TENANT_NAME = "";
            }
        }
        private async Task OnLostFocus_LookupFromTenant()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CFROM_TENANT_ID))
                {

                    LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel(); //use GSL's model
                    var loParam = new LML00600ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID,
                        CCUSTOMER_TYPE = "01",
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel._ReportParam.CFROM_TENANT_ID, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetTenant(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CFROM_TENANT_NAME = ""; //kosongin bind textbox name kalo gaada
                        _viewModel._ReportParam.CFROM_TENANT_ID = "";
                    }
                    else
                    {
                        _viewModel._ReportParam.CFROM_TENANT_ID = loResult.CTENANT_ID;
                        _viewModel._ReportParam.CFROM_TENANT_NAME = loResult.CTENANT_NAME; //assign bind textbox name kalo ada
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }
        private void BeforeOpen_lookupToTenant(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00600ParameterDTO()
            { CCOMPANY_ID = _clientHelper.CompanyId, CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID, CCUSTOMER_TYPE = "01", CUSER_ID = _clientHelper.UserId, CSEARCH_TEXT = "" };
            eventArgs.TargetPageType = typeof(LML00600);
        }
        private void AfterOpen_lookupToTenantAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00600DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel._ReportParam.CTO_TENANT_ID = loTempResult.CTENANT_ID;
                _viewModel._ReportParam.CTO_TENANT_NAME = loTempResult.CTENANT_NAME;
            }
            else
            {
                _viewModel._ReportParam.CTO_TENANT_ID = "";
                _viewModel._ReportParam.CTO_TENANT_NAME = "";
            }
        }
        private async Task OnLostFocus_LookupToTenant()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CTO_TENANT_ID))
                {

                    LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel(); //use GSL's model
                    var loParam = new LML00600ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID,
                        CCUSTOMER_TYPE = "01",
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel._ReportParam.CTO_TENANT_ID, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetTenant(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CTO_TENANT_NAME = ""; //kosongin bind textbox name kalo gaada
                        _viewModel._ReportParam.CFROM_TENANT_ID = "";
                        goto EndBlock;
                    }
                    else
                    {
                        _viewModel._ReportParam.CTO_TENANT_ID = loResult.CTENANT_ID;
                        _viewModel._ReportParam.CTO_TENANT_NAME = loResult.CTENANT_NAME; //assign bind textbox name kalo ada
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }
        #endregion

        #region lookupService
        private void BeforeOpen_lookupFromService(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00400ParameterDTO()
            { CCOMPANY_ID = _clientHelper.CompanyId, CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID, CCHARGE_TYPE_ID = "08", CUSER_ID = _clientHelper.UserId, CSEARCH_TEXT = "" };
            eventArgs.TargetPageType = typeof(LML00400);
        }
        private async Task AfterOpen_lookupFromServiceAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00400DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel._ReportParam.CFROM_SERVICE_ID = loTempResult.CCHARGES_ID;
                _viewModel._ReportParam.CFROM_SERVICE_NAME = loTempResult.CCHARGES_NAME;
            }
        }
        private async Task OnLostFocus_LookupFromService()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CFROM_SERVICE_ID))
                {

                    LookupLML00400ViewModel loLookupViewModel = new LookupLML00400ViewModel(); //use GSL's model
                    var loParam = new LML00400ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID,
                        CCHARGE_TYPE_ID = "08",
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel._ReportParam.CFROM_SERVICE_ID, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetUtitlityCharges(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CFROM_SERVICE_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel._ReportParam.CFROM_SERVICE_ID = loResult.CCHARGES_ID;
                    _viewModel._ReportParam.CFROM_SERVICE_NAME = loResult.CCHARGES_NAME; //assign bind textbox name kalo ada
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }
        private void BeforeOpen_lookupToService(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00400ParameterDTO()
            { CCOMPANY_ID = _clientHelper.CompanyId, CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID, CCHARGE_TYPE_ID = "08", CUSER_ID = _clientHelper.UserId, CSEARCH_TEXT = "" };
            eventArgs.TargetPageType = typeof(LML00400);
        }
        private async Task AfterOpen_lookupToServiceAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00400DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel._ReportParam.CTO_SERVICE_ID = loTempResult.CCHARGES_ID;
                _viewModel._ReportParam.CTO_SERVICE_NAME = loTempResult.CCHARGES_NAME;
            }
        }
        private async Task OnLostFocus_LookupToService()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CTO_SERVICE_ID))
                {

                    LookupLML00400ViewModel loLookupViewModel = new LookupLML00400ViewModel(); //use GSL's model
                    var loParam = new LML00400ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID,
                        CCHARGE_TYPE_ID = "08",
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel._ReportParam.CTO_SERVICE_ID, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetUtitlityCharges(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CTO_SERVICE_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel._ReportParam.CTO_SERVICE_ID = loResult.CCHARGES_ID;
                    _viewModel._ReportParam.CTO_SERVICE_NAME = loResult.CCHARGES_NAME; //assign bind textbox name kalo ada
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }
        #endregion

        #region print
        private async Task OnclickBtn_Print()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                //validation
                await Validation();

                //combine data
                GeneratePrintParam();

                //rute report
                if (_viewModel._Report_Type == "1" && _viewModel._GroupBy == "1")
                {
                    await Overtime_PrintSummaryByTenantAsync(_viewModel._ReportParam);
                }
                else if (_viewModel._Report_Type == "1" && _viewModel._GroupBy == "2")
                {
                    await Overtime_PrintSummaryByChargeAsync(_viewModel._ReportParam);
                }
                else if (_viewModel._Report_Type == "2" && _viewModel._GroupBy == "1")
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<PMR00601ParamDTO>(_viewModel._ReportParam);
                    await Overtime_PrintDetailByTenantAsync(loParam);
                }
                else if (_viewModel._Report_Type == "2" && _viewModel._GroupBy == "2")
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<PMR00601ParamDTO>(_viewModel._ReportParam);
                    await Overtime_PrintDetailByChargeAsync(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        private PMR00600ParamDTO GeneratePrintParam()
        {
            R_Exception loEx = new R_Exception();
            var loResult = new PMR00600ParamDTO();
            try
            {
                _viewModel._ReportParam.CPROPERTY_NAME = _viewModel._properties.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel._ReportParam.CPROPERTY_ID).CPROPERTY_NAME;
                _viewModel._ReportParam.CFROM_PERIOD = _viewModel._YearPeriod + _viewModel._MonthFromPeriod;
                _viewModel._ReportParam.CTO_PERIOD = _viewModel._YearPeriod + _viewModel._MonthToPeriod;
                var loFromDate = DateTime.ParseExact(_viewModel._ReportParam.CFROM_PERIOD, "yyyyMM", CultureInfo.InvariantCulture);
                var loToDate = DateTime.ParseExact(_viewModel._ReportParam.CTO_PERIOD, "yyyyMM", CultureInfo.InvariantCulture);
                _viewModel._ReportParam.CPERIOD_DISPLAY = loFromDate.Year != loToDate.Year || loFromDate.Month != loToDate.Month
                    ? $"{loFromDate:MMMM yyyy} – {loToDate:MMMM yyyy}"
                    : $"{loFromDate:MMMM yyyy}";
                _viewModel._ReportParam.CREPORT_TYPE = _viewModel._Report_Type;
                _viewModel._ReportParam.CSTATUS = _viewModel._ReportParam.LSTATUS ? _viewModel._Status : "";
                _viewModel._ReportParam.CINVOICE = _viewModel._ReportParam.LINVOICE ? _viewModel._Invoice : "";
                _viewModel._ReportParam.CSTATUS_DISPLAY = _viewModel._Status == "1" ? _localizer["_radio_Open"] : _localizer["_radio_CLosed"];
                _viewModel._ReportParam.CINVOICE_DISPLAY = _viewModel._Invoice == "1" ? _localizer["_radio_Invoiced"] : _localizer["_radio_Not_Invoiced"];
                _viewModel._ReportParam.CCOMPANY_ID = _clientHelper.CompanyId;
                _viewModel._ReportParam.CUSER_ID = _clientHelper.UserId;
                _viewModel._ReportParam.CREPORT_CULTURE = _clientHelper.ReportCulture.ToString();
                _viewModel._ReportParam.CLANG_ID = _clientHelper.Culture.TwoLetterISOLanguageName;
                _viewModel._ReportParam.CREPORT_TYPE_DISPLAY = _viewModel._Report_Type == "1" ? _localizer["_radio_Summary"] : _localizer["_radio_Detail"];
                _viewModel._ReportParam.CGROUP_TYPE_DISPLAY = _viewModel._GroupBy == "1" ? _localizer["_radio_Tenant"] : _localizer["_radio_Charge"];
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        private async Task Validation()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel._ReportParam.CPROPERTY_ID))
                {
                    loEx.Add("", _localizer["_val1"]);
                }
                if (string.IsNullOrWhiteSpace(_viewModel._MonthFromPeriod) || string.IsNullOrWhiteSpace(_viewModel._MonthToPeriod) || _viewModel._YearPeriod == 0)
                {
                    loEx.Add("", _localizer["_val2"]);
                }
                if (string.IsNullOrWhiteSpace(_viewModel._ReportParam.CFROM_BUILDING_ID) || string.IsNullOrWhiteSpace(_viewModel._ReportParam.CTO_BUILDING_ID))
                {
                    loEx.Add("", _localizer["_val3"]);
                }
                if (string.IsNullOrWhiteSpace(_viewModel._ReportParam.CFROM_DEPT_CODE) || string.IsNullOrWhiteSpace(_viewModel._ReportParam.CTO_DEPT_CODE))
                {
                    loEx.Add("", _localizer["_val4"]);
                }
                if (_viewModel._ReportParam.LTENANT && (string.IsNullOrWhiteSpace(_viewModel._ReportParam.CFROM_TENANT_ID) || string.IsNullOrWhiteSpace(_viewModel._ReportParam.CTO_TENANT_ID)))
                {
                    loEx.Add("", _localizer["_val5"]);
                }
                if (_viewModel._ReportParam.LSERVICE && (string.IsNullOrWhiteSpace(_viewModel._ReportParam.CFROM_SERVICE_ID) || string.IsNullOrWhiteSpace(_viewModel._ReportParam.CTO_SERVICE_ID)))
                {
                    loEx.Add("", _localizer["_val6"]);

                }
                if (loEx.HasError)
                {
                    loEx.ThrowExceptionIfErrors();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
        private async Task Overtime_PrintSummaryByTenantAsync(PMR00600ParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _reportService.GetReport(
                    "R_DefaultServiceUrlPM",
                    "PM",
                    "rpt/PMR00610Print/DownloadResultPrintPost",
                    "rpt/PMR00610Print/OvertimeSummaryByTenant_ReportListGet",
                    poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Overtime_PrintSummaryByChargeAsync(PMR00600ParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _reportService.GetReport(
                    "R_DefaultServiceUrlPM",
                    "PM",
                    "rpt/PMR00611Print/DownloadResultPrintPost",
                    "rpt/PMR00611Print/OvertimeSummaryByCharge_ReportListGet",
                    poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Overtime_PrintDetailByTenantAsync(PMR00601ParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _reportService.GetReport(
                    "R_DefaultServiceUrlPM",
                    "PM",
                    "rpt/PMR00621Print/DownloadResultPrintPost",
                    "rpt/PMR00621Print/OvertimeDetailByTenant_ReportListGet",
                    poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Overtime_PrintDetailByChargeAsync(PMR00601ParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _reportService.GetReport(
                    "R_DefaultServiceUrlPM",
                    "PM",
                    "rpt/PMR00622Print/DownloadResultPrintPost",
                    "rpt/PMR00622Print/OvertimeDetailByCharge_ReportListGet",
                    poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region save as

        private void BeforeOpen_PopupSaveAsAsync(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.PageTitle = _localizer["_title_saveas"];
            eventArgs.TargetPageType = typeof(PMR00601);
        }

        private async Task AfterOpen_PopupSaveAsAsync(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                string lsReportType = (string)eventArgs.Result;
                if (!string.IsNullOrWhiteSpace(lsReportType))
                {
                    string[] lcResultSaveAs = lsReportType.Split(',');
                    GeneratePrintParam();
                    await Validation();
                    var loSummaryParam = R_FrontUtility.ConvertObjectToObject<PMR00600ParamDTO>(_viewModel._ReportParam);
                    var loDetailParam = R_FrontUtility.ConvertObjectToObject<PMR00601ParamDTO>(_viewModel._ReportParam);
                    loSummaryParam.CREPORT_FILENAME = lcResultSaveAs[0];
                    loSummaryParam.CREPORT_FILEEXT = lcResultSaveAs[1];
                    loDetailParam.CREPORT_FILENAME = lcResultSaveAs[0];
                    loDetailParam.CREPORT_FILEEXT = lcResultSaveAs[1];

                    //rute report
                    if (_viewModel._Report_Type == "1" && _viewModel._GroupBy == "1")
                    {
                        await Overtime_PrintSummaryByTenantAsync(loSummaryParam);
                    }
                    else if (_viewModel._Report_Type == "1" && _viewModel._GroupBy == "2")
                    {
                        await Overtime_PrintSummaryByChargeAsync(loSummaryParam);
                    }
                    else if (_viewModel._Report_Type == "2" && _viewModel._GroupBy == "1")
                    {
                        await Overtime_PrintDetailByTenantAsync(loDetailParam);
                    }
                    else if (_viewModel._Report_Type == "2" && _viewModel._GroupBy == "2")
                    {
                        await Overtime_PrintDetailByChargeAsync(loDetailParam);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion


    }
}

