using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GSM06000Common;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GSM06000Model.ViewModel
{
    public class GSM06000ViewModel : R_ViewModel<GSM06000DTO>
    {
        private readonly GSM06000Model _model = new GSM06000Model();
        private readonly UploadCenterModel _uploadModel = new UploadCenterModel();

        public GSM06000DTO loEntity = new GSM06000DTO();
        public ObservableCollection<GSM06000DTO> loGridList = new ObservableCollection<GSM06000DTO>();

        public string PropertyBankContext = "";
        public string PropertyTypeContext = "";

        public List<GSM06000CodeDTO> loTypeModeList { get; set; } = new List<GSM06000CodeDTO>();
        public List<GSM06000CodeDTO> loTypeBankList { get; set; } = new List<GSM06000CodeDTO>();

        public async Task GetAllCashBankList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetAllCashBankStreamAsync(pcType: PropertyTypeContext, pcBank: PropertyBankContext);

                loGridList = new ObservableCollection<GSM06000DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTypeList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetCashTypeAsync();
#pragma warning disable CS8601 // Possible null reference assignment.
                PropertyTypeContext = loResult.Type[0].CCODE;
#pragma warning restore CS8601 // Possible null reference assignment.
                loTypeModeList = loResult.Type;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetBankList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetBankTypeAsync();
#pragma warning disable CS8601 // Possible null reference assignment.
                PropertyBankContext = loResult.Type[0].CCODE;
#pragma warning restore CS8601 // Possible null reference assignment.
                loTypeBankList = loResult.Type;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Validation(GSM06000DTO poEntity)
        {
            var loException = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(poEntity.CCB_CODE))
                    loException.Add("001", "CCB CODE cannot be null.");

                if (string.IsNullOrEmpty(poEntity.CCB_NAME))
                    loException.Add("002", "CCB NAME cannot be null.");

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }

        public async Task GetEntity(GSM06000DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task SaveCashBank(GSM06000DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {
                    poNewEntity.CCB_TYPE = PropertyBankContext;
                    poNewEntity.CBANK_TYPE = PropertyTypeContext;
                }

                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteCashBank(GSM06000DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                /*
                if (poEntity.CSTATUS != "00")
                {
                    var loErr = R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "3003");

                    loEx.Add(loErr);
                }
                */

                await _model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task<GSM06000UploadFileDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            GSM06000UploadFileDTO loResult = new GSM06000UploadFileDTO();

            try
            {
                loResult = await _uploadModel.DownloadTemplateFileAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        
        
    }
}