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
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using GSM02500COMMON.DTOs.GSM02530;
using System.ComponentModel.Design;
using R_BlazorFrontEnd.Interfaces;

namespace GSM02500FRONT
{
    public partial class UploadUnit : R_Page
    {
        [Inject] public R_IExcel Excel { get; set; }
        [Inject] private IClientHelper loClientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private UploadUnitViewModel loUploadUnitViewModel = new UploadUnitViewModel();

        private R_ConductorGrid _conGridUploadUnitRef;

        private R_Grid<UploadUnitDTO> _gridUploadUnitRef;

        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };

        private bool IsProcessEnabled = false;

        private string lcMessage = "";

        private int lnPercentage = 0;

        public void StateChangeInvoke()
        {
            StateHasChanged();
        }

        public void ShowErrorInvoke(R_Exception poException)
        {
            this.R_DisplayException(poException);
        }

        private void SetPercentageAndMessageInvoke(string pcMessage, int pnPercentage)
        {
            lcMessage = pcMessage;
            lnPercentage = pnPercentage;
        }

        public async Task ShowSuccessInvoke()
        {
            var loValidate = await R_MessageBox.Show("", _localizer["M002"], R_eMessageBoxButtonType.OK);
            if (loValidate == R_eMessageBoxResult.OK)
            {
                await this.Close(true, true);
            }
        }

        public void SetValidInvalidInvoke()
        {

        }


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loUploadUnitViewModel.loParameter = (UploadUnitParameterDTO)poParameter;
                loUploadUnitViewModel.SelectedUserId = loClientHelper.UserId;
                loUploadUnitViewModel.SelectedCompanyId = loClientHelper.CompanyId;
                loUploadUnitViewModel.StateChangeAction = StateChangeInvoke;
                loUploadUnitViewModel.ShowErrorAction = ShowErrorInvoke;
                loUploadUnitViewModel.SetPercentageAndMessageAction = SetPercentageAndMessageInvoke;
                loUploadUnitViewModel.SetValidInvalidAction = SetValidInvalidInvoke;
                loUploadUnitViewModel.ShowSuccessAction = async () =>
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
            List<UploadUnitExcelDTO> loExtract = new List<UploadUnitExcelDTO>();
            try
            {
                var loDataSet = Excel.R_ReadFromExcel(loUploadUnitViewModel.fileByte, new string[] { "Unit" });

                var loResult = R_FrontUtility.R_ConvertTo<UploadUnitExcelDTO>(loDataSet.Tables[0]);

                loExtract = new List<UploadUnitExcelDTO>(loResult);

                loUploadUnitViewModel.loUploadUnitList = loExtract.Select((x, i) => new UploadUnitDTO
                {
                    No = i + 1,
                    FloorId = x.FloorId,
                    UnitId = x.UnitId,
                    UnitName = x.UnitName,
                    UnitType = x.UnitType,
                    UnitView = x.UnitView,
                    GrossSize = x.GrossSize,
                    NetSize = x.NetSize,
                    CommonArea = x.CommonArea,
                    UnitCategory = x.UnitCategory,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    Notes = ""
                }).ToList();

                //CompanyId = loUploadUnitViewModel.SelectedCompanyId,
                //    PropertyId = loUploadUnitViewModel.loParameter.PropertyData.CPROPERTY_ID,
                //    BuildingId = loUploadUnitViewModel.loParameter.BuildingData.CBUILDING_ID,
                loUploadUnitViewModel.loUploadUnitDisplayList = new ObservableCollection<UploadUnitDTO>(loUploadUnitViewModel.loUploadUnitList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                var MatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
                loUploadUnitViewModel.IsErrorEmptyFile = MatchingError != null;
            }
            loEx.ThrowExceptionIfErrors();
        }
/*
        private void R_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (UploadUnitDTO)eventArgs.Data;

            if (loData.Var_Exists)
            {
                eventArgs.RowStyle = new R_GridRowRenderStyle
                {
                    FontColor = "red"
                };
            }
        }*/

        private async Task OnSaveToExcel()
        {
            var loEx = new R_Exception();

            try
            {
                List<SaveUnitToExcelDTO> loExcelList = new List<SaveUnitToExcelDTO>();

                loExcelList = loUploadUnitViewModel.loUploadUnitDisplayList.Select(x => new SaveUnitToExcelDTO()
                {
                    No = x.No,
                    UnitId = x.UnitId,
                    UnitName = x.UnitName,
                    UnitType = x.UnitType,
                    UnitView = x.UnitView,
                    GrossSize = x.GrossSize,
                    NetSize = x.NetSize,
                    CommonArea = x.CommonArea,
                    UnitCategory = x.UnitCategory,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    Valid = x.Valid,
                    Notes = x.Notes
                }).ToList();

                var loDataTable = R_FrontUtility.R_ConvertTo(loExcelList);
                loDataTable.TableName = "Unit";

                //export to excel
                var loByteFile = Excel.R_WriteToExcel(loDataTable);
                var saveFileName = $"Upload Error Unit.xlsx";

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
                loUploadUnitViewModel.fileByte = loMS.ToArray();

                ReadExcelFile();

                if (eventArgs.File.Name.Contains(".xlsx") == false)
                {
                    await R_MessageBox.Show("", _localizer["M003"], R_eMessageBoxButtonType.OK);
                }
                await _gridUploadUnitRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                if (loUploadUnitViewModel.IsErrorEmptyFile)
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
                //await loUploadUnitViewModel.ValidateUploadUnit();

                loUploadUnitViewModel.SumList = loUploadUnitViewModel.loUploadUnitDisplayList.Count();
                if (loUploadUnitViewModel.SumList > 0)
                {
                    IsProcessEnabled = true;
                }
                else
                {
                    IsProcessEnabled = false;
                }
                eventArgs.ListEntityResult = loUploadUnitViewModel.loUploadUnitDisplayList;
                loUploadUnitViewModel.IsUploadSuccesful = !loUploadUnitViewModel.VisibleError;
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
                await _conGridUploadUnitRef.R_SaveBatch();
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
                loUploadUnitViewModel.loUploadUnitList = (List<UploadUnitDTO>)eventArgs.Data;
                await loUploadUnitViewModel.SaveUploadUnitAsync();
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