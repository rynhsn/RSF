using Lookup_PMCOMMON.DTOs.LML01800;
using Lookup_PMCOMMON.DTOs.UtilityDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Lookup_PMCOMMON.DTOs.LML01900;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFrontResources;
using R_BlazorFrontEnd.Helpers;
using System.Linq;

namespace Lookup_PMModel.ViewModel.LML01900
{
    public class LookupLML01900ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        private PublicLookupLMGetRecordModel _modelGetRecord = new PublicLookupLMGetRecordModel();
        public ObservableCollection<LML01900DTO> GetList = new ObservableCollection<LML01900DTO>();
        public LML01900ParamaterDTO Parameter = new LML01900ParamaterDTO();

        public List<DepartmentDTO> DepartmentList = new List<DepartmentDTO>();
        //  public DepartmentDTO DepartmentValue = new DepartmentDTO();
        public List<SupervisorDTO> SupervisorList = new List<SupervisorDTO>();
        //  public SupervisorDTO SupervisorValue = new SupervisorDTO();
        public List<StaffTypeDTO> StaffTypeList = new List<StaffTypeDTO>();
        //     public StaffTypeDTO StaffTypeValue = new StaffTypeDTO();
        public bool LIncludeSupervisor = false;
        public bool LIncludeInactive = false;
        public bool LParamSpvExist = true;

