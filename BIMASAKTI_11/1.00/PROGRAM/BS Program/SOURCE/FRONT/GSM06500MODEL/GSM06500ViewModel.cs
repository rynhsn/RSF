using GSM06500Common;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GSM06500FrontResources;

namespace GSM06500Model
{
    public class GSM06500ViewModel : R_ViewModel<GSM06500DTO>
    {
        private GSM06500Model _model = new GSM06500Model();
        public ObservableCollection<GSM06500DTO> PaymentOfTermList = new ObservableCollection<GSM06500DTO>();
        public GSM06500DTO PaymentOfTerm { get; set; } = new GSM06500DTO();
        public List<GSM06500PropertyDTO> PropertyList { get; set; } = new List<GSM06500PropertyDTO>();
        public string PropertyValueContext = "";
        public bool _succesDelete;
        public bool _lPropertyExist;

        public async Task GetAllTermOfPaymentAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, "");

                var loResult = await _model.GetTermOfPaymentListAsyncModel();
                PaymentOfTermList = new ObservableCollection<GSM06500DTO>(loResult.ListData);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            
            loException.ThrowExceptionIfErrors();
        }
        public async Task DeleteTermOfPayment(GSM06500DTO poEntity)
        {
            _succesDelete = false;
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSM06500DTO
                {
                    CCOMPANY_ID = poEntity.CCOMPANY_ID,
                    CPROPERTY_ID = "",
                    CUSER_ID = poEntity.CUSER_ID,
                    CPAY_TERM_CODE = poEntity.CPAY_TERM_CODE,
                    CPAY_TERM_NAME = poEntity.CPAY_TERM_NAME,
                    IPAY_TERM_DAYS = poEntity.IPAY_TERM_DAYS
                };
                await _model.R_ServiceDeleteAsync(loParam);
                _succesDelete = true;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GSM06500DTO> GetTermOfPaymentOneRecord(GSM06500DTO poProperty)
        {
            var loEx = new R_Exception();
            GSM06500DTO loResult = new GSM06500DTO();

            try
            {
                var loParam = new GSM06500DTO
                {
                    CPROPERTY_ID = "",
                    CPAY_TERM_CODE = poProperty.CPAY_TERM_CODE
                };
                loResult = await _model.R_ServiceGetRecordAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<GSM06500DTO> SaveTermOfPayment(GSM06500DTO poNewEntity, R_eConductorMode peConductorMode)
        {
            var loEx = new R_Exception();
#pragma warning disable CS8600
            GSM06500DTO loResult = null;
#pragma warning restore CS8600

            try
            {
                poNewEntity.CPROPERTY_ID = "";
                loResult = await _model.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peConductorMode);
                PaymentOfTerm = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
#pragma warning disable CS8603
            return loResult;
#pragma warning restore CS8603
        }

        public void ValidationFieldEmpty(GSM06500DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrEmpty(poEntity.CPAY_TERM_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GSM06500_Class), "ValidationPaymentCode");
                    loEx.Add(loErr);
                    goto EndBlock;
                }
                if (string.IsNullOrEmpty(poEntity.CPAY_TERM_NAME))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GSM06500_Class), "ValidationPaymentName");
                    loEx.Add(loErr);
                    goto EndBlock;
                }
                if (poEntity.IPAY_TERM_DAYS > 999999)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GSM06500_Class), "ValidationTopDays");
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
    }
}
