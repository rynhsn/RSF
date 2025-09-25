using Lookup_PMCOMMON.DTOs.LML01800;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Lookup_PMCOMMON.DTOs;
using System.Linq;
using Lookup_PMCOMMON.DTOs.UtilityDTO;
using Lookup_PMFrontResources;
using R_BlazorFrontEnd.Helpers;
using System.Data.Common;

namespace Lookup_PMModel.ViewModel.LML01800
{
    public class LookupLML01800ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        private PublicLookupLMGetRecordModel _modelGetRecord = new PublicLookupLMGetRecordModel();
        public ObservableCollection<LML01800DTO> GetList = new ObservableCollection<LML01800DTO>();
        public LML01800ParameterDTO Parameter = new LML01800ParameterDTO();

        public List<PropertyDTO> PropertyList = new List<PropertyDTO>();
        public PropertyDTO PropertyValue = new PropertyDTO();
        public List<BuildingDTO> BuildingList = new List<BuildingDTO>();
        public BuildingDTO BuildingValue = new BuildingDTO();
        public List<FloorDTO> FloorList = new List<FloorDTO>();
        public FloorDTO FloorValue = new FloorDTO();
        public List<UnitDTO> UnitList = new List<UnitDTO>();
        public UnitDTO UnitValue = new UnitDTO();

        public bool LenabledFilter_Property;
        public bool LenabledFilter_Building;
        public bool LenabledFilter_Floor;
        public bool LenabledFilter_Unit;

        public async Task GetPropertyList()
        {
            R_Exception loEx = new R_Exception();
            List<PropertyDTO>? loReturn = new List<PropertyDTO>();
            try
            {
                LenabledFilter_Property = true;
                var tempResult = await _model.PropertyListAsync();
                if (tempResult.Data.Count > 0)
                {
                    PropertyList = tempResult.Data;
                    if (!string.IsNullOrWhiteSpace(Parameter.CPROPERTY_ID))
                    {
                        PropertyValue = PropertyList.FirstOrDefault(item => item.CPROPERTY_ID == Parameter.CPROPERTY_ID)
                                          ?? new PropertyDTO();
                        PropertyValue.CPROPERTY_ID = Parameter.CPROPERTY_ID;
                        LenabledFilter_Property = false;
                    }
                    else
                    {
                        PropertyValue = PropertyList[0];
                    }
                }
                else
                {
                    PropertyList = new List<PropertyDTO>();
                    PropertyValue = new PropertyDTO();
                    BuildingList = new List<BuildingDTO>();
                    BuildingValue = new BuildingDTO();
                    FloorList = new List<FloorDTO>();
                    FloorValue = new FloorDTO();
                    UnitList = new List<UnitDTO>();
                    UnitValue = new UnitDTO();

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
            R_Exception loEx = new R_Exception();
            List<BuildingDTO>? loReturn = new List<BuildingDTO>();
            try
            {
                LenabledFilter_Building = true;
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, PropertyValue.CPROPERTY_ID);
                var tempResult = await _model.BuildingListAsync();
                if (tempResult.Data.Count > 0)
                {
                    BuildingList = tempResult.Data;
                    if (!string.IsNullOrWhiteSpace(Parameter.CBUILDING_ID))
                    {
                        BuildingValue = BuildingList.FirstOrDefault(item => item.CBUILDING_ID == Parameter.CBUILDING_ID)
                                          ?? new BuildingDTO();
                        BuildingValue.CBUILDING_ID = Parameter.CBUILDING_ID;
                        LenabledFilter_Building = false;
                    }
                    else
                    {
                        BuildingValue = BuildingList[0];
                    }
                }
                else
                {
                    BuildingList = new List<BuildingDTO>();
                    BuildingValue = new BuildingDTO();
                    FloorList = new List<FloorDTO>();
                    FloorValue = new FloorDTO();
                    UnitList = new List<UnitDTO>();
                    UnitValue = new UnitDTO();

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetFloorList()
        {
            R_Exception loEx = new R_Exception();
            List<FloorDTO>? loReturn = new List<FloorDTO>();
            try
            {
                LenabledFilter_Floor = true;
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, PropertyValue.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CBUILDING_ID, BuildingValue.CBUILDING_ID);
                var tempResult = await _model.FloorListAsync();
                if (tempResult.Data.Count > 0)
                {
                    FloorList = tempResult.Data;
                    if (!string.IsNullOrWhiteSpace(Parameter.CFLOOR_ID))
                    {
                        FloorValue = FloorList.FirstOrDefault(item => item.CFLOOR_ID == Parameter.CFLOOR_ID)
                                          ?? new FloorDTO();
                        FloorValue.CFLOOR_ID = Parameter.CFLOOR_ID;
                        LenabledFilter_Floor = false;
                    }
                    else
                    {
                        FloorValue = FloorList[0];
                    }

                }
                else
                {
                    FloorList = new List<FloorDTO>();
                    FloorValue = new FloorDTO();
                    UnitList = new List<UnitDTO>();
                    UnitValue = new UnitDTO();

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
            List<UnitDTO>? loReturn = new List<UnitDTO>();
            try
            {
                LenabledFilter_Unit = true;
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, PropertyValue.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CBUILDING_ID, BuildingValue.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CFLOOR_ID, FloorValue.CFLOOR_ID);

                var tempResult = await _model.UnitListAsync();
                if (tempResult.Data.Count > 0)
                {
                    UnitList = tempResult.Data;
                    if (!string.IsNullOrWhiteSpace(Parameter.CUNIT_ID))
                    {
                        UnitValue = UnitList.FirstOrDefault(item => item.CUNIT_ID == Parameter.CUNIT_ID)
                                          ?? new UnitDTO();
                        UnitValue.CUNIT_ID = Parameter.CUNIT_ID;
                        LenabledFilter_Unit = false;
                    }
                    else
                    {
                        UnitValue = UnitList[0];
                    }
                }
                else
                {
                    UnitList = new List<UnitDTO>();
                    UnitValue = new UnitDTO();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public void ValidationFieldEmpty()
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(PropertyValue.CPROPERTY_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class_LookupPM), "_ValidationProperty");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(BuildingValue.CBUILDING_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class_LookupPM), "_ValidationBuilding");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(FloorValue.CFLOOR_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class_LookupPM), "_ValidationFloor");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(UnitValue.CUNIT_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class_LookupPM), "_ValidationUnit");
                    loEx.Add(loErr);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.HasError)
            {
                loEx.ThrowExceptionIfErrors();
            }
        }
        public async Task GetUnitTenantList(LML01800ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID ?? "");
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CBUILDING_ID, poParam.CBUILDING_ID ?? "");
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CFLOOR_ID, poParam.CFLOOR_ID ?? "");
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CUNIT_ID, poParam.CUNIT_ID ?? "");
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTENANT_ID, "");
                var loResult = await _model.LML01800UnitTenantListAsync();

                GetList = new ObservableCollection<LML01800DTO>(loResult.Data);
                if (GetList.Count() < 1)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class_LookupPM), "_NotFound");
                    loEx.Add(loErr);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task<LML01800DTO> GetUnitTenant(LML01800ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            LML01800DTO loRtn = null;
            try
            {
                LML01800DTO loResult = await _modelGetRecord.LML01800UnitTenantAsync(poParam);
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn!;
        }
    }
}
