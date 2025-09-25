using BlazorClientHelper;
using PMT01500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using PMT01500Common.DTO._7._Document;
using Microsoft.AspNetCore.Components.Forms;
using PMT01500Common.Utilities;
using R_BlazorFrontEnd.Enums;
using PMT01500Common.Utilities.Front;
using PMT01500Common.DTO._6._Deposit;
using R_BlazorFrontEnd.Controls.Enums;
using R_LockingFront;
using PMT01500Common.DTO._2._Agreement;

namespace PMT01500Front
{
    public partial class PMT01500Document
    {
        private readonly PMT01500DocumentViewModel _documentViewModel = new();
        [Inject] private IClientHelper? _clientHelper { get; set; }
        private R_Conductor? _conductorDocument;
        private R_Grid<PMT01500DocumentListDTO>? _gridDocument;

        PMT01500EventCallBackDTO _oEventCallBack = new PMT01500EventCallBackDTO();

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _documentViewModel.loParameterList = (PMT01500GetHeaderParameterDTO)poParameter;
                _documentViewModel.loParameterList.CUNIT_ID = _documentViewModel.loParameterList.CCHARGE_MODE == "02" ? _documentViewModel.loParameterList.CUNIT_ID : "";
                _documentViewModel.loParameterList.CUNIT_NAME = _documentViewModel.loParameterList.CCHARGE_MODE == "02" ? _documentViewModel.loParameterList.CUNIT_NAME : "";

                if (!string.IsNullOrEmpty(_documentViewModel.loParameterList.CREF_NO))
                {
                    await _documentViewModel.GetDocumentHeader();
                    await _gridDocument.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Locking

        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";

        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult? loLockResult = null;

            try
            {
                var loData = (PMT01500FrontDocumentDetailDTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT01500",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CREF_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT01500",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CREF_NO)
                    };

                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }

        #endregion

        private async Task GetListDocument(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _documentViewModel.GetDocumentList();
                eventArgs.ListEntityResult = _documentViewModel.loListPMT01500Document;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        #region Master CRUD

        private void ServiceR_Display(R_DisplayEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                switch (eventArgs.ConductorMode)
                {
                    case R_eConductorMode.Edit:
                        //Focus Async
                        break;
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<PMT01500FrontDocumentDetailDTO>(eventArgs.Data);
                PMT01500FrontDocumentDetailDTO loParam = new PMT01500FrontDocumentDetailDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _documentViewModel.loParameterList.CPROPERTY_ID,
                    CDEPT_CODE = _documentViewModel.loParameterList.CDEPT_CODE,
                    CREF_NO = _documentViewModel.loParameterList.CREF_NO,
                    CTRANS_CODE = _documentViewModel.loParameterList.CTRANS_CODE,
                    CDOC_NO = loData.CDOC_NO,
                    CUSER_ID = _clientHelper.UserId
                };

                await _documentViewModel.GetEntity(loParam);
                eventArgs.Result = _documentViewModel.loEntityDocument;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01500FrontDocumentDetailDTO>(eventArgs.Data);

                await _documentViewModel.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _documentViewModel.loEntityDocument;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01500FrontDocumentDetailDTO)eventArgs.Data;

                await _documentViewModel.GetEntity(loData);

                if (_documentViewModel.loEntityDocument != null)
                    await _documentViewModel.ServiceDelete(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Master UtilitiesConductor


        private async Task SetOther(R_SetEventArgs eventArgs)
        {
            _oEventCallBack.LContractorOnCRUDmode = eventArgs.Enable;
            //_oEventCallBack.CREF_NO = _documentViewModel.loParameterList.CREF_NO!;
            await InvokeTabEventCallbackAsync(_oEventCallBack);
        }

        private void AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                var loData = (PMT01500FrontDocumentDetailDTO)eventArgs.Data;
                loData.DDOC_DATE = DateTime.Now;
                loData.DEXPIRED_DATE = DateTime.Now.AddDays(1);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        #endregion

        #region R_Storage
        private async Task InputFileChangePMT01500Document(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01500FrontDocumentDetailDTO)_conductorDocument.R_GetCurrentData();

                // Set Data
                loData.CFileNameExtension = eventArgs.File.Name;
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                loData.OData = loMS.ToArray();
                loData.CFileExtension = Path.GetExtension(eventArgs.File.Name);
                loData.CFileName = Path.GetFileNameWithoutExtension(eventArgs.File.Name);
                //Set value
                loData.LINVOICE_TEMPLATE = true;
                loData.CINVOICE_TEMPLATE = loData.CFileName + loData.CFileExtension;
                loData.CDOC_FILE = loData.CFileName;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

    }
}
