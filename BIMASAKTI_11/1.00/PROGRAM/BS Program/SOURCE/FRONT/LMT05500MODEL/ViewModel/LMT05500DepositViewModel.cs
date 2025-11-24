using PMT05500COMMON.DTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using R_CommonFrontBackAPI;
using System.Globalization;
using R_BlazorFrontEnd.Helpers;
using PMT05500FrontResources;
using System.Linq;
using System.Collections.Generic;

namespace PMT05500Model.ViewModel
{
    public class LMT05500DepositViewModel : R_ViewModel<LMT05500DepositInfoFrontDTO>
    {
        private LMT05500DepositModel _model = new LMT05500DepositModel();

        public LMT05500DepositHeaderDTO _headerDeposit = new LMT05500DepositHeaderDTO();
        public ObservableCollection<LMT05500DepositListDTO> _depositList =
            new ObservableCollection<LMT05500DepositListDTO>();
        public ObservableCollection<LMT05500DepositDetailListDTO> _depositDetailList =
            new ObservableCollection<LMT05500DepositDetailListDTO>();

        public LMT05500DBParameter _currentDataAgreement = new LMT05500DBParameter();

        public LMT05500DepositListDTO _currentDeposit = null;
        public LMT05500DepositDetailListDTO _DataDepositDetail = new LMT05500DepositDetailListDTO ();

        public LMT05500DepositInfoFrontDTO _depositInfoData = new LMT05500DepositInfoFrontDTO();
        public LMT05500DepositInfoFrontDTO TemporaryData = new LMT05500DepositInfoFrontDTO();

        public List<OtherProgram> ValueBtnRefund { get; set; } = new List<OtherProgram>
        { new OtherProgram { Id = "00200", Name = "Cash Payment Journal" },
            new OtherProgram { Id = "01200", Name = "Wire Transfer Payment Journal" },
        new OtherProgram { Id = "02200", Name = "Cheque Payment Journal" }};

        public bool _IsDataNull = true;

        public bool _buttonView;
        public bool _lIsPayment;
        public bool _lRemainingAmountMoreThanZero;
        public string _buttonRefundValue = "00200";
        public bool _LPayment;
        public async Task GetHeaderDeposit(LMT05500DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loParam = new LMT05500DBParameter()
                {
                    CCOMPANY_ID = poParameter.CCOMPANY_ID,
                    CUSER_ID = poParameter.CUSER_ID,
                    CPROPERTY_ID = poParameter.CPROPERTY_ID,
                    CDEPT_CODE = poParameter.CDEPT_CODE,
                    CTRANS_CODE = poParameter.CTRANS_CODE,
                    CREF_NO = poParameter.CREF_NO,
                    CSEQ_NO = poParameter.CSEQ_NO
                };

                _headerDeposit = await _model.GetDepositHeaderAsyncModel(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task GetAllDepositList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, _currentDataAgreement.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, _currentDataAgreement.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANS_CODE, _currentDataAgreement.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREF_NO, _currentDataAgreement.CREF_NO);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGE_MODE, _currentDataAgreement.CCHARGE_MODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CBUILDING_ID, _currentDataAgreement.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CFLOOR_ID, _currentDataAgreement.CFLOOR_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CUNIT_ID, _currentDataAgreement.CUNIT_ID);
                //CR02Okt2024
                R_FrontContext.R_SetStreamingContext(ContextConstant.LINVOICED, true);

                var loResult = await _model.DepositListStreamAsyncModel();

                loResult.Data = loResult.Data!.Select(item =>
                {
                    item.DDEPOSIT_DATE = ConvertStringToDateTimeFormat(item.CDEPOSIT_DATE!);
                    return item;
                }).ToList();

                _depositList = new ObservableCollection<LMT05500DepositListDTO>(loResult.Data);

