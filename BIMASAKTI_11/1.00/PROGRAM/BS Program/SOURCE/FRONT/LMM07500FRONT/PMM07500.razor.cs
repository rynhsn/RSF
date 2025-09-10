using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using PMM07500COMMON;
using PMM07500COMMON.DTO_s.stamp_amount;
using PMM07500COMMON.DTO_s.stamp_code;
using PMM07500COMMON.DTO_s.stamp_date;
using PMM07500FrontResources;
using PMM07500MODEL.View_Models;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System.Globalization;

namespace PMM07500FRONT
{
    public partial class PMM07500 : R_Page
    {
        private PMM07500ViewModel _stampCodeViewModel = new();
        private PMM07510ViewModel _stampDateViewModel = new();
        private PMM07520ViewModel _stampAmountViewModel = new();

        private R_ConductorGrid _conStampCode;
        private R_ConductorGrid _conStampDate;
        private R_ConductorGrid _conStampAmount;

        private R_Grid<PMM07500GridDTO> _gridStampCode;
        private R_Grid<PMM07510GridDTO> _gridStampDate;
        private R_Grid<PMM07520GridDTO> _gridStampAmount;

        private R_ComboBox<PropertyDTO, string> _comboboxProperty;

        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper _clientHelper { get; set; }

        private bool _enableGridStampCode = true;
        private bool _enableGridStampDate = true;
        private bool _enableGridStampAmount = true;
        private bool _enableComboboxProperty = true;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _stampCodeViewModel.GetPropertyList();
                await _stampCodeViewModel.GetCurrencyList();
                await Task.Delay(300);
                if (!string.IsNullOrWhiteSpace(_stampCodeViewModel.PropertyId))
                {
                    _stampDateViewModel.PropertyId = _stampCodeViewModel.PropertyId;
                    _stampAmountViewModel.PropertyId = _stampCodeViewModel.PropertyId;
                    await _gridStampCode.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        public async Task ComboboxProperty_ValueChangedAsync(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _stampCodeViewModel.PropertyId = poParam;
                _stampDateViewModel.PropertyId = poParam;
                _stampAmountViewModel.PropertyId = poParam;
                await Task.Delay(300);
                if (!string.IsNullOrWhiteSpace(_stampCodeViewModel.PropertyId))
                {
                    _stampDateViewModel.StampDateList = new();
                    _stampAmountViewModel.StampAmountList = new();
                    await _gridStampCode.R_RefreshGrid(null);
                }
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
                var loData = (PMM07500GridDTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: PMM07500ContextConstant.DEFAULT_MODULE,
                plSendWithContext: true,
                plSendWithToken: true,
                pcHttpClientName: PMM07500ContextConstant.DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = PMM07500ContextConstant.PROGRAM_ID,
                        Table_Name = PMM07500ContextConstant.TABLE_NAME,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CSTAMP_CODE)
                    };


                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = PMM07500ContextConstant.PROGRAM_ID,
                        Table_Name = PMM07500ContextConstant.TABLE_NAME,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CSTAMP_CODE)
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

        //stampcode

        private async Task StampCode_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _stampCodeViewModel.GetStampCodeListAsync();
                eventArgs.ListEntityResult = _stampCodeViewModel.StampRateList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }

