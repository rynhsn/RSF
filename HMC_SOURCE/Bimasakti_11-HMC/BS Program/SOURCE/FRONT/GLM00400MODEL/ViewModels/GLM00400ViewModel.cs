using GLM00400COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GLM00400MODEL
{
    public class GLM00400ViewModel : R_ViewModel<GLM00400DTO>
    {
        private GLM00400Model _GLM00400Model = new GLM00400Model();

        public ObservableCollection<GLM00400DTO> AllocationJournalHDGrid { get; set; } = new ObservableCollection<GLM00400DTO>();

        public GLM00400InitialDTO InitialVar = new GLM00400InitialDTO();
        public GLM00400GLSystemParamDTO SystemParam = new GLM00400GLSystemParamDTO();

        public bool SetGridHasData { get; set; } = true;

        public async Task GetInitialVar(GLM00400InitialDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLM00400Model.GetInitialVarAsync(poParam);

                InitialVar = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSystemParam(GLM00400GLSystemParamDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLM00400Model.GetSystemParamAsync(poParam);

                SystemParam = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAllocationJournalHDList(GLM00400DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLM00400Model.GetAllocationJournalHDListAsync(poParam);

                SetGridHasData = loResult.Count > 0;

                AllocationJournalHDGrid = new ObservableCollection<GLM00400DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


    }
}
