using PMT00100COMMON.UnitList;
using PMT00100COMMON.UtilityDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using Global_PMCOMMON.DTOs.Response.Property;
using Global_PMModel.ViewModel;

namespace PMT00100MODEL.ViewModel
{
    public class PMT0100UnitListViewModel
    {
        private PMT00100ListModel _GetListModel = new PMT00100ListModel();
        private GlobalFunctionViewModel _viewModelGlobal = new GlobalFunctionViewModel();

        public ObservableCollection<PMT00100BuildingDTO> BuildingList = new ObservableCollection<PMT00100BuildingDTO>();
        public PMT00100BuildingDTO BuildingValue = new PMT00100BuildingDTO();

        public ObservableCollection<PMT00100DTO> BuildingUnitList = new ObservableCollection<PMT00100DTO>();
        public ObservableCollection<PMT00100AgreementByUnitDTO> AgreementByUnitList = new ObservableCollection<PMT00100AgreementByUnitDTO>();

        public PMT00100DbParameterDTO Parameter = new PMT00100DbParameterDTO();
        public List<PropertyDTO> PropertyList = new List<PropertyDTO>();
        public PropertyDTO PropertyValue = new PropertyDTO();
        public bool lPropertyExist = true;
        public bool btnChangeUnitAndRealease = true;
        public bool btnSold_Booking = true;
        public bool btnViewImage = true;
        public bool lControlButtonRedraft;
        public bool lControlButtonSubmit;
        public async Task GetPropertyList()
        {
            R_Exception loEx = new R_Exception();
            List<PropertyDTO>? loReturn = new List<PropertyDTO>();
            try
            {
                List<PropertyDTO> loResult = await _viewModelGlobal.GetPropertyList();
                if (loResult.Any())
                {
                    PropertyList = loResult;
                    PropertyValue = PropertyList[0];
                    Parameter.CPROPERTY_ID = PropertyValue.CPROPERTY_ID!;
                    lPropertyExist = true;
                }
                else
                {
                    lPropertyExist = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetBuildingList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(Parameter.CPROPERTY_ID))
                {
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, Parameter.CPROPERTY_ID);
                    var loResult = await _GetListModel.GetBuildingAsyncModel();
                    if (loResult.Data.Any())
                    {
                        BuildingList = new ObservableCollection<PMT00100BuildingDTO>(loResult.Data);
                    }
                    else
                    {
                        BuildingList = new ObservableCollection<PMT00100BuildingDTO>();
                        BuildingUnitList = new ObservableCollection<PMT00100DTO>();
                        AgreementByUnitList = new ObservableCollection<PMT00100AgreementByUnitDTO>();
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task GetBuildingUnitList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                Parameter.CFLOOR_ID = "";
                Parameter.CUNIT_CATEGORY_LIST = "01,03";
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, Parameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CBUILDING_ID, Parameter.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CFLOOR_ID, "");
                R_FrontContext.R_SetStreamingContext(ContextConstant.CUNIT_CATEGORY_LIST, Parameter.CUNIT_CATEGORY_LIST);

                var loResult = await _GetListModel.GetBuildingUnitAsyncModel();
                if (loResult.Data.Any())
                {
                    btnSold_Booking = true;
                    btnViewImage = true;
                    BuildingUnitList = new ObservableCollection<PMT00100DTO>(loResult.Data);
                }
                else
                {
                    btnSold_Booking = false;
                    btnViewImage = false;
                    BuildingUnitList = new ObservableCollection<PMT00100DTO>();
                    AgreementByUnitList = new ObservableCollection<PMT00100AgreementByUnitDTO>();
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task GetAgreementByUnitList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                Parameter.LOTHER_UNIT = false;

                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, Parameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CBUILDING_ID, Parameter.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CFLOOR_ID, Parameter.CFLOOR_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CUNIT_ID, Parameter.CUNIT_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.LOTHER_UNIT, Parameter.LOTHER_UNIT);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANS_CODE, "802011");
                var loResult = await _GetListModel.GetAgreementByUnitAsyncModel();

                if (loResult.Data.Any())
                {
                    btnChangeUnitAndRealease = true;
                    foreach (var item in loResult.Data)
                    {
                        item.DREF_DATE = ConvertStringToDateTimeFormat(item.CREF_DATE);
                    }
                    AgreementByUnitList = new ObservableCollection<PMT00100AgreementByUnitDTO>(loResult.Data);
                }
                else
                {
                    btnChangeUnitAndRealease = false;
                    AgreementByUnitList = new ObservableCollection<PMT00100AgreementByUnitDTO>();
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
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
