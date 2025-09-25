using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GLM00100Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GLM00100Model.ViewModel
{
    public class GLM00100ViewModel : R_ViewModel<GLM00100DTO>
    {
        private readonly GLM00100Model _modelGLM00100 = new GLM00100Model();
        public GLM00100DTO loEntityGLM00100 = new GLM00100DTO();
        
        //Create System Parameter
        public GLM00100ResultData liCheckerSystemParameter = new GLM00100ResultData();
        public GLM00100GSMPeriod loBindStartingValue = new GLM00100GSMPeriod();
        public GLM00100ParameterCreateSystemParameterResultDTO loCreateDataGLM00100 = new GLM00100ParameterCreateSystemParameterResultDTO();
        public string lcMessageBox = "Approved";
        public int liTempDataCSTARTING_YY = 0;


        public List<ComboBoxReversingJournalMode> ReversingJournalProcessGroupType { get; set; } =
            new List<ComboBoxReversingJournalMode>
            {
                new ComboBoxReversingJournalMode
                    { Id = 1, Description = "Automatically when commit reversing journal" },
                new ComboBoxReversingJournalMode
                    { Id = 2, Description = "By batch before closing period of reversing date" }
            };

        
        public async Task GetEntity(GLM00100DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                
               var loResult = await _modelGLM00100.R_ServiceGetRecordAsync(poEntity);
                
                loEntityGLM00100 = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveCashSystemParameter(GLM00100DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _modelGLM00100.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loEntityGLM00100 = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region CreateSystemParameter


        public List<GLM00100GetMonthDTO> loMonthGLM00100 { get; set; } = new List<GLM00100GetMonthDTO>
        {
            new GLM00100GetMonthDTO { Id = "01" },
            new GLM00100GetMonthDTO { Id = "02" },
            new GLM00100GetMonthDTO { Id = "03" },
            new GLM00100GetMonthDTO { Id = "04" },
            new GLM00100GetMonthDTO { Id = "05" },
            new GLM00100GetMonthDTO { Id = "06" },
            new GLM00100GetMonthDTO { Id = "07" },
            new GLM00100GetMonthDTO { Id = "08" },
            new GLM00100GetMonthDTO { Id = "09" },
            new GLM00100GetMonthDTO { Id = "10" },
            new GLM00100GetMonthDTO { Id = "11" },
            new GLM00100GetMonthDTO { Id = "12" },
        };
        
        public async Task<bool> GLM00100CreateSystemParameter()
        {
            var loEx = new R_Exception();

            var test = loCreateDataGLM00100.CSTARTING_MM;
            loCreateDataGLM00100.CSTARTING_YY = liTempDataCSTARTING_YY.ToString();

            await _modelGLM00100.CreateSystemParameterAsync(loCreateDataGLM00100);

            return true;
            
        }
        
        
        public async Task GLM00100GetDataValueForCreateSystemParameter()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _modelGLM00100.GetStartingPeriodYearAsync();

                liTempDataCSTARTING_YY = loResult.IMIN_YEAR;
                
                loBindStartingValue = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            
        }
        
        
        public async Task<bool> GLM00100GetCheckerSystemParameterAvailable()
        {
            bool loReturn = false;

                var loResult = await _modelGLM00100.GetCheckerSystemParameterAsync();
                if (loResult.IRESULT != 0)
                    loReturn = true;
                
                liCheckerSystemParameter = loResult;


            return loReturn;
        }

        #endregion
        
        

        public class ComboBoxReversingJournalMode
        {
            public int Id { get; set; }
            public string Description { get; set; }
        }
    }
}