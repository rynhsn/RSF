using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PMM00500Common.DTOs;
using PMM00500Common.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using RSP_PM_MAINTAIN_UNIT_CHARGESResources;

namespace PMM00500Back
{
    public class PMM00500Cls : R_BusinessObject<PMM00500DTO>
    {
        private RSP_PM_MAINTAIN_UNIT_CHARGESResources.Resources_Dummy_Class loRsp = new Resources_Dummy_Class();
        private LoggerPMM00500 _loggerPMM00500;
        private readonly ActivitySource _activitySource;
        public PMM00500Cls()
        {
            _loggerPMM00500 = LoggerPMM00500.R_GetInstanceLogger();
            _activitySource = PMM00500Activity.R_GetInstanceActivitySource();
        }

        #region CRUD
        protected override PMM00500DTO R_Display(PMM00500DTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override void R_Saving(PMM00500DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            throw new NotImplementedException();
        }

        protected override void R_Deleting(PMM00500DTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
        public List<ChargesTypeDTO> ChargesTypeDB(PMM00500ParameterDB poParameter)
        {
            using Activity activity = _activitySource.StartActivity("ChargesTypeDB");
            R_Exception loEx = new R_Exception();
            _loggerPMM00500.LogInfo("Start ChargesTypeDB PMM00500");
            List<ChargesTypeDTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn = null;
            DbCommand loCmd;
            string lcQuery = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = $"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO('BIMASAKTI', '{poParameter.CCOMPANY_ID}', '_BS_UNIT_CHARGES_TYPE', '', '{poParameter.CCULTURE}')";
                _loggerPMM00500.LogDebug("exec query function: {@lcQuery}", lcQuery);
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<ChargesTypeDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            if (loEx.Haserror)
            {
                _loggerPMM00500.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerPMM00500.LogInfo("End ChargesTypeDB PMM00500");
            return loRtn;
        }

    }
}
