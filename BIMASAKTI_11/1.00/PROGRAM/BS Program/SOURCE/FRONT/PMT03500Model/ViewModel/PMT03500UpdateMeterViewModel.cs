using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using PMT03500Common;
using PMT03500Common.DTOs;
using PMT03500Common.Params;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;  
using R_BlazorFrontEnd.Helpers;

namespace PMT03500Model.ViewModel
{
    public class PMT03500UpdateMeterViewModel : R_ViewModel<PMT03500UtilityMeterDetailDTO>
    {
        private PMT03500UpdateMeterModel _model = new PMT03500UpdateMeterModel();
        private PMT03500UtilityUsageModel _modelUtility = new PMT03500UtilityUsageModel();
        
        public PMT03500UpdateMeterHeader Header = new PMT03500UpdateMeterHeader();
        
        public ObservableCollection<PMT03500UtilityMeterDTO> GridList = new ObservableCollection<PMT03500UtilityMeterDTO>();
        public PMT03500UtilityMeterDetailDTO Entity = new PMT03500UtilityMeterDetailDTO();
        
        
        public List<PMT03500YearDTO> StartInvYearList = new List<PMT03500YearDTO>();
        public List<PMT03500PeriodDTO> StartInvMonthList = new List<PMT03500PeriodDTO>();
        public List<PMT03500FunctDTO> UtilityTypeList = new List<PMT03500FunctDTO>();
        
        public string CSTART_INV_PRD_YEAR { get; set; }
        public string CSTART_INV_PRD_MONTH { get; set; }

