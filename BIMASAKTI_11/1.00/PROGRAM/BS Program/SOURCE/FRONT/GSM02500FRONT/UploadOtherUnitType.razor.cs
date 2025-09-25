using BlazorClientHelper;
using GSM02500COMMON.DTOs.GSM02503;
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
using GSM02500COMMON.DTOs.GSM02540;
using GSM02500COMMON.DTOs.GSM02541;
using System.Collections.ObjectModel;
using R_BlazorFrontEnd.Interfaces;
using R_APICommonDTO;
using GSM02500COMMON.DTOs.GSM02531;

namespace GSM02500FRONT
{
    public partial class UploadOtherUnitType : R_Page
    {
        [Inject] public R_IExcel Excel { get; set; }
        [Inject] private IClientHelper loClientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private UploadOtherUnitTypeViewModel loUploadOtherUnitTypeViewModel = new UploadOtherUnitTypeViewModel();

        private R_ConductorGrid _conGridUploadOtherUnitTypeRef;

        private R_Grid<UploadOtherUnitTypeDTO> _gridUploadOtherUnitTypeRef;

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
                loUploadOtherUnitTypeViewModel.loParameter = (UploadOtherUnitTypeParameterDTO)poParameter;
                loUploadOtherUnitTypeViewModel.SelectedUserId = loClientHelper.UserId;
                loUploadOtherUnitTypeViewModel.SelectedCompanyId = loClientHelper.CompanyId;
                loUploadOtherUnitTypeViewModel.StateChangeAction = StateChangeInvoke;
                loUploadOtherUnitTypeViewModel.ShowErrorAction = ShowErrorInvoke;
                loUploadOtherUnitTypeViewModel.ShowSuccessAction = async () =>
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

        private void R_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (UploadOtherUnitTypeDTO)eventArgs.Data;

            if (loData.Valid == "N")
            {
                eventArgs.RowClass = "errorDataLValidIsFalse";//"errorDataLValidisFalse";
            }
        }

        public void ReadExcelFile()
        {
            var loEx = new R_Exception();
            List<UploadOtherUnitTypeExcelDTO> loExtract = new List<UploadOtherUnitTypeExcelDTO>();
            try
            {
                var loDataSet = Excel.R_ReadFromExcel(loUploadOtherUnitTypeViewModel.fileByte, new string[] { "OtherUnitType" });

                var loResult = R_FrontUtility.R_ConvertTo<UploadOtherUnitTypeExcelDTO>(loDataSet.Tables[0]);

                loExtract = new List<UploadOtherUnitTypeExcelDTO>(loResult);

                loUploadOtherUnitTypeViewModel.loUploadOtherUnitTypeList = loExtract.Select((x, i) => new UploadOtherUnitTypeDTO
                {
                    No = i + 1,
                    OtherUnitTypeCode = x.OtherUnitTypeCode,
                    OtherUnitTypeName = x.OtherUnitTypeName,
                    DepartmentCode = x.Department,
                    PropertyType = x.PropertyType,
                    GrossAreaSize = x.GrossAreaSize,
                    NetAreaSize = x.NetAreaSize,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    Notes = "",
                    CompanyId = loUploadOtherUnitTypeViewModel.SelectedCompanyId,
                    PropertyId = loUploadOtherUnitTypeViewModel.loParameter.PropertyData.CPROPERTY_ID
                }).ToList();

                loUploadOtherUnitTypeViewModel.loUploadOtherUnitTypeDisplayList = new ObservableCollection<UploadOtherUnitTypeDTO>(loUploadOtherUnitTypeViewModel.loUploadOtherUnitTypeList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                var MatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
                loUploadOtherUnitTypeViewModel.IsErrorEmptyFile = MatchingError != null;
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnSaveToExcel()
        {
            var loEx = new R_Exception();

            try
            {
                List<SaveOtherUnitTypeToExcelDTO> loExcelList = new List<SaveOtherUnitTypeToExcelDTO>();

                loExcelList = loUploadOtherUnitTypeViewModel.loUploadOtherUnitTypeDisplayList.Select(x => new SaveOtherUnitTypeToExcelDTO()
                {
                    No = x.No,
                    OtherUnitTypeCode = x.OtherUnitTypeCode,
                    OtherUnitTypeName = x.OtherUnitTypeName,
                    Department = x.DepartmentCode,
                    GrossAreaSize = x.GrossAreaSize,
                    NetAreaSize = x.NetAreaSize,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    Valid = x.Valid,
                    Notes = x.Notes,
                }).ToList();

                var loDataTable = R_FrontUtility.R_ConvertTo(loExcelList);
                loDataTable.TableName = "OtherUnitType";

                //export to excel
                var loByteFile = Excel.R_WriteToExcel(loDataTable);
                var saveFileName = $"Upload Error Other Unit Type.xlsx";

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
                loUploadOtherUnitTypeViewModel.fileByte = loMS.ToArray();

                ReadExcelFile();

                if (eventArgs.File.Name.Contains(".xlsx") == false)
                {
                    await R_MessageBox.Show("", _localizer["M003"], R_eMessageBoxButtonType.OK);
                }
                await _gridUploadOtherUnitTypeRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                if (loUploadOtherUnitTypeViewModel.IsErrorEmptyFile)
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
                loUploadOtherUnitTypeViewModel.SumList = loUploadOtherUnitTypeViewModel.loUploadOtherUnitTypeDisplayList.Count();
                if (loUploadOtherUnitTypeViewModel.SumList > 0)
                {
                    IsProcessEnabled = true;
                }
                else
                {
                    IsProcessEnabled = false;
                }
                eventArgs.ListEntityResult = loUploadOtherUnitTypeViewModel.loUploadOtherUnitTypeDisplayList;
                loUploadOtherUnitTypeViewModel.IsUploadSuccesful = !loUploadOtherUnitTypeViewModel.VisibleError;
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
                await _gridUploadOtherUnitTypeRef.R_SaveBatch();
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
                loUploadOtherUnitTypeViewModel.loUploadOtherUnitTypeList = (List<UploadOtherUnitTypeDTO>)eventArgs.Data;
                await loUploadOtherUnitTypeViewModel.SaveUploadOtherUnitTypeAsync();
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