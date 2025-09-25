using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500FRONT
{
    public partial class GSM02503Image : R_Page
    {

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                var loParameter = R_FrontUtility.ConvertObjectToObject<TempDTO>(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }

    public class TempDTO
    {
        public string? CPROPERTY_ID { get; set; }
        public string? CPROPERTY_NAME { get; set; }
        public string? CUNIT_TYPE_ID { get; set; }
        public string? CUNIT_TYPE_NAME { get; set; }
    }
}
