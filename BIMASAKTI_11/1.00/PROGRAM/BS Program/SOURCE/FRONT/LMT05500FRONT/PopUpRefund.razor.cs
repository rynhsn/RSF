using PMT05500COMMON.DTO;
using PMT05500Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT05500Front
{
    public partial class PopUpRefund : R_Page
    {
        private bool _isModalHidden = true;
        private LMT05500DepositViewModel _depositViewModel = new();


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _isModalHidden = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void Onchange(object poParam)
        {
            var loEx = new R_Exception();
            string lcValue = (string)poParam;
            try
            {
                _depositViewModel._buttonRefundValue = lcValue;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #region CloseButton
        private async Task OnClickOK()
        {
            var loEx = new R_Exception();
            try
            {
                ResultPopUpDTO loParam = new ResultPopUpDTO
                {
                    CODE_PROGRAM = _depositViewModel._buttonRefundValue,
                    IS_SUCCESS = true
                };
                await Close(true, loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task OnCancel()
        {
            var loEx = new R_Exception();
            try
            {
                await Close(true, null);
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
