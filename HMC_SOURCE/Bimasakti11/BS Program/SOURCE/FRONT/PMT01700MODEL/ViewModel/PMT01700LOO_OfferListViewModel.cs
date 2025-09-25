using PMT01700COMMON.Context._1._Other_Untit_List;
using PMT01700COMMON.DTO._2._LOO._1._LOO___Offer_List;
using PMT01700COMMON.DTO.Utilities.Front;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.DTO.Utilities.Print;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT01700MODEL.ViewModel
{
    public class PMT01700LOO_OfferListViewModel : R_ViewModel<PMT01700LOO_OfferList_OfferListDTO>
    {
        #region From Back

        private readonly PMT01700LOO_OfferListModel _model = new PMT01700LOO_OfferListModel();
        public ObservableCollection<PMT01700LOO_OfferList_OfferListDTO> loListOfferList = new ObservableCollection<PMT01700LOO_OfferList_OfferListDTO>();
        public ObservableCollection<PMT01700LOO_OfferList_UnitListDTO> loListUnitList = new ObservableCollection<PMT01700LOO_OfferList_UnitListDTO>();
        public PMT01700ParameterFrontChangePageDTO oParameter = new PMT01700ParameterFrontChangePageDTO();

        public List<ReportTemplateListDTO> ListReportTemplate = new List<ReportTemplateListDTO>();

        #endregion

        #region For Front

        public bool lControlButton;

        //Belom Tau Buat Apa
        public DateTime? dFilterFromOfferDate = DateTime.Now;
        public bool lFilterCancelled = false;


        //For Control Tab Page
        public bool lControlTabOfferList = true;
        public bool lControlTabOffer = true;
        public bool lControlTabUnit = true;
        //public bool lControlTabCharges = true;
        public bool lControlTabDeposit = true;

        //For Text Bulding
        public string? cBuildingSelectedUnit = "";

        public PMT01700LOO_OfferList_OfferListDTO loEntityOffer = new PMT01700LOO_OfferList_OfferListDTO();
        public bool lControlButtonRevise;
        public bool lControlButtonCancelLOO_LOC;
        public bool lControlButtonRedraft;
        public bool lControlButtonSubmit;
        public bool lControlButtonReverse;
        public bool lControlButtonPrint;

        public ParameterPrintDTO ParameterPrint = new ParameterPrintDTO();
        public string? SelectedReportTemplate { get; set; }

        #endregion

        #region Program
        public async Task GetOfferList()
        {
            R_Exception loEx = new R_Exception();
            try
            {

                string lcdFilterFromOfferDate = (oParameter.CTRANS_CODE == "802043")
                                              ? ConvertDateToStringFormat(dFilterFromOfferDate) ?? ""
                                              : "";
                string _filterCancelled = (oParameter.CTRANS_CODE == "802043" && lFilterCancelled == true)
                                            ? "90,98"
                                            : "";
                lcdFilterFromOfferDate = oParameter.CTRANS_CODE == "802053" ? "" : lcdFilterFromOfferDate;

                if (!string.IsNullOrEmpty(oParameter.CPROPERTY_ID))
                {
                    R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CPROPERTY_ID, oParameter.CPROPERTY_ID);
                    R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CTRANS_CODE, oParameter.CTRANS_CODE);
                    R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CFROM_REF_DATE, lcdFilterFromOfferDate);
                    R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CCANCELLED, _filterCancelled);

                    var loResult = await _model.GetOfferListAsync();
                    if (loResult.Data.Any())
                    {
                        foreach (var item in loResult.Data)
                        {
                            item.DREF_DATE = ConvertStringToDateTimeFormat(item.CREF_DATE);
                            item.DFOLLOW_UP_DATE = ConvertStringToDateTimeFormat(item.CFOLLOW_UP_DATE);
                            item.DEXPIRED_DATE = ConvertStringToDateTimeFormat(item.CEXPIRED_DATE);
                        }
                    }
                    loListOfferList = new ObservableCollection<PMT01700LOO_OfferList_OfferListDTO>(loResult.Data);
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
                if (!string.IsNullOrEmpty(oParameter.CREF_NO))
                {
                    R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CPROPERTY_ID, oParameter.CPROPERTY_ID);
                    R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CTRANS_CODE, oParameter.CTRANS_CODE);
                    R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CDEPT_CODE, oParameter.CDEPT_CODE);
                    R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CREF_NO, oParameter.CREF_NO);
                    var loResult = await _model.GetUnitListAsync();

                    loListUnitList = new ObservableCollection<PMT01700LOO_OfferList_UnitListDTO>(loResult.Data);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        #endregion
        #region PRint
        public async Task GetReportTemplateList(ParamaterGetReportTemplateListDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetDataGetReportTemplateListAsync(poParameter: poParam);
                if (loResult.Data != null && loResult.Data.Any())
                {
                    ListReportTemplate = loResult.Data;
                    SelectedReportTemplate = ListReportTemplate
                        .FirstOrDefault(x => x.LDEFAULT)
                        .CTEMPLATE_ID;
                }
                else
                {
                    SelectedReportTemplate = "";
                    loEx.Add("", "Data Template Not Found");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Button
        public async Task<PMT01700LOO_OfferList_OfferListDTO> ProcessUpdateAgreement(string lcNewStatus)
        {
            R_Exception loEx = new R_Exception();
            PMT01700LOO_OfferList_OfferListDTO loReturn = new PMT01700LOO_OfferList_OfferListDTO();
            try
            {
                if (!string.IsNullOrEmpty(loEntityOffer.CREF_NO))
                {
                    PMT01700LOO_ProcessOffer_DTO currentOffer = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_ProcessOffer_DTO>(loEntityOffer);
                    currentOffer.CNEW_STATUS = lcNewStatus;

                    var loResult = await _model.UpdateAgreementAsync(currentOffer);
                    loReturn = loResult;

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
        private string? ConvertDateToStringFormat(DateTime? ptEntity)
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
