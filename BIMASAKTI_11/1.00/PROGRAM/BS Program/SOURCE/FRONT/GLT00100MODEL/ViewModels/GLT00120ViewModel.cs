using GLT00100COMMON;
using Lookup_GSModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace GLT00100MODEL
{
    public class GLT00120ViewModel : R_ViewModel<GLT00100DTO>
    {
        #region Model
        private GLT00100Model _GLT00100Model = new GLT00100Model();
        private GLT00100UniversalModel _GLT00100UniversalModel = new GLT00100UniversalModel();
        #endregion

        #region Initial Property
        public GLT00100GLSystemParamDTO VAR_GL_SYSTEM_PARAM = new GLT00100GLSystemParamDTO();
        #endregion

        #region Public Property ViewModel
        public ObservableCollection<GLT00100DTO> JournalGrid { get; set; } = new ObservableCollection<GLT00100DTO>();
        #endregion
        public async Task GetInitialVarData()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLT00100UniversalModel.GetGLSystemParamAsync();

                VAR_GL_SYSTEM_PARAM = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetJournalList(GLT00100ParamDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLT00100Model.GetJournalListAsync(poParam);
                var loMappingDate = await ConvertDateOnListAsync(loResult);
                JournalGrid = new ObservableCollection<GLT00100DTO>(loMappingDate);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task UpdateJournalStatus(GLT00100UpdateStatusDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _GLT00100Model.UpdateJournalStatusAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<List<GLT00100DTO>> ConvertDateOnListAsync(List<GLT00100DTO> loParamList)
        {
            var loEx = new R_Exception();
            List<GLT00100DTO> loData = new List<GLT00100DTO>();
            try
            {
                foreach (var loDataItem in loParamList)
                {
                    if (!string.IsNullOrEmpty(loDataItem.CREF_DATE) && loDataItem.CREF_DATE.Length >= 8)
                    {
                        loDataItem.DREF_DATE = DateTime.ParseExact(loDataItem.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                    }
                    if (!string.IsNullOrEmpty(loDataItem.CDOC_DATE) && loDataItem.CDOC_DATE.Length >= 8)
                    {
                        loDataItem.DDOC_DATE = DateTime.ParseExact(loDataItem.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                    }
                    loData.Add(loDataItem);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loData;
        }
    }
}
