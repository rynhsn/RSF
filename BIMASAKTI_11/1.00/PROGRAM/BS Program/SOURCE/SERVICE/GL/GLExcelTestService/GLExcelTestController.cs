using System.Reflection;
using GLExcelTestCommon;
using Microsoft.AspNetCore.Mvc;
using R_Common;

namespace GLExcelTestService;

[ApiController]
[Route("api/[controller]/[action]")]
public class GLExcelTestController : ControllerBase
{
    [HttpPost]
    public GLExcelTestFileDTO GLExcelTestDownloadTemplateFile()
    {
        var loEx = new R_Exception();
        var loRtn = new GLExcelTestFileDTO();

        try
        {
            var loAsm = Assembly.Load("BIMASAKTI_GL_API");
            
            var lcResourceFile = "BIMASAKTI_GL_API.Template.GL_ACCOUNT_BUDGET_UPLOAD2.xlsx";

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