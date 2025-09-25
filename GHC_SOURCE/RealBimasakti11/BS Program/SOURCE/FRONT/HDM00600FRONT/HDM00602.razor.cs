using BlazorClientHelper;
using HDM00600MODEL.View_Model_s;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Interfaces;
using R_BlazorFrontEnd;
using HDM00600COMMON.DTO;
using HDM00600FrontResources;
using R_BlazorFrontEnd.Controls.Enums;
using R_APICommonDTO;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using HDM00600COMMON.DTO_s.Helper;
using R_BlazorFrontEnd.Controls.Events;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace HDM00600FRONT
{
    public partial class HDM00602 : R_Page
    {
        private HDM00602ViewModel _viewModel = new();
        private R_Grid<PriceListExcelDisplayDTO> _gridPricelist;
        private R_ConductorGrid _conPricelist;
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] private IClientHelper _clientHelper { get; set; }
        [Inject] private R_IExcel _excelProvider { get; set; }
        [Inject] private IJSRuntime JS { get; set; }
        private int _pageSize_Pricelist = 10;
        private R_Button _btnProcess, _btnSaveToExcel;
        private string _Message = "";
        private int _Percentage = 0;
        private bool _isFileHasData { get; set; }
        private R_eFileSelectAccept[] _accepts = { R_eFileSelectAccept.Excel };

        //methods - Invoke
        private void StateChangeInvoke()
        {
            StateHasChanged();
        }

        private void DisplayErrorInvoke(R_APIException poEx)
        {
            var loEx = R_FrontUtility.R_ConvertFromAPIException(poEx);
            R_DisplayException(loEx);
        }

        private async Task ShowSuccessInvoke()
        {
            R_Exception loEx = new();
            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_msg_batchComplete"], R_eMessageBoxButtonType.OK);
                if (loValidate == R_eMessageBoxResult.OK)
                {
                    await Close(true, null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void SetPercentageAndMessageInvoke(string pcMessage, int pnPercentage)
        {
            _Message = pcMessage;
            _Percentage = pnPercentage;
        }

        //method - override
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new();
            try
            {
                _viewModel._ccompanyId = _clientHelper.CompanyId;
                _viewModel._cuserId = _clientHelper.UserId;
                if (poParameter != null)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<PropertyDTO>(poParameter);
                    _viewModel._cpropertyId = loParam.CPROPERTY_ID;
                    _viewModel._cpropertyName = loParam.CPROPERTY_NAME;
                }
                _viewModel._stateChangeAction = StateChangeInvoke;
                _viewModel._showErrorAction = DisplayErrorInvoke;
                _viewModel._setPercentageAndMessageAction = SetPercentageAndMessageInvoke;
                _viewModel._showSuccessAction = async () =>
                {
                    await ShowSuccessInvoke();
                };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        //method - event
        private async Task Pricelist_GetlistAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = (List<PricelistReadExcelDTO>)eventArgs.Parameter;
                _viewModel.ConvertGridExelToGridDTO(loData);
                _viewModel._sumListPricelistExcel = loData.Count;
                eventArgs.ListEntityResult = _viewModel._pricelist_List;
                if (_viewModel._visibleError)
                {
                    await _btnProcess.FocusAsync();
                }
                else
                {
                    await _btnSaveToExcel.FocusAsync();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void Pricelist_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData =R_FrontUtility.ConvertObjectToObject<PriceListExcelDisplayDTO>(eventArgs.Data);
                if (loData.CVALID == "N")
                {
                    eventArgs.RowClass = "rowErrorData";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            
        }

        //method - inputfile
        private async Task Onchange_InputFIlePricelist(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //import from excel
                _viewModel._sourceFileName = eventArgs.File.Name;
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                var loFileByte = loMS.ToArray();

                //add filebyte to DTO
                var loExcel = _excelProvider;
                var loDataSet = loExcel.R_ReadFromExcel(loFileByte, new string[] { "Pricelist" });
                var loResult = R_FrontUtility.R_ConvertTo<PricelistReadExcelDTO>(loDataSet.Tables[0]);
                _isFileHasData = loResult.Count > 0;
                await _gridPricelist.R_RefreshGrid(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        //method - button
        private async Task OnClick_ButtonOK()
        {
            var loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_msg_confirmupload"], R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _viewModel.SaveBatch_PricelistExcelData();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task OnClick_ButtonSaveExcelAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                List<PricelistSaveToExcelDTO> loExcelList = new();
                loExcelList = _viewModel.ConvertGridDTOToExcel(_viewModel._pricelist_List.ToList())!;
                var loDataTable = R_FrontUtility.R_ConvertTo(loExcelList);
                loDataTable.TableName = "Pricelist";

                //export to excel
                var loByteFile = _excelProvider.R_WriteToExcel(loDataTable);
                var saveFileName = $"Upload Error Pricelist.xlsx";
                var loValidate = await R_MessageBox.Show("", _localizer["_msg_confirmasavetoexcel"], R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        private async Task OnClick_ButtonClose()
        {
            await this.Close(true, false);
        }
    }
}