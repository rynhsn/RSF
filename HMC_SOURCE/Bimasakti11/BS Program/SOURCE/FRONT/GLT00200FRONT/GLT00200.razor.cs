using GLT00200COMMON.DTOs.GLT00200;
using GLT00200MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Controls.MessageBox;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using R_BlazorFrontEnd.Helpers;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd;
using System.Data;
using BlazorClientHelper;
using R_BlazorFrontEnd.Controls.Enums;
using System;
using Microsoft.JSInterop;

namespace GLT00200FRONT
{
    public partial class GLT00200 : R_Page
    {
        [Inject] IJSRuntime JS { get; set; }

        [Inject] public R_IExcel Excel { get; set; }

        [Inject] private IClientHelper clientHelper { get; set; }

        private GLT00200ViewModel loImportJournalViewModel = new();

        private R_Conductor _conductorImportJournalRef;

        private R_Grid<GLT00200DetailDTO> _gridImportJournalRef;

        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };

        private bool IsSingleDCAmount = true;

        private bool IsFileExist = false;

        private bool IsSaveProcess = false;

        public void StateChangeInvoke()
        {
            StateHasChanged();
        }

        public async Task ShowErrorInvoke(R_Exception poException)
        {
            if (loImportJournalViewModel.PROCESS_TYPE == "SAVE_PROCESS")
            {
                var loValidate = await R_MessageBox.Show("", "Import Journal Failed!", R_eMessageBoxButtonType.OK);
                if (loValidate == R_eMessageBoxResult.OK && poException.HasError)
                {
                    this.R_DisplayException(poException);
                }
            }
            else
            {
                this.R_DisplayException(poException);
            }
        }

        public async Task ShowSuccessInvoke()
        {
            var loValidate = await R_MessageBox.Show("", "Import Journal Successful!", R_eMessageBoxButtonType.OK);

            if (loValidate == R_eMessageBoxResult.OK)
            {
                ResetFormProcess();
            }
        }


        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loImportJournalViewModel.LoginCompanyId = clientHelper.CompanyId;
                loImportJournalViewModel.LoginLanguageId = clientHelper.Culture.TwoLetterISOLanguageName;
                loImportJournalViewModel.LoginUserId = clientHelper.UserId;
                await loImportJournalViewModel.GetInitialProcess();
                loImportJournalViewModel.StateChangeAction = StateChangeInvoke;
                //loImportJournalViewModel.ShowErrorAction = ShowErrorInvoke;
                loImportJournalViewModel.ShowErrorAction = async (poParameter) =>
                {
                    await ShowErrorInvoke(poParameter);
                };
                loImportJournalViewModel.ShowSuccessAction = async () =>
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

        private async Task Grid_R_ServiceGetImportJournalListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loImportJournalViewModel.ImportJournalStreamAsync();
                //loImportJournalViewModel.loHeaderDisplay = loImportJournalViewModel.loHeader;
                //loImportJournalViewModel.loDetailDisplayList = new ObservableCollection<GLT00200DetailDTO>(loImportJournalViewModel.loDetailList);
                eventArgs.ListEntityResult = loImportJournalViewModel.loDetailDisplayList;
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
            var loData = new List<TemplateImportJournalDTO>();
            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure download this template?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await loImportJournalViewModel.DownloadTemplateImportJournalAsync();

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
            List< GLT00200DetailDTO> loTempList = new List<GLT00200DetailDTO>();
            GLT00200HeaderDTO loTempHeader = new GLT00200HeaderDTO();

