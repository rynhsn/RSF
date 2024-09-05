using BlazorClientHelper;
using GSM02500COMMON.DTOs.GSM02531;
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
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Interfaces;

namespace GSM02500FRONT
{
    public partial class UploadUnitUtility : R_Page
    {
        [Inject] public R_IExcel Excel { get; set; }
        [Inject] private IClientHelper loClientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private UploadUnitUtilityViewModel loUploadUnitUtilityViewModel = new UploadUnitUtilityViewModel();

        private R_ConductorGrid _conGridUploadUnitUtilityRef;

        private R_Grid<UploadUnitUtilityDTO> _gridUploadUnitUtilityRef;

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
                loUploadUnitUtilityViewModel.loParameter = (UploadUnitUtilityParameterDTO)poParameter;
                loUploadUnitUtilityViewModel.SelectedUserId = loClientHelper.UserId;
                loUploadUnitUtilityViewModel.SelectedCompanyId = loClientHelper.CompanyId;
                loUploadUnitUtilityViewModel.StateChangeAction = StateChangeInvoke;
                loUploadUnitUtilityViewModel.ShowErrorAction = ShowErrorInvoke;
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

        public void ReadExcelFile()
        {
            var loEx = new R_Exception();
            List<UploadUnitUtilityExcelDTO> loExtract = new List<UploadUnitUtilityExcelDTO>();
            try
            {
                var loDataSet = Excel.R_ReadFromExcel(loUploadUnitUtilityViewModel.fileByte, new string[] { "Unit Utility" });

                var loResult = R_FrontUtility.R_ConvertTo<UploadUnitUtilityExcelDTO>(loDataSet.Tables[0]);

                loExtract = new List<UploadUnitUtilityExcelDTO>(loResult);

                //loUploadUnitUtilityViewModel.loUploadUnitUtilityList = loExtract.Select((x, i) => new UploadUnitUtilityDTO
                //{
                //    No = i + 1,
                //    UtilityType = x.UtilityType,
                //    SeqNo = x.SeqNo,
                //    MeterNo = x.MeterNo,
                //    AliasMeterNo = x.AliasMeterNo,
                //    CalculationFactor = x.CalculationFactor,
                //    Capacity = x.Capacity,
                //    MaxResetValue = x.MaxResetValue,
                //    Active = x.Active,
                //    NonActiveDate = x.NonActiveDate,
                //    Notes = "",
                //    CompanyId = loUploadUnitUtilityViewModel.SelectedCompanyId,
                //    PropertyId = loUploadUnitUtilityViewModel.loParameter.PropertyData.CPROPERTY_ID,
                //    BuildingId = loUploadUnitUtilityViewModel.loParameter.BuildingData.CBUILDING_ID,
                //    FloorId = loUploadUnitUtilityViewModel.loParameter.FloorData.CFLOOR_ID,
                //    UnitId = loUploadUnitUtilityViewModel.loParameter.UnitData.CUNIT_ID,
                //    OtherUnitId = loUploadUnitUtilityViewModel.loParameter.OtherUnitData.COTHER_UNIT_ID
                //}).ToList();
                if (loUploadUnitUtilityViewModel.loParameter.LFLAG)
                {
                    loUploadUnitUtilityViewModel.loUploadUnitUtilityList = loExtract.Select((x, i) => new UploadUnitUtilityDTO
                    {
                        No = i + 1,
                        UtilityType = x.UtilityType,
                        SeqNo = x.SeqNo,
                        MeterNo = x.MeterNo,
                        AliasMeterNo = x.AliasMeterNo,
                        CalculationFactor = x.CalculationFactor,
                        Capacity = x.Capacity,
                        MaxResetValue = x.MaxResetValue,
                        Active = x.Active,
                        NonActiveDate = x.NonActiveDate,
                        Notes = "",
                        CompanyId = loUploadUnitUtilityViewModel.SelectedCompanyId,
                        PropertyId = loUploadUnitUtilityViewModel.loParameter.PropertyData.CPROPERTY_ID,
                        BuildingId = loUploadUnitUtilityViewModel.loParameter.BuildingData.CBUILDING_ID,
                        FloorId = loUploadUnitUtilityViewModel.loParameter.FloorData.CFLOOR_ID,
                        UnitId = loUploadUnitUtilityViewModel.loParameter.UnitData.CUNIT_ID
                    }).ToList();
                }
                else
                {
                    loUploadUnitUtilityViewModel.loUploadUnitUtilityList = loExtract.Select((x, i) => new UploadUnitUtilityDTO
                    {
                        No = i + 1,
                        UtilityType = x.UtilityType,
                        SeqNo = x.SeqNo,
                        MeterNo = x.MeterNo,
                        AliasMeterNo = x.AliasMeterNo,
                        CalculationFactor = x.CalculationFactor,
                        Capacity = x.Capacity,
                        MaxResetValue = x.MaxResetValue,
                        Active = x.Active,
                        NonActiveDate = x.NonActiveDate,
                        Notes = "",
                        CompanyId = loUploadUnitUtilityViewModel.SelectedCompanyId,
                        PropertyId = loUploadUnitUtilityViewModel.loParameter.PropertyData.CPROPERTY_ID,
                        OtherUnitId = loUploadUnitUtilityViewModel.loParameter.OtherUnitData.COTHER_UNIT_ID
                    }).ToList();
                }
                loUploadUnitUtilityViewModel.loUploadUnitUtilityDisplayList = new ObservableCollection<UploadUnitUtilityDTO>(loUploadUnitUtilityViewModel.loUploadUnitUtilityList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                var MatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
                loUploadUnitUtilityViewModel.IsErrorEmptyFile = MatchingError != null;
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnSaveToExcel()
        {
            var loEx = new R_Exception();

            try
            {
                List<SaveUnitUtilityToExcelDTO> loExcelList = new List<SaveUnitUtilityToExcelDTO>();

                loExcelList = loUploadUnitUtilityViewModel.loUploadUnitUtilityDisplayList.Select(x => new SaveUnitUtilityToExcelDTO()
                {
                    No = x.No,
                    UtilityType = x.UtilityType,
                    SeqNo = x.SeqNo,
                    MeterNo = x.MeterNo,
                    AliasMeterNo = x.AliasMeterNo,
                    CalculationFactor = x.CalculationFactor,
                    Capacity = x.Capacity,
                    MaxResetValue = x.MaxResetValue,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    Valid = x.Valid,
                    Notes = x.Notes
                }).ToList();

                var loDataTable = R_FrontUtility.R_ConvertTo(loExcelList);
                loDataTable.TableName = "Unit Utility";

                //export to excel
                var loByteFile = Excel.R_WriteToExcel(loDataTable);
                var saveFileName = $"Upload Error Unit Utility.xlsx";

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
                loUploadUnitUtilityViewModel.fileByte = loMS.ToArray();

                ReadExcelFile();

                if (eventArgs.File.Name.Contains(".xlsx") == false)
                {
                    await R_MessageBox.Show("", _localizer["M003"], R_eMessageBoxButtonType.OK);
                }
                await _gridUploadUnitUtilityRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                if (loUploadUnitUtilityViewModel.IsErrorEmptyFile)
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
                //await loUploadUnitUtilityViewModel.ValidateUploadUnitUtility();

                loUploadUnitUtilityViewModel.SumList = loUploadUnitUtilityViewModel.loUploadUnitUtilityDisplayList.Count();
                if (loUploadUnitUtilityViewModel.SumList > 0)
                {
                    IsProcessEnabled = true;
                }
                else
                {
                    IsProcessEnabled = false;
                }

                eventArgs.ListEntityResult = loUploadUnitUtilityViewModel.loUploadUnitUtilityDisplayList;
                loUploadUnitUtilityViewModel.IsUploadSuccesful = !loUploadUnitUtilityViewModel.VisibleError;
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
                await _conGridUploadUnitUtilityRef.R_SaveBatch();
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
                loUploadUnitUtilityViewModel.loUploadUnitUtilityList = (List<UploadUnitUtilityDTO>)eventArgs.Data;
                await loUploadUnitUtilityViewModel.SaveUploadUnitUtilityAsync();
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