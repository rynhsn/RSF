using System.Data;
using System.Data.Common;
using GSM02000Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM02000Back;

public class GSM02000Cls : R_BusinessObject<GSM02000DTO>
{
    protected override GSM02000DTO R_Display(GSM02000DTO poEntity)
    {
        throw new NotImplementedException();
    }

    protected override void R_Saving(GSM02000DTO poNewEntity, eCRUDMode poCRUDMode)
    {
        throw new NotImplementedException();
    }

    protected override void R_Deleting(GSM02000DTO poEntity)
    {
        throw new NotImplementedException();
    }
    
    public List<GSM02000GridDTO> SalesTaxListDb(string pcCompanyID, string pcUserLoginID)
    {
        R_Exception loEx = new R_Exception();
        List<GSM02000GridDTO> loRtn = null;

        try
        {
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("BimasaktiConnectionString"); 
            var loCmd = loDb.GetCommand();   
            
            var lcQuery = "EXEC RSP_GS_GET_SALES_TAX_LIST @CCOMPANY_ID, @CUSER_LOGIN_ID";
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, pcCompanyID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, pcUserLoginID);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM02000GridDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
}