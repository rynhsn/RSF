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
    public class LMM03700Controller : ControllerBase, ILMM03700
    {
        [HttpPost]
        public IAsyncEnumerable<TenantClassificationGroupDTO> GetTenantClassGroupList()
        {
            R_Exception loException = new R_Exception();
            List<TenantClassificationGroupDTO> loRtnTemp = null;
            LMM03700Cls loCls;
            try
            {
                loCls = new LMM03700Cls();
                loRtnTemp = loCls.GetTCGList(new TenantClassificationGroupDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
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
            return TCGStreamListHelper(loRtnTemp);
        }

        private async IAsyncEnumerable<TenantClassificationGroupDTO> TCGStreamListHelper(List<TenantClassificationGroupDTO> poList)
        {
            foreach(TenantClassificationGroupDTO loEntity in poList)
            {
                yield return loEntity;
            }
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<TenantClassificationGroupDTO> poParameter)
        {
            R_ServiceDeleteResultDTO loRtn = null;
            R_Exception loException = new R_Exception();
            LMM03700Cls loCls;
            try
            {
                loRtn = new R_ServiceDeleteResultDTO();
                loCls = new LMM03700Cls(); //create cls class instance
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
        public R_ServiceGetRecordResultDTO<TenantClassificationGroupDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<TenantClassificationGroupDTO> poParameter)
        {
            R_ServiceGetRecordResultDTO<TenantClassificationGroupDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            LMM03700Cls loCls;
            try
            {
                loCls = new LMM03700Cls(); //create cls class instance
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CPROPERTY_ID= R_Utility.R_GetStreamingContext<string>(LMM03700ContextConstant.CPROPERTY_ID);
                loRtn = new R_ServiceGetRecordResultDTO<TenantClassificationGroupDTO>();
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
        public R_ServiceSaveResultDTO<TenantClassificationGroupDTO> R_ServiceSave(R_ServiceSaveParameterDTO<TenantClassificationGroupDTO> poParameter)
        {
            R_ServiceSaveResultDTO<TenantClassificationGroupDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            LMM03700Cls loCls;
            try
            {
                loCls = new LMM03700Cls();
                loRtn = new R_ServiceSaveResultDTO<TenantClassificationGroupDTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(LMM03700ContextConstant.CPROPERTY_ID);
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
        public IAsyncEnumerable<PropertyDTO> LMM03700GetPropertyData()
        {
            R_Exception loException = new R_Exception();
            List<PropertyDTO> loRtnTemp = null;
            LMM03700Cls loCls;
            try
            {
                loCls = new LMM03700Cls();
                loRtnTemp = loCls.GetPropertyList(new PropertyDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return PropertyStreamListHelper(loRtnTemp);
        }

        private async IAsyncEnumerable<PropertyDTO> PropertyStreamListHelper(List<PropertyDTO> poList)
        {
            foreach (PropertyDTO loEntity in poList)
            {
                yield return loEntity;
            }
        }
    }
}