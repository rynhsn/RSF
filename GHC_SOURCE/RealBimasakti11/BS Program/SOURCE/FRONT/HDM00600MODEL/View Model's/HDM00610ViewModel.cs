using HDM00600COMMON;
using HDM00600COMMON.DTO;
using HDM00600COMMON.DTO.Helper;
using HDM00600COMMON.DTO_s.Helper;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace HDM00600MODEL.View_Model_s
{
    public class HDM00610ViewModel : R_ViewModel<PricelistDTO>
    {
        //var
        private HDM00600Model _initModel = new HDM00600Model();
        private HDM00601Model _modelNextPrice = new HDM00601Model();
        public List<PropertyDTO> _propertyList { get; set; } = new List<PropertyDTO>();
        public ObservableCollection<PricelistDTO> _pricelist_List { get; set; } = new ObservableCollection<PricelistDTO>();
        public PricelistDTO _nextPricelistRecord { get; set; } = new PricelistDTO();
        public string _propertyId { get; set; } = "";
        public string _propertyName { get; set; } = "";
        public string _propertyCurr { get; set; } = "";
        public enum eListPricingParamType { GetCurrent, GetNext, GetHistory }

        //method
        public async Task GetList_Pricelist(eListPricingParamType poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                //choose CTYPE as param
                string lcStatus = poParam switch
                {
                    eListPricingParamType.GetCurrent => "1",
                    eListPricingParamType.GetNext => "2",
                    eListPricingParamType.GetHistory => "3",
                    _ => ""
                };

                //set param context
                R_FrontContext.R_SetStreamingContext(PricelistMaster_ContextConstant.CPROPERTY_ID, _propertyId);
                R_FrontContext.R_SetStreamingContext(PricelistMaster_ContextConstant.CSTATUS, lcStatus);
                var loResult = await _initModel.GetList_PricelistAsync();
                _pricelist_List = new ObservableCollection<PricelistDTO>(loResult) ?? new ObservableCollection<PricelistDTO>();
                if (_pricelist_List.Count > 0)
                {
                    foreach (var item in _pricelist_List)
                    {
                        if (DateTime.TryParseExact(item.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var loDate))
                        {
                            item.DSTART_DATE = loDate;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetRecord_PricelistAsync(PricelistDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CPROPERTY_ID = _propertyId;
                var loResult = await _modelNextPrice.R_ServiceGetRecordAsync(poParam);
                if (!string.IsNullOrWhiteSpace(loResult.CSTART_DATE))
                {
                    loResult.DSTART_DATE = DateTime.ParseExact(loResult.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                _nextPricelistRecord = R_FrontUtility.ConvertObjectToObject<PricelistDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveRecord_PricelistAsync(PricelistDTO poParam, eCRUDMode poCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CPROPERTY_ID = _propertyId;
                poParam.CACTION = poCRUDMode switch
                {
                    eCRUDMode.AddMode => "NEW",
                    eCRUDMode.EditMode => "EDIT",
                    _ => ""
                };
                var loResult = await _modelNextPrice.R_ServiceSaveAsync(poParam, poCRUDMode);
                if (!string.IsNullOrWhiteSpace(loResult.CSTART_DATE))
                {
                    loResult.DSTART_DATE = DateTime.ParseExact(loResult.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                _nextPricelistRecord = R_FrontUtility.ConvertObjectToObject<PricelistDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteRecord_PricelistAsync(PricelistDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                poParam.CPROPERTY_ID = _propertyId;
                await _modelNextPrice.R_ServiceDeleteAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
