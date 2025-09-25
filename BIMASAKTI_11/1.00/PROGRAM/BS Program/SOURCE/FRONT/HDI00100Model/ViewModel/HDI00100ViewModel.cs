using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using HDI00100Common;
using HDI00100Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace HDI00100Model.ViewModel
{
    public class HDI00100ViewModel : R_ViewModel<HDI00100TaskSchedulerDTO>
    {
        private HDI00100Model _model = new HDI00100Model();

        public ObservableCollection<HDI00100TaskSchedulerDTO> GridList =
            new ObservableCollection<HDI00100TaskSchedulerDTO>();

        public List<HDI00100PropertyDTO> PropertyList = new List<HDI00100PropertyDTO>();

        public PageParam Param = new PageParam();

        public async Task Init(TabStatus poTabStatus, string pcPropertyId = "")
        {
            var loEx = new R_Exception();

            try
            {
                if (poTabStatus == TabStatus.Active)
                {
                    await GetProperty();
                    Param.IYEAR = DateTime.Now.Year;
                    Param.CSTATUS = "01";
                }
                else if (poTabStatus == TabStatus.Next)
                {
                    Param.CPROPERTY_ID = pcPropertyId;
                    Param.CSTATUS = "02";
                }
                else if (poTabStatus == TabStatus.History)
                {
                    Param.CPROPERTY_ID = pcPropertyId;
                    Param.CSTATUS = "03";
                    Param.DFROM_DATE = DateTime.Now;
                    Param.DTO_DATE = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetProperty()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn =
                    await _model.GetAsync<HDI00100ListDTO<HDI00100PropertyDTO>>(
                        nameof(IHDI00100.HDI00100GetPropertyList));
                PropertyList = loReturn.Data;
                Param.CPROPERTY_ID = PropertyList[0].CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetGridList()
        {
            var loEx = new R_Exception();

            try
            {
                if (Param.CSTATUS == "01")
                {
                    Param.CYEAR = Param.IYEAR.ToString();
                }
                else if (Param.CSTATUS == "03")
                {
                    Param.CFROM_DATE = Param.DFROM_DATE.ToString("yyyyMMdd");
                    Param.CTO_DATE = Param.DTO_DATE.ToString("yyyyMMdd");
                }

                R_FrontContext.R_SetStreamingContext(HDI00100ContextConstant.CPROPERTY_ID, Param.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(HDI00100ContextConstant.CYEAR, Param.CYEAR);
                R_FrontContext.R_SetStreamingContext(HDI00100ContextConstant.CFROM_DATE, Param.CFROM_DATE);
                R_FrontContext.R_SetStreamingContext(HDI00100ContextConstant.CTO_DATE, Param.CTO_DATE);
                R_FrontContext.R_SetStreamingContext(HDI00100ContextConstant.CBUILDING_ID, Param.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(HDI00100ContextConstant.CASSET_CODE, Param.CASSET_CODE);
                R_FrontContext.R_SetStreamingContext(HDI00100ContextConstant.CSTATUS, Param.CSTATUS);

                var loReturn = await _model.GetListStreamAsync<HDI00100TaskSchedulerDTO>(
                    nameof(IHDI00100.HDI00100GetTaskSchedulerListStream));


                GridList = new ObservableCollection<HDI00100TaskSchedulerDTO>(loReturn);
                foreach (var item in GridList)
                {
                    item.DSCHEDULE_DATE = DateTime.TryParseExact(item.CSCHEDULE_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var scheduleDate)
                        ? scheduleDate
                        : (DateTime?)null;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }

    public class PageParam
    {
        public string CPROPERTY_ID { get; set; } = "";
        public string CYEAR { get; set; } = "";
        public int IYEAR { get; set; }
        public string CFROM_DATE { get; set; } = "";
        public string CTO_DATE { get; set; } = "";
        public DateTime DFROM_DATE { get; set; }
        public DateTime DTO_DATE { get; set; }
        public bool LBUILDING { get; set; } = false;
        public bool LASSET { get; set; } = false;
        public string CBUILDING_ID { get; set; } = "";
        public string CBUILDING_NAME { get; set; } = "";
        public string CASSET_CODE { get; set; } = "";
        public string CASSET_NAME { get; set; } = "";
        public string CSTATUS { get; set; } = "";
    }

    public enum TabStatus
    {
        Active,
        Next,
        History
    }
}