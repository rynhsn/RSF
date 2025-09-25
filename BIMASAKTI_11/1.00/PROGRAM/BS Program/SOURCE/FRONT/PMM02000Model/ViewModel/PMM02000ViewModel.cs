using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PMM02000Common;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace PMM02000Model.ViewModel
{
    public class PMM02000ViewModel : R_ViewModel<PMM02000ExcelDTO>
    {
        private PMM02000Model _model = new PMM02000Model();
        
        public async Task<PMM02000ExcelDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            PMM02000ExcelDTO loResult = null;

            try
            {
                loResult = await _model.DownloadTemplateFileModel();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }

    public class PMM02000UploadViewModel : R_ViewModel<PMM02000FromExcelDTO>
    {
        public ObservableCollection<PMM02000FromExcelDTO> GridList = new ObservableCollection<PMM02000FromExcelDTO>();
        
        public string PropertyId { get; set; }

        public void ConvertGrid(List<PMM02000FromExcelDTO> poEntity)
        {
            GridList = new ObservableCollection<PMM02000FromExcelDTO>(poEntity);
            for (var i = 0; i < GridList.Count; i++)
            {
                GridList[i].NO = i + 1;
            }
        }
    }
}