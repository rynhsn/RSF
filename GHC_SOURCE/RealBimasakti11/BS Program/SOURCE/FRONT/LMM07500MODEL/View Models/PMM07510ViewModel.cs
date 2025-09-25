using LMM07500MODEL;
using PMM07500COMMON;
using PMM07500COMMON.DTO_s.stamp_date;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace PMM07500MODEL.View_Models
{
    public class PMM07510ViewModel : R_ViewModel<PMM07510GridDTO>
    {
        private PMM07510Model _model = new PMM07510Model();
        private PMM07500InitModel _initModel = new PMM07500InitModel();
        public ObservableCollection<PMM07510GridDTO> StampDateList { get; set; } = new ObservableCollection<PMM07510GridDTO>();

        public PMM07510GridDTO StampDate { get; set; } = new PMM07510GridDTO();
        public List<PropertyDTO> PropertyList { get; set; } = new List<PropertyDTO>();
        public string PropertyId { get; set; } = "";
        public string ParentId { get; set; } = "";
        public string StampCode { get; set; } = "";
        public DateTime? EffectiveDateDisplay { get; set; }

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

        public async Task GetStampDateListAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMM07500ContextConstant.CPROPERTY_ID, PropertyId);
                R_FrontContext.R_SetStreamingContext(PMM07500ContextConstant.CPARENT_ID, ParentId);
                var loResult = await _model.GetStampDateListAsync();
                foreach (var item in loResult)
                {
                    if (DateTime.TryParseExact(item.CDATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var loDate))
                    {
                        item.DDATE = loDate;
                    }
                }

                StampDateList = new ObservableCollection<PMM07510GridDTO>(loResult)??new ObservableCollection<PMM07510GridDTO>();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetStampDateRecordAsync(PMM07510GridDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CPROPERTY_ID = PropertyId;
                var loResult = await _model.R_ServiceGetRecordAsync(poParam);
                if (DateTime.TryParseExact(loResult.CDATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var loDate))
                {
                    loResult.DDATE = loDate;
                }
                StampDate = R_FrontUtility.ConvertObjectToObject<PMM07510GridDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveStampDateAsync(PMM07510GridDTO poParam, eCRUDMode poCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CPROPERTY_ID = PropertyId;
                poParam.CPARENT_ID = ParentId;
                poParam.CSTAMP_CODE = StampCode;
                poParam.CREC_ID = "";
                poParam.CDATE = poParam.DDATE.ToString("yyyyMMdd");
                poParam.CACTION = "NEW";
                var loResult = await _model.R_ServiceSaveAsync(poParam, poCRUDMode);
                if (DateTime.TryParseExact(loResult.CDATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var loDate))
                {
                    loResult.DDATE = loDate;
                }
                StampDate = R_FrontUtility.ConvertObjectToObject<PMM07510GridDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteStampDateAsync(PMM07510GridDTO poParam)
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