            try
            {
                var loCheckingDataTable = Excel.R_ReadFromExcel(loImportJournalViewModel.loFileByte, "A7:H8");

                var loCheckingResult = R_FrontUtility.R_ConvertTo<GLT00200DetailExcelDTO>(loCheckingDataTable);

                if (loImportJournalViewModel.IsSeparateDCAmount == true)
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

                loTempHeader.CDEPT_CODE = Excel.R_ReadValueCellFromExcel(loImportJournalViewModel.loFileByte, "B1").ToString();

                try
                {
                    loTempHeader.CREF_NO = Excel.R_ReadValueCellFromExcel(loImportJournalViewModel.loFileByte, "B2").ToString();
                }
                catch (Exception ex)
                {

                }

                loTempHeader.CREF_DATE = Excel.R_ReadValueCellFromExcel(loImportJournalViewModel.loFileByte, "B3").ToString();
                DateTime loRefDate = Convert.ToDateTime(loTempHeader.CREF_DATE);
                loTempHeader.CREF_DATE = loRefDate.ToString("dd-MMM-yyyy");

                loTempHeader.CDOC_NO = Excel.R_ReadValueCellFromExcel(loImportJournalViewModel.loFileByte, "B4").ToString();
                loTempHeader.CDOC_DATE = Excel.R_ReadValueCellFromExcel(loImportJournalViewModel.loFileByte, "B5").ToString();
                DateTime loDocDate = Convert.ToDateTime(loTempHeader.CDOC_DATE);
                loTempHeader.CDOC_DATE = loDocDate.ToString("dd-MMM-yyyy");

                loTempHeader.CTRANS_DESC = Excel.R_ReadValueCellFromExcel(loImportJournalViewModel.loFileByte, "B6").ToString();
                loTempHeader.CCURRENCY_CODE = Excel.R_ReadValueCellFromExcel(loImportJournalViewModel.loFileByte, "E1").ToString();
                loTempHeader.NLBASE_RATE = Convert.ToDecimal(Excel.R_ReadValueCellFromExcel(loImportJournalViewModel.loFileByte, "E3"));
                loTempHeader.NBBASE_RATE = Convert.ToDecimal(Excel.R_ReadValueCellFromExcel(loImportJournalViewModel.loFileByte, "E4"));
                loTempHeader.NLCURRENCY_RATE = Convert.ToDecimal(Excel.R_ReadValueCellFromExcel(loImportJournalViewModel.loFileByte, "G3"));
                loTempHeader.NBCURRENCY_RATE = Convert.ToDecimal(Excel.R_ReadValueCellFromExcel(loImportJournalViewModel.loFileByte, "G4"));

                loTempHeader.CLOCAL_CURRENCY_CODE = loImportJournalViewModel.loCompany.CLOCAL_CURRENCY_CODE;
                loTempHeader.CBASE_CURRENCY_CODE = loImportJournalViewModel.loCompany.CBASE_CURRENCY_CODE;
                
                var loDataTable = Excel.R_ReadFromExcel(loImportJournalViewModel.loFileByte, "A7:H1007");

                var loResult = R_FrontUtility.R_ConvertTo<GLT00200DetailExcelDTO>(loDataTable);

                if (loImportJournalViewModel.IsSeparateDCAmount)
                {
                    loTempList = loResult.Select(x => new GLT00200DetailDTO()
                    {
                        CGLACCOUNT_NO = x.Account_No,
                        CGLACCOUNT_NAME = x.Account_Name,
                        CCENTER_CODE = x.Center,
                        NDEBIT = x.Debit_Amount,
                        NCREDIT = x.Credit_Amount,
                        CDETAIL_DESC = x.Description,
                        CDOCUMENT_NO = x.Voucher_No,
                        CDOCUMENT_DATE = x.Voucher_Date
                    }).ToList();
                }
                else
                {
                    loTempList = loResult.Select((x, i) => new GLT00200DetailDTO()
                    {
                        SEQ_NO = i + 1,
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

                loImportJournalViewModel.loHeader = loTempHeader;
                loImportJournalViewModel.loDetailList = loTempList;

                await _gridImportJournalRef.R_RefreshGrid(null);

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

        private async Task SaveImportJournal()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loImportJournalViewModel.ImportJournalValidation();

                if (loEx.HasError)
                {

                }
                else
                {
                    await loImportJournalViewModel.SaveUploadFile();
                    /*if (loImportJournalViewModel.IsSaveSuccess)
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
                loImportJournalViewModel.loHeaderDisplay.Clear();
                loImportJournalViewModel.loDetailDisplayList.Clear();
                IsSaveProcess = false;
                loImportJournalViewModel.Percentage = 0;
                loImportJournalViewModel.Message = "";
                loImportJournalViewModel.StateChangeAction();
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
            loImportJournalViewModel.IsSeparateDCAmount = value;
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
                loImportJournalViewModel.loFileByte = loMS.ToArray();
                loImportJournalViewModel.lcFileName = args.File.Name;
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
                    CPROCESS_ID = loImportJournalViewModel.lcKeyGuid,
                    CFILE_NAME = loImportJournalViewModel.lcFileName
                };
                eventArgs.TargetPageType = typeof(ImportJournalError);
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
