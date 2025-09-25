using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using PQM00100COMMON;
using PQM00100COMMON.DTO_s;
using PQM00100MODEL.View_Model_s;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace PQM00100FRONT
{
    public partial class PQM00100 : R_Page
    {
        //var

        private R_Grid<ServiceGridDTO> _gridService;
        private R_ConductorGrid _conService;
        [Inject] private IClientHelper _clientHelper { get; set; }
        [Inject] private R_ILocalizer<PQM00100FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        private int _pageIndexService = PQM00100ContextConstant.GRIDSERVICE_PAGESIZE;
        private string _gridHeightService = PQM00100ContextConstant.GRIDSERVICE_SIZEHEIGHT;
        private PQM00100ViewModel _viewModelService = new();

        //method-override
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new();
            try
            {
                await _gridService.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        protected override async Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (ServiceGridDTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: PQM00100ContextConstant.DEFAULT_MODULE,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: PQM00100ContextConstant.DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = PQM00100ContextConstant.CPROGRAM_ID,
                        Table_Name = PQM00100ContextConstant.TABLE_NAME,
                        Key_Value = string.Join("|", loData.CSERVICE_ID)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = PQM00100ContextConstant.CPROGRAM_ID,
                        Table_Name = PQM00100ContextConstant.TABLE_NAME,
                        Key_Value = string.Join("|", loData.CSERVICE_ID)
                    };

                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }

        //method-grid events
        private async Task ServiceGrid_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelService.GetList_ServiceAsync();
                eventArgs.ListEntityResult = _viewModelService.ServiceList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceGrid_ServiceGetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelService.GetRecord_ServiceAsync(R_FrontUtility.ConvertObjectToObject<ServiceGridDTO>(eventArgs.Data));
                eventArgs.Result = _viewModelService.ServiceRecord;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void ServiceGrid_Display(R_DisplayEventArgs eventArgs)
        {
        }

        private void ServiceGrid_Saving(R_SavingEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                if (eventArgs.Data is ServiceGridDTO loData)
                {
                    loData.GetType().GetProperties()
                        .Where(p => p.PropertyType == typeof(string) && p.CanWrite)
                        .ToList()
                        .ForEach(p => p.SetValue(loData, (p.GetValue(loData) as string)?.Trim()));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceGrid_ValidationAsync(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                //get data
                var loData = R_FrontUtility.ConvertObjectToObject<ServiceGridDTO>(eventArgs.Data);
                if (string.IsNullOrWhiteSpace(loData.CSERVICE_ID))
                {
                    loEx.Add("", _localizer["_val1"]);
                }
                if (string.IsNullOrWhiteSpace(loData.CSERVICE_NAME))
                {
                    loEx.Add("", _localizer["_val2"]);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.HasError)
                eventArgs.Cancel = true;
            loEx.ThrowExceptionIfErrors();
        }

        private void ServiceGrid_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (ServiceGridDTO)eventArgs.Data;
                loData.CCREATE_BY = _clientHelper.UserId;
                loData.CUPDATE_BY = _clientHelper.UserId;
                loData.DCREATE_DATE = DateTime.Now;//set now date when adding data
                loData.DUPDATE_DATE = DateTime.Now;//set now date when adding data
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceGrid_ServiceSaveAsync(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelService.SaveRecord_ServiceAsync((ServiceGridDTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModelService.ServiceRecord;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceGrid_ServiceDeleteAsync(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<ServiceGridDTO>(eventArgs.Data);
                await _viewModelService.DeleteRecord_ServiceAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}