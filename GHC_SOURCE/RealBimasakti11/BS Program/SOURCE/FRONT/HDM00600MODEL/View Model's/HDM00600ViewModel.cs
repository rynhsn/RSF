using HDM00600COMMON;
using HDM00600COMMON.DTO;
using HDM00600COMMON.DTO.General;
using HDM00600COMMON.DTO.Helper;
using HDM00600COMMON.DTO_s.Helper;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI.Log;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HDM00600MODEL.View_Model_s
{
    public class HDM00600ViewModel
    {
        //var
        private HDM00600Model _model = new HDM00600Model();
        public List<PropertyDTO> _propertyList { get; set; } = new List<PropertyDTO>();
        public ObservableCollection<PricelistDTO> _pricelist_List { get; set; } = new ObservableCollection<PricelistDTO>();
        public PricelistDTO _pricelist_Record { get; set; } = new PricelistDTO();
        public string _propertyId { get; set; } = "";
        public string _propertyName { get; set; } = "";
        public string _propertyCurr { get; set; } = "";
        public string _pricelistId { get; set; } = "";
        public string _validId { get; set; } = "";
        public enum eListPricingParamType { GetCurrent, GetNext, GetHistory }

        //method
        public async Task GetPricingList(eListPricingParamType poParam)
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
                var loResult = await _model.GetList_PricelistAsync();
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
                    _pricelistId = _pricelist_List.FirstOrDefault().CPRICELIST_ID ?? "";
                    _validId = _pricelist_List.FirstOrDefault().CVALID_ID ?? "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task ActiveInactivePricingListAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _model.ActiveInactive_PricelistAsync(new ActiveInactivePricelistParam()
                {
                    CPROPERTY_ID = _propertyId,
                    CPRICELIST_ID = _pricelistId,
                    CVALID_ID = _validId,
                });
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
                var loResult = await _model.GetPropertyListAsync();
                if (loResult.Count > 0)
                {
                    _propertyList = new List<PropertyDTO>(loResult);
                    _propertyId = _propertyList[0].CPROPERTY_ID ?? "";
                    _propertyName = _propertyList[0].CPROPERTY_NAME ?? "";
                    _propertyCurr = _propertyList[0].CCURRENCY ?? "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GeneralFileByteDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            GeneralFileByteDTO loResult = null;

            try
            {
                loResult = await _model.DownloadFile_TemplateExcelUploadAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
