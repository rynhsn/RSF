using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BaseAOC_BS11Common.DTO.Request.Request.Single;
using BaseAOC_BS11Common.DTO.Response.Single;
using BaseAOC_BS11Model;
using PMT02500Common.DTO._1._AgreementList;
using PMT02500Common.DTO._1._AgreementList.Upload;
using PMT02500Common.Utilities;
using PMT02500Common.Utilities.Db;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace PMT02500Model.ViewModel
{
    public class PMT02500AgreementListViewModel : R_ViewModel<PMT02500AgreementListOriginalDTO>
    {
        #region From Back
        private readonly PMT02500AgreementListModel _modelPMT02500AgreementListModel = new PMT02500AgreementListModel();
        private readonly BaseAOCGetSingleDataModel _baseSingleDataModel = new BaseAOCGetSingleDataModel();
        public ObservableCollection<PMT02500AgreementListOriginalDTO> loListPMT02500Agreement = new ObservableCollection<PMT02500AgreementListOriginalDTO>();
        public PMT02500AgreementListOriginalDTO loEntity = new PMT02500AgreementListOriginalDTO();
        public ObservableCollection<PMT02500UnitListOriginalDTO> loListPMT02500UnitList = new ObservableCollection<PMT02500UnitListOriginalDTO>();
        public PMT02500ChangeStatusDTO? loEntityChangeStatus = new PMT02500ChangeStatusDTO();
        public List<PMT02500PropertyListDTO> loPropertyList { get; set; } = new List<PMT02500PropertyListDTO>();
        public PMT02500VarGsmTransactionCodeDTO loGsmTransactionCode = new PMT02500VarGsmTransactionCodeDTO();
        public PMT02500SelectedAgreementGetUnitDescriptionDTO loSelectedAgreementGetUnitDescription = new PMT02500SelectedAgreementGetUnitDescriptionDTO();
        public List<PMT02500ComboBoxDTO> loComboBoxDataCTransStatus { get; set; } = new List<PMT02500ComboBoxDTO>();
        public PMT02500ProcessResultDTO? loEntityResultChangeStatus = new PMT02500ProcessResultDTO();
        public PMT02500GetHeaderParameterDTO loParameterList = new PMT02500GetHeaderParameterDTO();
        public PMT02500GetHeaderParameterDTO loParameterChangeStatus = new PMT02500GetHeaderParameterDTO();
        #endregion

        #region For Front
        public bool lControlButtonSubmit = false;
        public bool lControlButtonRedraft = false;

        public string _cPropertyId = "";
        public bool _lComboBoxProperty = true;
        public PMT02500FrontTotalUnitandAreaAgreementListDTO _oTotalUnitAgreementList = new PMT02500FrontTotalUnitandAreaAgreementListDTO();

        public PMT02500AgreementListOriginalDTO _oTempData = new PMT02500AgreementListOriginalDTO();
        //Nyoba aje
        public string _cModeParameter = "";

        #endregion

        #region AgreementList

        public async Task GetPropertyList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelPMT02500AgreementListModel.GetPropertyListAsync();
                if (loResult.Any())
                {
                    loPropertyList = new List<PMT02500PropertyListDTO>(loResult);
                    if (string.IsNullOrEmpty(_cPropertyId))
                    {
                        _cPropertyId = loResult.First().CPROPERTY_ID!;
                        loParameterList.CPROPERTY_ID = _cPropertyId;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetUnitList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelPMT02500AgreementListModel.GetUnitListAsync(poParameter: loParameterList);
                loListPMT02500UnitList = new ObservableCollection<PMT02500UnitListOriginalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        /*
        public async Task GetSelectedAgreementGetUnitDescriptionAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CREF_NO))
                {
                    var loResult = await _modelPMT02500AgreementListModel.GetSelectedAgreementGetUnitDescriptionAsync(poParameter: loParameterList);
                    loSelectedAgreementGetUnitDescription.CUNIT_DESCRIPTION = string.IsNullOrEmpty(loParameterList.CREF_NO) ? "" : loResult.CUNIT_DESCRIPTION;
                }
                else
                {
                    loSelectedAgreementGetUnitDescription.CUNIT_DESCRIPTION = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        */

        public async Task GetAgreementList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(_cPropertyId))
                {
                    PMT02500UtilitiesParameterDbGetAgreementListDTO loParameter = new PMT02500UtilitiesParameterDbGetAgreementListDTO()
                    {
                        CTRANS_CODE = loParameterList.CTRANS_CODE,
                        CPROPERTY_ID = _cPropertyId,
                    };
                    var loResult = await _modelPMT02500AgreementListModel.GetAgreementListAsync(poParameter: loParameter);
                    if (loResult.Any())
                    {
                        foreach (var item in loResult)
                        {
                            item.DSTART_DATE = (DateTime)ConvertStringToDateTimeFormat(item.CSTART_DATE!)!;
                            item.DEND_DATE = (DateTime)ConvertStringToDateTimeFormat(item.CEND_DATE!)!;
                            item.DDOC_DATE = (DateTime)ConvertStringToDateTimeFormat(item.CDOC_DATE!)!;
                        }
                    }
                    loListPMT02500Agreement = new ObservableCollection<PMT02500AgreementListOriginalDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetVarGsmTransactionCode()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelPMT02500AgreementListModel.GetVarGsmTransactionCodeAsync();
                if (loResult.Any())
                {
                    loGsmTransactionCode = loResult.FirstOrDefault();
                    loParameterList.DataAgreement.LINCREMENT_FLAGFORCREF_NO = loGsmTransactionCode.LINCREMENT_FLAG;
                }
                else
                {
                    loEx.Add("Err", "Transaction Code Not Found");

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task<PMT02500TemplateFileUploadDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            PMT02500TemplateFileUploadDTO loResult = new PMT02500TemplateFileUploadDTO();

            try
            {
                loResult = await _modelPMT02500AgreementListModel.DownloadTemplateFileAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #endregion

        #region ChangeStatus

        public async Task GetComboBoxDataCTransStatus()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelPMT02500AgreementListModel.GetComboBoxDataCTransStatusAsync();
                loComboBoxDataCTransStatus = new List<PMT02500ComboBoxDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChangeStatus()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterChangeStatus.CREF_NO))
                {
                    var loResult = await _modelPMT02500AgreementListModel.GetChangeStatusAsync(loParameterChangeStatus);
                    loResult.DREF_DATE = ConvertStringToDateTimeFormat(loResult.CREF_DATE);
                    loResult.DHAND_OVER_DATE = ConvertStringToDateTimeFormat(loResult.CHAND_OVER_DATE);
                    loResult.DSTART_DATE = ConvertStringToDateTimeFormat(loResult.CSTART_DATE);
                    loResult.DEND_DATE = ConvertStringToDateTimeFormat(loResult.CEND_DATE);
                    loResult.DACCEPT_DATE = ConvertStringToDateTimeFormat(loResult.CACCEPT_DATE);
                    loEntityChangeStatus = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessChangeStatus(PMT02500ChangeStatusParameterDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poEntity.CNEW_STATUS = loEntityChangeStatus.CAGREEMENT_STATUS;
                poEntity.CACCEPT_DATE = ConvertDateTimeToStringFormat(loEntityChangeStatus.DACCEPT_DATE);
                poEntity.CREASON = string.IsNullOrEmpty(loEntityChangeStatus.CREASON) ? "" : loEntityChangeStatus.CREASON;
                poEntity.CNOTES = loEntityChangeStatus.CNOTES;


                if (!string.IsNullOrEmpty(poEntity.CPROPERTY_ID))
                {
                    var loResult = await _modelPMT02500AgreementListModel.ProcessChangeStatusAsync(poEntity);
                    loEntityResultChangeStatus = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion



        #region Proses Update Status

        public async Task<BaseAOCResponseUpdateAgreementStatusDTO> ProsesUpdateAgreementStatus(string pcStatus)
        {
            BaseAOCResponseUpdateAgreementStatusDTO loReturn = new BaseAOCResponseUpdateAgreementStatusDTO();
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loEntity.CREF_NO))
                {
                    var loParam = new BaseAOCParameterRequestUpdateAgreementStatusDTO()
                    {
                        CPROPERTY_ID = loParameterList.CPROPERTY_ID!,
                        CDEPT_CODE = loParameterList.CDEPT_CODE!,
                        CTRANS_CODE = loParameterList.CTRANS_CODE!,
                        CREF_NO = loEntity.CREF_NO,
                        CNEW_STATUS = pcStatus,

                    };

                    var loResult = await _baseSingleDataModel.ProsesUpdateAgreementStatusAsync(loParam);
                    loReturn = loResult;
                }
                else
                {
                    loReturn.LSUCCESS = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        #endregion


        #region Utilities

        private DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null, kembalikan DateTime.MinValue atau nilai default yang sesuai
                //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                return null;
            }
            else
            {
                // Parse string ke DateTime
                DateTime result;
                if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return result;
                }
                else
                {
                    // Jika parsing gagal, kembalikan DateTime.MinValue atau nilai default yang sesuai
                    //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                    return null;
                }
            }
        }

        private string? ConvertDateTimeToStringFormat(DateTime? ptEntity)
        {
            if (!ptEntity.HasValue || ptEntity.Value == null)
            {
                // Jika ptEntity adalah null atau DateTime.MinValue, kembalikan null
                return null;
            }
            else
            {
                // Format DateTime ke string "yyyyMMdd"
                return ptEntity.Value.ToString("yyyyMMdd");
            }
        }

        #endregion

    }
}