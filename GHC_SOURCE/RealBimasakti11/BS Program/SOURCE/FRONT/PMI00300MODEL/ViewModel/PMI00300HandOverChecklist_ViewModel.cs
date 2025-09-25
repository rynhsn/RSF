using PMI00300COMMON.DTO;
using PMI00300FrontResources;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PMI00300MODEL.ViewModel
{
    public class PMI00300HandOverChecklist_ViewModel
    {
        private PMI00300AgreementFormModel loModel = new PMI00300AgreementFormModel();
        public List<PMI00300HandOverChecklistDTO> loHandOverChecklist = new List<PMI00300HandOverChecklistDTO>();
        public PMI00300HandOverChecklistParamDTO loParam = new PMI00300HandOverChecklistParamDTO();

        public async Task GetList_HandOverChecklist(R_ILocalizer<Resources_Dummy_Class> poParamLocalizer)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                loHandOverChecklist = await loModel.GetList_HandOverChecklistAsync(loParam) ?? new List<PMI00300HandOverChecklistDTO>();
                if (loHandOverChecklist != null)
                {
                    foreach (var item in loHandOverChecklist)
                    {
                        item.CSTATUS = item.LSTATUS ? poParamLocalizer["_Ok"] : poParamLocalizer["_NotOK"];
                        item.CQUANTITY_DISPLAY = item.IBASE_QUANTITY > 0 ? $"- {item.IACTUAL_QUANTITY} / {item.IBASE_QUANTITY} {item.CUNIT}" : "";
                        item.CNOTES = string.IsNullOrWhiteSpace(item.CNOTES) ? "-" : item.CNOTES;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}