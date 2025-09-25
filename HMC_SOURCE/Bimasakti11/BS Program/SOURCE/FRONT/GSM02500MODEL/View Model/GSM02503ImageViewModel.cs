using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02503;
using GSM02500COMMON.DTOs.GSM02530;
using GSM02500FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GSM02500MODEL.View_Model
{
    public class GSM02503ImageViewModel : R_ViewModel<GSM02503ImageDTO>
    {
        private GSM02503ImageModel loModel = new GSM02503ImageModel();

        private GSM02500Model loSharedModel = new GSM02500Model();

        public ShowUnitTypeImageDTO loShowImage = null;

        public ShowUnitTypeImageResultDTO loShowImageRtn = null;

        public ShowUnitTypeImageParameterDTO loShowImageParam = new ShowUnitTypeImageParameterDTO();

        public GSM02503ImageDTO loImageDetail = null;

        public ObservableCollection<GSM02503ImageDTO> loImageList = new ObservableCollection<GSM02503ImageDTO>();

        public GSM02503ImageListDTO loRtn = null;

        public SelectedPropertyDTO SelectedProperty = new SelectedPropertyDTO();

        public SelectedUnitTypeDTO SelectedUnitType = new SelectedUnitTypeDTO();

        public string SelectedImageId = "";

        public byte[] ImageByte = default;

        public string FileName = "";

        public string FileExtension = "";

        public async Task ShowUnitTypeImageAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loShowImageParam.CSELECTED_PROPERTY_ID = SelectedProperty.CPROPERTY_ID;
                loShowImageParam.CSELECTED_UNIT_TYPE_ID = SelectedUnitType.CUNIT_TYPE_ID;
                loShowImageParam.CSELECTED_IMAGE_ID = SelectedImageId;
                loShowImageRtn = await loModel.ShowUnitTypeImageAsync(loShowImageParam);
                loShowImage = loShowImageRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetSelectedPropertyAsync()
        {
            R_Exception loException = new R_Exception();
            SelectedPropertyParameterDTO loParam = null;

            try
            {
                loParam = new SelectedPropertyParameterDTO()
                {
                    Data = SelectedProperty
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                SelectedProperty = await loSharedModel.GetSelectedPropertyAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetSelectedUnitTypeAsync()
        {
            R_Exception loException = new R_Exception();
            SelectedUnitTypeParameterDTO loParam = null;
            try
            {
                loParam = new SelectedUnitTypeParameterDTO()
                {
                    Data = SelectedUnitType,
                    CSELECTED_PROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_UNIT_TYPE_ID_CONTEXT, SelectedUnitType.CUNIT_TYPE_ID);
                SelectedUnitType = await loSharedModel.GetSelectedUnitTypeAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

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

        public async Task GetImageListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02503_PROPERTY_ID_STREAMING_CONTEXT, SelectedProperty.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02503_UNIT_TYPE_ID_STREAMING_CONTEXT, SelectedUnitType.CUNIT_TYPE_ID);
                loRtn = await loModel.GetUnitTypeImageListStreamAsync();

                loImageList = new ObservableCollection<GSM02503ImageDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetImageAsync(GSM02503ImageDTO poEntity)
        {
            R_Exception loEx = new R_Exception();

            try
            {

                GSM02503ImageParameterDTO loParam = new GSM02503ImageParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = SelectedProperty.CPROPERTY_ID,
                    CUNIT_TYPE_ID = SelectedUnitType.CUNIT_TYPE_ID
                };

                GSM02503ImageParameterDTO loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loImageDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveImageAsync(GSM02503ImageDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();

            try
            {

                GSM02503ImageParameterDTO loParam = new GSM02503ImageParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = SelectedProperty.CPROPERTY_ID,
                    CUNIT_TYPE_ID = SelectedUnitType.CUNIT_TYPE_ID,
                    OIMAGE = ImageByte,
                    CFILE_NAME = FileName,
                    CFILE_EXTENSION = FileExtension
                };

                //var loByteToString = Convert.ToBase64String(ImageByte);

                GSM02503ImageParameterDTO loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loImageDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteImageAsync(GSM02503ImageDTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                await ShowUnitTypeImageAsync();

                GSM02503ImageParameterDTO loParam = new GSM02503ImageParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = SelectedProperty.CPROPERTY_ID,
                    CUNIT_TYPE_ID = SelectedUnitType.CUNIT_TYPE_ID,
                    OIMAGE = loShowImage.OIMAGE,
                };

                //var loByteToString = Convert.ToBase64String(loShowImage.OIMAGE);
                await loModel.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}