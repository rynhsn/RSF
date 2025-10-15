using Global_PMCOMMON.DTOs.Response.Property;
using Global_PMModel.ViewModel;
using PMM10000COMMON.SLA_Call_Type;
using PMM10000COMMON.SLA_Category;
using PMM10000COMMON.UtilityDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMM10000MODEL.DTO;
using PMM10000FrontResources;
using R_BlazorFrontEnd.Helpers;
using System.ComponentModel;

namespace PMM10000MODEL.ViewModel
{
    public class PMM10000CategoryViewModel : R_ViewModel<PMM10000CategoryDTO>
    {
        private GlobalFunctionViewModel _viewModelGlobal = new GlobalFunctionViewModel();
        private PMM10000ListModel _GetListModel = new PMM10000ListModel();
        private PMM10000CRUDCategoryModel _CRUDCategoryModel = new PMM10000CRUDCategoryModel();
        public List<PropertyDTO> _PropertyList = new List<PropertyDTO>();
        public List<PMM10000TreeDTO> _CategoryGrid { get; set; } = new List<PMM10000TreeDTO>();
        public PropertyDTO _PropertyValue = new PropertyDTO();
        public bool lPropertyExist = true;
        public bool _dropdownProperty = true;

        public PMM10000CategoryDTO _CategoryData = new PMM10000CategoryDTO();
        public BindingList<PMM10000CategoryDTO> _CategoryList
        { get; set; } = new BindingList<PMM10000CategoryDTO>();
        public async Task GetPropertyList()
        {
            R_Exception loEx = new R_Exception();
            List<PropertyDTO>? loReturn = new List<PropertyDTO>();
            try
            {
                List<PropertyDTO> loResult = await _viewModelGlobal.GetPropertyList();
                if (loResult.Any())
                {
                    _PropertyList = loResult;
                    _PropertyValue = _PropertyList[0];
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

        public async Task GetCategoryList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, _PropertyValue.CPROPERTY_ID);
                var loResult = await _GetListModel.GetCategoryListModel();

                var loGridData = loResult.Data.Select(x =>
                      new PMM10000TreeDTO
                      {
                          ParentId = x.CPARENT_CATEGORY_ID == "" ? null : x.CPARENT_CATEGORY_ID,
                          ParentName = x.CPARENT_NAME,
                          Id = x.CCATEGORY_ID,
                          CCATEGORY_ID = x.CCATEGORY_ID,
                          CPROPERTY_ID = x.CPROPERTY_ID,
                          Name = x.CCATEGORY_NAME,
                          CNOTES = x.CNOTES,
                          Description = string.Format("[{0}] {1} - {2}", x.ILEVEL, x.CCATEGORY_ID, x.CCATEGORY_NAME),
                          Level = x.ILEVEL

                      }).ToList();
                _CategoryGrid = loGridData.ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task GetCategoryComboBoxList()
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, _PropertyValue.CPROPERTY_ID);
                var loResult = await _GetListModel.GetCategoryListModel();
                _CategoryList = new BindingList<PMM10000CategoryDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #region CRUD
        public async Task GetEntity(PMM10000CategoryDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            //PMM10000CategoryDTO loResult = new PMM10000CategoryDTO();
            try
            {
                var loResult = await _CRUDCategoryModel.R_ServiceGetRecordAsync(poEntity);
                _CategoryData = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            //return loResult;
        }

        public async Task ServiceDelete(PMM10000CategoryDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                await _CRUDCategoryModel.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMM10000CategoryDTO poNewEntity, R_eConductorMode peCRUDMode)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _CRUDCategoryModel.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peCRUDMode);
                _CategoryData = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        #region Validation
        public void ValidationFieldEmpty(PMM10000CategoryDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(poEntity.CCATEGORY_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM10000_Class), "ValidationCategoryID");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(poEntity.CCATEGORY_NAME))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM10000_Class), "ValidationCategoryName");
                    loEx.Add(loErr);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
