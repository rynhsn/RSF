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

namespace GSM02500FRONT
{
    public partial class UploadUnitPromotion : R_Page
    {
        [Inject] public R_IExcel Excel { get; set; }
        [Inject] private IClientHelper loClientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private UploadUnitPromotionViewModel loUploadUnitPromotionViewModel = new UploadUnitPromotionViewModel();

        private R_ConductorGrid _conGridUploadUnitPromotionRef;

        private R_Grid<UploadUnitPromotionDTO> _gridUploadUnitPromotionRef;

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
                loUploadUnitPromotionViewModel.loParameter = (UploadUnitPromotionParameterDTO)poParameter;
                loUploadUnitPromotionViewModel.SelectedUserId = loClientHelper.UserId;
                loUploadUnitPromotionViewModel.SelectedCompanyId = loClientHelper.CompanyId;
                loUploadUnitPromotionViewModel.StateChangeAction = StateChangeInvoke;
                loUploadUnitPromotionViewModel.ShowErrorAction = ShowErrorInvoke;
                loUploadUnitPromotionViewModel.ShowSuccessAction = async () =>
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
            List<UploadUnitPromotionExcelDTO> loExtract = new List<UploadUnitPromotionExcelDTO>();
            try
            {
                var loDataSet = Excel.R_ReadFromExcel(loUploadUnitPromotionViewModel.fileByte, new string[] { "UnitPromotion" });

                var loResult = R_FrontUtility.R_ConvertTo<UploadUnitPromotionExcelDTO>(loDataSet.Tables[0]);

                loExtract = new List<UploadUnitPromotionExcelDTO>(loResult);

                loUploadUnitPromotionViewModel.loUploadUnitPromotionList = loExtract.Select((x, i) => new UploadUnitPromotionDTO
                {
                    No = i + 1,
                    UnitPromotionId = x.UnitPromotionId,
                    UnitPromotionName = x.UnitPromotionName,
                    UnitPromotionType = x.UnitPromotionType,
                    Building = x.Building,
                    Floor = x.Floor,
                    Location = x.Location,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    Notes = "",
                    CompanyId = loUploadUnitPromotionViewModel.SelectedCompanyId,
                    PropertyId = loUploadUnitPromotionViewModel.loParameter.PropertyData.CPROPERTY_ID
                }).ToList();

                loUploadUnitPromotionViewModel.loUploadUnitPromotionDisplayList = new ObservableCollection<UploadUnitPromotionDTO>(loUploadUnitPromotionViewModel.loUploadUnitPromotionList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                var MatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
                loUploadUnitPromotionViewModel.IsErrorEmptyFile = MatchingError != null;
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnSaveToExcel()
        {
            var loEx = new R_Exception();

            try
            {
                List<SaveUnitPromotionToExcelDTO> loExcelList = new List<SaveUnitPromotionToExcelDTO>();

                loExcelList = loUploadUnitPromotionViewModel.loUploadUnitPromotionDisplayList.Select(x => new SaveUnitPromotionToExcelDTO()
                {
                    UnitPromotionId = x.UnitPromotionId,
                    UnitPromotionName = x.UnitPromotionName,
                    UnitPromotionType = x.UnitPromotionType,
                    Building = x.Building,
                    Floor = x.Floor,
                    Location = x.Location,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    Notes = x.Notes,
                }).ToList();

                var loDataTable = R_FrontUtility.R_ConvertTo(loExcelList);
                loDataTable.TableName = "UnitPromotion";

                //export to excel
                var loByteFile = Excel.R_WriteToExcel(loDataTable);
                var saveFileName = $"Upload Error Unit Promotion  .xlsx";

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
                loUploadUnitPromotionViewModel.fileByte = loMS.ToArray();

                ReadExcelFile();

                if (eventArgs.File.Name.Contains(".xlsx") == false)
                {
                    await R_MessageBox.Show("", _localizer["M003"], R_eMessageBoxButtonType.OK);
                }
                await _gridUploadUnitPromotionRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                if (loUploadUnitPromotionViewModel.IsErrorEmptyFile)
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
                //await loUploadUnitPromotionViewModel.ValidateUploadUnitPromotion();
                loUploadUnitPromotionViewModel.SumList = loUploadUnitPromotionViewModel.loUploadUnitPromotionDisplayList.Count();
                if (loUploadUnitPromotionViewModel.SumList > 0)
                {
                    IsProcessEnabled = true;
                }
                else
                {
                    IsProcessEnabled = false;
                }

                eventArgs.ListEntityResult = loUploadUnitPromotionViewModel.loUploadUnitPromotionDisplayList;
                loUploadUnitPromotionViewModel.IsUploadSuccesful = !loUploadUnitPromotionViewModel.VisibleError;
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
                await _conGridUploadUnitPromotionRef.R_SaveBatch();
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
                loUploadUnitPromotionViewModel.loUploadUnitPromotionList = (List<UploadUnitPromotionDTO>)eventArgs.Data;
                await loUploadUnitPromotionViewModel.SaveUploadUnitPromotionAsync();
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