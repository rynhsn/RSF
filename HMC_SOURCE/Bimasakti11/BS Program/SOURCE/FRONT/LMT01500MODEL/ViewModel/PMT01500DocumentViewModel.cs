using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using PMT01500Common.Utilities;
using PMT01500Common.DTO._7._Document;
using R_CommonFrontBackAPI;
using System.Globalization;
using System.Linq;
using PMT01500Common.DTO._6._Deposit;
using R_BlazorFrontEnd.Helpers;

namespace PMT01500Model.ViewModel
{
    public class PMT01500DocumentViewModel : R_ViewModel<PMT01500FrontDocumentDetailDTO>
    {
        #region From Back
        private readonly PMT01500DocumentModel _modelPMT01500DocumentModel = new PMT01500DocumentModel();
        public ObservableCollection<PMT01500DocumentListDTO> loListPMT01500Document = new ObservableCollection<PMT01500DocumentListDTO>();
        public PMT01500FrontDocumentDetailDTO loEntityDocument = new PMT01500FrontDocumentDetailDTO();
        public PMT01500DocumentHeaderDTO loEntityDocumentHeader = new PMT01500DocumentHeaderDTO();
        public List<PMT01500PropertyListDTO> loPropertyList { get; set; } = new List<PMT01500PropertyListDTO>();
        public PMT01500GetHeaderParameterDTO loParameterList = new PMT01500GetHeaderParameterDTO();
        #endregion

        #region For Front

        #endregion

        #region Document

        public async Task GetDocumentHeader()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelPMT01500DocumentModel.GetDocumentHeaderAsync(poParameter: loParameterList);
                    loResult.CUNIT_ID = loParameterList.CUNIT_ID;
                    loResult.CUNIT_NAME = loParameterList.CUNIT_NAME;
                    loEntityDocumentHeader = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetDocumentList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelPMT01500DocumentModel.GetDocumentListAsync(poParameter: loParameterList);
                    if (loResult.Any())
                    {
                        foreach (var item in loResult)
                        {
                            item.DDOC_DATE = ConvertStringToDateTimeFormat(item.CDOC_DATE)!;
                            item.DEXPIRED_DATE = ConvertStringToDateTimeFormat(item.CEXPIRED_DATE)!;
                        }
                    }
                    loListPMT01500Document = new ObservableCollection<PMT01500DocumentListDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(PMT01500FrontDocumentDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {

                var loResult = await _modelPMT01500DocumentModel.R_ServiceGetRecordAsync(ConvertToEntityBack(poEntity));

                loEntityDocument = ConvertToEntityFront(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT01500FrontDocumentDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                poNewEntity.CPROPERTY_ID = string.IsNullOrEmpty(poNewEntity.CPROPERTY_ID) ? loParameterList.CPROPERTY_ID : poNewEntity.CPROPERTY_ID;
                poNewEntity.CDEPT_CODE = string.IsNullOrEmpty(poNewEntity.CDEPT_CODE) ? loParameterList.CDEPT_CODE : poNewEntity.CDEPT_CODE;
                poNewEntity.CREF_NO = string.IsNullOrEmpty(poNewEntity.CREF_NO) ? loParameterList.CREF_NO : poNewEntity.CREF_NO;
                poNewEntity.CTRANS_CODE = string.IsNullOrEmpty(poNewEntity.CTRANS_CODE) ? loParameterList.CTRANS_CODE : poNewEntity.CTRANS_CODE;

                var loResult = await _modelPMT01500DocumentModel.R_ServiceSaveAsync(ConvertToEntityBack(poNewEntity), peCRUDMode);

                loEntityDocument = ConvertToEntityFront(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT01500FrontDocumentDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {

                // set Add PropertyId and Charges Type
                poEntity.CPROPERTY_ID = string.IsNullOrEmpty(poEntity.CPROPERTY_ID) ? loParameterList.CPROPERTY_ID : poEntity.CPROPERTY_ID;
                poEntity.CDEPT_CODE = string.IsNullOrEmpty(poEntity.CDEPT_CODE) ? loParameterList.CDEPT_CODE : poEntity.CDEPT_CODE;
                poEntity.CREF_NO = string.IsNullOrEmpty(poEntity.CREF_NO) ? loParameterList.CREF_NO : poEntity.CREF_NO;
                poEntity.CTRANS_CODE = string.IsNullOrEmpty(poEntity.CTRANS_CODE) ? loParameterList.CTRANS_CODE : poEntity.CTRANS_CODE;

                // Validation Before Delete
                await _modelPMT01500DocumentModel.R_ServiceDeleteAsync(ConvertToEntityBack(poEntity));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion


        #region Utilities

        private PMT01500FrontDocumentDetailDTO ConvertToEntityFront(PMT01500DocumentDetailDTO poEntity)
        {
            var loEx = new R_Exception();
            PMT01500FrontDocumentDetailDTO? loReturn = null;

            try
            {
                if (poEntity != null)
                {
                    loReturn = R_FrontUtility.ConvertObjectToObject<PMT01500FrontDocumentDetailDTO>(poEntity);
                    loReturn.DDOC_DATE = ConvertStringToDateTimeFormat(poEntity.CDOC_DATE);
                    loReturn.DEXPIRED_DATE = ConvertStringToDateTimeFormat(poEntity.CEXPIRED_DATE);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loReturn!;
        }

        private PMT01500DocumentDetailDTO ConvertToEntityBack(PMT01500FrontDocumentDetailDTO poEntity)
        {
            var loEx = new R_Exception();
            PMT01500DocumentDetailDTO? loReturn = null;

            try
            {
                if (poEntity != null)
                {
                    loReturn = R_FrontUtility.ConvertObjectToObject<PMT01500DocumentDetailDTO>(poEntity);
                    loReturn.CDOC_DATE = ConvertDateTimeToStringFormat(poEntity.DDOC_DATE);
                    loReturn.CEXPIRED_DATE = ConvertDateTimeToStringFormat(poEntity.DEXPIRED_DATE);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loReturn!;
        }

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