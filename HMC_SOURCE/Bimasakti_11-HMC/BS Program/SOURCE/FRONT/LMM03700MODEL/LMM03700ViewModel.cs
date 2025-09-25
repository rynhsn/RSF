using LMM03700Common;
using LMM03700Common.DTO_s;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM03700Model
{
    public class LMM03700ViewModel : R_ViewModel<TenantClassificationGroupDTO>
    {
        private LMM03700Model _modelLMM03700 = new LMM03700Model();
        public ObservableCollection<TenantClassificationGroupDTO> _TenantClassificationGroupList { get; set; } = new ObservableCollection<TenantClassificationGroupDTO>();
        public TenantClassificationGroupDTO _TenantClassificationGroup { get; set; } = new TenantClassificationGroupDTO();
        public List<PropertyDTO> _PropertyList { get; set; } = new List<PropertyDTO>();
        public string _propertyId { get; set; } = "";

        public async Task GetTenantClassGroupList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(LMM03700ContextConstant.CPROPERTY_ID, _propertyId);
                var loResult = await _modelLMM03700.GetTenantClassRecord();
                _TenantClassificationGroupList = new ObservableCollection<TenantClassificationGroupDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetTenantClassGroupRecord(TenantClassificationGroupDTO loParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                loParam.CPROPERTY_ID = _propertyId;
                var loResult = await _modelLMM03700.R_ServiceGetRecordAsync(loParam);
                _TenantClassificationGroup = R_FrontUtility.ConvertObjectToObject<TenantClassificationGroupDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
       
        
        public async Task SaveTenantClassGroup(TenantClassificationGroupDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();
            try
            {
                poNewEntity.CPROPERTY_ID = _propertyId;
                var loResult = await _modelLMM03700.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                _TenantClassificationGroup = R_FrontUtility.ConvertObjectToObject<TenantClassificationGroupDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteTenantClassGroup(TenantClassificationGroupDTO loParam)
        {
            var loEx = new R_Exception();

            try
            {
                loParam.CPROPERTY_ID = _propertyId;
                await _modelLMM03700.R_ServiceDeleteAsync(loParam);
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
                var loResult = await _modelLMM03700.GetPropertyListAsync();
                if (loResult.Count<1)
                {
                    return;
                }
                _PropertyList = new List<PropertyDTO>(loResult);
                _propertyId = _PropertyList.FirstOrDefault().CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
