using BlazorClientHelper;
using GSM02500COMMON.DTOs.GSM02502;
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
using GSM02500COMMON.DTOs.GSM02503;
using System.Collections.ObjectModel;
using R_BlazorFrontEnd.Interfaces;

namespace GSM02500FRONT
{
    public partial class UploadUnitType : R_Page
    {
        [Inject] public R_IExcel Excel { get; set; }
        [Inject] private IClientHelper loClientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private UploadUnitTypeViewModel loUploadUnitTypeViewModel = new UploadUnitTypeViewModel();

        private R_ConductorGrid _conGridUploadUnitTypeRef;

        private R_Grid<UploadUnitTypeDTO> _gridUploadUnitTypeRef;

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
                loUploadUnitTypeViewModel.loParameter = (UploadUnitTypeParameterDTO)poParameter;
                loUploadUnitTypeViewModel.SelectedUnitTypeCategory = loUploadUnitTypeViewModel.loParameter.SelectedUnitTypeCategory;
                loUploadUnitTypeViewModel.SelectedUserId = loClientHelper.UserId;
                loUploadUnitTypeViewModel.SelectedCompanyId = loClientHelper.CompanyId;
                loUploadUnitTypeViewModel.StateChangeAction = StateChangeInvoke;
                loUploadUnitTypeViewModel.ShowErrorAction = ShowErrorInvoke;
                loUploadUnitTypeViewModel.ShowSuccessAction = async () =>
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
            List<UploadUnitTypeExcelDTO> loExtract = new List<UploadUnitTypeExcelDTO>();
            try
            {
                var loDataSet = Excel.R_ReadFromExcel(loUploadUnitTypeViewModel.fileByte, new string[] { "UnitType" });

                var loResult = R_FrontUtility.R_ConvertTo<UploadUnitTypeExcelDTO>(loDataSet.Tables[0]);

                loExtract = new List<UploadUnitTypeExcelDTO>(loResult);

                loUploadUnitTypeViewModel.loUploadUnitTypeList = loExtract.Select((x, i) => new UploadUnitTypeDTO
                {
                    No = i + 1,
                    UnitTypeCode = x.UnitTypeCode,
                    UnitTypeName = x.UnitTypeName,
                    UnitTypeCtg = loUploadUnitTypeViewModel.SelectedUnitTypeCategory,
                    Description = x.Description,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    GrossAreaSize = x.GrossAreaSize,
                    NetAreaSize = x.NetAreaSize,
                    Notes = "",
                    CompanyId = loUploadUnitTypeViewModel.SelectedCompanyId,
                    PropertyId = loUploadUnitTypeViewModel.loParameter.PropertyData.CPROPERTY_ID
                }).ToList();

                loUploadUnitTypeViewModel.loUploadUnitTypeDisplayList = new ObservableCollection<UploadUnitTypeDTO>(loUploadUnitTypeViewModel.loUploadUnitTypeList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                var MatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
                loUploadUnitTypeViewModel.IsErrorEmptyFile = MatchingError != null;
            }
            loEx.ThrowExceptionIfErrors();
        }


        private async Task OnSaveToExcel()
        {
            var loEx = new R_Exception();

            try
            {
                List<SaveUnitTypeToExcelDTO> loExcelList = new List<SaveUnitTypeToExcelDTO>();

                loExcelList = loUploadUnitTypeViewModel.loUploadUnitTypeDisplayList.Select(x => new SaveUnitTypeToExcelDTO()
                {
                    No = x.No,
                    UnitTypeCode = x.UnitTypeCode,
                    UnitTypeName = x.UnitTypeName,
                    UnitTypeCtg = x.UnitTypeCtg,
                    Description = x.Description,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    Valid = x.Valid,
                    Notes = x.Notes,
                }).ToList();

                var loDataTable = R_FrontUtility.R_ConvertTo(loExcelList);
                loDataTable.TableName = "UnitType";

                //export to excel
                var loByteFile = Excel.R_WriteToExcel(loDataTable);
                var saveFileName = $"Upload Error Unit Type .xlsx";

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
                loUploadUnitTypeViewModel.fileByte = loMS.ToArray();

                ReadExcelFile();

                if (eventArgs.File.Name.Contains(".xlsx") == false)
                {
                    await R_MessageBox.Show("", _localizer["M003"], R_eMessageBoxButtonType.OK);
                }
                await _gridUploadUnitTypeRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                if (loUploadUnitTypeViewModel.IsErrorEmptyFile)
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
                //await loUploadUnitTypeViewModel.ValidateUploadUnitType();
                loUploadUnitTypeViewModel.SumList = loUploadUnitTypeViewModel.loUploadUnitTypeDisplayList.Count();
                if (loUploadUnitTypeViewModel.SumList > 0)
                {
                    IsProcessEnabled = true;
                }
                else
                {
                    IsProcessEnabled = false;
                }

                eventArgs.ListEntityResult = loUploadUnitTypeViewModel.loUploadUnitTypeDisplayList;
                loUploadUnitTypeViewModel.IsUploadSuccesful = !loUploadUnitTypeViewModel.VisibleError;
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
                await _conGridUploadUnitTypeRef.R_SaveBatch();
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
                loUploadUnitTypeViewModel.loUploadUnitTypeList = (List<UploadUnitTypeDTO>)eventArgs.Data;
                await loUploadUnitTypeViewModel.SaveUploadUnitTypeAsync();
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