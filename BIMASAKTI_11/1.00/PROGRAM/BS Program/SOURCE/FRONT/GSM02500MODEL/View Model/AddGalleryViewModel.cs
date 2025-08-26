using GSM02500COMMON.DTOs.GSM02520;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02503;
using System.Reflection;
using GSM02500COMMON.DTOs;
using R_BlazorFrontEnd.Helpers;
using GSM02500FrontResources;

namespace GSM02500MODEL.View_Model
{
    public class AddGalleryViewModel : R_ViewModel<GSM02503ImageSaveDTO>
    {
        private GSM02570Model loModel = new GSM02570Model();

        public ImageTabParameterDTO loParameter = new ImageTabParameterDTO();
        /*
                public AddUnitTypeImageResultDTO loImage = new AddUnitTypeImageResultDTO();*/

        public string SelectedUserId = "";
        public string SelectedCompanyId = "";

        public bool VisibleError = false;
        public bool IsErrorEmptyFile = false;
        public bool IsUploadSuccesful = true;

        public byte[] loImageByte;


        public void ImageValidation(GSM02503ImageDTO poParam)
        {
            bool llCancel = false;

            R_Exception loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CIMAGE_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V001"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
