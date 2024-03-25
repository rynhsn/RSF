using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LMT03500Common;
using LMT03500Common.DTOs;
using LMT03500Common.Params;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;  
using R_BlazorFrontEnd.Helpers;

namespace LMT03500Model.ViewModel
{
    public class LMT03500UpdateMeterViewModel : R_ViewModel<LMT03500UtilityMeterDetailDTO>
    {
        private LMT03500UpdateMeterModel _model = new LMT03500UpdateMeterModel();
        private LMT03500UtilityUsageModel _modelUtility = new LMT03500UtilityUsageModel();
        
        public LMT03500UpdateMeterHeader Header = new LMT03500UpdateMeterHeader();
        
        public ObservableCollection<LMT03500UtilityMeterDTO> GridList = new ObservableCollection<LMT03500UtilityMeterDTO>();
        public LMT03500UtilityMeterDetailDTO Entity = new LMT03500UtilityMeterDetailDTO();
        
        
        public List<LMT03500YearDTO> StartInvYearList = new List<LMT03500YearDTO>();
        public List<LMT03500PeriodDTO> StartInvMonthList = new List<LMT03500PeriodDTO>();
        
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
                // var loParam = (LMT03500UpdateMeterHeader)poParam;
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.CPROPERTY_ID,Header.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.CBUILDING_ID,Header.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.CTENANT_ID, "");
                var loReturn = await _model.GetListStreamAsync<LMT03500UtilityMeterDTO>(nameof(ILMT03500UpdateMeter
                    .LMT03500GetUtilityMeterListStream));
                GridList = new ObservableCollection<LMT03500UtilityMeterDTO>(loReturn);
                
                //dummy data grid list, 10 data
                // for (int i = 0; i < 10; i++)
                // {
                //     GridList.Add(new LMT03500UtilityMeterDTO
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

        public async Task GetRecord(LMT03500UtilityMeterDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new LMT03500UtilityMeterDetailParam()
                {
                    CPROPERTY_ID = poEntity.CPROPERTY_ID,
                    CBUILDING_ID = poEntity.CBUILDING_ID,
                    CUNIT_ID = poEntity.CUNIT_ID,
                    CCHARGES_TYPE = poEntity.CUTILITY_TYPE,
                    CCHARGES_ID = poEntity.CCHARGES_ID
                };
                
                var loReturn = await _model.GetAsync<LMT03500SingleDTO<LMT03500UtilityMeterDetailDTO>, LMT03500UtilityMeterDetailParam>(nameof(ILMT03500UpdateMeter.LMT03500GetUtilityMeterDetail), loParam);
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

        public async Task GetAgreementTenant(LMT03500UpdateMeterHeader poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new LMT03500AgreementUtilitiesParam
                {
                    CPROPERTY_ID = poParam.CPROPERTY_ID,
                    CBUILDING_ID = poParam.CBUILDING_ID,
                    CUNIT_ID = poParam.CUNIT_ID
                };
                
                var loReturn = await _model.GetAsync<LMT03500SingleDTO<LMT03500AgreementUtilitiesDTO>, LMT03500AgreementUtilitiesParam>(nameof(ILMT03500UpdateMeter.LMT03500GetAgreementUtilities), loParam);

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

        public async Task UpdateMeterNo(LMT03500UtilityMeterDetailDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new LMT03500UpdateChangeMeterNoParam
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
                
                await _model.ExecuteAsync<LMT03500UtilityMeterDetailDTO, LMT03500UpdateChangeMeterNoParam>(nameof(ILMT03500UpdateMeter.LMT03500UpdateMeterNo), loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            
            }
            
            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task ChangeMeterNo(LMT03500UtilityMeterDetailDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new LMT03500UpdateChangeMeterNoParam
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
                
                await _model.ExecuteAsync<LMT03500UtilityMeterDetailDTO, LMT03500UpdateChangeMeterNoParam>(nameof(ILMT03500UpdateMeter.LMT03500ChangeMeterNo), loParam);
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
                var loParam = new LMT03500PeriodParam { CYEAR = lcYear.Year.ToString() };

                var loReturn =
                    await _modelUtility.GetAsync<LMT03500ListDTO<LMT03500PeriodDTO>, LMT03500PeriodParam>(
                        nameof(ILMT03500UtilityUsage.LMT03500GetPeriodList), loParam);
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
                var loReturn = await _modelUtility.GetAsync<LMT03500ListDTO<LMT03500YearDTO>>(nameof(ILMT03500UtilityUsage.LMT03500GetYearList));
                StartInvYearList = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
    }
}