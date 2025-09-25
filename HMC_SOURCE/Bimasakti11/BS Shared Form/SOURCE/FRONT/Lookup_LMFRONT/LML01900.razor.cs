using Lookup_PMCOMMON.DTOs.LML01800;
using Lookup_PMCOMMON.DTOs.LML01900;
using Lookup_PMCOMMON.DTOs.UtilityDTO;
using Lookup_PMModel.ViewModel.LML01800;
using Lookup_PMModel.ViewModel.LML01900;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_PMFRONT
{
    public partial class LML01900 : R_Page
    {
        private LookupLML01900ViewModel _viewModel = new LookupLML01900ViewModel();
        private R_Grid<LML01900DTO> GridRef;
        private int _pageSize = 12;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.Parameter = (LML01900ParamaterDTO)poParameter;
                _viewModel.LIncludeSupervisor = _viewModel.Parameter.LSUPERVISOR;
                //_viewModel.LParamSpvExist = !string.IsNullOrEmpty(_viewModel.Parameter.CSUPERVISOR_ID);
                _viewModel.Parameter.CACTIVE_INACTIVE = string.IsNullOrEmpty(_viewModel.Parameter.CACTIVE_INACTIVE) ? "1" : _viewModel.Parameter.CACTIVE_INACTIVE;
                _viewModel.LIncludeInactive = _viewModel.Parameter.CACTIVE_INACTIVE == "0";

                await GridRef!.R_RefreshGrid(_viewModel.Parameter);
                await DepartmentListRecord(null);
                await StaffTypeListRecord(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #region Department
        private async Task DepartmentListRecord(R_ServiceGetListRecordEventArgs? eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetDepartmentTypeList();

                if (_viewModel.DepartmentList.Count > 0)
                {
                    await SupervisorListRecord(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task DepartmentDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();
            string lsDept = (string)poParam;
            try
            {
                _viewModel.GetList = new();

                DepartmentDTO DeptTemp = _viewModel.DepartmentList
                    .FirstOrDefault(data => data.CDEPT_CODE == lsDept)!;

                _viewModel.Parameter.CDEPT_CODE = DeptTemp.CDEPT_CODE;
                _viewModel.Parameter.CDEPT_NAME= DeptTemp.CDEPT_NAME;

                if (_viewModel.StaffTypeList.Count > 0)
                {
                    _viewModel.Parameter.CSTAFF_TYPE_ID = _viewModel.StaffTypeList[0].CCODE;
                    _viewModel.Parameter.CSTAFF_TYPE_NAME = _viewModel.StaffTypeList[0].CNAME;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }
        #endregion
        #region Supervisor
        private async Task SupervisorListRecord(R_ServiceGetListRecordEventArgs? eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetSupervisorTypeList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task SupervisorDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();
            string lsSupervisor = (string)poParam;
            try
            {
                _viewModel.GetList = new(); 
                SupervisorDTO SupervisorTemp = _viewModel.SupervisorList
                    .FirstOrDefault(data => data.CSUPERVISOR == lsSupervisor)!;
                _viewModel.Parameter.CSUPERVISOR_ID = SupervisorTemp.CSUPERVISOR;
                _viewModel.Parameter.CSUPERVISOR_NAME = SupervisorTemp.CSUPERVISOR_NAME;

                if (_viewModel.StaffTypeList.Count > 0)
                {
                    _viewModel.Parameter.CSTAFF_TYPE_ID = _viewModel.StaffTypeList[0].CCODE;
                    _viewModel.Parameter.CSTAFF_TYPE_NAME = _viewModel.StaffTypeList[0].CNAME;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }
        #endregion
        #region Staff Type
        private async Task StaffTypeListRecord(R_ServiceGetListRecordEventArgs? eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetStaffTypeList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task StaffTypeDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();
            string lsStaffType = (string)poParam;
            try
            {
                _viewModel.GetList = new();
                StaffTypeDTO StaffTypeTemp = _viewModel.StaffTypeList
                    .FirstOrDefault(data => data.CCODE == lsStaffType)!;

                _viewModel.Parameter.CSTAFF_TYPE_ID = StaffTypeTemp.CCODE;
                _viewModel.Parameter.CSTAFF_TYPE_NAME= StaffTypeTemp.CNAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }

        private async Task BtnRefresh()
        {
            var loEx = new R_Exception();
            try
            {
               // _viewModel.ValidationFieldEmpty();
                //_viewModel.Parameter.CDEPT_CODE = _viewModel.DepartmentValue.CDEPT_CODE;
                //_viewModel.Parameter.CSUPERVISOR_ID = _viewModel.SupervisorValue.CSUPERVISOR;
                //_viewModel.Parameter.CSTAFF_TYPE_ID = _viewModel.StaffTypeValue.CCODE;
                _viewModel.Parameter.LSUPERVISOR = _viewModel.LIncludeSupervisor;

                string tempActive = _viewModel.LIncludeInactive == true ? "0" : "1";
                _viewModel.Parameter.CACTIVE_INACTIVE = tempActive;

                await GridRef!.R_RefreshGrid(_viewModel.Parameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
           await  R_DisplayExceptionAsync(loEx);
        }
        public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (LML01900ParamaterDTO)eventArgs.Parameter;
                await _viewModel.GetStaffList(loParam);
                eventArgs.ListEntityResult = _viewModel.GetList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        public async Task Button_OnClickOkAsync()
        {
            var loData = GridRef.GetCurrentData();
            await this.Close(true, loData);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }
        #endregion
    }
}
