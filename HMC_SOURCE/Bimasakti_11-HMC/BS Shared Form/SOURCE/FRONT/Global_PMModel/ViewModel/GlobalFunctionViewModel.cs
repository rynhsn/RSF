using Global_PMCOMMON.DTOs.Response.Invoice_Type;
using Global_PMCOMMON.DTOs.Response.Property;
using Global_PMCOMMON.DTOs.User_Param_Detail;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_PMModel.ViewModel
{
    public class GlobalFunctionViewModel
    {
        private readonly GlobalFunctionModel _model = new GlobalFunctionModel();

        public async Task<GetUserParamDetailDTO> GetUserParamDetail(GetUserParamDetailParameterDTO poParam)
        {
            var loEx = new R_Exception();
            GetUserParamDetailDTO? loReturn = null;
            try
            {
                var loResult = await _model.UserParamDetailAsync(poParam);
                loReturn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loReturn!;
        }
        public async Task<List<PropertyDTO>> GetPropertyList()
        {
            R_Exception loEx = new R_Exception();
            List<PropertyDTO>? loReturn = null;
            try
            {
                var loResult = await _model.PropertyListAsync();

                if (loResult.Data.Any())
                {
                    loReturn = loResult.Data;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loReturn!;
        }
        public async Task<List<InvoiceTypeDTO>> GetInvoiceType(InvoiceTypeParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            List<InvoiceTypeDTO>? loReturn = null;
            try
            {
                var loResult = await _model.InvoiceTypeAsync(poParam);

                if (loResult.Data.Any())
                {
                    loReturn = loResult.Data;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loReturn!;
        }

    }
}
