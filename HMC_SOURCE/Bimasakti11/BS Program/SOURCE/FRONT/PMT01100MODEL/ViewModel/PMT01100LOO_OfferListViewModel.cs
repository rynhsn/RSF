using PMT01100Common.DTO._1._Unit_List;
using PMT01100Common.DTO._2._LOO._1._LOO___Offer_List;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using PMT01100Common.Utilities.Front;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using PMT01100Common.Utilities.Db;
using System.Globalization;
using System.Linq;
using R_BlazorFrontEnd.Helpers;

namespace PMT01100Model.ViewModel
{
    public class PMT01100LOO_OfferListViewModel : R_ViewModel<PMT01100LOO_OfferList_OfferListDTO>
    {
        #region From Back

        private readonly PMT01100LOO_OfferListModel _model = new PMT01100LOO_OfferListModel();
        public ObservableCollection<PMT01100LOO_OfferList_OfferListDTO> loListOfferList = new ObservableCollection<PMT01100LOO_OfferList_OfferListDTO>();
        public ObservableCollection<PMT01100LOO_OfferList_UnitListDTO> loListUnitList = new ObservableCollection<PMT01100LOO_OfferList_UnitListDTO>();
        public PMT01100ParameterFrontChangePageDTO oParameter = new PMT01100ParameterFrontChangePageDTO();

        #endregion


        #region For Front

        public bool lControlButton;

        //Belom Tau Buat Apa
        public DateTime? dFilterFromOfferDate;
        public bool lFilterCancelled = false;


        //For Control Tab Page
        public bool lControlTabOfferList = true;
        public bool lControlTabOffer = true;
        public bool lControlTabUnit = true;
        //public bool lControlTabCharges = true;
        public bool lControlTabDeposit = true;

        //For Text Bulding
        public string? cBuildingSelectedUnit = "";

        #endregion

        #region Program

        public async Task GetOfferList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(oParameter.CPROPERTY_ID))
                {
                    PMT01100UtilitiesParameterOfferListDTO loParam = R_FrontUtility.ConvertObjectToObject<PMT01100UtilitiesParameterOfferListDTO>(oParameter);
                    var loResult = await _model.GetOfferListAsync(poParameter: loParam);
                    if (loResult.Data.Any())
                    {
                        foreach (var item in loResult.Data)
                        {
                            item.DREF_DATE = ConvertStringToDateTimeFormat(item.CREF_DATE);
                            item.DFOLLOW_UP_DATE = ConvertStringToDateTimeFormat(item.CFOLLOW_UP_DATE);
                        }
                    }
                    loListOfferList = new ObservableCollection<PMT01100LOO_OfferList_OfferListDTO>(loResult.Data);
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
                    oParameter.CPROPERTY_ID = oParameter.CPROPERTY_ID;
                    //oParameter.CTRANS_CODE = "802041";
                    //oParameter.CTRANS_CODE = "802030";
                    PMT01100UtilitiesParameterGetUnitListDTO loParam = new PMT01100UtilitiesParameterGetUnitListDTO()
                    {
                        CPROPERTY_ID = oParameter.CPROPERTY_ID,
                        CDEPT_CODE = oParameter.CDEPT_CODE,
                        CTRANS_CODE = oParameter.CTRANS_CODE,
                        CREF_NO = oParameter.CREF_NO,
                    };
                    var loResult = await _model.GetUnitListAsync(poParameter: loParam);
                    loListUnitList = new ObservableCollection<PMT01100LOO_OfferList_UnitListDTO>(loResult.Data);
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

        #endregion

    }
}
