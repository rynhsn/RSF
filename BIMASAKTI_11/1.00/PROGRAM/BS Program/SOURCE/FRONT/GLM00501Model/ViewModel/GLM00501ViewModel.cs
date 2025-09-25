using System;
using System.Threading.Tasks;
using GLExcelTestCommon;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace GLM00501Model.ViewModel
{
    public class GLM00501ViewModel : R_ViewModel<GLExcelTestFileDTO>
    {
        private GLM00501Model _model = new GLM00501Model();
        
        public async Task<GLExcelTestFileDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            GLExcelTestFileDTO loResult = null;

            try
            {
                loResult = await _model.GLM00500DownloadTemplateFileModel();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}