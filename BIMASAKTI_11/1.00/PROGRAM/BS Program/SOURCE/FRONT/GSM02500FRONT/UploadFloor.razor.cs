using BlazorClientHelper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02520;
using GSM02500MODEL.View_Model;
using System.Collections.ObjectModel;
using GSM02500COMMON.DTOs.GSM02530;
using R_BlazorFrontEnd.Enums;
using GSM02500COMMON.DTOs.GSM02500;
using System.Data;
using R_BlazorFrontEnd.Interfaces;

namespace GSM02500FRONT
{
    public partial class UploadFloor : R_Page
    {
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] public R_IExcel Excel { get; set; }
        [Inject] private IClientHelper loClientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        private DataSet loExcelDataSet = new DataSet();

        private UploadFloorViewModel loUploadFloorViewModel = new UploadFloorViewModel();

        private R_ConductorGrid _conGridUploadFloorRef;

        private R_Grid<UploadFloorDTO> _gridUploadFloorRef;

        private UploadUnitViewModel loUploadUnitViewModel = new UploadUnitViewModel();

        private R_ConductorGrid _conGridUploadUnitRef;

        private R_Grid<UploadUnitDTO> _gridUploadUnitRef;

        private R_TabStrip _tabStripRef;

        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };

        private bool IsProcessEnabled = false;

        private bool IsUploadCancel = false;

        private int lnValid = 0;

        private int lnInvalid = 0;

        private int lnTotalRow = 0;

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
        public async Task StartUploadUnitInvoke()
        {
            await _conGridUploadUnitRef.R_SaveBatch();
        }

        public void SetValidInvalidInvoke()
        {
            if (_tabStripRef.ActiveTab.Id == "Floor")
            {
                lnValid = loUploadFloorViewModel.SumValid;
                lnInvalid = loUploadFloorViewModel.SumInvalid;
                lnTotalRow = loUploadFloorViewModel.SumList;
            }
            else if (_tabStripRef.ActiveTab.Id == "Unit")
            {
                lnValid = loUploadUnitViewModel.SumValid;
                lnInvalid = loUploadUnitViewModel.SumInvalid;
                lnTotalRow = loUploadUnitViewModel.SumList;
            }
        }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loUploadFloorViewModel.loParameter = (UploadFloorParameterDTO)poParameter;
                loUploadFloorViewModel.SelectedUserId = loClientHelper.UserId;
                loUploadFloorViewModel.SelectedCompanyId = loClientHelper.CompanyId;

                loUploadFloorViewModel.StateChangeAction = StateChangeInvoke;
                loUploadFloorViewModel.ShowErrorAction = ShowErrorInvoke;
                loUploadFloorViewModel.SetPercentageAndMessageAction = SetPercentageAndMessageInvoke;
                loUploadFloorViewModel.SetValidInvalidAction = SetValidInvalidInvoke;
                loUploadFloorViewModel.ShowSuccessAction = async () =>
                {
                    await ShowSuccessInvoke();
                };
                loUploadFloorViewModel.StartUploadUnitAction = async () =>
                {
                    await StartUploadUnitInvoke();
                };

                loUploadUnitViewModel.SelectedUserId = loClientHelper.UserId;
                loUploadUnitViewModel.SelectedCompanyId = loClientHelper.CompanyId;

                loUploadUnitViewModel.loParameter.PropertyData = loUploadFloorViewModel.loParameter.PropertyData;
                loUploadUnitViewModel.loParameter.BuildingData = loUploadFloorViewModel.loParameter.BuildingData;
                loUploadUnitViewModel.loParameter.FloorData = new SelectedFloorDTO();

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

        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            if (eventArgs.Id == "Floor")
            {
                lnValid = loUploadFloorViewModel.SumValid;
                lnInvalid = loUploadFloorViewModel.SumInvalid;
                lnTotalRow = loUploadFloorViewModel.SumList;
            }
            else if (eventArgs.Id == "Unit")
            {
                lnValid = loUploadUnitViewModel.SumValid;
                lnInvalid = loUploadUnitViewModel.SumInvalid;
                lnTotalRow = loUploadUnitViewModel.SumList;
            }
        }


        public void ReadExcelUnitSheetFile()
        {
            var loEx = new R_Exception();
            List<UploadUnitExcelDTO> loExtract = new List<UploadUnitExcelDTO>();
            try
            {
                var loDataSet = Excel.R_ReadFromExcel(loUploadFloorViewModel.fileByte, new string[] { "Unit" });

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

        public void ReadExcelFloorSheetFile()
        {
            var loEx = new R_Exception();
            List<UploadFloorExcelDTO> loExtract = new List<UploadFloorExcelDTO>();
            try
            {
                var loDataSet = Excel.R_ReadFromExcel(loUploadFloorViewModel.fileByte, new string[] { "Floor" });

                var loResult = R_FrontUtility.R_ConvertTo<UploadFloorExcelDTO>(loDataSet.Tables[0]);

                loExtract = new List<UploadFloorExcelDTO>(loResult);

                loUploadFloorViewModel.loUploadFloorList = loExtract.Select((x, i) => new UploadFloorDTO
                {
                    No = i + 1,
                    FloorCode = x.FloorCode,
                    FloorName = x.FloorName,
                    Description = x.Description,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    UnitType = x.UnitType,
                    UnitCategory = x.UnitCategory,
                    Notes = ""
                }).ToList();

                loUploadFloorViewModel.loUploadFloorDisplayList = new ObservableCollection<UploadFloorDTO>(loUploadFloorViewModel.loUploadFloorList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                var MatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
                loUploadFloorViewModel.IsErrorEmptyFile = MatchingError != null;
            }
            loEx.ThrowExceptionIfErrors();
        }
/*
        private void R_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (UploadFloorDTO)eventArgs.Data;

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
            R_Exception loEx = new R_Exception();
            List<SaveFloorToExcelDTO> loExcelFloorList = new List<SaveFloorToExcelDTO>();
            List<SaveUnitFloorToExcelDTO> loExcelUnitList = new List<SaveUnitFloorToExcelDTO>();
            DataTable loDataTable;
            try
            {

                loExcelFloorList = loUploadFloorViewModel.loUploadFloorDisplayList.Select(x => new SaveFloorToExcelDTO()
                {
                    No = x.No,
                    FloorCode = x.FloorCode,
                    FloorName = x.FloorName,
                    Description = x.Description,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    UnitType = x.UnitType,
                    UnitCategory = x.UnitCategory,
                    Valid = x.Valid,
                    Notes = x.Notes,
                }).ToList();

                loDataTable = R_FrontUtility.R_ConvertTo(loExcelFloorList);
                loDataTable.TableName = "Floor";

                loExcelDataSet.Tables.Add(loDataTable);

                loExcelUnitList = loUploadUnitViewModel.loUploadUnitDisplayList.Select(x => new SaveUnitFloorToExcelDTO()
                {
                    No = x.No,
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
                    Valid = x.Valid,
                    Notes = x.Notes
                }).ToList();

                loDataTable = R_FrontUtility.R_ConvertTo(loExcelUnitList);
                loDataTable.TableName = "Unit";

                loExcelDataSet.Tables.Add(loDataTable);

                //export to excel
                var loByteFile = Excel.R_WriteToExcel(loExcelDataSet);
                var saveFileName = $"Upload Error Floor.xlsx";

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

                if (eventArgs.File.Name.Contains(".xlsx") == false)
                {
                    await R_MessageBox.Show("", _localizer["M003"], R_eMessageBoxButtonType.OK);
                    return;
                }

                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                loUploadFloorViewModel.fileByte = loMS.ToArray();

                ReadExcelFloorSheetFile();
                ReadExcelUnitSheetFile();

                await _gridUploadFloorRef.R_RefreshGrid(null);
                await _gridUploadUnitRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                if (loUploadFloorViewModel.IsErrorEmptyFile || loUploadUnitViewModel.IsErrorEmptyFile)
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

        private async Task Grid_R_ServiceGetFloorListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //await loUploadFloorViewModel.ValidateUploadFloor();

                loUploadFloorViewModel.SumList = loUploadFloorViewModel.loUploadFloorDisplayList.Count();
                if (loUploadFloorViewModel.SumList > 0)
                {
                    IsProcessEnabled = true;
                }
                else
                {
                    IsProcessEnabled = false;
                }

                eventArgs.ListEntityResult = loUploadFloorViewModel.loUploadFloorDisplayList;
                loUploadFloorViewModel.IsUploadSuccesful = !loUploadFloorViewModel.VisibleError;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_R_ServiceGetUnitListRecord(R_ServiceGetListRecordEventArgs eventArgs)
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
                IsUploadCancel = false;
                await _conGridUploadFloorRef.R_SaveBatch();
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
        private async Task R_BeforeSaveFloorBatch(R_BeforeSaveBatchEventArgs events)
        {
            R_Exception loEx = new R_Exception();
            try
            {

                var loTemp = await R_MessageBox.Show("", _localizer["M005"], R_eMessageBoxButtonType.YesNo);

                if (loTemp == R_eMessageBoxResult.No)
                {
                    events.Cancel = true;
                    IsUploadCancel = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_ServiceSaveFloorBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loUploadFloorViewModel.loUploadFloorList = (List<UploadFloorDTO>)eventArgs.Data;
                await loUploadFloorViewModel.SaveUploadFloorAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_AfterSaveFloorBatch(R_AfterSaveBatchEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_BeforeSaveUnitBatch(R_BeforeSaveBatchEventArgs events)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                events.Cancel = IsUploadCancel;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_ServiceSaveUnitBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

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

        private async Task R_AfterSaveUnitBatch(R_AfterSaveBatchEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

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