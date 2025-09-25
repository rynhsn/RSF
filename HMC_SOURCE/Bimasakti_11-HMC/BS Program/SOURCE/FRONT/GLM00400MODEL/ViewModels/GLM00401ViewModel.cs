using GLM00400COMMON;
using GLM00400FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GLM00400MODEL
{
    public class GLM00401ViewModel : R_ViewModel<GLM00400PrintParamDTO>
    {
        private GLM00400Model _GLM00400Model = new GLM00400Model();

        public List<GLM00400DTO> AllocationJournalFromGrid { get; set; } = new List<GLM00400DTO>();
        public List<GLM00400DTO> AllocationJournalToGrid { get; set; } = new List<GLM00400DTO>();

        public async Task GetAllocationJournalFromToList(GLM00400DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLM00400Model.GetAllocationJournalHDListAsync(poParam);

                AllocationJournalFromGrid = loResult;
                AllocationJournalToGrid = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ValidationAllocation(GLM00400PrintParamDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

                lCancel = string.IsNullOrEmpty(poParam.CDEPT_CODE);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "4005"));
                }

                lCancel = string.IsNullOrEmpty(poParam.CFROM_ALLOC_NO);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "4006"));
                }

                lCancel = string.IsNullOrEmpty(poParam.CTO_ALLOC_NO);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "4007"));
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
