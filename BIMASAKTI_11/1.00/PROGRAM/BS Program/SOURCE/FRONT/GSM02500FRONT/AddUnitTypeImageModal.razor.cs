using BlazorClientHelper;
using GSM02500COMMON.DTOs.GSM02520;
using GSM02500MODEL.View_Model;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02503;
using R_CommonFrontBackAPI;
using System.Diagnostics.Tracing;

namespace GSM02500FRONT
{
    public partial class AddUnitTypeImageModal : R_Page
    {
        public AddUnitTypeImageViewModel loAddImageViewModel = new AddUnitTypeImageViewModel();

        public AddUnitTypeImageResultDTO loImage = new AddUnitTypeImageResultDTO();

        private R_Conductor _conGridUploadImageRef;

        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Image };

        private bool IsInputHidden = false;

        private bool IsFormHidden = true;

        private bool IsFileExist = false;

        private bool IsErrorEmptyFile = false;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private async Task OnChangeInputFile(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loByteFile = await R_FrontUtility.ConvertStreamToByteAsync(eventArgs.File.OpenReadStream());
                string loFile = eventArgs.File.Name;

                loImage.OIMAGE = loByteFile;
                loImage.CFILE_NAME = Path.GetFileNameWithoutExtension(loFile);
                loImage.CFILE_EXTENSION = Path.GetExtension(loFile);

                if (eventArgs.File.Name.Length > 0)
                {
                    IsFileExist = true;
                }
                else
                {
                    IsFileExist = false;
                }
            }
            catch (Exception ex)
            {
                if (IsErrorEmptyFile)
                {
                    await R_MessageBox.Show("", "File is Empty", R_eMessageBoxButtonType.OK);
                }
                else
                {
                    loEx.Add(ex);
                }
            }
        B:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnOk()
        {
            R_Exception loException = new R_Exception();
            try
            {
                IsInputHidden = true;
                IsFormHidden = !IsInputHidden;
            }
            catch (Exception ex)
            {

                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
/*
        private async Task ServiceSaveAddImage(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                await loAddImageViewModel.SaveImageAsync(new GSM02503ImageDTO
                {
                    CIMAGE_ID = loResult.CIMAGE_ID,
                    CIMAGE_NAME = loResult.CIMAGE_NAME
                }, eCRUDMode.AddMode);

                loImage.LRESULT = true;
                await this.Close(true, loImage);
            }
            catch (Exception ex)
            {

                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }*/

        private async Task OnSave()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loImage.LRESULT = true;
                await this.Close(true, loImage);
            }
            catch (Exception ex)
            {

                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private async Task OnCancel()
        {
            loImage.LRESULT = false;
            await this.Close(false, loImage);
        }
    }
}
