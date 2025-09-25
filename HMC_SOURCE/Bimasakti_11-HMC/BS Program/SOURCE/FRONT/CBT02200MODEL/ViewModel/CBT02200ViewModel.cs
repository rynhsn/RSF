using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using CBT02200COMMON.DTO.CBT02200;
using CBT02200COMMON.DTO;

namespace CBT02200MODEL.ViewModel
{
    public class CBT02200ViewModel : R_ViewModel<CBT02200GridDTO>
    {
        private CBT02200Model loModel = new CBT02200Model();

        public CBT02200DTO loHeader { get; set; } = new CBT02200DTO();

        public CBT02200GridDTO loChequeHeader = new CBT02200GridDTO();

        public CBT02200GridDetailDTO loChequeDetail = new CBT02200GridDetailDTO();

        public ObservableCollection<CBT02200GridDTO> loChequeHeaderList = new ObservableCollection<CBT02200GridDTO>();

        public ObservableCollection<CBT02200GridDetailDTO> loChequeDetailList = new ObservableCollection<CBT02200GridDetailDTO>();

        public InitialProcessDTO loInitialProcess = null;

        public List<GetDeptLookupListDTO> loDeptLookupList = null;

        public List<GetStatusDTO> loStatusList = new List<GetStatusDTO>();

        public bool IsSearchFunction = false;

        public GetPeriodYearRangeDTO loPeriodYearRange = new GetPeriodYearRangeDTO();

        public GetTransCodeInfoDTO loTransCodeInfo = new GetTransCodeInfoDTO();

        public async Task InitialProcessAsync()
        {
            R_Exception loException = new R_Exception();
            InitialProcessResultDTO loRtn = null;
            try
            {
                loRtn = await loModel.InitialProcessAsync();
                loInitialProcess = loRtn.Data;
                loPeriodYearRange = loInitialProcess.PeriodYearRange;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetDeptLookupListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            GetDeptLookupListResultDTO loResult = new GetDeptLookupListResultDTO();
            try
            {
                loResult = await loModel.GetDeptLookupListStreamAsync();
                loDeptLookupList = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task GetStatusListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            GetStatusResultDTO loResult = new GetStatusResultDTO();
            try
            {
                loResult = await loModel.GetStatusListStreamAsync();
                loResult.Data.Add(new GetStatusDTO()
                {
                    CCODE = "",
                    CNAME = "ALL"
                });
                loStatusList = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetChequeHeaderListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            CBT02200GridResultDTO loResult = new CBT02200GridResultDTO();
            CBT02200GridParameterDTO loParam = null;
            try
            {
                if (string.IsNullOrWhiteSpace(loHeader.CSTATUS))
                {
                    loHeader.CSTATUS = "";
                }
                loParam = new CBT02200GridParameterDTO()
                {
                    CDEPT_CODE = loHeader.CDEPT_CODE,
                    CPERIOD = loHeader.IPERIOD_YY.ToString() + loHeader.CPERIOD_MM,
                    CSTATUS = loHeader.CSTATUS,
                    CCB_CODE = loHeader.CCB_CODE,
                    CCB_ACCOUNT_NO = loHeader.CCB_ACCOUNT_NO,
                    CTRANS_CODE = "190020"
                };
                if (IsSearchFunction)
                {
                    loParam.CSEARCH_TEXT = loHeader.CSEARCH_TEXT;
                }
                else
                {
                    loParam.CSEARCH_TEXT = "";
                }
                R_FrontContext.R_SetStreamingContext(ContextConstant.CBT02200_GRID_HEADER_STREAMING_CONTEXT, loParam);
                loResult = await loModel.GetChequeHeaderListStreamAsync();
                loResult.Data.ForEach(x =>
                {
                    x.DREF_DATE = DateTime.ParseExact(x.CREF_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    x.DCHEQUE_DATE = DateTime.ParseExact(x.CCHEQUE_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    x.DDUE_DATE = DateTime.ParseExact(x.CDUE_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                });
                loChequeHeaderList = new ObservableCollection<CBT02200GridDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetChequeDetailListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            CBT02200GridDetailResultDTO loResult = new CBT02200GridDetailResultDTO();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CBT02200_GRID_DETAIL_REC_ID_STREAMING_CONTEXT, loChequeHeader.CREC_ID);
                loResult = await loModel.GetChequeDetailListStreamAsync();

                loResult.Data.ForEach(x => x.DDOCUMENT_DATE = DateTime.ParseExact(x.CDOCUMENT_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture));

                loChequeDetailList = new ObservableCollection<CBT02200GridDetailDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task UpdateStatusAsync(UpdateStatusParameterDTO poParam)
        {
            R_Exception loException = new R_Exception();
            try
            {
                await loModel.UpdateStatusAsync(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
