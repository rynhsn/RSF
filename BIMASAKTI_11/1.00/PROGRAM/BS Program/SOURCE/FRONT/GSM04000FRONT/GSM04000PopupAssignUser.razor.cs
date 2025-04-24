using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using GSM04000Common;
using GSM04000Model;

namespace GSM04000Front
{
    public partial class GSM04000PopupAssignUser : R_Page
    {
        private GSM04100ViewModel _deptUserViewModel = new GSM04100ViewModel();
        private R_Grid<UserDepartmentDTO> _gridDeptUserToAssignRef;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                string lcDeptCode = (string)poParameter;
                _deptUserViewModel._DepartmentCode = lcDeptCode;
                await _gridDeptUserToAssignRef.R_RefreshGrid(lcDeptCode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _deptUserViewModel.GetUserToAssignList();
                eventArgs.ListEntityResult = _deptUserViewModel._UsersToAssignList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        public async Task Button_OnClickOkAsync()
        {
            var loData = _gridDeptUserToAssignRef.GetCurrentData();
            await this.Close(true, loData);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }
    }
}