        public async Task Init(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                Header.CPROPERTY_ID = poParam.ToString();
                await GetPeriodList();
                await GetYearList();
                await GetUtilityTypeList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetList(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                // var loParam = (PMT03500UpdateMeterHeader)poParam;
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CPROPERTY_ID,Header.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CBUILDING_ID,Header.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CTENANT_ID, "");
                var loReturn = await _model.GetListStreamAsync<PMT03500UtilityMeterDTO>(nameof(IPMT03500UpdateMeter
                    .LMT03500GetUtilityMeterListStream));
                GridList = new ObservableCollection<PMT03500UtilityMeterDTO>(loReturn);
                
                //dummy data grid list, 10 data
                // for (int i = 0; i < 10; i++)
                // {
                //     GridList.Add(new PMT03500UtilityMeterDTO
                //     {
                //         CUTILITY_TYPE = "01",
                //         CUTILITY_TYPE_NAME = "Electricity",
                //         CCHARGES_ID = "C000"+i,
                //         CCHARGES_NAME = "Electricity",
                //         CMETER_NO = "M000"+i,
                //         CPROPERTY_ID = "P000"+i,
                //         CBUILDING_ID = "B000"+i,
                //         CFLOOR_ID = "F000"+i,
                //         CUNIT_ID = "U000"+i,
                //         CREF_NO = "R000"+i,
                //         CSTATUS = "A",
                //         CUPDATE_BY = "U000"+i,
                //         DUPDATE_DATE = DateTime.Now,
                //         CCREATE_BY = "C000"+i,
                //         DCREATE_DATE = DateTime.Now
                //     });
                // }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetRecord(PMT03500UtilityMeterDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMT03500UtilityMeterDetailParam()
                {
                    CPROPERTY_ID = poEntity.CPROPERTY_ID,
                    CBUILDING_ID = poEntity.CBUILDING_ID,
                    CUNIT_ID = poEntity.CUNIT_ID,
                    CCHARGES_TYPE = poEntity.CUTILITY_TYPE,
                    CCHARGES_ID = poEntity.CCHARGES_ID
                };
                
                var loReturn = await _model.GetAsync<PMT03500SingleDTO<PMT03500UtilityMeterDetailDTO>, PMT03500UtilityMeterDetailParam>(nameof(IPMT03500UpdateMeter.LMT03500GetUtilityMeterDetail), loParam);
                Entity = loReturn.Data;
                
                if (Entity.CSTART_INV_PRD != null)
                {
                    CSTART_INV_PRD_YEAR = Entity.CSTART_INV_PRD.Substring(0,4);
                    CSTART_INV_PRD_MONTH = Entity.CSTART_INV_PRD.Substring(4,2);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAgreementTenant(PMT03500UpdateMeterHeader poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMT03500AgreementUtilitiesParam
                {
                    CPROPERTY_ID = poParam.CPROPERTY_ID,
                    CBUILDING_ID = poParam.CBUILDING_ID,
                    CUNIT_ID = poParam.CUNIT_ID
                };
                
                var loReturn = await _model.GetAsync<PMT03500SingleDTO<PMT03500AgreementUtilitiesDTO>, PMT03500AgreementUtilitiesParam>(nameof(IPMT03500UpdateMeter.LMT03500GetAgreementUtilities), loParam);

                if(loReturn.Data != null)
                {
                    Header.CREF_NO = loReturn.Data.CREF_NO;
                    Header.CTENANT_ID = loReturn.Data.CTENANT_ID;
                    Header.CTENANT_NAME = loReturn.Data.CTENANT_NAME;
                }   
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task UpdateMeterNo(PMT03500UtilityMeterDetailDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMT03500UpdateChangeMeterNoParam
                {
                    CPROPERTY_ID = poParam.CPROPERTY_ID,
                    CREF_NO = poParam.CREF_NO,
                    CUNIT_ID = poParam.CUNIT_ID,
                    CTENANT_ID = poParam.CTENANT_ID,
                    CUTILITY_TYPE = poParam.CUTILITY_TYPE,
                    CMETER_NO = poParam.CMETER_NO,
                    IMETER_START = Int32.Parse(poParam.CMETER_START),
                    CSTART_INV_PRD = poParam.CSTART_INV_PRD,
                    CSTART_DATE = poParam.CSTART_DATE
                    
                };
                
                await _model.ExecuteAsync<PMT03500UtilityMeterDetailDTO, PMT03500UpdateChangeMeterNoParam>(nameof(IPMT03500UpdateMeter.LMT03500UpdateMeterNo), loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            
            }
            
            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task ChangeMeterNo(PMT03500UtilityMeterDetailDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMT03500UpdateChangeMeterNoParam
                {
                    CPROPERTY_ID = poParam.CPROPERTY_ID,
                    CREF_NO = poParam.CREF_NO,
                    CUNIT_ID = poParam.CUNIT_ID,
                    CTENANT_ID = poParam.CTENANT_ID,
                    CUTILITY_TYPE = poParam.CUTILITY_TYPE,
                    IFROM_METER_NO = Int32.Parse(poParam.CFROM_METER_NO),
                    IMETER_END = Int32.Parse(poParam.CMETER_END),
                    ITO_METER_NO = Int32.Parse(poParam.CTO_METER_NO),
                    IMETER_START = Int32.Parse(poParam.CMETER_START),
                    CSTART_INV_PRD = poParam.CSTART_INV_PRD,
                    CSTART_DATE = poParam.CSTART_DATE
                    
                };
                
                await _model.ExecuteAsync<PMT03500UtilityMeterDetailDTO, PMT03500UpdateChangeMeterNoParam>(nameof(IPMT03500UpdateMeter.LMT03500ChangeMeterNo), loParam);
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
                var lcYear = DateTime.Now;
                var loParam = new PMT03500PeriodParam { CYEAR = lcYear.Year.ToString() };

                var loReturn =
                    await _modelUtility.GetAsync<PMT03500ListDTO<PMT03500PeriodDTO>, PMT03500PeriodParam>(
                        nameof(IPMT03500UtilityUsage.LMT03500GetPeriodList), loParam);
                StartInvMonthList = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetYearList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn = await _modelUtility.GetAsync<PMT03500ListDTO<PMT03500YearDTO>>(nameof(IPMT03500UtilityUsage.LMT03500GetYearList));
                StartInvYearList = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetUtilityTypeList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn = await _modelUtility.GetAsync<PMT03500ListDTO<PMT03500FunctDTO>>(nameof(IPMT03500UtilityUsage.LMT03500GetUtilityTypeList));
                UtilityTypeList = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
    }
}