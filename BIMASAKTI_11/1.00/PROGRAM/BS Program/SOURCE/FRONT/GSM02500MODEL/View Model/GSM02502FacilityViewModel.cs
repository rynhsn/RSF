using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs.GSM02502Charge;
using GSM02500COMMON.DTOs.GSM02502Facility;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500MODEL.View_Model
{
    public class GSM02502FacilityViewModel : R_ViewModel<GSM02502FacilityDTO>
    {
        private GSM02502FacilityModel loModel = new GSM02502FacilityModel();

        public GSM02502FacilityDTO loFacility = null;

        public ObservableCollection<GSM02502FacilityDTO> loFacilityList = new ObservableCollection<GSM02502FacilityDTO>();

        public List<GSM02502FacilityTypeDTO> loFacilityTypeList = new List<GSM02502FacilityTypeDTO>();

        public GSM02502FacilityResultDTO loRtn = null;

        public string SelectedProperty;

        public string SelectedUnitTypeCategory;

        public async Task GetFacilityListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02502_FACILITY_PROPERTY_ID_STREAMING_CONTEXT, SelectedProperty);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02502_FACILITY_UNIT_TYPE_CATEGORY_ID_STREAMING_CONTEXT, SelectedUnitTypeCategory);
                loRtn = await loModel.GetFacilityListStreamAsync();
                loFacilityList = new ObservableCollection<GSM02502FacilityDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetFacilityTypeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            GSM02502FacilityTypeResultDTO loResult = null;
            try
            {
                loResult = await loModel.GetFacilityTypeListStreamAsync();
                loFacilityTypeList = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetFacilityAsync(GSM02502FacilityDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            GSM02502FacilityParameterDTO loParam = null;
            GSM02502FacilityParameterDTO loResult = null;
            try
            {
                loParam = new GSM02502FacilityParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = SelectedProperty,
                    CUNIT_TYPE_CATEGORY_ID = SelectedUnitTypeCategory
                };
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loFacility = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveFacilityAsync(GSM02502FacilityDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            GSM02502FacilityParameterDTO loParam = null;
            GSM02502FacilityParameterDTO loResult = null;
            try
            {
                loParam = new GSM02502FacilityParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = SelectedProperty,
                    CUNIT_TYPE_CATEGORY_ID = SelectedUnitTypeCategory
                };
                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loFacility = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteFacilityAsync(GSM02502FacilityDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM02502FacilityParameterDTO loParam = null;

            try
            {
                loParam = new GSM02502FacilityParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = SelectedProperty,
                    CUNIT_TYPE_CATEGORY_ID = SelectedUnitTypeCategory
                };
                await loModel.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
