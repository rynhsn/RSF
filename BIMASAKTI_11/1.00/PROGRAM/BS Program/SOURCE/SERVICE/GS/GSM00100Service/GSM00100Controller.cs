using GSM00100Back;
using GSM00100Common;
using Microsoft.AspNetCore.Mvc;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM00100Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM00100Controller : ControllerBase, IGSM00100
    {
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM00100DTO> poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                var loCls = new GSM00100Cls();

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
        public R_ServiceGetRecordResultDTO<GSM00100DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM00100DTO> poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM00100DTO>();

            try
            {
                var loCls = new GSM00100Cls();

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
        public R_ServiceSaveResultDTO<GSM00100DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM00100DTO> poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new R_ServiceSaveResultDTO<GSM00100DTO>();

            try
            {
                var loCls = new GSM00100Cls();

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
        public GSM00100GenericResultDTO<List<GSM00100DTOList>> GetSMTPList()
        {
            var loEx = new R_Exception();
            GSM00100GenericResultDTO<List<GSM00100DTOList>> loRtn = null;

            try
            {
                //var a = R_BackGlobalVar.COMPANY_ID;
                //var b = R_Context.R_GetStreamingContext("test");
                //var c = R_Context.R_GetContext("test");

                var loCls = new GSM00100Cls();

                var loResult = loCls.GetSMTPList();
                loRtn = new GSM00100GenericResultDTO<List<GSM00100DTOList>> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSM00100GenericResultDTO<bool> CheckDelete(GSM00100DTO poParam)
        {
            var loEx = new R_Exception();
            GSM00100GenericResultDTO<bool> loRtn = null;

            try
            {
                var loCls = new GSM00100Cls();

                var llResult = loCls.CheckDelete(poParam);
                loRtn = new GSM00100GenericResultDTO<bool> { Data = llResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSM00100GenericResultDTO TestSendEmail(EmailDTO poParam)
        {
            var loEx = new R_Exception();
            GSM00100GenericResultDTO loRtn = null;

            try
            {
                var loCls = new GSM00100Cls();

                loCls.TestSendEmail(poParam);
                loRtn = new GSM00100GenericResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSM00100GenericResultDTO CheckSupportedEmailProvider()
        {
            var loEx = new R_Exception();
            GSM00100GenericResultDTO loRtn = null;

            try
            {
                var loCls = new GSM00100Cls();

                loCls.CheckSupportedEmailProvider();
                loRtn = new GSM00100GenericResultDTO();
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