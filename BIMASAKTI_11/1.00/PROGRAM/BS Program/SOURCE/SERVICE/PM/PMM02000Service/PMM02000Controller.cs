using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using PMM02000Common;
using R_Common;

namespace PMM02000Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class PMM02000Controller : ControllerBase
{
    [HttpPost]
    public PMM02000ExcelDTO PMM02000DownloadTemplateFile()
    {
        var loEx = new R_Exception();
        var loRtn = new PMM02000ExcelDTO();

        try
        {
            var loAsm = Assembly.Load("BIMASAKTI_PM_API");
            
            var lcResourceFile = "BIMASAKTI_PM_API.Template.Salesman.xlsx";

            using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
            {
                var ms = new MemoryStream();
                resFilestream.CopyTo(ms);
                var bytes = ms.ToArray();

                loRtn.FileBytes = bytes;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }
}