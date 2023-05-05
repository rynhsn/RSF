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
        string lcCmd;
        R_Db loDb;
        DbConnection loConn;

        try
        {
            lcCmd = "EXEC RSP_GS_GET_SALES_TAX_LIST {0}, {1}";
            loDb = new R_Db();
            loConn = loDb.GetConnection("BimasaktiConnectionString");   
            loRtn = loDb.SqlExecObjectQuery<GSM02000GridDTO>(lcCmd, loConn, pcCompanyID, pcUserLoginID);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
}