        public bool LenabledFilter_Department;
        public bool LenabledFilter_Supervisor;
        public bool LenabledFilter_StaffType;
        public async Task GetDepartmentTypeList()
        {
            R_Exception loEx = new R_Exception();
            List<DepartmentDTO>? loReturn = new List<DepartmentDTO>();
            try
            {
                LenabledFilter_Department = true;
                var tempResult = await _model.DepartmentListAsync();
                if (tempResult.Data.Count > 0)
                {
                    DepartmentList = tempResult.Data;
                    if (!string.IsNullOrWhiteSpace(Parameter.CDEPT_CODE))
                    {
                        var DepartmentValue = DepartmentList.FirstOrDefault(item => item.CDEPT_CODE == Parameter.CDEPT_CODE)
                                          ?? new DepartmentDTO();
                        Parameter.CDEPT_CODE = DepartmentValue.CDEPT_CODE;
                        Parameter.CDEPT_NAME = DepartmentValue.CDEPT_NAME;
                        LenabledFilter_Department = false;
                    }
                    else
                    {
                        var DepartmentValue = DepartmentList[0];
                        Parameter.CDEPT_CODE = DepartmentValue.CDEPT_CODE;
                        Parameter.CDEPT_NAME = DepartmentValue.CDEPT_NAME;
                    }
                }
                else
                {
                    DepartmentList = new List<DepartmentDTO>();
                    Parameter.CDEPT_CODE = "";
                    Parameter.CDEPT_NAME = "";
                    SupervisorList = new List<SupervisorDTO>();
                    Parameter.CSUPERVISOR_ID = "";
                    Parameter.CSUPERVISOR_NAME = "";
                    StaffTypeList = new List<StaffTypeDTO>();
                    Parameter.CSTAFF_TYPE_ID = "";
                    Parameter.CSTAFF_TYPE_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetSupervisorTypeList()
        {
            R_Exception loEx = new R_Exception();
            List<SupervisorDTO>? loReturn = new List<SupervisorDTO>();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, Parameter.CPROPERTY_ID);
                LenabledFilter_Supervisor = true;
                var tempResult = await _model.SupervisorListAsync();
                if (tempResult.Data.Count > 0)
                {
                    SupervisorList = tempResult.Data;
                    if (!string.IsNullOrWhiteSpace(Parameter.CSUPERVISOR_ID))
                    {
                        var SupervisorValue = SupervisorList.FirstOrDefault(item => item.CSUPERVISOR == Parameter.CSUPERVISOR_ID)
                                                   ?? new SupervisorDTO();

                        Parameter.CSUPERVISOR_ID = SupervisorValue.CSUPERVISOR;
                        Parameter.CSUPERVISOR_NAME = SupervisorValue.CSUPERVISOR_NAME;
                        LenabledFilter_Supervisor = false;
                    }
                    else
                    {
                        var SupervisorValue = SupervisorList[0];
                        Parameter.CSUPERVISOR_ID = SupervisorValue.CSUPERVISOR;
                        Parameter.CSUPERVISOR_NAME = SupervisorValue.CSUPERVISOR_NAME;
                    }
                }
                else
                {
                    SupervisorList = new List<SupervisorDTO>();
                    Parameter.CSUPERVISOR_ID = "";
                    Parameter.CSUPERVISOR_NAME = "";
                    StaffTypeList = new List<StaffTypeDTO>();
                    Parameter.CSTAFF_TYPE_ID = "";
                    Parameter.CSTAFF_TYPE_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetStaffTypeList()
        {
            R_Exception loEx = new R_Exception();
            List<UnitDTO>? loReturn = new List<UnitDTO>();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CAPPLICATION, "BIMASAKTI");
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCLASS_ID, "_BS_STAFF_TYPE");
                LenabledFilter_StaffType = true;
                var tempResult = await _model.StaffTypeListAsync();

                if (tempResult.Data.Count == 0)
                {
                    StaffTypeList = new List<StaffTypeDTO>();
                    Parameter.CSTAFF_TYPE_ID = "";
                    Parameter.CSTAFF_TYPE_NAME = "";
                    return;
                }

                StaffTypeList = tempResult.Data;

                // default
                var defaultStaffType = StaffTypeList.FirstOrDefault() ?? new StaffTypeDTO();

                if (string.IsNullOrWhiteSpace(Parameter.CSTAFF_TYPE_ID) || Parameter.CSTAFF_TYPE_ID == "00")
                {
                    Parameter.CSTAFF_TYPE_ID = defaultStaffType.CCODE;
                    Parameter.CSTAFF_TYPE_NAME = defaultStaffType.CNAME;
                    LenabledFilter_StaffType = true;
                }
                else
                {
                    var staffTypeValue = StaffTypeList.FirstOrDefault(item => item.CCODE == Parameter.CSTAFF_TYPE_ID)
                                         ?? new StaffTypeDTO();

                    Parameter.CSTAFF_TYPE_ID = staffTypeValue.CCODE;
                    Parameter.CSTAFF_TYPE_NAME = staffTypeValue.CNAME;
                    LenabledFilter_StaffType = false;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetStaffList(LML01900ParamaterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID ?? "");
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CSUPERVISOR_ID, poParam.CSUPERVISOR_ID ?? "");
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.LSUPERVISOR, poParam.LSUPERVISOR);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CDEPT_CODE, poParam.CDEPT_CODE ?? "");
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CACTIVE_INACTIVE, poParam.CACTIVE_INACTIVE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CSTAFF_TYPE, poParam.CSTAFF_TYPE_ID ?? "");
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CSTAFF_ID, "");
                var loResult = await _model.LML01900StaffListAsync();

                GetList = new ObservableCollection<LML01900DTO>(loResult.Data);
                if (GetList.Count() < 1)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class_LookupPM), "_NotFound");
                    loEx.Add(loErr);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public void ValidationFieldEmpty()
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(Parameter.CDEPT_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class_LookupPM), "_ValidationDepartment");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(Parameter.CSUPERVISOR_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class_LookupPM), "_ValidationSupervisor");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(Parameter.CSTAFF_TYPE_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class_LookupPM), "_ValidationStaffType");
                    loEx.Add(loErr);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.HasError)
            {
                loEx.ThrowExceptionIfErrors();
            }
        }
        public async Task<LML01900DTO> GetStaff(LML01900ParamaterDTO poParam)
        {
            var loEx = new R_Exception();
            LML01900DTO loRtn = null;
            try
            {
                LML01900DTO loResult = await _modelGetRecord.LML01900StaffAsync(poParam);
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn!;
        }
    }
}
