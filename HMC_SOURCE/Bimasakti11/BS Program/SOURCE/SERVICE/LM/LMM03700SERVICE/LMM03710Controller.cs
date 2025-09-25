using LMM03700Back;
using LMM03700Common;
using LMM03700Common.DTO_s;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM03700Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LMM03710Controller : ControllerBase, ILMM03710
    {

        private async IAsyncEnumerable<T> StreamHelper<T>(List<T> entityList)
        {
            foreach (T entity in entityList)
            {
                yield return entity;
            }
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<TenantClassificationDTO> poParameter)
        {
            R_ServiceDeleteResultDTO loRtn = null;
            R_Exception loException = new R_Exception();
            LMM03710Cls loCls;
            try
            {
                loRtn = new R_ServiceDeleteResultDTO();
                loCls = new LMM03710Cls();
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<TenantClassificationDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<TenantClassificationDTO> poParameter)
        {
            R_ServiceGetRecordResultDTO<TenantClassificationDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            LMM03710Cls loCls;
            try
            {
                loCls = new LMM03710Cls(); //create cls class instance
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loRtn = new R_ServiceGetRecordResultDTO<TenantClassificationDTO>();
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return loRtn;

        }

        [HttpPost]
        public R_ServiceSaveResultDTO<TenantClassificationDTO> R_ServiceSave(R_ServiceSaveParameterDTO<TenantClassificationDTO> poParameter)
        {
            R_ServiceSaveResultDTO<TenantClassificationDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            LMM03710Cls loCls;
            try
            {
                loCls = new LMM03710Cls();
                loRtn = new R_ServiceSaveResultDTO<TenantClassificationDTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(LMM03700ContextConstant.CPROPERTY_ID);
                poParameter.Entity.CTENANT_CLASSIFICATION_GROUP_ID = R_Utility.R_GetStreamingContext<string>(LMM03700ContextConstant.CTENANT_CLASSIFICATION_GROUP_ID);
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<TenantClassificationDTO> GetTenantClassificationList()
        {
            R_Exception loException = new R_Exception();
            List<TenantClassificationDTO> loRtnTemp = null;
            LMM03710Cls loCls;
            try
            {
                loCls = new LMM03710Cls();
                loRtnTemp = loCls.GetTCList(new TenantClassificationDBListMaintainParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CTENANT_CLASSIFICATION_GROUP_ID = R_Utility.R_GetStreamingContext<string>(LMM03700ContextConstant.CTENANT_CLASSIFICATION_GROUP_ID),
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(LMM03700ContextConstant.CPROPERTY_ID),
                    CUSER_ID = R_BackGlobalVar.USER_ID
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return StreamHelper<TenantClassificationDTO>(loRtnTemp);
        }

        [HttpPost]
        public IAsyncEnumerable<TenantDTO> GetAssignedTenantList()
        {
            R_Exception loException = new R_Exception();
            List<TenantDTO> loRtnTemp = null;
            LMM03710Cls loCls;
            try
            {
                loCls = new LMM03710Cls();
                loRtnTemp = loCls.GetAssignedTenantList(new TenantClassificationDBListMaintainParamDTO()
                {

                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(LMM03700ContextConstant.CPROPERTY_ID),
                    CTENANT_CLASSIFICATION_ID = R_Utility.R_GetStreamingContext<string>(LMM03700ContextConstant.CTENANT_CLASSIFICATION_ID),
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return StreamHelper<TenantDTO>(loRtnTemp);
        }

        [HttpPost]
        public IAsyncEnumerable<TenantDTO> GetAvailableTenantList()
        {
            R_Exception loException = new R_Exception();
            List<TenantDTO> loRtnTemp = null;
            LMM03710Cls loCls;
            try
            {
                loCls = new LMM03710Cls();
                loRtnTemp = loCls.GetTenantToAssigntList(new TenantClassificationDBListMaintainParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CTENANT_CLASSIFICATION_GROUP_ID = R_Utility.R_GetStreamingContext<string>(LMM03700ContextConstant.CTENANT_CLASSIFICATION_GROUP_ID),
                    CTENANT_CLASSIFICATION_ID = R_Utility.R_GetStreamingContext<string>(LMM03700ContextConstant.CTENANT_CLASSIFICATION_ID),
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(LMM03700ContextConstant.CPROPERTY_ID),
                    CUSER_ID = R_BackGlobalVar.USER_ID
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return StreamHelper<TenantDTO>(loRtnTemp);
        }

        [HttpPost]
        public IAsyncEnumerable<TenantDTO> GetTenantToMoveList()
        {
            R_Exception loException = new R_Exception();
            List<TenantDTO> loRtnTemp = null;
            LMM03710Cls loCls;
            try
            {
                loCls = new LMM03710Cls();
                loRtnTemp = loCls.GetTenanToMoveList(new TenantParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CTENANT_CLASSIFICATION_ID = R_Utility.R_GetStreamingContext<string>(LMM03700ContextConstant.CTENANT_CLASSIFICATION_ID),
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(LMM03700ContextConstant.CPROPERTY_ID),
                    CUSER_ID = R_BackGlobalVar.USER_ID
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return StreamHelper<TenantDTO>(loRtnTemp);
        }

        [HttpPost]
        public TenantResultDumpDTO AssignTenant(TenantParamDTO poParam)
        {
            R_Exception loException = new R_Exception();
            LMM03710Cls loCls;
            try
            {
                loCls = new LMM03710Cls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loCls.AssignTenant(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return new();
        }

        [HttpPost]
        public TenantResultDumpDTO MoveTenant(TenantMoveParamDTO poParam)
        {
            R_Exception loException = new R_Exception();
            LMM03710Cls loCls;
            try
            {
                loCls = new LMM03710Cls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loCls.MoveTenantUsingRDT(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return new();
        }

    }
}
