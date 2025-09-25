using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PMT01500Common.DTO._5._Invoice_Plan;
using PMT01500Common.Utilities;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace PMT01500Model.ViewModel
{
    public class PMT01500InvoicePlanViewModel : R_ViewModel<PMT01500BlankDTO>
    {
        #region From Back
        private readonly PMT01500InvoicePlanModel _modelPMT01500InvoicePlanModel = new PMT01500InvoicePlanModel();
        public ObservableCollection<PMT01500InvoicePlanListDTO> loListPMT01500InvoicePlanList = new ObservableCollection<PMT01500InvoicePlanListDTO>();
        public ObservableCollection<PMT01500InvoicePlanChargesListDTO> loListPMT01500InvoicePlanChargesList = new ObservableCollection<PMT01500InvoicePlanChargesListDTO>();
        public PMT01500InvoicePlanHeaderDTO loEntityInvoicePlanHeader = new PMT01500InvoicePlanHeaderDTO();
        public PMT01500GetHeaderParameterDTO loParameterList = new PMT01500GetHeaderParameterDTO();
        #endregion
        
        #region For Front
        #endregion

        #region InvoicePlan

        public async Task GetInvoicePlanHeader()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelPMT01500InvoicePlanModel.GetInvoicePlanHeaderAsync(poParameter: loParameterList);
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
                    var loResult = await _modelPMT01500InvoicePlanModel.GetInvoicePlanChargeListAsync(poParameter: loParameterList);
                    loListPMT01500InvoicePlanChargesList = new ObservableCollection<PMT01500InvoicePlanChargesListDTO>(loResult);
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
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelPMT01500InvoicePlanModel.GetInvoicePlanListAsync(poParameter: loParameterList);
                    loListPMT01500InvoicePlanList = new ObservableCollection<PMT01500InvoicePlanListDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        #endregion


    }
}