using Lookup_PMCOMMON.DTOs.LML01600;
using Lookup_PMCOMMON.DTOs.LML01700;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Lookup_PMCOMMON.DTOs;
using R_BlazorFrontEnd.Helpers;
using System.Globalization;
using Lookup_PMFrontResources;

namespace Lookup_PMModel.ViewModel.LML01700
{
    public class LookupLML01700ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        private PublicLookupLMGetRecordModel _modelGetRecord = new PublicLookupLMGetRecordModel();
        public ObservableCollection<LML01700DTO> _GetListCancelReceiptFromCustomer = new ObservableCollection<LML01700DTO>();
        public ObservableCollection<LML01700DTO> _GetListPrerequisiteCustomer = new ObservableCollection<LML01700DTO>();
        public LML01700InitalProcessDTO _GetInitialProcess = new LML01700InitalProcessDTO();
        public LML01700DTO _CancelReceiptFromCustomerData = new LML01700DTO();
        public LML01700ParameterDTO _Parameter = new LML01700ParameterDTO();
        public int _PeriodYear = DateTime.Now.Year;
        public string _PeriodMonth = DateTime.Now.Month.ToString("D2");
        public List<LML01700GetMonthDTO>? GetPeriodMonth { get; set; }
        public async Task GetInitialProcess()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _model.GetInitialProcessAsyncModel();
                _GetInitialProcess = R_FrontUtility.ConvertObjectToObject<LML01700InitalProcessDTO>(loResult);


            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }
        public async Task GetCancelReceiptFromCustomerList(LML01700ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPERIOD, poParam.CPERIOD);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCUSTOMER_ID, poParam.CCUSTOMER_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CREC_ID, poParam.CREC_ID ?? "");
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CDEPT_CODE, poParam.CDEPT_CODE ?? "");
                var loResult = await _model.LML01700CancelReceiptFromCustomerAsync();

                if (loResult != null)
                {
                    foreach (var item in loResult.Data)
                    {
                        item.DREF_DATE = ConvertStringToDateTimeFormat(item.CREF_DATE!);
                    }
                    _GetListCancelReceiptFromCustomer = new ObservableCollection<LML01700DTO>(loResult.Data);
                }
                else
                {
                    _GetListCancelReceiptFromCustomer = new ObservableCollection<LML01700DTO>();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetLML01700PrerequisiteCustReceiptList(LML01700ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CRECEIPT_ID, poParam.CRECEIPT_ID);
                var loResult = await _model.LML01700PrerequisiteCustReceiptAsync();
                if (loResult != null)
                {
                    foreach (var item in loResult.Data)
                    {
                        item.DREF_DATE = ConvertStringToDateTimeFormat(item.CREF_DATE!);
                    }
                    _GetListPrerequisiteCustomer = new ObservableCollection<LML01700DTO>(loResult.Data);
                }
                else
                {
                    _GetListPrerequisiteCustomer = new ObservableCollection<LML01700DTO>();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public void ValidationFieldEmpty()
        {
            var loEx = new R_Exception();
            try
            {
                string lcPeriodYear = _PeriodYear.ToString();
                if (string.IsNullOrWhiteSpace(lcPeriodYear))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class_LookupPM), "_ValidationPeriod");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(_PeriodMonth))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class_LookupPM), "_ValidationPeriodMonth");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(_Parameter.CCUSTOMER_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class_LookupPM), "_ValidationCustomer");
                    loEx.Add(loErr);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.HasError)
            {
                loEx.ThrowExceptionIfErrors();
            }
        }
        public void ValidationProcess()
        {
            var loEx = new R_Exception();
            try
            {
                var loErr = R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class_LookupPM), "_ValidationPrerequisite");
                loEx.Add(loErr);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.HasError)
            {
                loEx.ThrowExceptionIfErrors();
            }
        }
        public void GetMonth()
        {
            GetPeriodMonth = new List<LML01700GetMonthDTO>();

            for (int i = 1; i <= 12; i++)
            {
                string monthId = i.ToString("D2");
                LML01700GetMonthDTO month = new LML01700GetMonthDTO { Id = monthId };
                GetPeriodMonth.Add(month);
            }

        }
        private DateTime? ConvertStringToDateTimeFormat(string pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null, kembalikan DateTime.MinValue atau nilai default yang sesuai
                return null; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
            }
            // Parse string ke DateTime
            DateTime result;
            if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            // Jika parsing gagal, kembalikan DateTime.MinValue atau nilai default yang sesuai
            return null; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
        }
    }
}
