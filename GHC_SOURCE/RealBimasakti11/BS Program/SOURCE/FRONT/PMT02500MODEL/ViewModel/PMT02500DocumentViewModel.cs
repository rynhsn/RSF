using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using PMT02500Common.Utilities;
using PMT02500Common.DTO._7._Document;
using R_CommonFrontBackAPI;
using System.Globalization;
using System.Linq;
using R_BlazorFrontEnd.Helpers;

namespace PMT02500Model.ViewModel
{
    public class PMT02500DocumentViewModel : R_ViewModel<PMT02500FrontDocumentDetailDTO>
    {
        #region From Back
        private readonly PMT02500DocumentModel _modelPMT02500DocumentModel = new PMT02500DocumentModel();
        public ObservableCollection<PMT02500DocumentListDTO> loListPMT02500Document = new ObservableCollection<PMT02500DocumentListDTO>();
        public PMT02500FrontDocumentDetailDTO loEntityDocument = new PMT02500FrontDocumentDetailDTO();
        public PMT02500DocumentHeaderDTO loEntityDocumentHeader = new PMT02500DocumentHeaderDTO();
        public List<PMT02500PropertyListDTO> loPropertyList { get; set; } = new List<PMT02500PropertyListDTO>();
        public PMT02500GetHeaderParameterDTO loParameterList = new PMT02500GetHeaderParameterDTO();
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
                    var loResult = await _modelPMT02500DocumentModel.GetDocumentHeaderAsync(poParameter: loParameterList);
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
                    var loResult = await _modelPMT02500DocumentModel.GetDocumentListAsync(poParameter: loParameterList);
                    if (loResult.Any())
                    {
                        foreach (var item in loResult)
                        {
                            item.DDOC_DATE = ConvertStringToDateTimeFormat(item.CDOC_DATE)!;
                            item.DEXPIRED_DATE = ConvertStringToDateTimeFormat(item.CEXPIRED_DATE)!;
                        }
                    }
                    loListPMT02500Document = new ObservableCollection<PMT02500DocumentListDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(PMT02500FrontDocumentDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {

                var loResult = await _modelPMT02500DocumentModel.R_ServiceGetRecordAsync(ConvertToEntityBack(poEntity));

                loEntityDocument = ConvertToEntityFront(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT02500FrontDocumentDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                poNewEntity.CPROPERTY_ID = string.IsNullOrEmpty(poNewEntity.CPROPERTY_ID) ? loParameterList.CPROPERTY_ID : poNewEntity.CPROPERTY_ID;
                poNewEntity.CDEPT_CODE = string.IsNullOrEmpty(poNewEntity.CDEPT_CODE) ? loParameterList.CDEPT_CODE! : poNewEntity.CDEPT_CODE;
                poNewEntity.CREF_NO = string.IsNullOrEmpty(poNewEntity.CREF_NO) ? loParameterList.CREF_NO : poNewEntity.CREF_NO;
                poNewEntity.CTRANS_CODE = string.IsNullOrEmpty(poNewEntity.CTRANS_CODE) ? loParameterList.CTRANS_CODE : poNewEntity.CTRANS_CODE;

                var loResult = await _modelPMT02500DocumentModel.R_ServiceSaveAsync(ConvertToEntityBack(poNewEntity), peCRUDMode);

                loEntityDocument = ConvertToEntityFront(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT02500FrontDocumentDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {

                // set Add PropertyId and Charges Type
                poEntity.CPROPERTY_ID = string.IsNullOrEmpty(poEntity.CPROPERTY_ID) ? loParameterList.CPROPERTY_ID : poEntity.CPROPERTY_ID;
                poEntity.CDEPT_CODE = string.IsNullOrEmpty(poEntity.CDEPT_CODE) ? loParameterList.CDEPT_CODE! : poEntity.CDEPT_CODE;
                poEntity.CREF_NO = string.IsNullOrEmpty(poEntity.CREF_NO) ? loParameterList.CREF_NO : poEntity.CREF_NO;
                poEntity.CTRANS_CODE = string.IsNullOrEmpty(poEntity.CTRANS_CODE) ? loParameterList.CTRANS_CODE : poEntity.CTRANS_CODE;

                // Validation Before Delete
                await _modelPMT02500DocumentModel.R_ServiceDeleteAsync(ConvertToEntityBack(poEntity));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion


        #region Utilities

        private PMT02500FrontDocumentDetailDTO ConvertToEntityFront(PMT02500DocumentDetailDTO poEntity)
        {
            var loEx = new R_Exception();
            PMT02500FrontDocumentDetailDTO? loReturn = null;

            try
            {
                if (poEntity != null)
                {
                    loReturn = R_FrontUtility.ConvertObjectToObject<PMT02500FrontDocumentDetailDTO>(poEntity);
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

        private PMT02500DocumentDetailDTO ConvertToEntityBack(PMT02500FrontDocumentDetailDTO poEntity)
        {
            var loEx = new R_Exception();
            PMT02500DocumentDetailDTO? loReturn = null;

            try
            {
                if (poEntity != null)
                {
                    loReturn = R_FrontUtility.ConvertObjectToObject<PMT02500DocumentDetailDTO>(poEntity);
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