using PMT01700COMMON.Context._1._Other_Untit_List;
using PMT01700COMMON.DTO._1._Other_Unit_List;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.DTO.Utilities.Front;
using PMT01700COMMON.DTO.Utilities.ParamDb.UnitList;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT01700MODEL.ViewModel
{
    public class PMT01700UnitListViewModel : R_ViewModel<PMT01700OtherUnitList_OtherUnitListDTO>
    {
        #region From Back

        private readonly PMT01700UnitListModel _model = new PMT01700UnitListModel();

        public List<PMT01700PropertyListDTO> loPropertyList { get; set; } = new List<PMT01700PropertyListDTO>();
    public ObservableCollection<PMT01700OtherUnitList_OtherUnitListDTO> loOtherUnitList = new ObservableCollection<PMT01700OtherUnitList_OtherUnitListDTO>();
        public PMT01700VarGsmTransactionCodeDTO loGsmTransactionCode = new PMT01700VarGsmTransactionCodeDTO();

        public ObservableCollection<PMT01700OtherUnitList_OtherSelectedUnitDTO> loSelectedUnitList = new ObservableCollection<PMT01700OtherUnitList_OtherSelectedUnitDTO>();
        #endregion
        #region For Front

        public PMT01700UtilitiesParameterPropertyDTO oProperty_oDataOtherUnit = new PMT01700UtilitiesParameterPropertyDTO();
        public PMT01700ParameterFrontChangePageDTO oParameterNewOffer = new PMT01700ParameterFrontChangePageDTO();
        //For Enable Disable Property
        public bool lControlData = true;

        //For Control Tab Page
        public bool lControlTabUnitList = true;
        public bool lControlTabLOO = true;
        public bool lControlTabLOC = true;

        #endregion

        #region Program
        public async Task GetPropertyList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.PropertyListStreamAsyncModel();
                if (loResult.Data.Any())
                {
                    loPropertyList = new List<PMT01700PropertyListDTO>(loResult.Data);
                    if (string.IsNullOrEmpty(oProperty_oDataOtherUnit.CPROPERTY_ID))
                    {
                        oProperty_oDataOtherUnit.CPROPERTY_ID = loResult.Data.First().CPROPERTY_ID!;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetOtherUnitList()
        {
            R_Exception loEx = new R_Exception();

            try
            {

                if (!string.IsNullOrEmpty(oProperty_oDataOtherUnit.CPROPERTY_ID))
                {
                    R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CPROPERTY_ID, oProperty_oDataOtherUnit.CPROPERTY_ID);
                    R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.LEVENT, true);

                    var loResult = await _model.OtherUnitListStreamAsyncModel();
                    if (loResult.Data.Any())
                    {
                        foreach (var item in loResult.Data)
                        {
                            item.DSTART_DATE = ConvertStringToDateTimeFormat(item.CSTART_DATE!)!;
                            item.DEND_DATE = ConvertStringToDateTimeFormat(item.CEND_DATE!)!;
                        }
                    }
                    loOtherUnitList = new ObservableCollection<PMT01700OtherUnitList_OtherUnitListDTO>(loResult.Data);
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
