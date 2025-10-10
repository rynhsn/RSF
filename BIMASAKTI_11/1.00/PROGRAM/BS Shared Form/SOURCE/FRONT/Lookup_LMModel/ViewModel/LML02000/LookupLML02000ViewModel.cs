using Lookup_PMCOMMON.DTOs;
using Lookup_PMCOMMON.DTOs.LML02000;
using Lookup_PMModel.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_PMModel.ViewModel.LML02000
{
    public class LookupLML02000ViewModel : R_ViewModel<PML02000TreeDTO>
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        private PublicLookupLMGetRecordModel _modelGetRecord = new PublicLookupLMGetRecordModel();
        public ObservableCollection<PML02000TreeDTO> TenantCategoryGrid = new ObservableCollection<PML02000TreeDTO>();
        public List<LML02000DTO> TenantCategoryListResult = new List<LML02000DTO>();
        public LML02000ParameterDTO poPar = new LML02000ParameterDTO();

        public async Task LML02000TenantCategoryList(LML02000ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID ?? "");
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPARENT_ID, poParam.CPARENT_ID ?? "");
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.LCHILD_ONLY, poParam.LCHILD_ONLY);

                var loResult = await _model.LML02000TenantCategoryListdata();
                TenantCategoryListResult = loResult;

                

                if (poParam.LCHILD_ONLY == true)
                {
                    TenantCategoryListResult[1].LHAS_CHILD = true;
                    TenantCategoryListResult[0].CPARENT_ID = null;
                }

                var loGridData = loResult.Select(x =>
                new PML02000TreeDTO
                {
                    ParentId = (x.ILEVEL==0)?null:x.CPARENT_ID,
                    ParentName = x.CPARENT_NAME,
                    Id = x.CCATEGORY_ID,
                    Name = x.CCATEGORY_NAME,
                    Description = x.CCATEGORY_ID_NAME,
                    Level = x.ILEVEL,
                    Note = x.CCATEGORY_NAME
                }).ToList();

                TenantCategoryGrid = new ObservableCollection<PML02000TreeDTO>(loGridData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<LML02000DTO> LML02000TenantCategory(LML02000ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            LML02000DTO loRtn = null;
            try
            {
                var loResult = await _modelGetRecord.LML02000TenantCategoryAsync(poParameter);
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}
