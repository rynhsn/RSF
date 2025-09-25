using PMT01100Common.DTO._1._Unit_List;
using PMT01100Common.Utilities;
using PMT01100Common.Utilities.Db;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PMT01100Model.ViewModel
{
    public class PMT01100UnitListViewModel : R_ViewModel<PMT01100UnitList_BuildingDTO>
    {
        #region From Back

        private readonly PMT01100UnitListModel _modelPMT01100UnitListModel = new PMT01100UnitListModel();

        public ObservableCollection<PMT01100UnitList_BuildingDTO> loListBuilding = new ObservableCollection<PMT01100UnitList_BuildingDTO>();

        public ObservableCollection<PMT01100UnitList_UnitListDTO> loListUnitList = new ObservableCollection<PMT01100UnitList_UnitListDTO>();

        public ObservableCollection<PMT01100UnitList_SelectedUnitDTO> loSelectedUnitList = new ObservableCollection<PMT01100UnitList_SelectedUnitDTO>();

        public PMT01100UtilitiesParameterPropertyandBuildingDTO oParameterForGetUnitList = new PMT01100UtilitiesParameterPropertyandBuildingDTO();

        public List<PMT01100PropertyListDTO> loPropertyList { get; set; } = new List<PMT01100PropertyListDTO>();

        public PMT01100VarGsmTransactionCodeDTO loGsmTransactionCode = new PMT01100VarGsmTransactionCodeDTO();

        #endregion

        #region For Front

        public PMT01100UtilitiesParameterPropertyDTO oPropertyId = new PMT01100UtilitiesParameterPropertyDTO();

        //For Enable Disable Property
        public bool lControlData = true;

        //For Control Tab Page
        public bool lControlTabUnitList = true;
        public bool lControlTabLOO = true;
        public bool lControlTabLOC = true;

        #endregion

        #region Program

        public async Task GetBuildingList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(oPropertyId.CPROPERTY_ID))
                {
                    var loResult = await _modelPMT01100UnitListModel.GetBuildingListAsync(poParameter: oPropertyId);
                    if (loResult.Data.Any())
                    {
                        foreach (var item in loResult.Data)
                        {
                            item.SetBuildingIdAndName();
                        }
                    }
                    loListBuilding = new ObservableCollection<PMT01100UnitList_BuildingDTO>(loResult.Data);
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

                if (!string.IsNullOrEmpty(oParameterForGetUnitList.CPROPERTY_ID))
                {
                    var loResult = await _modelPMT01100UnitListModel.GetUnitListAsync(poParameter: oParameterForGetUnitList);
                    if (loResult.Data.Any())
                    {
                        foreach (var item in loResult.Data)
                        {
                            item.DSTART_DATE = ConvertStringToDateTimeFormat(item.CSTART_DATE!)!;
                            item.DEND_DATE = ConvertStringToDateTimeFormat(item.CEND_DATE!)!;
                        }
                    }
                    loListUnitList = new ObservableCollection<PMT01100UnitList_UnitListDTO>(loResult.Data);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelPMT01100UnitListModel.GetPropertyListAsync();
                if (loResult.Data.Any())
                {
                    loPropertyList = new List<PMT01100PropertyListDTO>(loResult.Data);
                    if (string.IsNullOrEmpty(oPropertyId.CPROPERTY_ID))
                    {
                        oPropertyId.CPROPERTY_ID = oParameterForGetUnitList.CPROPERTY_ID = loResult.Data.First().CPROPERTY_ID!;
                    }
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
