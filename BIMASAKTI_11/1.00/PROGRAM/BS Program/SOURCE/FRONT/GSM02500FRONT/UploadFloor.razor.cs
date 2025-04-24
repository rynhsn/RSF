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
using GSM02500COMMON.DTOs.GSM02531;
using R_APICommonDTO;

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

        private UploadUnitUtilityViewModel loUploadUnitUtilityViewModel = new UploadUnitUtilityViewModel();

        private R_ConductorGrid _conGridUploadUnitUtilityRef;

        private R_Grid<UploadUnitUtilityDTO> _gridUploadUnitUtilityRef;

        private R_TabStrip _tabStripRef;

        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };

        private bool IsProcessEnabled = false;

        private bool IsUploadCancel = false;

        private bool IsErrorHappened = false;

        private int lnValid = 0;

        private int lnInvalid = 0;

        private int lnTotalRow = 0;

        private string lcMessage = "";

        private int lnPercentage = 0;

        public void StateChangeInvoke()
        {
            StateHasChanged();
        }

        public void ShowErrorInvoke(R_APIException poException)
        {
            var loEx = R_FrontUtility.R_ConvertFromAPIException(poException);
            this.R_DisplayException(loEx);
        }

        private void SetPercentageAndMessageInvoke(string pcMessage, int pnPercentage)
        {
            lcMessage = pcMessage;
            lnPercentage = pnPercentage;
        }

        public async Task ShowSuccessInvoke()
        {
            if (loUploadFloorViewModel.IsUploadSuccesful && loUploadUnitViewModel.IsUploadSuccesful && loUploadUnitUtilityViewModel.IsUploadSuccesful)
            {
                var loValidate = await R_MessageBox.Show("", _localizer["M002"], R_eMessageBoxButtonType.OK);
                if (loValidate == R_eMessageBoxResult.OK)
                {
                    await this.Close(true, true);
                }
            }
            else
            {
                IsErrorHappened = true;
            }
        }
        public async Task StartUploadUnitInvoke()
        {
            if (_gridUploadUnitRef.DataSource.Count > 0)
            {
                await _gridUploadUnitRef.R_SaveBatch();
            }
            else
            {
                if (_gridUploadUnitUtilityRef.DataSource.Count > 0)
                {
                    await _gridUploadUnitUtilityRef.R_SaveBatch();
                }
            }
        }
        public async Task StartUploadUnitUtilityInvoke()
        {
            if (_gridUploadUnitUtilityRef.DataSource.Count > 0)
            {
                await _gridUploadUnitUtilityRef.R_SaveBatch();
            }
            else
            {
                if (loUploadFloorViewModel.IsUploadSuccesful && loUploadUnitViewModel.IsUploadSuccesful && loUploadUnitUtilityViewModel.IsUploadSuccesful)
                {
                    var loValidate = await R_MessageBox.Show("", _localizer["M002"], R_eMessageBoxButtonType.OK);
                    if (loValidate == R_eMessageBoxResult.OK)
                    {
                        await this.Close(true, true);
                    }
                }
                else
                {
                    IsErrorHappened = true;
                }
            }
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
                loUploadUnitViewModel.StartUploadUnitUtilityAction = async () =>
                {
                    await StartUploadUnitUtilityInvoke();
                };

                loUploadUnitViewModel.IsUploadFromFloor = true;
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

                loUploadUnitUtilityViewModel.SelectedUserId = loClientHelper.UserId;
                loUploadUnitUtilityViewModel.SelectedCompanyId = loClientHelper.CompanyId;

                loUploadUnitUtilityViewModel.loParameter.PropertyData = loUploadFloorViewModel.loParameter.PropertyData;
                loUploadUnitUtilityViewModel.loParameter.BuildingData = loUploadFloorViewModel.loParameter.BuildingData;
                loUploadUnitUtilityViewModel.loParameter.FloorData = new SelectedFloorDTO();
                loUploadUnitUtilityViewModel.loParameter.UnitData = new SelectedUnitDTO();
                loUploadUnitUtilityViewModel.loParameter.LFLAG = true;

                loUploadUnitUtilityViewModel.StateChangeAction = StateChangeInvoke;
                loUploadUnitUtilityViewModel.ShowErrorAction = ShowErrorInvoke;
                loUploadUnitUtilityViewModel.SetPercentageAndMessageAction = SetPercentageAndMessageInvoke;
                loUploadUnitUtilityViewModel.SetValidInvalidAction = SetValidInvalidInvoke;
                loUploadUnitUtilityViewModel.ShowSuccessAction = async () =>
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
            else if (eventArgs.Id == "Utility")
            {
                lnValid = loUploadUnitUtilityViewModel.SumValid;
                lnInvalid = loUploadUnitUtilityViewModel.SumInvalid;
                lnTotalRow = loUploadUnitUtilityViewModel.SumList;
            }
        }


        public void ReadExcelUnitSheetFile()
        {
            R_Exception loEx = new R_Exception();
            R_Exception loTempEx = new R_Exception();
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
                    StrataStatus = x.StrataStatus,
                    LeaseStatus = x.LeaseStatus,
                    UnitCategory = x.UnitCategory,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    Notes = ""
                }).ToList();

                loUploadUnitViewModel.loUploadUnitDisplayList = new ObservableCollection<UploadUnitDTO>(loUploadUnitViewModel.loUploadUnitList);
            }
            catch (Exception ex)
            {
                loTempEx.Add(ex);
                if (loTempEx.ErrorList.FirstOrDefault().ErrDescp == "SkipNumberOfRowsStart was out of range: 0")
                {
                    loUploadUnitViewModel.IsErrorEmptyFile = true;
                }
                else
                {
                    loEx.Add(ex);
                    loUploadUnitViewModel.IsErrorEmptyFile = false;
                }
            }
            loEx.ThrowExceptionIfErrors();
        }

        public void ReadExcelFloorSheetFile()
        {
            R_Exception loEx = new R_Exception();
            R_Exception loTempEx = new R_Exception();
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
                loTempEx.Add(ex);
                if (loTempEx.ErrorList.FirstOrDefault().ErrDescp == "SkipNumberOfRowsStart was out of range: 0")
                {
                    loUploadFloorViewModel.IsErrorEmptyFile = true;
                }
                else
                {
                    loEx.Add(ex);
                    loUploadFloorViewModel.IsErrorEmptyFile = false;
                }
            }
            loEx.ThrowExceptionIfErrors();
        }

        public void ReadExcelUnitUtilitySheetFile()
        {
            R_Exception loEx = new R_Exception();
            R_Exception loTempEx = new R_Exception();
            List<UploadUnitUtilityExcelDTO> loExtract = new List<UploadUnitUtilityExcelDTO>();
            try
            {
                var loDataSet = Excel.R_ReadFromExcel(loUploadFloorViewModel.fileByte, new string[] { "Utility" });

                var loResult = R_FrontUtility.R_ConvertTo<UploadUnitUtilityExcelDTO>(loDataSet.Tables[0]);

                loExtract = new List<UploadUnitUtilityExcelDTO>(loResult);

                loUploadUnitUtilityViewModel.loUploadUnitUtilityList = loExtract.Select((x, i) => new UploadUnitUtilityDTO
                {
                    No = i + 1,
                    FloorId = x.FloorId,
                    UnitId = x.UnitId,
                    UtilityType = x.UtilityType,
                    SeqNo = x.SeqNo,
                    MeterNo = x.MeterNo,
                    AliasMeterNo = x.AliasMeterNo,
                    CalculationFactor = x.CalculationFactor,
                    Capacity = x.Capacity,
                    MaxResetValue = x.MaxResetValue,
                    Active = x.Active,
                    Notes = ""
                }).ToList();

                loUploadUnitUtilityViewModel.loUploadUnitUtilityDisplayList = new ObservableCollection<UploadUnitUtilityDTO>(loUploadUnitUtilityViewModel.loUploadUnitUtilityList);
            }
            catch (Exception ex)
            {
                loTempEx.Add(ex);
                if (loTempEx.ErrorList.FirstOrDefault().ErrDescp == "SkipNumberOfRowsStart was out of range: 0")
                {
                    loUploadUnitUtilityViewModel.IsErrorEmptyFile = true;
                }
                else
                {
                    loEx.Add(ex);
                    loUploadUnitUtilityViewModel.IsErrorEmptyFile = false;
                }
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
            List<SaveUnitUtilityFloorToExcelDTO> loExcelUnitUtilityList = new List<SaveUnitUtilityFloorToExcelDTO>();
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
                    StrataStatus = x.StrataStatus,
                    LeaseStatus = x.LeaseStatus,
                    UnitCategory = x.UnitCategory,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    Valid = x.Valid,
                    Notes = x.Notes
                }).ToList();

                loDataTable = R_FrontUtility.R_ConvertTo(loExcelUnitList);
                loDataTable.TableName = "Unit";

                loExcelDataSet.Tables.Add(loDataTable);

                loExcelUnitUtilityList = loUploadUnitUtilityViewModel.loUploadUnitUtilityDisplayList.Select(x => new SaveUnitUtilityFloorToExcelDTO()
                {
                    No = x.No,
                    FloorId = x.FloorId,
                    UnitId = x.UnitId,
                    UtilityType = x.UtilityType,
                    SeqNo = x.SeqNo,
                    MeterNo = x.MeterNo,
                    AliasMeterNo = x.AliasMeterNo,
                    CalculationFactor = x.CalculationFactor,
                    Capacity = x.Capacity,
                    MaxResetValue = x.MaxResetValue,
                    Active = x.Active,
                    Valid = x.Valid,
                    Notes = x.Notes
                }).ToList();

                loDataTable = R_FrontUtility.R_ConvertTo(loExcelUnitUtilityList);
                loDataTable.TableName = "UnitUtility";

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
                _gridUploadFloorRef.DataSource.Clear();
                _gridUploadUnitRef.DataSource.Clear();
                _gridUploadUnitUtilityRef.DataSource.Clear();
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
                ReadExcelUnitUtilitySheetFile();

                await _gridUploadFloorRef.R_RefreshGrid(null);
                await _gridUploadUnitRef.R_RefreshGrid(null);
                await _gridUploadUnitUtilityRef.R_RefreshGrid(null);
                if (_gridUploadFloorRef.DataSource.Count > 0 || _gridUploadUnitRef.DataSource.Count > 0 || _gridUploadUnitUtilityRef.DataSource.Count > 0)
                {
                    IsProcessEnabled = true;
                }
                else
                {
                    IsProcessEnabled = false;
                }
            }
            catch (Exception ex)
            {
                if (loUploadFloorViewModel.IsErrorEmptyFile && loUploadUnitViewModel.IsErrorEmptyFile && loUploadUnitUtilityViewModel.IsErrorEmptyFile)
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
                //if (loUploadFloorViewModel.SumList > 0)
                //{
                //    IsProcessEnabled = true;
                //}
                //else
                //{
                //    IsProcessEnabled = false;
                //}

                eventArgs.ListEntityResult = loUploadFloorViewModel.loUploadFloorDisplayList;
                //loUploadFloorViewModel.IsUploadSuccesful = !loUploadFloorViewModel.VisibleError;
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
                //if (loUploadUnitViewModel.SumList > 0)
                //{
                //    IsProcessEnabled = true;
                //}
                //else
                //{
                //    IsProcessEnabled = false;
                //}
                eventArgs.ListEntityResult = loUploadUnitViewModel.loUploadUnitDisplayList;
                //loUploadUnitViewModel.IsUploadSuccesful = !loUploadUnitViewModel.VisibleError;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_R_ServiceGetUnitUtilityListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //await loUploadUnitUtilityViewModel.ValidateUploadUnitUtility();

                loUploadUnitUtilityViewModel.SumList = loUploadUnitUtilityViewModel.loUploadUnitUtilityDisplayList.Count();
                //if (loUploadUnitUtilityViewModel.SumList > 0)
                //{
                //    IsProcessEnabled = true;
                //}
                //else
                //{
                //    IsProcessEnabled = false;
                //}
                eventArgs.ListEntityResult = loUploadUnitUtilityViewModel.loUploadUnitUtilityDisplayList;
                //loUploadUnitUtilityViewModel.IsUploadSuccesful = !loUploadUnitUtilityViewModel.VisibleError;
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
                if (_gridUploadFloorRef.DataSource.Count > 0)
                {
                    await _gridUploadFloorRef.R_SaveBatch();
                }
                else
                {
                    if (_gridUploadUnitRef.DataSource.Count > 0)
                    {
                        await _gridUploadUnitRef.R_SaveBatch();
                    }
                    else
                    {
                        if (_gridUploadUnitUtilityRef.DataSource.Count > 0)
                        {
                            await _gridUploadUnitUtilityRef.R_SaveBatch();
                        }
                    }
                }
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

        private async Task R_BeforeSaveUnitUtilityBatch(R_BeforeSaveBatchEventArgs events)
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

        private async Task R_ServiceSaveUnitUtilityBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loUploadUnitUtilityViewModel.loUploadUnitUtilityList = (List<UploadUnitUtilityDTO>)eventArgs.Data;
                await loUploadUnitUtilityViewModel.SaveUploadUnitUtilityAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_AfterSaveUnitUtilityBatch(R_AfterSaveBatchEventArgs eventArgs)
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