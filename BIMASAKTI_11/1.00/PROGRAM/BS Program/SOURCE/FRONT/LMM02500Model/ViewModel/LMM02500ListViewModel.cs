using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LMM02500Common;
using LMM02500Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace LMM02500Model.ViewModel
{
    public class LMM02500ListViewModel : R_ViewModel<LMM02500TenantGroupDTO>
    {
        private LMM02500InitModel _initModel = new LMM02500InitModel();
        private LMM02500ListModel _model = new LMM02500ListModel();
        public ObservableCollection<LMM02500TenantGroupDTO> GridList = new ObservableCollection<LMM02500TenantGroupDTO>();
        public LMM02500TenantGroupDTO Entity = new LMM02500TenantGroupDTO();

        public List<LMM02500PropertyDTO> PropertyList = new List<LMM02500PropertyDTO>();

        public string PropertyId = string.Empty;

        public async Task Init()
        {
            await GetPropertyList();
        }

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _initModel.GetAsync<LMM02500ListDTO<LMM02500PropertyDTO>>(nameof(ILMM02500Init.LMM02500GetPropertyList));
                PropertyList = loReturn.Data;
                PropertyId = PropertyList.FirstOrDefault().CPROPERTY_ID ?? string.Empty;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetList()
        {
            var loEx = new R_Exception();
            
            try
            {
                R_FrontContext.R_SetStreamingContext(LMM02500ContextConstant.CPROPERTY_ID, PropertyId);
                var loReturn = await _model.GetListStreamAsync<LMM02500TenantGroupDTO>(nameof(ILMM02500List.LMM02500GetTenantGroupList));
                GridList = new ObservableCollection<LMM02500TenantGroupDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            
            loEx.ThrowExceptionIfErrors();
        }
    }
}