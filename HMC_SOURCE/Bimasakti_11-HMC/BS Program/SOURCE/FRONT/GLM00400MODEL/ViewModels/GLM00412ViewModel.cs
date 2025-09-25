using GLM00400COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GLM00400MODEL
{
    public class GLM00412ViewModel : R_ViewModel<GLM00414DTO>
    {
        private GLM00410Model _GLM00410Model = new GLM00410Model();
        public ObservableCollection<GLM00414DTO> AllocationPeriodGrid { get; set; } = new ObservableCollection<GLM00414DTO>();
        public ObservableCollection<GLM00415DTO> AllocationPeriodCenterGrid { get; set; } = new ObservableCollection<GLM00415DTO>();

        public int Year { get; set; } = DateTime.Now.Year;
        public string AllocationId { get; set; } = "";

        public async Task GetAllocationPeriodList(GLM00414DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                poParam.CCYEAR = Year.ToString();

                var loResult = await _GLM00410Model.GetAllocationPeriodListAsync(poParam);

                AllocationPeriodGrid = new ObservableCollection<GLM00414DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetAllocationPeriodCenterList(GLM00415DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLM00410Model.GetAllocationPeriodByTargetCenterListAsync(poParam);

                AllocationPeriodCenterGrid = new ObservableCollection<GLM00415DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
