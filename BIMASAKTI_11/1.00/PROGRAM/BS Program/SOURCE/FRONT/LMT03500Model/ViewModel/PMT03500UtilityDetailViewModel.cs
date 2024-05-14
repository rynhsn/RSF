using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using PMT03500Common;
using PMT03500Common.DTOs;
using PMT03500Common.Params;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace PMT03500Model.ViewModel
{
    public class PMT03500UtilityDetailViewModel : R_ViewModel<PMT03500UtilityUsageDetailDTO>
    {
        private PMT03500UtilityUsageModel _model = new PMT03500UtilityUsageModel();
        public PMT03500DetailParam Param = new PMT03500DetailParam();
        public PMT03500UtilityUsageDetailDTO Entity = new PMT03500UtilityUsageDetailDTO();

        public List<PMT03500RateWGListDTO> RateList = new List<PMT03500RateWGListDTO>();

        public int index = 0;

        public decimal NSTANDING_CONSUMPTION = 0;
        public decimal NSUBTOTAL_ADM = 0;
        public decimal NTOTAL_ADM = 0;
        public decimal NADMIN = 0;

        public string DisplayEC = "d-none";
        public string DisplayWG = "d-none";

        public async Task Init(object obj)
        {
            // var loParam = (PMT03500UtilityUsageDTO)obj;     
            Param = (PMT03500DetailParam)obj;

            switch (Param.EUTILITY_TYPE)
            {
                case EPMT03500UtilityUsageType.EC:
                    DisplayEC = "d-block";
                    break;
                case EPMT03500UtilityUsageType.WG:
                    DisplayWG = "d-block";
                    break;
            }
        }
        
        public async Task GetRateWGList(PMT03500UtilityUsageDTO poParam,PMT03500UtilityUsageDetailDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMT03500RateParam()
                {
                    // CPROPERTY_ID = PropertyId,
                    CPROPERTY_ID = poParam.CPROPERTY_ID,
                    CCHARGE_TYPE_ID = poParam.CCHARGES_TYPE,
                    CCHARGES_ID = poParam.CCHARGES_ID
                };
                var loReturn = await _model.GetAsync<PMT03500ListDTO<PMT03500RateWGListDTO>, PMT03500RateParam>(
                    nameof(IPMT03500UtilityUsage.PMT03500GetRateWGList), loParam);

                RateList = loReturn.Data;

                // Urutkan RateList berdasarkan IUP_TO_USAGE
                RateList = RateList.OrderBy(x => x.IUP_TO_USAGE).ToList();

                // Set IMIN_USAGE
                for (int i = 0; i < RateList.Count; i++)
                {
                    RateList[i].IMIN_USAGE = i == 0 ? 0 : RateList[i - 1].IUP_TO_USAGE + 1;
                    if (i == 0)
                    {
                        RateList[i].NUSAGE = poEntity.IMETER_USAGE > RateList[i].IUP_TO_USAGE
                            ? RateList[i].IUP_TO_USAGE
                            : poEntity.IMETER_USAGE;
                    }
                    else
                    {
                        RateList[i].NUSAGE =
                            poEntity.IMETER_USAGE - RateList[i - 1].IUP_TO_USAGE > RateList[i].IUP_TO_USAGE
                                ? RateList[i].IUP_TO_USAGE - RateList[i - 1].IUP_TO_USAGE
                                : poEntity.IMETER_USAGE - RateList[i - 1].IUP_TO_USAGE;
                    }

                    RateList[i].NUSAGE = Math.Max(0, RateList[i].NUSAGE);
                    RateList[i].NSUB_TOTAL_ROW = RateList[i].NUSAGE_CHARGE * RateList[i].NUSAGE * poEntity.NCF;
                    NSTANDING_CONSUMPTION += RateList[i].NSUB_TOTAL_ROW;
                }

                NSUBTOTAL_ADM = poEntity.NADMIN_FEE_PCT / 100 * NSTANDING_CONSUMPTION;

                NTOTAL_ADM = NSTANDING_CONSUMPTION + NSUBTOTAL_ADM;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetRecord(PMT03500UtilityUsageDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMT03500UtilityUsageDetailParam
                {
                    // CPROPERTY_ID = poParam.CPROPERTY_ID,
                    // CREF_NO = poParam.CREF_NO,
                    // CCHARGES_TYPE = poParam.CCHARGES_TYPE

                    CPROPERTY_ID = poParam.CPROPERTY_ID,
                    CDEPT_CODE = poParam.CDEPT_CODE,
                    CTRANS_CODE = poParam.CTRANS_CODE,
                    CREF_NO = poParam.CREF_NO,
                    CUNIT_ID = poParam.CUNIT_ID,
                    CFLOOR_ID = poParam.CFLOOR_ID,
                    CBUILDING_ID = poParam.CBUILDING_ID,
                    CCHARGES_TYPE = poParam.CCHARGES_TYPE,
                    CCHARGES_ID = poParam.CCHARGES_ID,
                    CSEQ_NO = poParam.CSEQ_NO,
                    CINV_PRD = poParam.CINV_PRD
                };
                var loReturn =
                    await _model
                        .GetAsync<PMT03500SingleDTO<PMT03500UtilityUsageDetailDTO>, PMT03500UtilityUsageDetailParam>(
                            nameof(IPMT03500UtilityUsage.PMT03500GetUtilityUsageDetail), loParam);
                Entity = _helperGetDetail(loReturn.Data);
                
                if (Param.EUTILITY_TYPE == EPMT03500UtilityUsageType.WG)
                {
                    await GetRateWGList(Param.OUTILITY_HEADER, Entity);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetRecordPhoto(PMT03500UtilityUsageDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMT03500UtilityUsageDetailParam
                {
                    // CPROPERTY_ID = poParam.CPROPERTY_ID,
                    // CREF_NO = poParam.CREF_NO,
                    // CCHARGES_TYPE = poParam.CCHARGES_TYPE

                    CPROPERTY_ID = poParam.CPROPERTY_ID,
                    CDEPT_CODE = poParam.CDEPT_CODE,
                    CTRANS_CODE = poParam.CTRANS_CODE,
                    CREF_NO = poParam.CREF_NO,
                    CUNIT_ID = poParam.CUNIT_ID,
                    CFLOOR_ID = poParam.CFLOOR_ID,
                    CBUILDING_ID = poParam.CBUILDING_ID,
                    CCHARGES_TYPE = poParam.CCHARGES_TYPE,
                    CCHARGES_ID = poParam.CCHARGES_ID,
                    CSEQ_NO = poParam.CSEQ_NO,
                    CINV_PRD = poParam.CINV_PRD
                };
                var loReturn =
                    await _model
                        .GetAsync<PMT03500SingleDTO<PMT03500UtilityUsageDetailDTO>, PMT03500UtilityUsageDetailParam>(
                            nameof(IPMT03500UtilityUsage.PMT03500GetUtilityUsageDetailPhoto), loParam);
                Entity = _helperGetDetail(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private PMT03500UtilityUsageDetailDTO _helperGetDetail(PMT03500UtilityUsageDetailDTO Entity)
        {
            var loReturn = Entity;
            loReturn.CINV_YEAR = Entity.CINV_PRD.Substring(0, 4);
            loReturn.CINV_MONTH = Entity.CINV_PRD.Substring(4, 2);
            loReturn.CUTILITY_YEAR = Entity.CUTILITY_PRD.Substring(0, 4);
            loReturn.CUTILITY_MONTH = Entity.CUTILITY_PRD.Substring(4, 2);
            loReturn.DSTART_DATE = DateTime.ParseExact(Entity.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
            loReturn.DEND_DATE = DateTime.ParseExact(Entity.CEND_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
            return loReturn;
        }
    }
}