using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using PMT01500Common.DTO._1._AgreementList;
using PMT01500Common.DTO._1._AgreementList.Upload;
using PMT01500Common.Utilities;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace PMT01500Model.ViewModel
{
    public class PMT01500AgreementListViewModel : R_ViewModel<PMT01500AgreementListOriginalDTO>
    {
        #region From Back
        private readonly PMT01500AgreementListModel _modelPMT01500AgreementListModel = new PMT01500AgreementListModel();
        public ObservableCollection<PMT01500AgreementListOriginalDTO> loListPMT01500Agreement = new ObservableCollection<PMT01500AgreementListOriginalDTO>();
        public ObservableCollection<PMT01500UnitListOriginalDTO> loListPMT01500UnitList = new ObservableCollection<PMT01500UnitListOriginalDTO>();
        public PMT01500ChangeStatusDTO? loEntityChangeStatus = new PMT01500ChangeStatusDTO();
        public List<PMT01500PropertyListDTO> loPropertyList { get; set; } = new List<PMT01500PropertyListDTO>();
        public PMT01500VarGsmTransactionCodeDTO loGsmTransactionCode = new PMT01500VarGsmTransactionCodeDTO();
        public PMT01500SelectedAgreementGetUnitDescriptionDTO loSelectedAgreementGetUnitDescription = new PMT01500SelectedAgreementGetUnitDescriptionDTO();
        public List<PMT01500ComboBoxDTO> loComboBoxDataCTransStatus { get; set; } = new List<PMT01500ComboBoxDTO>();
        public PMT01500ProcessResultDTO? loEntityResultChangeStatus = new PMT01500ProcessResultDTO();
        public PMT01500GetHeaderParameterDTO loParameterList = new PMT01500GetHeaderParameterDTO();
        public PMT01500GetHeaderParameterDTO loParameterChangeStatus = new PMT01500GetHeaderParameterDTO();
        #endregion

        #region For Front
        public string _cPropertyId = "";
        public bool _lComboBoxProperty = true;
        public PMT01500FrontTotalUnitandAreaAgreementListDTO _oTotalUnitAgreementList = new PMT01500FrontTotalUnitandAreaAgreementListDTO();

        public PMT01500AgreementListOriginalDTO _oTempData = new PMT01500AgreementListOriginalDTO();
        //Nyoba aje
        public string _cModeParameter = "";

        #endregion

        #region AgreementList

        public async Task GetPropertyList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelPMT01500AgreementListModel.GetPropertyListAsync();
                if (loResult.Any())
                {
                    loPropertyList = new List<PMT01500PropertyListDTO>(loResult);
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
                var loResult = await _modelPMT01500AgreementListModel.GetUnitListAsync(poParameter: loParameterList);
                loListPMT01500UnitList = new ObservableCollection<PMT01500UnitListOriginalDTO>(loResult);
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
                    var loResult = await _modelPMT01500AgreementListModel.GetSelectedAgreementGetUnitDescriptionAsync(poParameter: loParameterList);
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
                    var loResult = await _modelPMT01500AgreementListModel.GetAgreementListAsync(pcCPROPERTY_ID: _cPropertyId);
                    if (loResult.Any())
                    {
                        foreach (var item in loResult)
                        {
                            item.DSTART_DATE = (DateTime)ConvertStringToDateTimeFormat(item.CSTART_DATE!)!;
                            item.DEND_DATE = (DateTime)ConvertStringToDateTimeFormat(item.CEND_DATE!)!;
                        }
                    }
                    loListPMT01500Agreement = new ObservableCollection<PMT01500AgreementListOriginalDTO>(loResult);
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
                var loResult = await _modelPMT01500AgreementListModel.GetVarGsmTransactionCodeAsync();
                loGsmTransactionCode = loResult.FirstOrDefault();
                loParameterList.DataAgreement.LINCREMENT_FLAGFORCREF_NO = loGsmTransactionCode.LINCREMENT_FLAG;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task<PMT01500UploadFileDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            PMT01500UploadFileDTO loResult = new PMT01500UploadFileDTO();

            try
            {
                loResult = await _modelPMT01500AgreementListModel.DownloadTemplateFileAsync();
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
                var loResult = await _modelPMT01500AgreementListModel.GetComboBoxDataCTransStatusAsync();
                loComboBoxDataCTransStatus = new List<PMT01500ComboBoxDTO>(loResult);
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
                    var loResult = await _modelPMT01500AgreementListModel.GetChangeStatusAsync(loParameterChangeStatus);
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

        public async Task ProcessChangeStatus(PMT01500ChangeStatusParameterDTO poEntity)
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
                    var loResult = await _modelPMT01500AgreementListModel.ProcessChangeStatusAsync(poEntity);
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