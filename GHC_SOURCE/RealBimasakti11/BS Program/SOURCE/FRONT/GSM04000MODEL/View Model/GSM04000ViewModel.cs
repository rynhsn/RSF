using GSM04000Common;
using GSM04000Common.DTO_s;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GSM04000Model
{
    public class GSM04000ViewModel : R_ViewModel<DepartmentDTO>
    {
        private GSM04000Model _model = new GSM04000Model();

        public ObservableCollection<DepartmentDTO> _DepartmentList { get; set; } = new ObservableCollection<DepartmentDTO>();

        public DepartmentDTO _Department { get; set; } = new DepartmentDTO();

        public string _departmentCode { get; set; } = "";

        public bool _activeDept { get; set; }

        public bool _isUserDeptExist { get; set; }

        public string _sourceFileName { get; set; }

        #region Department

        public async Task GetDepartmentList()
        {
            R_Exception loEx = new R_Exception();
            List<DepartmentDTO> loResult = null;
            try
            {
                loResult = new List<DepartmentDTO>();
                loResult = await _model.GetGSM04000ListAsync();
                _DepartmentList = new ObservableCollection<DepartmentDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetDepartment(DepartmentDTO poDept)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                DepartmentDTO loParam = new DepartmentDTO();
                loParam = poDept;
                var loResult = await _model.R_ServiceGetRecordAsync(loParam);
                _Department = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveDepartment(DepartmentDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                _Department = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteDepartment(DepartmentDTO poDept)
        {
            var loEx = new R_Exception();

            try
            {
                DepartmentDTO loParam = new DepartmentDTO();
                loParam = poDept;
                await _model.R_ServiceDeleteAsync(poDept);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ActiveInactiveProcessAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _model.ActiveInactiveDepartmentAsync( new ActiveInactiveParam()
                {
                    CDEPT_CODE = _departmentCode,
                    LNEW_ACTIVE_STATUS=_activeDept,
                });
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task CheckIsUserDeptExistAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.CheckIsUserDeptExistAsync(new DepartmentDTO()
                {
                    CDEPT_CODE = _departmentCode,
                });
                _isUserDeptExist = loResult.LIS_USER_DEPT_EXIST;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteAssignedUserWhenChangeEveryone()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _model.DeleteDeptUserWhenChangingEveryoneAsync(new DepartmentDTO()
                {
                    CDEPT_CODE = _departmentCode
                });
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region 

        public async Task<UploadFileDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            UploadFileDTO loResult = null;

            try
            {
                loResult = await _model.DownloadUploadDeptTemplateAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #endregion


    }

}

