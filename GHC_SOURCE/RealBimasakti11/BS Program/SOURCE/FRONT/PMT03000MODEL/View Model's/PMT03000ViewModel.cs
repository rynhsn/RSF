using PMT03000COMMON;
using PMT03000COMMON.DTO_s;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace PMT03000MODEL.View_Model_s
{
    public class PMT03000ViewModel
    {
        private PMT03000Model _model = new PMT03000Model();
        public List<PropertyDTO> Properties { get; set; } = new List<PropertyDTO>();
        public ObservableCollection<BuildingDTO> Buildings { get; set; } = new ObservableCollection<BuildingDTO>();
        public ObservableCollection<BuildingUnitDTO> BuildingUnits { get; set; } = new ObservableCollection<BuildingUnitDTO>();
        public ObservableCollection<TransByUnitDTO> TransByUnits { get; set; } = new ObservableCollection<TransByUnitDTO>();
        public string PropertyId { get; set; } = "";
        public string PropertyName { get; set; } = "";
        public string PrpertyCurrency { get; set; } = "";
        public string BuildingId { get; set; } = "";
        public BuildingDTO Building { get; set; } = new BuildingDTO();
        public BuildingUnitDTO BuildingUnit { get; set; } = new BuildingUnitDTO();
        public TransByUnitDTO TransByUnit { get; set; } = new TransByUnitDTO();

        //method
        public async Task GetList_PropertyAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetList_PropertyAsync();
                Properties = new List<PropertyDTO>(loResult);
                PropertyId = Properties.FirstOrDefault().CPROPERTY_ID ?? "";
                PropertyName = Properties.FirstOrDefault(x => x.CPROPERTY_ID == PropertyId).CPROPERTY_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetList_BuildingAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMT03000ContextConstant.CPROPERTY_ID, PropertyId);
                var loResult = await _model.GetList_BuildingAsync();
                foreach (var item in loResult)
                {
                    item.CBUILDING_DISPLAY = $"{item.CBUILDING_NAME} ({item.CBUILDING_ID})";
                }
                Buildings = new ObservableCollection<BuildingDTO>(loResult) ?? new ObservableCollection<BuildingDTO>();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetList_BuildingUnitAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMT03000ContextConstant.CPROPERTY_ID, Building.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT03000ContextConstant.CBUILDING_ID, Building.CBUILDING_ID);
                var loResult = await _model.GetList_BuildingUnitAsync();
                BuildingUnits = new ObservableCollection<BuildingUnitDTO>(loResult) ?? new ObservableCollection<BuildingUnitDTO>();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetList_TransUnitAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMT03000ContextConstant.CPROPERTY_ID, BuildingUnit.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT03000ContextConstant.CBUILDING_ID, BuildingUnit.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(PMT03000ContextConstant.CFLOOR_ID, BuildingUnit.CFLOOR_ID);
                R_FrontContext.R_SetStreamingContext(PMT03000ContextConstant.CUNIT_ID, BuildingUnit.CUNIT_ID);
                R_FrontContext.R_SetStreamingContext(PMT03000ContextConstant.CUNIT_TYPE_CATEGORY_ID, BuildingUnit.CUNIT_TYPE_CATEGORY_ID);

                var loResult = await _model.GetList_TransByUnitAsync();
                TransByUnits = new ObservableCollection<TransByUnitDTO>(loResult) ?? new ObservableCollection<TransByUnitDTO>();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
