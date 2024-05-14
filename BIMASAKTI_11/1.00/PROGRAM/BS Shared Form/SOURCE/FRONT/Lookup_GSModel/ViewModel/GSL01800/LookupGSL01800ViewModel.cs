using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL01800ViewModel
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL01800TreeDTO> CategoryGrid = new ObservableCollection<GSL01800TreeDTO>();
        public List<GSL01800DTO> ListResult = new List<GSL01800DTO>();  
        public async Task GetCategoryList(GSL01800DTOParameter poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL01800GetCategoryListAsync(poParameter);
                ListResult = loResult;

                var loGridData = loResult.Select(x =>
                new GSL01800TreeDTO
                {
                    ParentId = x.CPARENT,
                    ParentName = x.CPARENT_NAME,
                    Id = x.CCATEGORY_ID,
                    Name = x.CCATEGORY_NAME,
                    Description = x.CCATEGORY_TYPE_DESCR,
                    Level = x.ILEVEL,
                    DisplayTree = x.ILEVEL_CCATEGORY_ID_CCATEGORY_NAME_DISPLAY
                }).ToList();

                CategoryGrid = new ObservableCollection<GSL01800TreeDTO>(loGridData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GSL01800DTO> GetCategory(GSL01800DTOParameter poParameter)
        {
            var loEx = new R_Exception();
            GSL01800DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL01800GetCategoryAsync(poParameter);
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
