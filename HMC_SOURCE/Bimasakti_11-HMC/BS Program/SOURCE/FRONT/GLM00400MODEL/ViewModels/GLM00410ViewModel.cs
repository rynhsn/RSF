using GLM00400COMMON;
using GLM00400FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GLM00400MODEL
{
    public class GLM00410ViewModel : R_ViewModel<GLM00410DTO>
    {
        private GLM00410Model _GLM00410Model = new GLM00410Model();

        public GLM00410DTO AllocationJournalDT { get; set; } = new GLM00410DTO();

        public bool OnCRUDMode = false;

        public ObservableCollection<GLM00411DTO> AllocationAccountGrid { get; set; } = new ObservableCollection<GLM00411DTO>();

        public async Task GetAllocationAccountList(GLM00411DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLM00410Model.GetAllocationAccountListAsync(poParam);

                AllocationAccountGrid = new ObservableCollection<GLM00411DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetAllocationJournalDT(GLM00410DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLM00410Model.R_ServiceGetRecordAsync(poParam);

                AllocationJournalDT = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ValidationAllocationJournalDT(GLM00410DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                bool lCancel;

                lCancel = string.IsNullOrEmpty(poEntity.CALLOC_NO);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "4001"));
                }
                else
                {
                    lCancel = poEntity.CALLOC_NO.Length > 20;
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "4002"));
                    }
                }

                lCancel = string.IsNullOrEmpty(poEntity.CALLOC_NAME);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "4003"));
                }
                else
                {
                    lCancel = poEntity.CALLOC_NAME.Length > 100;
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "4004"));
                    }
                }

                lCancel = string.IsNullOrEmpty(poEntity.CDEPT_CODE);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "4005"));
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveAllocationJournalDT(GLM00410DTO poEntity, eCRUDMode poCRUDMODE)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLM00410Model.R_ServiceSaveAsync(poEntity, poCRUDMODE);

                AllocationJournalDT = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteAllocationJournalDT(GLM00410DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                await _GLM00410Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
