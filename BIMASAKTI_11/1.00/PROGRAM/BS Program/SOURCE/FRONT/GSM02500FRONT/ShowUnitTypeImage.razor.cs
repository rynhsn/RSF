using BlazorClientHelper;
using GSM02500COMMON.DTOs.GSM02520;
using GSM02500MODEL.View_Model;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500FRONT
{
    public partial class ShowUnitTypeImage : R_Page
    {
        private byte[] OIMAGE;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                OIMAGE = (byte[])poParameter;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
