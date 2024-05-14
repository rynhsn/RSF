using BlazorClientHelper;
using GSM02500COMMON.DTOs.GSM02520;
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
using System.Collections.ObjectModel;
using GSM02500COMMON.DTOs.GSM02502;
using R_BlazorFrontEnd.Interfaces;

namespace GSM02500FRONT
{
    public partial class UploadUnitTypeCategory : R_Page
    {
        [Inject] public R_IExcel Excel { get; set; }
        [Inject] private IClientHelper loClientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private UploadUnitTypeCategoryViewModel loUploadUnitTypeCategoryViewModel = new UploadUnitTypeCategoryViewModel();

        private R_ConductorGrid _conGridUploadUnitTypeCategoryRef;

        private R_Grid<UploadUnitTypeCategoryDTO> _gridUploadUnitTypeCategoryRef;

        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };

        private bool IsProcessEnabled = false;

        public void StateChangeInvoke()
        {
            StateHasChanged();
        }

        public void ShowErrorInvoke(R_Exception poException)
        {
            this.R_DisplayException(poException);
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
                loUploadUnitTypeCategoryViewModel.loParameter = (UploadUnitTypeCategoryParameterDTO)poParameter;
                loUploadUnitTypeCategoryViewModel.SelectedUserId = loClientHelper.UserId;
                loUploadUnitTypeCategoryViewModel.SelectedCompanyId = loClientHelper.CompanyId;
                loUploadUnitTypeCategoryViewModel.StateChangeAction = StateChangeInvoke;
                loUploadUnitTypeCategoryViewModel.ShowErrorAction = ShowErrorInvoke;
                loUploadUnitTypeCategoryViewModel.ShowSuccessAction = async () =>
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
            List<UploadUnitTypeCategoryExcelDTO> loExtract = new List<UploadUnitTypeCategoryExcelDTO>();
            try
            {
                var loDataSet = Excel.R_ReadFromExcel(loUploadUnitTypeCategoryViewModel.fileByte, new string[] { "UnitTypeCategory" });

                var loResult = R_FrontUtility.R_ConvertTo<UploadUnitTypeCategoryExcelDTO>(loDataSet.Tables[0]);

                loExtract = new List<UploadUnitTypeCategoryExcelDTO>(loResult);

                loUploadUnitTypeCategoryViewModel.loUploadUnitTypeCategoryList = loExtract.Select((x, i) => new UploadUnitTypeCategoryDTO
                {
                    No = i + 1,
                    UnitTypeCategoryCode = x.UnitTypeCategoryCode,
                    UnitTypeCategoryName = x.UnitTypeCategoryName,
                    Description = x.Description,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    Notes = "",
                    CompanyId = loUploadUnitTypeCategoryViewModel.SelectedCompanyId,
                    PropertyId = loUploadUnitTypeCategoryViewModel.loParameter.PropertyData.CPROPERTY_ID
                }).ToList();
                loUploadUnitTypeCategoryViewModel.loUploadUnitTypeCategoryDisplayList = new ObservableCollection<UploadUnitTypeCategoryDTO>(loUploadUnitTypeCategoryViewModel.loUploadUnitTypeCategoryList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                var MatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
                loUploadUnitTypeCategoryViewModel.IsErrorEmptyFile = MatchingError != null;
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnSaveToExcel()
        {
            var loEx = new R_Exception();

            try
            {
                List<SaveUnitTypeCategoryToExcelDTO> loExcelList = new List<SaveUnitTypeCategoryToExcelDTO>();

                loExcelList = loUploadUnitTypeCategoryViewModel.loUploadUnitTypeCategoryDisplayList.Select(x => new SaveUnitTypeCategoryToExcelDTO()
                {
                    No = x.No,
                    UnitTypeCategoryCode = x.UnitTypeCategoryCode,
                    UnitTypeCategoryName = x.UnitTypeCategoryName,
                    Description = x.Description,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    Valid = x.Valid,
                    Notes = x.Notes,
                }).ToList();

                var loDataTable = R_FrontUtility.R_ConvertTo(loExcelList);
                loDataTable.TableName = "UnitTypeCategory";

                //export to excel
                var loByteFile = Excel.R_WriteToExcel(loDataTable);
                var saveFileName = $"Upload Error Unit Type Category.xlsx";

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
                loUploadUnitTypeCategoryViewModel.fileByte = loMS.ToArray();

                ReadExcelFile();

                if (eventArgs.File.Name.Contains(".xlsx") == false)
                {
                    await R_MessageBox.Show("", _localizer["M003"], R_eMessageBoxButtonType.OK);
                }
                await _gridUploadUnitTypeCategoryRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                if (loUploadUnitTypeCategoryViewModel.IsErrorEmptyFile)
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
                //await loUploadUnitTypeCategoryViewModel.ValidateUploadUnitTypeCategory();
                loUploadUnitTypeCategoryViewModel.SumList = loUploadUnitTypeCategoryViewModel.loUploadUnitTypeCategoryDisplayList.Count();
                if (loUploadUnitTypeCategoryViewModel.SumList > 0)
                {
                    IsProcessEnabled = true;
                }
                else
                {
                    IsProcessEnabled = false;
                }
                eventArgs.ListEntityResult = loUploadUnitTypeCategoryViewModel.loUploadUnitTypeCategoryDisplayList;
                loUploadUnitTypeCategoryViewModel.IsUploadSuccesful = !loUploadUnitTypeCategoryViewModel.VisibleError;
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
                await _conGridUploadUnitTypeCategoryRef.R_SaveBatch();
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
                loUploadUnitTypeCategoryViewModel.loUploadUnitTypeCategoryList = (List<UploadUnitTypeCategoryDTO>)eventArgs.Data;
                await loUploadUnitTypeCategoryViewModel.SaveUploadUnitTypeCategoryAsync();
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