using GSM02500COMMON.DTOs.GSM02503;
using GSM02500COMMON.DTOs.GSM02530;
using GSM02500MODEL.View_Model;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500FRONT
{
    public partial class GSM02503Image : R_Page
    {
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        public ImageTabParameterDTO loParameter = new ImageTabParameterDTO();

        public GSM02503ImageViewModel loImageViewModel = new();

        private R_Conductor _conductorImageRef;

        private R_Grid<GSM02503ImageDTO> _gridImageRef;

        private bool IsShowEnabled = false;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loParameter = (ImageTabParameterDTO)poParameter;

                loImageViewModel.SelectedProperty.CPROPERTY_ID = loParameter.CSELECTED_PROPERTY_ID;
                loImageViewModel.SelectedUnitType.CUNIT_TYPE_ID = loParameter.CSELECTED_UNIT_TYPE_ID;

                await loImageViewModel.GetSelectedUnitTypeAsync();

                await _gridImageRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public void BeforeOpenAddPopUp(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(AddUnitTypeImageModal);
        }

        private void Grid_AfterDeleteImage()
        {
            if (loImageViewModel.loImageList.Count() == 0)
            {
                IsShowEnabled = false;
            }
            else if (loImageViewModel.loImageList.Count() > 0)
            {
                IsShowEnabled = true;
            }
        }

        private async Task Grid_ServiceDeleteImage(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM02503ImageDTO loData = (GSM02503ImageDTO)eventArgs.Data;
                await loImageViewModel.DeleteImageAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task AfterOpenAddPopUp(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success)
                {
                    AddUnitTypeImageResultDTO loResult = (AddUnitTypeImageResultDTO)eventArgs.Result;
                    if (loResult.LRESULT)
                    {
                        loImageViewModel.ImageValidation(new GSM02503ImageDTO
                        {
                            CIMAGE_ID = loResult.CIMAGE_ID,
                            CIMAGE_NAME = loResult.CIMAGE_NAME
                        });
                        if (loException.HasError)
                        {
                            return;
                        }

                        loImageViewModel.ImageByte = loResult.OIMAGE;
                        loImageViewModel.FileName = loResult.CFILE_NAME;
                        loImageViewModel.FileExtension = loResult.CFILE_EXTENSION;

                        await loImageViewModel.SaveImageAsync(new GSM02503ImageDTO
                        {
                            CIMAGE_ID = loResult.CIMAGE_ID,
                            CIMAGE_NAME = loResult.CIMAGE_NAME
                        }, eCRUDMode.AddMode);

                        if (loException.HasError == false)
                        {
                            await _gridImageRef.R_RefreshGrid(null);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task BeforeOpenShowPopUp(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loImageViewModel.ShowUnitTypeImageAsync();

                if (loImageViewModel.loShowImage.OIMAGE != null)
                {
                    eventArgs.Parameter = loImageViewModel.loShowImage.OIMAGE;
                    eventArgs.TargetPageType = typeof(ShowUnitTypeImage);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task AfterOpenShowPopUp(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }


        #region Image

        private async Task Grid_DisplayImage(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02503ImageDTO)_conductorImageRef.R_GetCurrentData();
                loImageViewModel.loImageDetail = loParam;
                loImageViewModel.SelectedImageId = loParam.CIMAGE_ID;
            }
        }

        private async Task Grid_R_ServiceGetImageListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loImageViewModel.GetImageListStreamAsync();
                if (loImageViewModel.loImageList.Count() > 0)
                {
                    IsShowEnabled = true;
                }
                else
                {
                    IsShowEnabled = false;
                }
                eventArgs.ListEntityResult = loImageViewModel.loImageList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetImageRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02503ImageDTO loParam = (GSM02503ImageDTO)eventArgs.Data;
                await loImageViewModel.GetImageAsync(loParam);

                eventArgs.Result = loImageViewModel.loImageDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
