using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LMM02500Common.DTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace LMM02500Model.ViewModel
{
    public class LMM02520ViewModeLMM02520MoveTenantViewModel : R_ViewModel<TenantListForMoveProcessDTO>
    {
        private readonly LMM02500Model _modelLMM02500 = new LMM02500Model();
        private readonly LMM02520Model _modelLMM02520 = new LMM02520Model();

        //public string lcTenantId = "";
        public string? lcPropertyId = "";
        public string? lcPropertyToTenantGroup = "";
        public string? lcTenantGroupId = "";
        public string? lcTenantId = "";


        public LMM02500ProfileDTO loFromTenantCategory = new LMM02500ProfileDTO();
        public List<TenantListForMoveProcessDTO> loGetTenantBatchList = new List<TenantListForMoveProcessDTO>();

        public List<LMM02520GridDTO> loRtn = new List<LMM02520GridDTO>();

        public ObservableCollection<TenantListForMoveProcessDTO> loTenantList =
            new ObservableCollection<TenantListForMoveProcessDTO>();

        public LMM02500ProfileDTO loToTenantCategory = new LMM02500ProfileDTO();

        public List<LMM02500ProfileDTO> loToTenantListLMM02500 = new List<LMM02500ProfileDTO>();


        public async Task GetAllTenantGroupList(string pcCPROPERTY_ID)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _modelLMM02500.GetAllTenantGroupStreamAsync(pcCPROPERTY_ID);

                loToTenantListLMM02500 = new List<LMM02500ProfileDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetEntity(TenantGroupForMoveParameterFrontDTO poEntity, string pcMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM02500ProfileDTO();

                if (pcMode == "FROM")
                {
                    loParam.CPROPERTY_ID = poEntity.CPROPERTY_ID;
                    loParam.CTENANT_GROUP_ID = poEntity.CFROM_TENANT_GROUP;
                    lcPropertyId = poEntity.CPROPERTY_ID;
                    lcTenantGroupId = poEntity.CFROM_TENANT_GROUP;
                    lcPropertyToTenantGroup = poEntity.CFROM_TENANT_GROUP;

                    var loResult = await _modelLMM02500.R_ServiceGetRecordAsync(loParam);
                    loFromTenantCategory = loResult;
                    loToTenantCategory.CTENANT_GROUP_NAME = loFromTenantCategory.CTENANT_GROUP_NAME;
                }
                else if (pcMode == "TO")
                {
                    loParam.CTENANT_GROUP_ID = poEntity.CTO_TENANT_GROUP;
                    loParam.CPROPERTY_ID = poEntity.CPROPERTY_ID;
                    var loResult = await _modelLMM02500.R_ServiceGetRecordAsync(loParam);
                    loToTenantCategory = loResult;
                }
                else
                {
                    loEx.Add("", "Mode can't be null");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetAllTenantGroupList()
        {
            var loEx = new R_Exception();

            try
            {
                if (lcPropertyId != null && lcTenantGroupId != null)
                {
                    var loResult = await _modelLMM02520.GetAllTenantGroupListStreamAsync(lcPropertyId, lcTenantGroupId);

                    loRtn = new List<LMM02520GridDTO>(loResult);
                }
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTenantListForMoveProcess()
        {
            var loEx = new R_Exception();

            try
            {
                loTenantList.Clear();
                foreach (var item in loRtn)
                    loTenantList.Add(new TenantListForMoveProcessDTO
                        { CTENANT_ID = item.CTENANT_ID, CTENANT_NAME = item.CTENANT_NAME });
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveMoveTenantGroupCategory()
        {
            var loException = new R_Exception();

            try
            {
                foreach (var item in loGetTenantBatchList)
                    if (item.LSELECTED)
                        lcTenantId += "(\'" + item.CTENANT_ID + "\'), ";

                if (!string.IsNullOrEmpty(lcTenantId))
                    lcTenantId = lcTenantId.Substring(0, lcTenantId.Length - 2); // Remove the last comma and space

                ObjectParameterLMM02500MoveTenantGroup loContent = new ObjectParameterLMM02500MoveTenantGroup()
                {
                    CPROPERTY_ID = lcPropertyId,
                    CFROM_TENANT_GROUP = loFromTenantCategory.CTENANT_GROUP_ID,
                    CTO_TENANT_GROUP = lcPropertyToTenantGroup,
                    CTENANT_ID = lcTenantId
                };

                await _modelLMM02520.SaveMoveTenantGroupCategoryAsync(poParameter: loContent);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
    }
}