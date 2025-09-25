using GLT00500COMMON.DTOs;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLT00500MODEL.ViewModel;
using BlazorClientHelper;

namespace GLT00500FRONT
{
    public partial class GLT00500 : R_Page
    {
        [Inject] IJSRuntime JS { get; set; }

        [Inject] public R_IExcel Excel { get; set; }

        [Inject] private IClientHelper clientHelper { get; set; }

        private GLT00500ViewModel loImportAdjustmentJournalViewModel = new();

        private R_Conductor _conductorImportAdjustmentJournalRef;

        private R_Grid<GLT00500DetailDTO> _gridImportAdjustmentJournalRef;

        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };

        private bool IsSingleDCAmount = true;

        private bool IsFileExist = false;

        private bool IsSelectedFileExist = false;

        private bool IsErrorExist = false;

        private bool IsSaveProcess = false;

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
            await R_MessageBox.Show("", "Import Journal Successful!", R_eMessageBoxButtonType.OK);
        }


        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loImportAdjustmentJournalViewModel.LoginCompanyId = clientHelper.CompanyId;
                loImportAdjustmentJournalViewModel.LoginLanguageId = clientHelper.Culture.TwoLetterISOLanguageName;
                loImportAdjustmentJournalViewModel.LoginUserId = clientHelper.UserId;
                await loImportAdjustmentJournalViewModel.GetInitialProcess();
                loImportAdjustmentJournalViewModel.StateChangeAction = StateChangeInvoke;
                loImportAdjustmentJournalViewModel.ShowErrorAction = ShowErrorInvoke;
                loImportAdjustmentJournalViewModel.ShowSuccessAction = async () =>
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

        private async Task Grid_R_ServiceGetImportAdjustmentJournalListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loImportAdjustmentJournalViewModel.ImportAdjustmentJournalStreamAsync();
                eventArgs.ListEntityResult = loImportAdjustmentJournalViewModel.loDetailDisplayList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickTemplate()
        {
            R_Exception loException = new R_Exception();
            var loData = new List<TemplateImportAdjustmentJournalDTO>();
            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure download this template?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await loImportAdjustmentJournalViewModel.DownloadTemplateImportAdjustmentJournalAsync();

                    var saveFileName = $"GL_JOURNAL_UPLOAD_TEMPLATE.zip";
                    /*
                                        var saveFileName = $"Staff {CenterViewModel.PropertyValueContext}.xlsx";*/

                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task OnClickImport()
        {
            R_Exception loException = new R_Exception();
            List<GLT00500DetailDTO> loTempList = new List<GLT00500DetailDTO>();
            GLT00500HeaderDTO loTempHeader = new GLT00500HeaderDTO();

            try
            {
                var loCheckingDataTable = Excel.R_ReadFromExcel(loImportAdjustmentJournalViewModel.loFileByte, "A7:H8");

                var loCheckingResult = R_FrontUtility.R_ConvertTo<GLT00500DetailExcelDTO>(loCheckingDataTable);

                if (loImportAdjustmentJournalViewModel.IsSeparateDCAmount == true)
                {
                    if (loCheckingResult.FirstOrDefault().Db_Cr != "")
                    {
                        await R_MessageBox.Show("", "Invalid template file format!", R_eMessageBoxButtonType.OK);
                        return;
                    }
                }
                else
                {
                    if (loCheckingResult.FirstOrDefault().Db_Cr == "")
                    {
                        await R_MessageBox.Show("", "Invalid template file format!", R_eMessageBoxButtonType.OK);
                        return;
                    }
                }

                loTempHeader.CDEPT_CODE = Excel.R_ReadValueCellFromExcel(loImportAdjustmentJournalViewModel.loFileByte, "B1").ToString();
                loTempHeader.CREF_NO = Excel.R_ReadValueCellFromExcel(loImportAdjustmentJournalViewModel.loFileByte, "B2").ToString();

                loTempHeader.CREF_DATE = Excel.R_ReadValueCellFromExcel(loImportAdjustmentJournalViewModel.loFileByte, "B3").ToString();
                DateTime loRefDate = Convert.ToDateTime(loTempHeader.CREF_DATE);
                loTempHeader.CREF_DATE = loRefDate.ToString("dd-MMM-yyyy");

                loTempHeader.CDOC_NO = Excel.R_ReadValueCellFromExcel(loImportAdjustmentJournalViewModel.loFileByte, "B4").ToString();
                loTempHeader.CDOC_DATE = Excel.R_ReadValueCellFromExcel(loImportAdjustmentJournalViewModel.loFileByte, "B5").ToString();
                DateTime loDocDate = Convert.ToDateTime(loTempHeader.CDOC_DATE);
                loTempHeader.CDOC_DATE = loDocDate.ToString("dd-MMM-yyyy");

                loTempHeader.CTRANS_DESC = Excel.R_ReadValueCellFromExcel(loImportAdjustmentJournalViewModel.loFileByte, "B6").ToString();
                loTempHeader.CCURRENCY_CODE = Excel.R_ReadValueCellFromExcel(loImportAdjustmentJournalViewModel.loFileByte, "E1").ToString();
                loTempHeader.NLBASE_RATE = Convert.ToDecimal(Excel.R_ReadValueCellFromExcel(loImportAdjustmentJournalViewModel.loFileByte, "E3"));
                loTempHeader.NBBASE_RATE = Convert.ToDecimal(Excel.R_ReadValueCellFromExcel(loImportAdjustmentJournalViewModel.loFileByte, "E4"));
                loTempHeader.NLCURRENCY_RATE = Convert.ToDecimal(Excel.R_ReadValueCellFromExcel(loImportAdjustmentJournalViewModel.loFileByte, "G3"));
                loTempHeader.NBCURRENCY_RATE = Convert.ToDecimal(Excel.R_ReadValueCellFromExcel(loImportAdjustmentJournalViewModel.loFileByte, "G4"));

                loTempHeader.CLOCAL_CURRENCY_CODE = loImportAdjustmentJournalViewModel.loCompany.CLOCAL_CURRENCY_CODE;
                loTempHeader.CBASE_CURRENCY_CODE = loImportAdjustmentJournalViewModel.loCompany.CBASE_CURRENCY_CODE;

                var loDataTable = Excel.R_ReadFromExcel(loImportAdjustmentJournalViewModel.loFileByte, "A7:H1007");

                var loResult = R_FrontUtility.R_ConvertTo<GLT00500DetailExcelDTO>(loDataTable);

                if (loImportAdjustmentJournalViewModel.IsSeparateDCAmount)
                {
                    loTempList = loResult.Select((x, i) => new GLT00500DetailDTO()
                    {
                        CGLACCOUNT_NO = x.Account_No,
                        CGLACCOUNT_NAME = x.Account_Name,
                        CCENTER_CODE = x.Center_Code,
                        NDEBIT = x.Debit_Amount,
                        NCREDIT = x.Credit_Amount,
                        CDETAIL_DESC = x.Description,
                        CDOCUMENT_NO = x.Voucher_No,
                        CDOCUMENT_DATE = x.Voucher_Date
                    }).ToList();
                }
                else
                {
                    loTempList = loResult.Select((x, i) => new GLT00500DetailDTO()
                    {
                        CGLACCOUNT_NO = x.Account_No,
                        CGLACCOUNT_NAME = x.Account_Name,
                        CCENTER_CODE = x.Center,
                        CDBCR = x.Db_Cr,
                        NTRANS_AMOUNT = x.Amount,
                        CDETAIL_DESC = x.Description,
                        CDOCUMENT_NO = x.Voucher_No,
                        CDOCUMENT_DATE = x.Voucher_Date
                    }).ToList();
                }

                loImportAdjustmentJournalViewModel.loHeader = loTempHeader;
                loImportAdjustmentJournalViewModel.loDetailList = loTempList;

                await _gridImportAdjustmentJournalRef.R_RefreshGrid(null);

                if (loException.HasError == false)
                {
                    IsFileExist = true;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task SaveImportAdjustmentJournal()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loImportAdjustmentJournalViewModel.ImportAdjustmentJournalValidation();

                if (loEx.HasError)
                {

                }
                else
                {
                    await loImportAdjustmentJournalViewModel.SaveUploadFile();
                    /*if (loImportAdjustmentJournalViewModel.IsSaveSuccess)
                    {
                        await R_MessageBox.Show("", "Import Journal Successful!", R_eMessageBoxButtonType.OK);
                    }
                    else
                    {
                        IsErrorExist = true;
                        await R_MessageBox.Show("", "Import Journal failed! Click button View Error Logs for more details.", R_eMessageBoxButtonType.OK);
                    }*/
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            IsSaveProcess = true;
            loEx.ThrowExceptionIfErrors();
        }

        private void ResetFormProcess()
        {
            R_Exception loException = new R_Exception();
            try
            {
                IsFileExist = false;
                IsSelectedFileExist = false;
                IsErrorExist = false;
                loImportAdjustmentJournalViewModel.loHeaderDisplay.Clear();
                loImportAdjustmentJournalViewModel.loDetailDisplayList.Clear();
                IsSaveProcess = false;
                loImportAdjustmentJournalViewModel.Percentage = 0;
                loImportAdjustmentJournalViewModel.Message = "";
                loImportAdjustmentJournalViewModel.StateChangeAction();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void ChechBoxValueChanged(bool value)
        {
            IsSingleDCAmount = !value;
            loImportAdjustmentJournalViewModel.IsSeparateDCAmount = value;
        }

        private async Task InputFileOnChange(InputFileChangeEventArgs args)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                ResetFormProcess();
                //import from excel
                var loMS = new MemoryStream();
                await args.File.OpenReadStream().CopyToAsync(loMS);
                loImportAdjustmentJournalViewModel.loFileByte = loMS.ToArray();
                IsSelectedFileExist = true;
                loImportAdjustmentJournalViewModel.lcFileName = args.File.Name;
                await OnClickImport();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Before_Open_ViewErrorLogs_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                eventArgs.Parameter = new OpenViewErrorLogsParameterDTO()
                {
                    CPROCESS_ID = loImportAdjustmentJournalViewModel.lcKeyGuid,
                    CFILE_NAME = loImportAdjustmentJournalViewModel.lcFileName
                };
                eventArgs.TargetPageType = typeof(ImportAdjustmentJournalError);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void After_Open_ViewErrorLogs_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
        }
    }
}
