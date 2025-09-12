using PMB01600Common;
using PMB01600Common.DTOs;
using PMB01600Common.Params;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB01600Model.ViewModel
{
    public class PMB01600ViewModel : R_ViewModel<PMB01600BillingStatementHeaderDTO>
    {
        private PMB01600Model _model = new PMB01600Model();
        public ObservableCollection<PMB01600BillingStatementHeaderDTO> GridHeaderList = new ObservableCollection<PMB01600BillingStatementHeaderDTO>();
        public ObservableCollection<PMB01600BillingStatementDetailDTO> GridDetailList = new ObservableCollection<PMB01600BillingStatementDetailDTO>();
        public PMB01600BillingStatementHeaderDTO EntityHeader = new PMB01600BillingStatementHeaderDTO();

        public List<PMB01600PropertyDTO> PropertyList = new List<PMB01600PropertyDTO>();
        public PMB01600SystemParamDTO SystemParam = new PMB01600SystemParamDTO();
        public PMB01600YearRangeDTO YearRange = new PMB01600YearRangeDTO();
        public List<PMB01600PeriodDTO> PeriodList = new List<PMB01600PeriodDTO>();

        public PMB01600Parameter Param = new PMB01600Parameter();

        public string TenantType = "01";

        public async Task Init()
        {
            await GetPropertyList();
            await GetSystemParam();
            await GetYearRange();
            await GetPeriodList();
        }

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.PMB01600GetPropertyList();
                PropertyList = loResult.Data;
                if (PropertyList.Count > 0)
                {
                    Param.CPROPERTY_ID = PropertyList.FirstOrDefault().CPROPERTY_ID;
                    Param.CPROPERTY_NAME = PropertyList.Where(x => x.CPROPERTY_ID == Param.CPROPERTY_ID).FirstOrDefault().CPROPERTY_NAME;
                }
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
                var loResult = await _model.PMB01600GetYearRange();
                YearRange = loResult.Data;
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
                var loParam = new PMB01600PeriodParam
                {
                    CYEAR = DateTime.Now.Year.ToString(),
                };

                var loResult = await _model.PMB01600GetPeriodList(loParam);
                PeriodList = loResult.Data;
                if (PeriodList.Count > 0)
                {
                    Param.CMONTH = PeriodList.Where(x => x.CPERIOD_NO == DateTime.Now.Month.ToString("D2")).FirstOrDefault().CPERIOD_NO;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetSystemParam()
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new PMB01600SystemParamParam
                {
                    CPROPERTY_ID = Param.CPROPERTY_ID,
                };

                var loResult = await _model.PMB01600GetSystemParam(loParam);
                SystemParam = loResult.Data;

                if(SystemParam != null)
                {
                    Param.IYEAR = int.Parse(SystemParam.CSOFT_PERIOD_YY);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetBillingStatementHeaderList()
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(PMB01600ContextConstantHeader.CPROPERTY_ID, Param.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMB01600ContextConstantHeader.CFROM_TENANT_ID, Param.CFROM_TENANT_ID);
                R_FrontContext.R_SetStreamingContext(PMB01600ContextConstantHeader.CTO_TENANT_ID, Param.CTO_TENANT_ID);
                R_FrontContext.R_SetStreamingContext(PMB01600ContextConstantHeader.CREF_PRD, Param.CREF_PRD);

                var loReturn = await _model.GetListStreamAsync<PMB01600BillingStatementHeaderDTO>(nameof(IPMB01600.PMB01600GetBillingStatementHeaderListStream));
                GridHeaderList = new ObservableCollection<PMB01600BillingStatementHeaderDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetBillingStatementDetailList()
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(PMB01600ContextConstantDetail.CPROPERTY_ID, EntityHeader.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMB01600ContextConstantDetail.CTENANT_ID, EntityHeader.CTENANT_ID);
                R_FrontContext.R_SetStreamingContext(PMB01600ContextConstantDetail.CREF_PRD, EntityHeader.CPERIOD);

                var loReturn = await _model.GetListStreamAsync<PMB01600BillingStatementDetailDTO>(nameof(IPMB01600.PMB01600GetBillingStatementDetailListStream));
                GridDetailList = new ObservableCollection<PMB01600BillingStatementDetailDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


    }

    public class PMB01600Parameter
    {
        public string CPROPERTY_ID { get; set; } = "";
        public string CPROPERTY_NAME { get; set; } = "";
        public string CFROM_TENANT_ID { get; set; } = "";
        public string CTO_TENANT_ID { get; set; } = "";
        public string CFROM_TENANT_NAME { get; set; } = "";
        public string CTO_TENANT_NAME { get; set; } = "";
        public string CREF_PRD { get; set; } = "";
        public int IYEAR { get; set; }
        public string CMONTH { get; set; } = "";
    }
}
