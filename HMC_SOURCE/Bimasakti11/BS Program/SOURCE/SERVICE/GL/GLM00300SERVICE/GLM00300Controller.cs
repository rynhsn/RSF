using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GLM00300Back;
using GLM00300Common;
using GLM00300Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GLM00300Service
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class GLM00300Controller : ControllerBase, IGLM00300
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GLM00300DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GLM00300DTO> poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GLM00300DTO>();
            GLM00300ParameterDB loDbPar;
            try
            {
                var loCls = new GLM00300Cls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GLM00300DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GLM00300DTO> poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new R_ServiceSaveResultDTO<GLM00300DTO>();
            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                var loCls = new GLM00300Cls();

                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GLM00300DTO> poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new R_ServiceDeleteResultDTO();
            var loCls = new GLM00300Cls();
            try
            {
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GLM00300DTO> GetBudgetWeightingNameList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00300DTO> loRtn = null;
            GLM00300ParameterDB loDbPar;
            GLM00300Cls loCls;
            List<GLM00300DTO> loRtnTemp;
            try
            {
                loDbPar = new GLM00300ParameterDB();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                loCls = new GLM00300Cls();
                loRtnTemp = loCls.GetBudgetWeightingList(loDbPar);
                loRtn = GetBudgetWeightingStream(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn; ;
        }

        [HttpPost]
        public CurrencyCodeListDTO GetCurrencyCodeList()
        {
            var loEx = new R_Exception();
            CurrencyCodeListDTO loRtn = null;
            GLM00300ParameterDB loDbPar;
            GLM00300Cls loCls;
            try
            {
                loDbPar = new GLM00300ParameterDB();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loCls = new GLM00300Cls();

                loRtn = new CurrencyCodeListDTO();
                loRtn.Data = loCls.GetCurrencyList(loDbPar);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }


        #region Helper
        private async IAsyncEnumerable<GLM00300DTO> GetBudgetWeightingStream(List<GLM00300DTO> poParameter)
        {
            foreach (GLM00300DTO item in poParameter)
            {
                yield return item;
            }
        }

        #endregion
    }
}
