using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using GSM04000Common;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using R_CommonFrontBackAPI;
using R_BlazorFrontEnd.Helpers;

namespace GSM04000Model
{
    public class GSM04100ViewModel : R_ViewModel<UserDepartmentDTO>
    {
        private GSM04100Model _model = new GSM04100Model();
        public ObservableCollection<UserDepartmentDTO> _DepartmentUserList { get; set; } = new ObservableCollection<UserDepartmentDTO>();
        public ObservableCollection<UserDepartmentDTO> _UsersToAssignList { get; set; } = new ObservableCollection<UserDepartmentDTO>();
        public UserDepartmentDTO _DepartmentUser { get; set; } = new UserDepartmentDTO();
        public UserDepartmentDTO _UserToAssign { get; set; } = new UserDepartmentDTO();
        public string _DepartmentCode { get; set; } = "";
        public string _DepartmentName { get; set; } = "";

        public const string CPROGRAM_CODE = "GSM04000";
        public async Task GetDeptUserListByDeptCode()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, _DepartmentCode);
                var loResult = await _model.GetUserDeptListAsync();
                _DepartmentUserList = new ObservableCollection<UserDepartmentDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetDepartmentUser(UserDepartmentDTO poDept)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, _DepartmentCode);
                UserDepartmentDTO loParam = new UserDepartmentDTO();
                loParam = poDept;
                var loResult = await _model.R_ServiceGetRecordAsync(loParam);
                _DepartmentUser = R_FrontUtility.ConvertObjectToObject<UserDepartmentDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetUserToAssignList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROGRAM_CODE, CPROGRAM_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, _DepartmentCode);
                var loResult = await _model.GetUserListAsync();
                _UsersToAssignList = new ObservableCollection<UserDepartmentDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task AssignUserToDept(UserDepartmentDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROGRAM_CODE, CPROGRAM_CODE);
                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                _UserToAssign = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteUserDepartment(DepartmentDTO poDept)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<UserDepartmentDTO>(poDept);
                await _model.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