        private async Task StampCode_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _stampCodeViewModel.GetStampCodeRecordAsync(R_FrontUtility.ConvertObjectToObject<PMM07500GridDTO>(eventArgs.Data));
                eventArgs.Result = _stampCodeViewModel.StampRate;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task StampCode_RDisplay(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Normal)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<PMM07500GridDTO>(eventArgs.Data);
                    _stampCodeViewModel.StampRate = loParam;
                    _stampDateViewModel.StampCode = loParam.CSTAMP_CODE;
                    _stampAmountViewModel.StampCode = loParam.CSTAMP_CODE;
                    _stampDateViewModel.ParentId = loParam.CREC_ID;
                    _stampAmountViewModel.GrandParentId = loParam.CREC_ID;
                    await _gridStampDate.R_RefreshGrid(null);
                    if (_stampDateViewModel.StampDateList.Count < 1)
                    {
                        _stampAmountViewModel.StampAmountList = new();
                        _stampDateViewModel.EffectiveDateDisplay = null;
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task StampCode_BeforeDeleteAsync(R_BeforeDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (await R_MessageBox.Show(_localizer["_msg_title_confirmation"], _localizer["_confirm_delete_stampcode"], R_BlazorFrontEnd.Controls.MessageBox.R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.No)
                {
                    eventArgs.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task StampCode_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM07500GridDTO>(eventArgs.Data);
                await _stampCodeViewModel.DeleteStampCodeAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        private void StampCode_AfterDelete()
        {
            R_Exception loEx = new();
            try
            {
                _stampCodeViewModel.StampRate = new();
                _stampDateViewModel.StampDateList = new();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void StampCode_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = (PMM07500GridDTO)eventArgs.Data;
                loData.DUPDATE_DATE = new DateTime(2077, 12, 31, 23, 59, 59);
                loData.DCREATE_DATE = new DateTime(2077, 12, 31, 23, 59, 59);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void StampCode_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (PMM07500GridDTO)eventArgs.Data;
                if (string.IsNullOrWhiteSpace(loData.CSTAMP_CODE) || string.IsNullOrEmpty(loData.CSTAMP_CODE))
                {
                    loEx.Add("", _localizer["_val_stampcode1"]);
                    eventArgs.Cancel = true;
                }

                if (string.IsNullOrWhiteSpace(loData.CDESCRIPTION) || string.IsNullOrEmpty(loData.CDESCRIPTION))
                {
                    loEx.Add("", _localizer["_val_stampcode2"]);
                    eventArgs.Cancel = true;
                }

                if (string.IsNullOrWhiteSpace(loData.CCURRENCY_DISPLAY))
                {
                    loEx.Add("", _localizer["_val_stampcode3"]);
                    eventArgs.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        private async Task StampCode_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM07500GridDTO>(eventArgs.Data);
                await _stampCodeViewModel.SaveStampCodeAsync(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _stampCodeViewModel.StampRate;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        private void StampCode_SetOther(R_SetEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                _enableComboboxProperty = eventArgs.Enable;
                _enableGridStampAmount = eventArgs.Enable;
                _enableGridStampDate = eventArgs.Enable;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        //stampdate

        private async Task StampDate_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _stampDateViewModel.GetStampDateListAsync();
                eventArgs.ListEntityResult = _stampDateViewModel.StampDateList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }

        private async Task StampDate_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _stampDateViewModel.GetStampDateRecordAsync(R_FrontUtility.ConvertObjectToObject<PMM07510GridDTO>(eventArgs.Data));
                eventArgs.Result = _stampDateViewModel.StampDate;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task StampDate_RDisplayAsync(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Normal)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<PMM07510GridDTO>(eventArgs.Data);
                    _stampDateViewModel.EffectiveDateDisplay = DateTime.ParseExact(loParam.CDATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                    _stampAmountViewModel.ParentId = loParam.CREC_ID;
                    _stampAmountViewModel.EffectiveDate = loParam.CDATE;
                    await _gridStampAmount.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void StampDate_SetOther(R_SetEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                _enableGridStampCode = eventArgs.Enable;
                _enableGridStampAmount = eventArgs.Enable;
                _enableComboboxProperty = eventArgs.Enable;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void StampDate_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = (PMM07510GridDTO)eventArgs.Data;
                loData.DDATE = DateTime.Now;
                loData.DUPDATE_DATE = DateTime.Now;
                loData.DCREATE_DATE = DateTime.Now;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void StampDate_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (PMM07510GridDTO)eventArgs.Data;
                if (loData.DDATE == null)
                {
                    loEx.Add("", _localizer["_val_stampdate1"]);
                    eventArgs.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        private async Task StampDate_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM07510GridDTO>(eventArgs.Data);
                await _stampDateViewModel.SaveStampDateAsync(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _stampDateViewModel.StampDate;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        private async Task StampDate_BeforeDeleteAsync(R_BeforeDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (await R_MessageBox.Show(_localizer["_msg_title_confirmation"], _localizer["_confirm_delete_stampdate"], R_BlazorFrontEnd.Controls.MessageBox.R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.No)
                {
                    eventArgs.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task StampDate_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _stampDateViewModel.DeleteStampDateAsync(R_FrontUtility.ConvertObjectToObject<PMM07510GridDTO>(eventArgs.Data));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        private void StampDate_AfterDelete()
        {
            R_Exception loEx = new();
            try
            {
                _stampDateViewModel.EffectiveDateDisplay = null;
                _stampAmountViewModel.StampAmountList = new();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        //stampamount

        private async Task StampAmount_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _stampAmountViewModel.GetStampAmountListAsync();
                eventArgs.ListEntityResult = _stampAmountViewModel.StampAmountList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }

        private async Task StampAmount_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _stampAmountViewModel.GetStampAmountRecordAsync(R_FrontUtility.ConvertObjectToObject<PMM07520GridDTO>(eventArgs.Data));
                eventArgs.Result = _stampAmountViewModel.StampAmount;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void StampAmount_RDisplay(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Normal)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<PMM07520GridDTO>(eventArgs.Data);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task StampAmount_BeforeDeleteAsync(R_BeforeDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (await R_MessageBox.Show(_localizer["_msg_title_confirmation"], _localizer["_confirm_delete_stampamount"], R_BlazorFrontEnd.Controls.MessageBox.R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.No)
                {
                    eventArgs.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task StampAmount_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _stampAmountViewModel.DeleteStampAmountAsync(R_FrontUtility.ConvertObjectToObject<PMM07520GridDTO>(eventArgs.Data));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        private void StampAmount_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = (PMM07520GridDTO)eventArgs.Data;
                loData.DUPDATE_DATE = DateTime.Now;
                loData.DCREATE_DATE = DateTime.Now;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void StampAmount_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (PMM07520GridDTO)eventArgs.Data;
                if (loData.NMIN_AMOUNT == 0)
                {
                    loEx.Add("", _localizer["_val_stampamount1"]);
                    eventArgs.Cancel = true;
                }
                if (loData.NSTAMP_AMOUNT == 0)
                {
                    loEx.Add("", _localizer["_val_stampamount2"]);
                    eventArgs.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        private async Task StampAmount_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _stampAmountViewModel.SaveStampAmountAsync(R_FrontUtility.ConvertObjectToObject<PMM07520GridDTO>(eventArgs.Data), (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _stampAmountViewModel.StampAmount;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        private void StampAmount_SetOther(R_SetEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                _enableGridStampCode = eventArgs.Enable;
                _enableGridStampDate = eventArgs.Enable;
                _enableComboboxProperty = eventArgs.Enable;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        //process

        private async Task RefreshBtn_OnclickAsync()
        {
            var loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrWhiteSpace(_stampCodeViewModel.PropertyId))
                {
                    _stampDateViewModel.PropertyId = _stampCodeViewModel.PropertyId;
                    _stampAmountViewModel.PropertyId = _stampCodeViewModel.PropertyId;
                    _stampDateViewModel.StampDateList = new();
                    _stampAmountViewModel.StampAmountList = new();
                    await _gridStampCode.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
    }
}
