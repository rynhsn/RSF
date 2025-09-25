using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMM00500Common;
using PMM00500Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace PMM00500Model
{
    public class PMM00500ViewModel : R_ViewModel<PMM00500DTO>
    {
        private PMM00500Model _model = new PMM00500Model();
        public ObservableCollection<ChargesTypeDTO> ChargesTypeList { get; set; } = new ObservableCollection<ChargesTypeDTO>();
        public string PropId = "";
        public string CharTypeId = "";
        public string CharId = "";
        public PMM00500DTO ChargeType = new PMM00500DTO();
        public async Task GetChargeTypeGridList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn = await _model.GetAllChargesTypeAsync();
                if (loReturn != null)
                {
                    ChargesTypeList = new ObservableCollection<ChargesTypeDTO>(loReturn.Data);
                    if (CharTypeId == "" && loReturn.Data.Count > 0)
                    {
                        var firstCharges = ChargesTypeList.FirstOrDefault();
                        CharTypeId = firstCharges.CCHARGE_TYPE_ID = firstCharges.CCODE ?? "";
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
