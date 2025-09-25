using LMM07500MODEL;
using PMM07500COMMON;
using PMM07500COMMON.DTO_s.stamp_amount;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMM07500MODEL.View_Models
{
    public class PMM07520ViewModel : R_ViewModel<PMM07520GridDTO>
    {
        private PMM07520Model _model = new PMM07520Model();
        private PMM07500InitModel _initModel = new PMM07500InitModel();
        public ObservableCollection<PMM07520GridDTO> StampAmountList { get; set; } = new ObservableCollection<PMM07520GridDTO>();

        public PMM07520GridDTO StampAmount { get; set; } = new PMM07520GridDTO();
        public List<PropertyDTO> PropertyList { get; set; } = new List<PropertyDTO>();

        public string PropertyId { get; set; } = "";
        public string StampCode { get; set; } = "";
        public string ParentId { get; set; } = "";
        public string EffectiveDate { get; set; } = "";
        public string GrandParentId { get; set; } = "";

        public async Task GetPropertyList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _initModel.GetPropertyListAsync();
                if (loResult.Count > 0)
                {
                    PropertyList = new List<PropertyDTO>(loResult);
                    PropertyId = PropertyList.FirstOrDefault().CPROPERTY_ID;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetStampAmountListAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMM07500ContextConstant.CPROPERTY_ID, PropertyId);
                R_FrontContext.R_SetStreamingContext(PMM07500ContextConstant.CPARENT_ID, ParentId);
                R_FrontContext.R_SetStreamingContext(PMM07500ContextConstant.CGRAND_PARENT_ID, GrandParentId);

                var loResult = await _model.GetStampAmountAsync();
                StampAmountList = new ObservableCollection<PMM07520GridDTO>(loResult)??new ObservableCollection<PMM07520GridDTO>();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetStampAmountRecordAsync(PMM07520GridDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CPROPERTY_ID = PropertyId;
                var loResult = await _model.R_ServiceGetRecordAsync(poParam);
                StampAmount = R_FrontUtility.ConvertObjectToObject<PMM07520GridDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveStampAmountAsync(PMM07520GridDTO poParam, eCRUDMode poCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CPROPERTY_ID = PropertyId;
                poParam.CSTAMP_CODE = StampCode;
                poParam.CGRAND_PARENT_ID = GrandParentId;
                poParam.CPARENT_ID = ParentId;
                poParam.CDATE = EffectiveDate;
                switch (poCRUDMode)
                {
                    case eCRUDMode.NormalMode:
                        break;
                    case eCRUDMode.AddMode:
                        poParam.CACTION = "NEW";
                        poParam.CREC_ID = "";
                        break;
                    case eCRUDMode.EditMode:
                        poParam.CACTION = "EDIT";
                        break;
                    case eCRUDMode.DeleteMode:
                        break;
                    default:
                        break;
                }
                var loResult = await _model.R_ServiceSaveAsync(poParam, poCRUDMode);
                StampAmount = R_FrontUtility.ConvertObjectToObject<PMM07520GridDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteStampAmountAsync(PMM07520GridDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                poParam.CPROPERTY_ID = PropertyId;
                await _model.R_ServiceDeleteAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
