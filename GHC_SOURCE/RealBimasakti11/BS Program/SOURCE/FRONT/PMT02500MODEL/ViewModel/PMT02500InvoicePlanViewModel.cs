using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using PMT02500Common.DTO._5._Invoice_Plan;
using PMT02500Common.Utilities;
using PMT02500Common.Utilities.Front.Invoice_Plan;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace PMT02500Model.ViewModel
{
    public class PMT02500InvoicePlanViewModel : R_ViewModel<PMT02500BlankDTO>
    {
        #region From Back
        private readonly PMT02500InvoicePlanModel _modelPMT02500InvoicePlanModel = new PMT02500InvoicePlanModel();
        public ObservableCollection<PMT02500InvoicePlanListDTO> loListPMT02500InvoicePlanList = new ObservableCollection<PMT02500InvoicePlanListDTO>();
        public ObservableCollection<PMT02500InvoicePlanChargesListDTO> loListPMT02500InvoicePlanChargesList = new ObservableCollection<PMT02500InvoicePlanChargesListDTO>();
        public PMT02500InvoicePlanHeaderDTO loEntityInvoicePlanHeader = new PMT02500InvoicePlanHeaderDTO();
        public PMT02500GetHeaderParameterDTO loParameterList = new PMT02500GetHeaderParameterDTO();
        #endregion

        #region For Front

        public PMT02500InvoicePlanCountedHeaderDTO loCountedDataHeader = new PMT02500InvoicePlanCountedHeaderDTO();
        public PMT02500InvoicePlanParameterDbDTO loParameterInvoicePlanList = new PMT02500InvoicePlanParameterDbDTO();
        public PMT02500InvoicePlanChargesListDTO EntityCharges = new PMT02500InvoicePlanChargesListDTO();
        #endregion

        #region InvoicePlan

        public async Task GetInvoicePlanHeader()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelPMT02500InvoicePlanModel.GetInvoicePlanHeaderAsync(poParameter: loParameterList);
                    loEntityInvoicePlanHeader = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetInvoicePlanChargeList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelPMT02500InvoicePlanModel.GetInvoicePlanChargeListAsync(poParameter: loParameterList);
                    foreach (var item in loResult)
                    {
                        item.CCHARGES_ID_WITH_NAME = string.Format("{0}({1})", item.CCHARGES_NAME, item.CCHARGES_ID);
                    }
                    loListPMT02500InvoicePlanChargesList = new ObservableCollection<PMT02500InvoicePlanChargesListDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetInvoicePlanList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterInvoicePlanList.CCHARGES_ID))
                {
                    var loResult = await _modelPMT02500InvoicePlanModel.GetInvoicePlanListAsync(poParameter: loParameterInvoicePlanList);
                    foreach (var item in loResult)
                    {
                        item.DSTART_DATE = ConvertStringToDateTimeFormat(item.CSTART_DATE)!;
                        item.DEND_DATE = ConvertStringToDateTimeFormat(item.CEND_DATE)!;
                    }
                    loListPMT02500InvoicePlanList = new ObservableCollection<PMT02500InvoicePlanListDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        #endregion
        public DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null, kembalikan DateTime.MinValue atau nilai default yang sesuai
                //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                return null;
            }
            else
            {
                // Parse string ke DateTime
                DateTime result;
                if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return result;
                }
                else
                {
                    // Jika parsing gagal, kembalikan DateTime.MinValue atau nilai default yang sesuai
                    //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                    return null;
                }
            }
        }



    }
}