using BlazorClientHelper;
using GSM02500COMMON.DTOs.GSM02540;
using GSM02500MODEL.View_Model;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02541;
using System.Collections.ObjectModel;
using R_BlazorFrontEnd.Interfaces;
using R_APICommonDTO;

namespace GSM02500FRONT
{
    public partial class UploadOtherUnit : R_Page
    {
        [Inject] public R_IExcel Excel { get; set; }
        [Inject] private IClientHelper loClientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private UploadOtherUnitViewModel loUploadOtherUnitViewModel = new UploadOtherUnitViewModel();

        private R_ConductorGrid _conGridUploadOtherUnitRef;

        private R_Grid<UploadOtherUnitDTO> _gridUploadOtherUnitRef;

        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };

        private bool IsProcessEnabled = false;

        public void StateChangeInvoke()
        {
            StateHasChanged();
        }

        public void ShowErrorInvoke(R_APIException poException)
        {
            var loEx = R_FrontUtility.R_ConvertFromAPIException(poException);
            this.R_DisplayException(loEx);
        }

        public async Task ShowSuccessInvoke()
        {
            var loValidate = await R_MessageBox.Show("", _localizer["M002"], R_eMessageBoxButtonType.OK);
            if (loValidate == R_eMessageBoxResult.OK)
            {
                await this.Close(true, true);
            }
        }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loUploadOtherUnitViewModel.loParameter = (UploadOtherUnitParameterDTO)poParameter;
                loUploadOtherUnitViewModel.SelectedUserId = loClientHelper.UserId;
                loUploadOtherUnitViewModel.SelectedCompanyId = loClientHelper.CompanyId;
                loUploadOtherUnitViewModel.StateChangeAction = StateChangeInvoke;
                loUploadOtherUnitViewModel.ShowErrorAction = ShowErrorInvoke;
                loUploadOtherUnitViewModel.ShowSuccessAction = async () =>
                {
                    await ShowSuccessInvoke();
                };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void ReadExcelFile()
        {
            var loEx = new R_Exception();
            List<UploadOtherUnitExcelDTO> loExtract = new List<UploadOtherUnitExcelDTO>();
            try
            {
                var loDataSet = Excel.R_ReadFromExcel(loUploadOtherUnitViewModel.fileByte, new string[] { "OtherUnit" });

                var loResult = R_FrontUtility.R_ConvertTo<UploadOtherUnitExcelDTO>(loDataSet.Tables[0]);

                loExtract = new List<UploadOtherUnitExcelDTO>(loResult);

                loUploadOtherUnitViewModel.loUploadOtherUnitList = loExtract.Select((x, i) => new UploadOtherUnitDTO
                {
                    No = i + 1,
                    OtherUnitId = x.OtherUnitId,
                    OtherUnitName = x.OtherUnitName,
                    OtherUnitType = x.OtherUnitType,
                    Building = x.Building,
                    Floor = x.Floor,
                    Location = x.Location,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    UnitView = x.UnitView,
                    GrossSize = x.GrossSize,
                    NetSize = x.NetSize,
                    LeaseStatus = x.LeaseStatus,
                    Notes = "",
                    CompanyId = loUploadOtherUnitViewModel.SelectedCompanyId,
                    PropertyId = loUploadOtherUnitViewModel.loParameter.PropertyData.CPROPERTY_ID
                }).ToList();

                loUploadOtherUnitViewModel.loUploadOtherUnitDisplayList = new ObservableCollection<UploadOtherUnitDTO>(loUploadOtherUnitViewModel.loUploadOtherUnitList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                var MatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
                loUploadOtherUnitViewModel.IsErrorEmptyFile = MatchingError != null;
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnSaveToExcel()
        {
            var loEx = new R_Exception();

            try
            {
                List<SaveOtherUnitToExcelDTO> loExcelList = new List<SaveOtherUnitToExcelDTO>();

                loExcelList = loUploadOtherUnitViewModel.loUploadOtherUnitDisplayList.Select(x => new SaveOtherUnitToExcelDTO()
                {
                    No = x.No,
                    OtherUnitId = x.OtherUnitId,
                    OtherUnitName = x.OtherUnitName,
                    OtherUnitType = x.OtherUnitType,
                    Building = x.Building,
                    Floor = x.Floor,
                    Location = x.Location,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    UnitView = x.UnitView,
                    GrossSize = x.GrossSize,
                    NetSize = x.NetSize,
                    LeaseStatus = x.LeaseStatus,
                    Valid = x.Valid,
                    Notes = x.Notes,
                }).ToList();

                var loDataTable = R_FrontUtility.R_ConvertTo(loExcelList);
                loDataTable.TableName = "OtherUnit";

                //export to excel
                var loByteFile = Excel.R_WriteToExcel(loDataTable);
                var saveFileName = $"Upload Error Other Unit.xlsx";

                await JS.downloadFileFromStreamHandler(saveFileName, loByteFile);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        B:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnChangeInputFile(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                loUploadOtherUnitViewModel.fileByte = loMS.ToArray();

                ReadExcelFile();

                if (eventArgs.File.Name.Contains(".xlsx") == false)
                {
                    await R_MessageBox.Show("", _localizer["M003"], R_eMessageBoxButtonType.OK);
                }
                await _gridUploadOtherUnitRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                if (loUploadOtherUnitViewModel.IsErrorEmptyFile)
                {
                    await R_MessageBox.Show("", _localizer["M004"], R_eMessageBoxButtonType.OK);
                }
                else
                {
                    loEx.Add(ex);
                }
            }
        B:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //await loUploadOtherUnitViewModel.ValidateUploadOtherUnit();
                loUploadOtherUnitViewModel.SumList = loUploadOtherUnitViewModel.loUploadOtherUnitDisplayList.Count();
                if (loUploadOtherUnitViewModel.SumList > 0)
                {
                    IsProcessEnabled = true;
                }
                else
                {
                    IsProcessEnabled = false;
                }

                eventArgs.ListEntityResult = loUploadOtherUnitViewModel.loUploadOtherUnitDisplayList;
                loUploadOtherUnitViewModel.IsUploadSuccesful = !loUploadOtherUnitViewModel.VisibleError;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnProcess()
        {
            R_Exception loException = new R_Exception();
            try
            {
                await _gridUploadOtherUnitRef.R_SaveBatch();
            }
            catch (Exception ex)
            {

                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private async Task OnCancel()
        {
            await this.Close(true, false);
        }
        private async Task R_BeforeSaveBatch(R_BeforeSaveBatchEventArgs events)
        {
            var loEx = new R_Exception();
            try
            {

                var loTemp = await R_MessageBox.Show("", _localizer["M005"], R_eMessageBoxButtonType.YesNo);

                if (loTemp == R_eMessageBoxResult.No)
                {
                    events.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                loUploadOtherUnitViewModel.loUploadOtherUnitList = (List<UploadOtherUnitDTO>)eventArgs.Data;
                await loUploadOtherUnitViewModel.SaveUploadOtherUnitAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_AfterSaveBatch(R_AfterSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}