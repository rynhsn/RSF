using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using PMT06000Common;
using PMT06000Common.DTOs;
using PMT06000Common.Params;
using PMT06000FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace PMT06000Model.ViewModel
{
    public class PMT06000ServiceViewModel : R_ViewModel<PMT06000OvtServiceDTO>
    {
        private PMT06000ServiceModel _model = new PMT06000ServiceModel();
        public PMT06000OvtServiceDTO Entity = new PMT06000OvtServiceDTO();
        
        public async Task GetEntity(PMT06000OvtServiceDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                Entity = await _model.R_ServiceGetRecordAsync(poEntity);
                Entity.DDATE_IN = DateTime.ParseExact(Entity.CDATE_IN + " " + Entity.CTIME_IN, "yyyyMMdd HH:mm", CultureInfo.InvariantCulture);
                Entity.DDATE_OUT = DateTime.ParseExact(Entity.CDATE_OUT + " " + Entity.CTIME_OUT, "yyyyMMdd HH:mm", CultureInfo.InvariantCulture);
                // Entity.DDATE_IN = DateTime.ParseExact(Entity.CDATE_IN, "yyyyMMdd", CultureInfo.InvariantCulture);
                // Entity.DDATE_OUT = DateTime.ParseExact(Entity.CDATE_OUT, "yyyyMMdd", CultureInfo.InvariantCulture);
                // Entity.DTIME_IN = DateTime.ParseExact(Entity.CTIME_IN, "HH:mm", CultureInfo.InvariantCulture);
                // Entity.DTIME_OUT = DateTime.ParseExact(Entity.CTIME_OUT, "HH:mm", CultureInfo.InvariantCulture);
                
                //satukan date_in dan time_in ke DDATE_IN
                // Entity.DDATE_IN = Entity.DDATE_IN.Value.Add(Entity.DTIME_IN.Value.TimeOfDay);
                // Entity.DDATE_OUT = Entity.DDATE_OUT.Value.Add(Entity.DTIME_OUT.Value.TimeOfDay);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task SaveEntity(PMT06000OvtServiceDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                Entity = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                Entity.DDATE_IN = DateTime.ParseExact(Entity.CDATE_IN + " " + Entity.CTIME_IN, "yyyyMMdd HH:mm", CultureInfo.InvariantCulture);
                Entity.DDATE_OUT = DateTime.ParseExact(Entity.CDATE_OUT + " " + Entity.CTIME_OUT, "yyyyMMdd HH:mm", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task DeleteEntity(PMT06000OvtServiceDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CFLAG = "SVC";
                await _model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
    
    public class PMT06000UnitViewModel : R_ViewModel<PMT06000OvtUnitDTO>
    {
        private PMT06000UnitModel _model = new PMT06000UnitModel();
        public PMT06000OvtUnitDTO Entity = new PMT06000OvtUnitDTO();
        
        public async Task GetEntity(PMT06000OvtUnitDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Entity = await _model.R_ServiceGetRecordAsync(poEntity);
                Entity = poEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task SaveEntity(PMT06000OvtUnitDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                Entity = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task DeleteEntity(PMT06000OvtUnitDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CFLAG = "UNIT";
                await _model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }

    public class PMT06000ViewModel : R_ViewModel<PMT06000OvtDTO>
    {
        private PMT06000InitModel _initModel = new PMT06000InitModel();
        private PMT06000OvertimeModel _model = new PMT06000OvertimeModel();
        public ObservableCollection<PMT06000OvtGridDTO> OvertimeGridList = new ObservableCollection<PMT06000OvtGridDTO>();
        public ObservableCollection<PMT06000OvtServiceGridDTO> OvertimeServiceGridList = new ObservableCollection<PMT06000OvtServiceGridDTO>();
        public ObservableCollection<PMT06000OvtUnitDTO> OvertimeUnitGridList = new ObservableCollection<PMT06000OvtUnitDTO>();
        
        public PMT06000OvtDTO Entity = new PMT06000OvtDTO();
        public PMT06000OvtServiceDTO EntityService = new PMT06000OvtServiceDTO();
        public PMT06000ParameterDTO Caller = new PMT06000ParameterDTO();

        public List<PMT06000PropertyDTO> PropertyList = new List<PMT06000PropertyDTO>();
        public List<PMT06000PeriodDTO> PeriodList = new List<PMT06000PeriodDTO>();
        public PMT06000YearRangeDTO YearRange = new PMT06000YearRangeDTO();

        public string SelectedPropertyId = "";
        // period no = 2 digit bulan sekarang  
        public string SelectedPeriodNo = DateTime.Now.Month.ToString("D2");
        public int SelectedYear = DateTime.Now.Year;
        public string SelectedPeriodType = "A";
        
        public string TRANS_CODE = "802400";
        
        public List<KeyValuePair<string, string>> RadioPeriodType = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("A", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "AllPeriod")),
            new KeyValuePair<string, string>("S", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "SelectPeriod"))
        };

        public async Task GetEntity(PMT06000OvtDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                Entity = await _model.R_ServiceGetRecordAsync(poEntity);
                
                if(Entity != null)
                {
                    Entity.DREF_DATE = DateTime.ParseExact(Entity.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                    Entity.IINV_YEAR = int.Parse(Entity.CINV_YEAR);
                }
                else
                {
                    Entity = new PMT06000OvtDTO();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task SaveEntity(PMT06000OvtDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                Entity = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                Entity.DREF_DATE = DateTime.ParseExact(Entity.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                Entity.IINV_YEAR = int.Parse(Entity.CINV_YEAR);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task DeleteEntity(PMT06000OvtDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CFLAG = "OVT";
                await _model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _initModel.GetAsync<PMT06000ListDTO<PMT06000PropertyDTO>>(
                        nameof(IPMT06000Init.PMT06000GetPropertyList));
                PropertyList = loReturn.Data;
                SelectedPropertyId = PropertyList.Count>0? PropertyList[0].CPROPERTY_ID : SelectedPropertyId;
                // Data.CPROPERTY_ID = SelectedPropertyId;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodList()
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMT06000YearParam { CYEAR = SelectedYear.ToString() };

                var loReturn =
                    await _initModel.GetAsync<PMT06000ListDTO<PMT06000PeriodDTO>, PMT06000YearParam>(
                        nameof(IPMT06000Init.PMT06000GetPeriodList), loParam);
                PeriodList = loReturn.Data;
                // SelectedPeriodNo = PeriodList[0].CPERIOD_NO;
                // Data.CINV_PRD = SelectedPeriodNo;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetYearRange()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _initModel.GetAsync<PMT06000SingleDTO<PMT06000YearRangeDTO>>(
                        nameof(IPMT06000Init.PMT06000GetYearRange));
                YearRange = loReturn.Data;
                SelectedYear = DateTime.Now.Year;
                // Data.IINV_YEAR = SelectedYear;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetOvertimeGridList()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMT06000ContextConstant.CPROPERTY_ID, SelectedPropertyId);
                R_FrontContext.R_SetStreamingContext(PMT06000ContextConstant.CPERIOD, SelectedPeriodType=="S" ? SelectedYear + SelectedPeriodNo : "");
                R_FrontContext.R_SetStreamingContext(PMT06000ContextConstant.CTRANS_CODE, TRANS_CODE);
                // R_FrontContext.R_SetStreamingContext(PMT06000ContextConstant.CTRANS_STATUS, "");
                // R_FrontContext.R_SetStreamingContext(PMT06000ContextConstant.COVERTIME_STATUS, "");
                // R_FrontContext.R_SetStreamingContext(PMT06000ContextConstant.CAGREEMENT_NO, "");

                var loReturn =
                    await _model.GetListStreamAsync<PMT06000OvtGridDTO>(
                        nameof(IPMT06000Overtime.PMT06000GetOvertimeListStream));
                
                loReturn.ForEach(loItem =>
                {
                    loItem.DREF_DATE = DateTime.TryParseExact(loItem.CREF_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal, out var ldRefDate)
                        ? ldRefDate
                        : (DateTime?)null;

                    loItem.DINVOICE_DATE = DateTime.TryParseExact(loItem.CINVOICE_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal, out var ldInvDate)
                        ? ldInvDate
                        : (DateTime?)null;
                });
                
                OvertimeGridList = new ObservableCollection<PMT06000OvtGridDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetOvertimeServiceGridList()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMT06000ContextConstant.CPARENT_ID, Entity.CREC_ID);

                var loReturn =
                    await _model.GetListStreamAsync<PMT06000OvtServiceGridDTO>(
                        nameof(IPMT06000Overtime.PMT06000GetOvertimeServiceListStream));
                OvertimeServiceGridList = new ObservableCollection<PMT06000OvtServiceGridDTO>(loReturn);
                
                foreach (var loItem in OvertimeServiceGridList)
                {
                    loItem.DDATE_IN = DateTime.ParseExact(loItem.CDATE_IN + " " + loItem.CTIME_IN, "yyyyMMdd HH:mm", CultureInfo.InvariantCulture);
                    loItem.DDATE_OUT = DateTime.ParseExact(loItem.CDATE_OUT + " " + loItem.CTIME_OUT, "yyyyMMdd HH:mm", CultureInfo.InvariantCulture);
                    // loItem.DTIME_IN = DateTime.ParseExact(loItem.CTIME_IN, "HH:mm", CultureInfo.InvariantCulture);
                    // loItem.DTIME_OUT = DateTime.ParseExact(loItem.CTIME_OUT, "HH:mm", CultureInfo.InvariantCulture);
                    
                    //satukan date_in dan time_in ke DDATE_IN
                    // loItem.DDATE_IN = loItem.DDATE_IN.Value.Add(loItem.DTIME_IN.Value.TimeOfDay);
                    // loItem.DDATE_OUT = loItem.DDATE_OUT.Value.Add(loItem.DTIME_OUT.Value.TimeOfDay);
                    
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetOvertimeUnitGridList()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMT06000ContextConstant.CPARENT_ID, EntityService.CREC_ID);

                var loReturn =
                    await _model.GetListStreamAsync<PMT06000OvtUnitDTO>(
                        nameof(IPMT06000Overtime.PMT06000GetOvertimeUnitListStream));
                OvertimeUnitGridList = new ObservableCollection<PMT06000OvtUnitDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void OnChangedComboOnList(string value, string formName)
        {
            switch (formName)
            {
                case "property":
                    SelectedPropertyId = value;
                    break;
                case "periodNo":
                    SelectedPeriodNo = value;
                    break;
            }
        }

        public async Task ProcessSubmit()
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMT06000ProcessSubmitParam();
                loParam.CPROPERTY_ID = Data.CPROPERTY_ID;
                loParam.CREC_ID = Data.CREC_ID;
                loParam.CNEW_STATUS = Data.CTRANS_STATUS == "00" ? "10" : "00";
                
                var loReturn = await _model.GetAsync<PMT06000SingleDTO<PMT06000OvtUnitDTO>, PMT06000ProcessSubmitParam>(nameof(IPMT06000Overtime.PMT06000ProcessSubmit), loParam);
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }

}