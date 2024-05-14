using GSM02500COMMON.DTOs.GSM02502Charge;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs;

namespace GSM02500MODEL.View_Model
{
    public class GSM02502ChargeViewModel : R_ViewModel<GSM02502ChargeDTO>
    {
        private GSM02502ChargeModel loModel = new GSM02502ChargeModel();

        public GSM02502ChargeDTO loCharge = null;

        public ObservableCollection<GSM02502ChargeDTO> loChargeList = new ObservableCollection<GSM02502ChargeDTO>();

        public GSM02502ChargeResultDTO loRtn = null;

        public List<GSM02502ChargeComboboxDTO> loChargeTypeList = null;

        public GSM02502ChargeComboboxResultDTO loChargeTypeRtn = null;

        //public string SelectedProperty;

        //public string SelectedUnitTypeCategory;

        public ChargeTabParameterDTO loTabParameter = new ChargeTabParameterDTO();

        public async Task GetChargeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02502_CHARGE_PROPERTY_ID_STREAMING_CONTEXT, loTabParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02502_CHARGE_UNIT_TYPE_CATEGORY_ID_STREAMING_CONTEXT, loTabParameter.CUNIT_TYPE_CATEGORY_ID);
                loRtn = await loModel.GetChargeListStreamAsync();
                loChargeList = new ObservableCollection<GSM02502ChargeDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetChargeComboBoxListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02502_CHARGE_COMBOBOX_CLASS_ID_STREAMING_CONTEXT, "_BS_UTILITY_CHARGES_TYPE");

                loChargeTypeRtn = await loModel.GetChargeComboBoxListStreamAsync();
                loChargeTypeList = loChargeTypeRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetChargeAsync(GSM02502ChargeDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            GSM02502ChargeParameterDTO loParam = null;
            GSM02502ChargeParameterDTO loResult = null;
            try
            {
                loParam = new GSM02502ChargeParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loTabParameter.CPROPERTY_ID,
                    CSELECTED_UNIT_TYPE_CATEGORY_ID = loTabParameter.CUNIT_TYPE_CATEGORY_ID
                };

                //R_FrontContext.R_SetContext(ContextConstant.GSM02502_CHARGE_PROPERTY_ID_CONTEXT, SelectedProperty);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02502_CHARGE_UNIT_TYPE_CATEGORY_ID_CONTEXT, SelectedUnitTypeCategory);
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loCharge = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveChargeAsync(GSM02502ChargeDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            GSM02502ChargeParameterDTO loParam = null;
            GSM02502ChargeParameterDTO loResult = null;
            try
            {
                loParam = new GSM02502ChargeParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loTabParameter.CPROPERTY_ID,
                    CSELECTED_UNIT_TYPE_CATEGORY_ID = loTabParameter.CUNIT_TYPE_CATEGORY_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02502_CHARGE_PROPERTY_ID_CONTEXT, SelectedProperty);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02502_CHARGE_UNIT_TYPE_CATEGORY_ID_CONTEXT, SelectedUnitTypeCategory);
                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loCharge = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteChargeAsync(GSM02502ChargeDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM02502ChargeParameterDTO loParam = null;
            try
            {
                loParam = new GSM02502ChargeParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loTabParameter.CPROPERTY_ID,
                    CSELECTED_UNIT_TYPE_CATEGORY_ID = loTabParameter.CUNIT_TYPE_CATEGORY_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02502_CHARGE_PROPERTY_ID_CONTEXT, SelectedProperty);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02502_CHARGE_UNIT_TYPE_CATEGORY_ID_CONTEXT, SelectedUnitTypeCategory);
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
