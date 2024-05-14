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

namespace GSM02500FRONT
{
    public partial class UploadUnitPromotionType : R_Page
    {
        [Inject] public R_IExcel Excel { get; set; }
        [Inject] private IClientHelper loClientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private UploadUnitPromotionTypeViewModel loUploadUnitPromotionTypeViewModel = new UploadUnitPromotionTypeViewModel();

        private R_ConductorGrid _conGridUploadUnitPromotionTypeRef;

        private R_Grid<UploadUnitPromotionTypeDTO> _gridUploadUnitPromotionTypeRef;

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
                loUploadUnitPromotionTypeViewModel.loParameter = (UploadUnitPromotionTypeParameterDTO)poParameter;
                loUploadUnitPromotionTypeViewModel.SelectedUserId = loClientHelper.UserId;
                loUploadUnitPromotionTypeViewModel.SelectedCompanyId = loClientHelper.CompanyId;
                loUploadUnitPromotionTypeViewModel.StateChangeAction = StateChangeInvoke;
                loUploadUnitPromotionTypeViewModel.ShowErrorAction = ShowErrorInvoke;
                loUploadUnitPromotionTypeViewModel.ShowSuccessAction = async () =>
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
            List<UploadUnitPromotionTypeExcelDTO> loExtract = new List<UploadUnitPromotionTypeExcelDTO>();
            try
            {
                var loDataSet = Excel.R_ReadFromExcel(loUploadUnitPromotionTypeViewModel.fileByte, new string[] { "UnitPromotionType" });

                var loResult = R_FrontUtility.R_ConvertTo<UploadUnitPromotionTypeExcelDTO>(loDataSet.Tables[0]);

                loExtract = new List<UploadUnitPromotionTypeExcelDTO>(loResult);

                loUploadUnitPromotionTypeViewModel.loUploadUnitPromotionTypeList = loExtract.Select((x, i) => new UploadUnitPromotionTypeDTO
                {
                    No = i + 1,
                    UnitPromotionTypeCode = x.UnitPromotionTypeCode,
                    UnitPromotionTypeName = x.UnitPromotionTypeName,
                    GrossAreaSize = x.GrossAreaSize,
                    NetAreaSize = x.NetAreaSize,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    Notes = "",
                    CompanyId = loUploadUnitPromotionTypeViewModel.SelectedCompanyId,
                    PropertyId = loUploadUnitPromotionTypeViewModel.loParameter.PropertyData.CPROPERTY_ID
                }).ToList();

                loUploadUnitPromotionTypeViewModel.loUploadUnitPromotionTypeDisplayList = new ObservableCollection<UploadUnitPromotionTypeDTO>(loUploadUnitPromotionTypeViewModel.loUploadUnitPromotionTypeList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                var MatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
                loUploadUnitPromotionTypeViewModel.IsErrorEmptyFile = MatchingError != null;
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnSaveToExcel()
        {
            var loEx = new R_Exception();

            try
            {
                List<SaveUnitPromotionTypeToExcelDTO> loExcelList = new List<SaveUnitPromotionTypeToExcelDTO>();

                loExcelList = loUploadUnitPromotionTypeViewModel.loUploadUnitPromotionTypeDisplayList.Select(x => new SaveUnitPromotionTypeToExcelDTO()
                {
                    No = x.No,
                    UnitPromotionTypeCode = x.UnitPromotionTypeCode,
                    UnitPromotionTypeName = x.UnitPromotionTypeName,
                    GrossAreaSize = x.GrossAreaSize,
                    NetAreaSize = x.NetAreaSize,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    Valid = x.Valid,
                    Notes = x.Notes,
                }).ToList();

                var loDataTable = R_FrontUtility.R_ConvertTo(loExcelList);
                loDataTable.TableName = "UnitPromotionType";

                //export to excel
                var loByteFile = Excel.R_WriteToExcel(loDataTable);
                var saveFileName = $"Upload Error Unit Promotion Type .xlsx";

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
                loUploadUnitPromotionTypeViewModel.fileByte = loMS.ToArray();

                ReadExcelFile();

                if (eventArgs.File.Name.Contains(".xlsx") == false)
                {
                    await R_MessageBox.Show("", _localizer["M003"], R_eMessageBoxButtonType.OK);
                }
                await _gridUploadUnitPromotionTypeRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                if (loUploadUnitPromotionTypeViewModel.IsErrorEmptyFile)
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
                loUploadUnitPromotionTypeViewModel.SumList = loUploadUnitPromotionTypeViewModel.loUploadUnitPromotionTypeDisplayList.Count();
                if (loUploadUnitPromotionTypeViewModel.SumList > 0)
                {
                    IsProcessEnabled = true;
                }
                else
                {
                    IsProcessEnabled = false;
                }
                eventArgs.ListEntityResult = loUploadUnitPromotionTypeViewModel.loUploadUnitPromotionTypeDisplayList;
                loUploadUnitPromotionTypeViewModel.IsUploadSuccesful = !loUploadUnitPromotionTypeViewModel.VisibleError;
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
                await _conGridUploadUnitPromotionTypeRef.R_SaveBatch();
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
                loUploadUnitPromotionTypeViewModel.loUploadUnitPromotionTypeList = (List<UploadUnitPromotionTypeDTO>)eventArgs.Data;
                await loUploadUnitPromotionTypeViewModel.SaveUploadUnitPromotionTypeAsync();
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