                if (_depositList.Count > 0)
                {
                    _currentDeposit = _depositList[0];
                    //_buttonOnDepositGrid = true;
                    _lIsPayment = _currentDeposit.LPAYMENT;
                }
                else
                {
                    _currentDeposit = null!;
                    _lIsPayment = false;
                    _depositDetailList.Clear();
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task GetAllDepositDetailList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (_currentDeposit != null)
                {
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, _currentDeposit.CPROPERTY_ID);
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, _currentDeposit.CDEPT_CODE);
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANS_CODE, _currentDeposit.CTRANS_CODE);
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CREF_NO, _currentDeposit.CREF_NO);
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CSEQ_NO, (_currentDeposit.CSEQ_NO??""));
                }
                var loResult = await _model.DepositDetailListAsyncModel();

                loResult.Data = loResult.Data!.Select(item =>
                {
                    item.DREF_DATE = ConvertStringToDateTimeFormat(item.CREF_DATE!);
                    return item;
                }).ToList();

                _depositDetailList = new ObservableCollection<LMT05500DepositDetailListDTO>(loResult.Data);

                if (_depositDetailList.Count > 0)
                {
                    _buttonView = true;
                }
                else
                {
                    _buttonView = false;
                    //_currentDepositDetail = null;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #region CRUD

        public async Task GetEntity(LMT05500DepositInfoFrontDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _model.R_ServiceGetRecordAsync(poEntity: ConvertToEntityBack(poEntity));
                _depositInfoData = ConvertToEntityFront(loResult);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(LMT05500DepositInfoFrontDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CCHARGE_MODE = _currentDeposit.CCHARGE_MODE;
                poEntity.CBUILDING_ID = _currentDeposit.CBUILDING_ID;
                poEntity.CFLOOR_ID = _currentDeposit.CFLOOR_ID;
                poEntity.CUNIT_ID = _currentDeposit.CUNIT_ID;

                // Validation Before Delete
                await _model.R_ServiceDeleteAsync(ConvertToEntityBack(poEntity));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(LMT05500DepositInfoFrontDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poNewEntity.CCOMPANY_ID = _currentDeposit.CCOMPANY_ID;
                poNewEntity.CUSER_ID = _currentDeposit.CUSER_ID;
                poNewEntity.CPROPERTY_ID = _currentDeposit.CPROPERTY_ID;
                poNewEntity.CCHARGE_MODE = _currentDeposit.CCHARGE_MODE;
                poNewEntity.CBUILDING_ID = _currentDeposit.CBUILDING_ID;
                poNewEntity.CFLOOR_ID = _currentDeposit.CFLOOR_ID;
                poNewEntity.CUNIT_ID = _currentDeposit.CUNIT_ID;
                poNewEntity.CDEPT_CODE = _currentDeposit.CDEPT_CODE;
                poNewEntity.CREF_NO = _currentDeposit.CREF_NO;
                poNewEntity.CTRANS_CODE = _currentDeposit.CTRANS_CODE;

                var loResult = await _model.R_ServiceSaveAsync(ConvertToEntityBack(poNewEntity), peCRUDMode);
                _depositInfoData = ConvertToEntityFront(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Validation
        public void ValidationFieldEmpty(LMT05500DepositInfoFrontDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                if (poEntity.LCONTRACTOR && string.IsNullOrWhiteSpace(poEntity.CCONTRACTOR_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT05500_Class), "5501");
                    loEx.Add(loErr);
                    goto EndBlock;
                }
                if (string.IsNullOrEmpty(poEntity.CDEPOSIT_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT05500_Class), "5502");
                    loEx.Add(loErr);
                    goto EndBlock;
                }
                if (string.IsNullOrEmpty(poEntity.DDEPOSIT_DATE.ToString()))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT05500_Class), "5503");
                    loEx.Add(loErr);
                    goto EndBlock;
                }
                if (poEntity.NDEPOSIT_AMT < 1)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT05500_Class), "5504");
                    loEx.Add(loErr);
                    goto EndBlock;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region ConvertDTO
        public LMT05500DepositInfoDTO ConvertToEntityBack(LMT05500DepositInfoFrontDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            LMT05500DepositInfoDTO? loReturn = null;
            try
            {
                loReturn = R_FrontUtility.ConvertObjectToObject<LMT05500DepositInfoDTO>(poEntity);
                loReturn.CDEPOSIT_DATE = ConvertDateTimeToStringFormat(poEntity.DDEPOSIT_DATE);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }
        public LMT05500DepositInfoFrontDTO ConvertToEntityFront(LMT05500DepositInfoDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            LMT05500DepositInfoFrontDTO? loReturn = null;
            try
            {
                loReturn = R_FrontUtility.ConvertObjectToObject<LMT05500DepositInfoFrontDTO>(poEntity);
                loReturn.DDEPOSIT_DATE = ConvertStringToDateTimeFormat(poEntity.CDEPOSIT_DATE);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }
        public DateTime? ConvertStringToDateTimeFormat(string pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null,maka kembalikan null
                return null;
            }

            // Parse string ke DateTime
            DateTime result;
            if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            // Jika parsing gagal,null
            return null;
        }
        private string ConvertDateTimeToStringFormat(DateTime? ptEntity)
        {
            if (ptEntity == DateTime.MinValue)
            {
                // Jika DateTime adalah DateTime.MinValue, kembalikan string kosong
                return ""; // atau null, tergantung pada kebutuhan Anda
            }

            // Format DateTime ke string "yyyyMMdd"
            return ptEntity?.ToString("yyyyMMdd")!;
        }

        #endregion
    }
}